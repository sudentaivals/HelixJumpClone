using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float _rangeBetweenPlatforms;
    [SerializeField] Transform _platforms;

    private void Awake()
    {
        int counter = 0;
        foreach (Transform tr in _platforms)
        {
            tr.position = transform.position + new Vector3(0, counter * _rangeBetweenPlatforms * transform.localScale.x, 0);
            counter++;
        }
    }
}
