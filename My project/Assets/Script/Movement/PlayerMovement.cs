using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _slowSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _rotationSpeedl;
    private bool isRunning = false;
    private bool isSlowed = false;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMoveInput;
    private Vector2 _movementInputSmoothVelocity;



    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() 
    {   
        SetPlayerVelocity();
        RotateInDirecttionOfInput();     
    }

    private void Update()
    {
    // Toggle running when Left Shift is pressed.
    if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
    {
        isRunning = !isRunning;
    }

    // Toggle slowing down when Alt is pressed.
    if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
    {
    isSlowed = !isSlowed;
    }

    }
    private void SetPlayerVelocity()
    {
    _smoothedMoveInput = Vector2.SmoothDamp(_smoothedMoveInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);

    float currentSpeed = _speed;

    if (isRunning)
    {
        currentSpeed = _runningSpeed;
    }
    else if (isSlowed)
    {
        currentSpeed = _slowSpeed;
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

}
