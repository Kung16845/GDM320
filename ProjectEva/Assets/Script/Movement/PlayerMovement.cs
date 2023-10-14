using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _slowSpeed;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _rotationSpeedl;
    private float _originalSpeed; // Store the original speed.
    private float _originalSlowSpeed; // Store the original slow speed.
    private float _originalRunningSpeed;
    private bool isRunning = false;
    private bool isSlowed = false;
    private bool isAiming = false; // Flag to track aiming.
    private Rigidbody2D _rigidbody;
    public Collider2D CircleSound;
    private Vector2 _movementInput;
    private Vector2 _smoothedMoveInput;
    private Vector2 _movementInputSmoothVelocity;
    private Pistol _pistol; // Reference to the Pistol script.
    private SanityScaleController sanityScaleController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _pistol = GetComponent<Pistol>();
    }
    private void Start()
    {
        // Store the original speed values.
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        _originalSpeed = _speed;
        _originalSlowSpeed = _slowSpeed;
        _originalRunningSpeed = _runningSpeed;
        // MakeTrigger2DSound();
    }
    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirecttionOfInput();
    }
    private void Update()
    {
        if (transform.hasChanged)
        {
            Debug.Log("The transform has changed!");
            MakeTrigger2DSound();
            transform.hasChanged = false;
        }
        else
            StartCoroutine(ReduceRadiusSound());

        // Toggle running when Left Shift is pressed and not aiming.
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            if (isAiming)
            {
                isAiming = false; // Cancel aiming instantly when Shift is pressed.
            }
            else
            {
                isRunning = !isRunning;
            }
        }

        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            isSlowed = !isSlowed;
        }

        // Toggle aiming when Right Mouse Button is pressed.
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            isAiming = !isAiming;
            // If aiming is toggled off while Shift is pressed, cancel running.
            if (isAiming && isRunning)
            {
                isRunning = false;
            }
        }

        // Check if aiming is released and set speed to normal.
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isAiming = false;
        }

    }
    public void MakeTrigger2DSound()
    {
        Debug.Log("Make Sound");
        CircleSound.GetComponent<CircleCollider2D>().radius = 5;
        StartCoroutine(ReduceRadiusSound());
    }
    IEnumerator ReduceRadiusSound()
    {
        CircleSound.GetComponent<CircleCollider2D>().radius -= 1;
        yield return new WaitForSeconds(1f);
        Debug.Log("ReduceRadiusSound is Complete");
        if(CircleSound.GetComponent<CircleCollider2D>().radius > 0.1)
            StartCoroutine(ReduceRadiusSound());
    }
    private void SetPlayerVelocity()
    {
        _smoothedMoveInput = Vector2.SmoothDamp(_smoothedMoveInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);

        float currentSpeed = _speed * sanityScaleController.GetSpeedScale();
        
        if (isRunning)
        {
            currentSpeed = _runningSpeed * sanityScaleController.GetSpeedScale();
            
        }
        else if (isSlowed || isAiming)
        {
            currentSpeed = _slowSpeed * sanityScaleController.GetSpeedScale();
        }

        _rigidbody.velocity = _smoothedMoveInput * currentSpeed;
    }

    private void RotateInDirecttionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMoveInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeedl * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    public void ReduceSpeedDuringReload()
    {
        _speed = _slowSpeed * sanityScaleController.GetSpeedScale();
        _runningSpeed = _slowSpeed * sanityScaleController.GetSpeedScale();
    }
    public void RestoreNormalSpeed()
    {
        // Restore the player's normal speed.
        _speed = _originalSpeed;
        _slowSpeed = _originalSlowSpeed;
        _runningSpeed = _originalRunningSpeed;
    }
    public float GetCurrentSpeed()
    {
        float currentSpeed = _speed * sanityScaleController.GetSpeedScale();

        if (isRunning)
        {
            currentSpeed = _runningSpeed;
        }
        else if (isSlowed || isAiming)
        {
            currentSpeed = _slowSpeed;
        }

        return currentSpeed;
    }
}
