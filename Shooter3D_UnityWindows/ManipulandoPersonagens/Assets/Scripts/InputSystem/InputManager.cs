using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameControls _gameControls;

    private void Awake()
    {
        _gameControls = new GameControls();
    }

    private void OnEnable()
    {
        _gameControls.Enable();
    }

    private void OnDisable()
    {
        _gameControls.Disable();
    }

    public GameControls GameControls
    {
        get { return _gameControls; }
    }
}
