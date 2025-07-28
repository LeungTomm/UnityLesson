using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestory : MonoBehaviour
{
    [SerializeField] float timeTillDestory = 3f;

    private void Start()
    {
        Destroy(gameObject, timeTillDestory);
    }
}
