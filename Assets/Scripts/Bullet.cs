using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Tween moveTween = null;


    public void Fire(Transform target, Action OnComplete = null)
    {
        moveTween.Stop();

        Vector3 lastPos = transform.position;

        transform.DOJump(target.position, 2, 1, 0.5f).OnUpdate(() => 
        {
            Vector3 dir = lastPos - transform.position;
            transform.right = -dir;
            lastPos = transform.position;
        }).OnComplete(() => 
        {
            OnComplete?.Invoke();
            Destroy(gameObject);
        });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
