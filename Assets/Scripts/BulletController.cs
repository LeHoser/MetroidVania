using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Vector2 _moveDir;


    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _moveDir * _bulletSpeed;
        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
