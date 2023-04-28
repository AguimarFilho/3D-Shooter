using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private InputManager _inputManager;

    [SerializeField] private CharacterController _characterController;

    private PlayerLocomotion _playerLocomotion;

    private float inputX;
    private float inputY;

    [HideInInspector] public bool jump;

    private void Awake()
    {
        _playerLocomotion = FindObjectOfType<PlayerLocomotion>();
    }
    
    private void Update()
    {
        UpdateInputValues();
        UpdateAnimations();

        if(_characterController.isGrounded)
        {
            _animator.SetBool("Jumping", jump);
        }
        else
        {
            _animator.SetBool("Jumping", jump);
        }
    }
    
    private void UpdateInputValues()
    {
        inputX = _inputManager.GameControls.Player.Locomotion.ReadValue<Vector2>().x;
        inputY = _inputManager.GameControls.Player.Locomotion.ReadValue<Vector2>().y;
    } 

    private void UpdateAnimations()
    {
        _animator.SetFloat("InputX", inputX);
        _animator.SetFloat("InputY", inputY);
    }
}
