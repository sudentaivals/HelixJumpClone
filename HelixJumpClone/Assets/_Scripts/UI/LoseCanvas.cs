using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCanvas : CanvasPresent
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.Subscribe(EventBusEvent.GameOver, ActivateCanvas);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.Unsubscribe(EventBusEvent.GameOver, ActivateCanvas);
    }


}
