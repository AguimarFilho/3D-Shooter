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

    [HideInInspector] public float _velocityY;
    private Vector3 _newDirection;

    [Header("Ground & Air Verification")]
    [Space(15)]

    [SerializeField] private float _groundDetectionRayStartPoint = 0.5f;
    [SerializeField] private float _minimumDistanceToFall = 1f;
    [SerializeField] private float _groundDirectionRayDistance = 0.2f;
    [SerializeField] private LayerMask _ignorForGroundCheck;
    private bool IsGrounded = false;
    private bool IsInAir;


    private AnimationManager _animationManager;

    [Header("Gravity Parameters")]

    [SerializeField] private float _gravityScale = -9.81f;
    [SerializeField] private float _gravityMultiplier = 1f;
    private Vector3 _normalVector;
    private Vector3 _targetPosition;
    private float _isInAirTime;

    public bool _isJumpPressed;

    private void Awake()
    {
        _animationManager = FindObjectOfType<AnimationManager>();


        //_inputManager.GameControls.Player.Jump.performed += Jump;
        //_inputManager.GameControls.Player.Jump.canceled += Jump;
    }

    private void Update()
    {
        ApplyGravity(_newDirection);
        Walk();
    }
    private void LateUpdate()
    {
        _isInAirTime = _isInAirTime + Time.deltaTime;
    }

    private void Walk()
    {
        Vector2 walkDirection = _inputManager.GameControls.Player.Locomotion.ReadValue<Vector2>();

        _newDirection = new Vector3(walkDirection.x, 0, walkDirection.y);
        _newDirection.y = _velocityY;

        _characterController.Move(_newDirection * (_walkspeed * Time.deltaTime));
    }

    private void ApplyGravity(Vector3 moveDirection)
    {
        _newDirection.y = _velocityY;

        IsGrounded = false;
        RaycastHit Hit;
        Vector3 origin = transform.localPosition;
        origin.y += _groundDetectionRayStartPoint;

        if(Physics.Raycast(origin, transform.forward, out Hit, 0.4f))
        {
            _velocityY = -1f;
        }
        if(IsInAir)
        {
            _velocityY = _gravityScale * _gravityMultiplier;
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * _groundDirectionRayDistance;

        Debug.DrawRay(origin, -Vector3.up * _minimumDistanceToFall, Color.red, 0.1f, false);

        if(Physics.Raycast(origin, -Vector3.up, out Hit, _minimumDistanceToFall, _ignorForGroundCheck))
        {
            _normalVector = Hit.normal;
            Vector3 tp = Hit.point;
            IsGrounded = true;
            _targetPosition.y = tp.y;

            if(IsInAir)
            {
                if(_isInAirTime > 0.5f)
                {
                    Debug.Log("To na transição da queda pro  chão");
                    _isInAirTime = 0;
                }
                else
                {
                    Debug.Log("To no chão");
                    _isInAirTime = 0;
                }

                IsInAir = false;
            }
        }
        else
        {
            if(IsGrounded)
            {
                IsGrounded = false;
            }
            if(IsInAir == false)
            {
                Debug.Log("Caindo porra");

                IsInAir = true;
            }
        }
    }

   private void Jump(InputAction.CallbackContext ctx)
    {
        _isJumpPressed = ctx.ReadValueAsButton();
        
        if(IsGrounded && _isJumpPressed)
        {
            newDirection.y += _jumpForce;

            if (!IsGrounded)
            {
                newDirection.y = _velocityY;
            }
        }
    }
}
