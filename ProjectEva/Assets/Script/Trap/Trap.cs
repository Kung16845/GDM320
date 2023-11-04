using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    public float trapDuration = 5.0f; // How long the trap lasts
    public float sliderFillRate = 1.0f; // Rate at which the slider fills when pressing 'E'
    public TrapController trapController;
    public Slider slider;
    public RawImage sliderImage; // Reference to the child Image component
    private bool isActivated = false;
    private float currentDuration = 0.0f;
    public float sliderValue = 0.0f;
    public SoundManager soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    
    void Update()
    {
        if (isActivated)
        {
            if (sliderValue < 100)
            {
                // Fill the slider when 'E' is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    sliderValue += sliderFillRate * Time.deltaTime;
                    // Update the Slider's value here
                    slider.value = sliderValue / 100.0f; // Normalize the value to be between 0 and 1

                    // Change the color of the child Image when 'E' is pressed
                    StartCoroutine(ChangeSliderColorWhilePressingE(Color.grey));
                }

                // Restrict the slider value to be between 0 and 100
                sliderValue = Mathf.Clamp(sliderValue, 0f, 100f);

                // Check if the slider is completely filled
                if (sliderValue >= 100)
                {
                    slider.value = 0;
                    Destroy(this.gameObject);
                    ReleasePlayer();
                }
            }
            else
            {
                // Continue trapping the player until the trap duration expires
                currentDuration += Time.deltaTime;
                if (currentDuration >= trapDuration)
                {
                    Destroy(this.gameObject);
                    slider.value = 0;
                    ReleasePlayer();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            soundManager.PlaySound("Trapped");
            isActivated = true;
            trapController.PlayerHitByTrap();
        }
    }

    void ReleasePlayer()
    {
        isActivated = false;
        sliderValue = 0.0f;
        currentDuration = 0.0f;

        // Reset the child Image color to its normal state

        trapController.PlayerReleaseTrap();
    }

    public float returnSlidervalue()
    {
        return sliderValue;
    }

    // Function to change the color of the child Image component
    IEnumerator ChangeSliderColorWhilePressingE(Color color)
    {
        sliderImage.color = color;
        yield return new WaitForSeconds(0.1f);
        sliderImage.color = Color.white;
    }
}
