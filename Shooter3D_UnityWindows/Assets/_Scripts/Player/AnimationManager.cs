using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private InputManager _inputManager;

    private float inputX;
    private float inputY;

    private void Update()
    {
        UpdateInputValues();
        UpdateAnimations();
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
