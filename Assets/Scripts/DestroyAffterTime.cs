using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAffterTime : MonoBehaviour
{
    public float time = 3f;


    private void OnEnable()
    {
        StartCoroutine(IEDestroy());
    }


    private IEnumerator IEDestroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
