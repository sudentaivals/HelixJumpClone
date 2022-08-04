using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Transform _level;
    [SerializeField] float _rotationSensivity;
    private Vector3 _previousMousePosition;

    private bool _isInputAvailable = true;

    private void OnEnable()
    {
        EventBus.Subscribe(EventBusEvent.GameOver, DisableControls);
        EventBus.Subscribe(EventBusEvent.Victory, DisableControls);

    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventBusEvent.GameOver, DisableControls);
        EventBus.Unsubscribe(EventBusEvent.Victory, DisableControls);

    }

    private void DisableControls(UnityEngine.Object sender, EventArgs args)
    {
        _isInputAvailable = false;
    }

    private void EnableControls(UnityEngine.Object sender, EventArgs args)
    {
        _isInputAvailable = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && _isInputAvailable)
        {
            var delta = _previousMousePosition - Input.mousePosition;
            _level.Rotate(0, delta.x * _rotationSensivity, 0);
        }
        _previousMousePosition = Input.mousePosition;
    }
}
