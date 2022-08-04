using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            GameManager.Instance.TriggerGameState(GameState.Victory);
        }
    }
}
