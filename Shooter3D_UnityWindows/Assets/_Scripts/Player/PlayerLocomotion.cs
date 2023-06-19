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
// Verificação de solo e tags
    [Header("Ground, Checks and Tags")]
    [Space(15)]
    [SerializeField] private float _minimumDistanceToFall = 1f; // Distância mínima para detectar queda
    [SerializeField] private LayerMask _ignoreForGroundCheck; // LayerMask para ignorar na verificação de solo
    [SerializeField] private GameObject _leftFoot; // Pé esquerdo
    [SerializeField] private Vector3 _leftFootOffset; // Deslocamento do pé esquerdo
    [SerializeField] private Vector3 _centerOffset; // Deslocamento do centro do personagem
    [SerializeField] private GameObject _rightFoot; // Pé direito
    [SerializeField] private Vector3 _rightFootOffset; // Deslocamento do pé direito
    private bool _isGrounded = false;



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


        _inputManager.GameControls.Player.Jump.performed += OnJump;
        _inputManager.GameControls.Player.Jump.canceled += OnJump;
    }

    private void Update()
    {
        HandleGravity();
        Jump();
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

    private void HandleGravity()
    {
        // Verifica se algum dos pés ou o centro do personagem está no chão
        bool isCenterGrounded = Physics.Raycast(transform.position + _centerOffset, -Vector3.up, out RaycastHit centerHit, _minimumDistanceToFall, _ignoreForGroundCheck);
        bool isRightFootGrounded = Physics.Raycast(_rightFoot.transform.position + _rightFootOffset, -Vector3.up, out RaycastHit rightFootHit, _minimumDistanceToFall, _ignoreForGroundCheck);
        bool isLeftFootGrounded = Physics.Raycast(_leftFoot.transform.position + _leftFootOffset, -Vector3.up, out RaycastHit leftFootHit, _minimumDistanceToFall, _ignoreForGroundCheck);


        bool isAnyFootGrounded = isCenterGrounded || isRightFootGrounded || isLeftFootGrounded;

        if (isAnyFootGrounded)
        {
            // O personagem está no chão
            _isGrounded = true;
            _velocityY = -1f; // Reiniciar a velocidade vertical
        }
        else if(!isAnyFootGrounded)
        {
            // O personagem está no ar
            _isGrounded = false;
            _velocityY += _gravityScale * _gravityMultiplier * Time.deltaTime; // Aplicar gravidade
        }
    }

    private void OnDrawGizmos()
    {
    // Exibe os raios no Editor Unity para verificar a detecção de solo
    Gizmos.color = Color.red;
    Gizmos.DrawRay(transform.position + _centerOffset, -Vector3.up * _minimumDistanceToFall);
    Gizmos.DrawRay(_rightFoot.transform.position + _rightFootOffset, -Vector3.up * _minimumDistanceToFall);
    Gizmos.DrawRay(_leftFoot.transform.position + _leftFootOffset, -Vector3.up * _minimumDistanceToFall);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        _isJumpPressed = ctx.ReadValueAsButton();
    }

    private void Jump()
    {
        if(_isGrounded && _isJumpPressed)
        {
            _velocityY += _jumpForce;

            if (!_isGrounded)
            {
                _velocityY = _velocityY;
            }
        }
    }
}
