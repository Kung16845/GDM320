using UnityEngine;
using UnityEngine.UI;

public class TrapSlider : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider
    public float fillSpeed = 1.0f; // Speed at which the slider fills
    private bool isFilling; // A flag to control filling

    private void Start()
    {
        slider.value = 0f; // Initialize the slider's value to zero
    }

    private void Update()
    {
        if (isFilling)
        {
            FillSlider();
        }
    }

    // Function to start filling the slider
    public void StartFilling()
    {
        isFilling = true;
    }

    // Function to stop filling the slider
    public void StopFilling()
    {
        isFilling = false;
    }

    // Function to fill the slider
    public void FillSlider()
    {
        if (slider.value < 1.0f)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

}
