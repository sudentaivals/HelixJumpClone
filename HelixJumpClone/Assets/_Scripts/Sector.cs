using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    [SerializeField] bool _isGood;
    [SerializeField] float _bounceDotLimit = 0.5f;

    public void DestroySector()
    {
        gameObject.AddComponent<FlyingSector>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<Player>(out var player)) return;

        var dot = Vector3.Dot(collision.GetContact(0).normal.normalized * -1, Vector3.up);
        if (dot < _bounceDotLimit) return;
        if (_isGood)
        {
            player.Jump();

        }
        else
        {
            GameManager.Instance.TriggerGameState(GameState.GameOver);
        }
    }

}
