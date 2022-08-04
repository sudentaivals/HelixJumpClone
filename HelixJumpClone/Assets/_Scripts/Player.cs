using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _jumpForce;
    [SerializeField] float _maximumFallingSpeed;
    [SerializeField] GameObject _jumpVfx;
    [Header("SFX")]
    [SerializeField] AudioClip _jumpSfx;
    [SerializeField][Range(0f,1f)] float _jumpSfxVolume;
    private Rigidbody _rb;
    private bool _isJumpReady = true;
    private Vector3 _jumpDirection = Vector3.up;

    public Platform CurrentPlatform { get; private set; }

    private void OnEnable()
    {
        EventBus.Subscribe(EventBusEvent.GameOver, DisableBall);
        EventBus.Subscribe(EventBusEvent.Victory, DisableBall);

    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventBusEvent.GameOver, DisableBall);
        EventBus.Unsubscribe(EventBusEvent.Victory, DisableBall);

    }

    public void Jump()
    {
        if (_isJumpReady)
        {
            EventBus.Publish(EventBusEvent.PlaySound, this, new PlaySoundEventArgs(_jumpSfxVolume, _jumpSfx));
            _rb.velocity = Vector3.zero;
            _rb.AddForce(_jumpDirection * _jumpForce, ForceMode.Impulse);
            var jumpParticle = Instantiate(_jumpVfx, transform.position, _jumpVfx.transform.rotation);
            Destroy(jumpParticle.gameObject, 1f);
            ScoreManager.Instance.BreakCombo();
            StartCoroutine(JumpCooldown());
        }
    }

    private void DisableBall(UnityEngine.Object sender, EventArgs args)
    {
        _rb.isKinematic = true;
        _isJumpReady = false;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private IEnumerator JumpCooldown()
    {
        _isJumpReady = false;
        yield return new WaitForSeconds(0.2f);
        _isJumpReady = true;
    }

    public void SetPlatform(Platform newPlatform)
    {
        CurrentPlatform = newPlatform;
    }


    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, _maximumFallingSpeed, 100f), _rb.velocity.z);
    }

}
