using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private float _walkspeed;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private InputManager _inputManager;

    private void Walk()
    {
        Vector2 walkDirection = _inputManager.GameControls.Player.Locomotion.ReadValue<Vector2>();

        Vector3 newDirection = new Vector3(walkDirection.x, 0, walkDirection.y);

        _characterController.Move(newDirection * (_walkspeed * Time.deltaTime));
    }

    private void Update()
    {
        Walk();
    }
}
