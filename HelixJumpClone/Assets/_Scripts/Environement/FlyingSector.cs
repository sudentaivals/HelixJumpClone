using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSector : MonoBehaviour
{
    private Rigidbody _rb;
    private float _rotationSpeed = 180;
    private float _maxForce = 30;
    private float _minForce = 20;
    [SerializeField]private Vector3 _rotationAxis;
    void Start()
    {
        Destroy(gameObject.GetComponent<MeshCollider>());
        _rb = gameObject.AddComponent<Rigidbody>();
        SelectRotationAxis();
        transform.parent = null;
        PushPlatform();
        Destroy(gameObject, 2f);
    }

    private void SelectRotationAxis()
    {
        _rotationAxis = new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
    }
    private void PushPlatform()
    {
        var direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), Random.Range(0f, 1f), Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y)).normalized;
        _rb.AddForce(direction * Random.Range(_minForce, _maxForce), ForceMode.Impulse);
    }

    void Update()
    {
        transform.Rotate(_rotationAxis * _rotationSpeed * Time.deltaTime);
    }
}
