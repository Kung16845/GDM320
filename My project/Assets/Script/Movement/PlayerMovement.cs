using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    [SerializeField] private float _rotationSpeedl;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMoveInput;
    private Vector2 _movementInputSmoothVelocity;
    private Animator _AnimatorForMovement;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() 
    {   
        SetPlayerVelocity();
        RotateInDirecttionOfInput();
    }
    private void SetPlayerVelocity() 
    {
        _smoothedMoveInput = Vector2.SmoothDamp(_smoothedMoveInput,_movementInput,ref _movementInputSmoothVelocity,0.1f);

        _rigidbody.velocity = _smoothedMoveInput * _speed;
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

    private void SetAnimationForMoveMent(Vector2 PlayerMovement)
    {
        _AnimatorForMovement.SetFloat("Horizontal", PlayerMovement.x);
    }
    
}
