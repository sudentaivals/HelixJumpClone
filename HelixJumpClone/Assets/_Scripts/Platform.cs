using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Vector3 _destroyCheckerOffset;
    [SerializeField] float _boxWidthAndHeight;
    [SerializeField] LayerMask _destroyerMask;
    private Collider[] _destroyers = new Collider[1];
    private bool _platformDestroyed = false;
    private bool IsDestroyCheckerTouchPlayer => Physics.OverlapBoxNonAlloc(transform.position + _destroyCheckerOffset, new Vector3(_boxWidthAndHeight * transform.lossyScale.x, 0.25f, _boxWidthAndHeight * transform.lossyScale.z), _destroyers, Quaternion.identity, _destroyerMask) > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.SetPlatform(this);
        }
    }

    private void FixedUpdate()
    {
        if (IsDestroyCheckerTouchPlayer && !_platformDestroyed)
        {
            DestroyPlatform();
            EventBus.Publish(EventBusEvent.PlatformDestroyed, this, null);
        }
    }

    private void DestroyPlatform()
    {
        foreach (Transform tr in transform)
        {
            if (tr.gameObject.TryGetComponent(out Sector sector))
            {
                sector.DestroySector();
            }
        }
        _platformDestroyed = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position + _destroyCheckerOffset, new Vector3(_boxWidthAndHeight * transform.lossyScale.x, 0.25f, _boxWidthAndHeight * transform.lossyScale.z));
    }

}