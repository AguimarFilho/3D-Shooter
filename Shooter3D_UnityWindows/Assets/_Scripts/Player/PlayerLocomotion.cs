using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private float _walkspeed;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private InputManager _inputManager;

    public float _jumpForce;

    [SerializeField] private float _gravityScale = -9.81f;
    [SerializeField] private float _gravityMultiplier = 1f;

    [HideInInspector] public float _velocityY;
    private Vector3 newDirection;

    private AnimationManager _animationManager;

    private void Awake()
    {
        _animationManager = FindObjectOfType<AnimationManager>();
    }
    
    private void Start()
    {
        _inputManager.GameControls.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        ApplyGravity();
        Walk();
    }

    private void Walk()
    {
        Vector2 walkDirection = _inputManager.GameControls.Player.Locomotion.ReadValue<Vector2>();

        newDirection = new Vector3(walkDirection.x, 0, walkDirection.y);
        newDirection.y = _velocityY;

        _characterController.Move(newDirection * (_walkspeed * Time.deltaTime));
    }

    private void ApplyGravity()
    {
        if(_characterController.isGrounded && _velocityY < 0f)
        {
            _velocityY = 0f;
        }
        else
        {
            _velocityY += _gravityScale * _gravityMultiplier * Time.deltaTime;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if(_characterController.isGrounded)
        {
            _animationManager.jump = true;
           
            _velocityY += _jumpForce;
        }
        else
        {
            _animationManager.jump = false;
        }
    }
}
