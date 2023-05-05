using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private InputManager _inputManager;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private PlayerLocomotion _playerLocomotion;

    private float inputX;
    private float inputY;

    [HideInInspector] public bool jump;


    private void Update()
    {
        UpdateInputValues();
        UpdateAnimations();

        JumpAndFallAnimations();
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

    private void JumpAndFallAnimations()
    {
        if(_playerLocomotion._isJumpPressed && _characterController.isGrounded)
        {
            _animator.SetBool("Jumping", true);
        }
        
        if(!_playerLocomotion._isJumpPressed)
        {
            if(!_characterController.isGrounded)
            {
                _animator.SetBool("Falling", true);
            }
        }

        if (_characterController.isGrounded && !_playerLocomotion._isJumpPressed)
        {
            _animator.SetBool("Jumping", false);
            _animator.SetBool("Falling", false);
        }
    }
}
