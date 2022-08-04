using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] float _speed;
    [SerializeField] Vector3 _offset;

    void Update()
    {
        if (_player.CurrentPlatform == null) return;

        var targetPosition = _player.CurrentPlatform.transform.position + _offset;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
