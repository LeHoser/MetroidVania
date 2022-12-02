using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private GameObject _impactEffect;

    public Vector2 _moveDir;

    [SerializeField] private int _damageAmount = 1;


    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _moveDir * _bulletSpeed;
        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(_damageAmount);
        }

        Destroy(this.gameObject);

        Instantiate(_impactEffect, transform.position, Quaternion.identity);
    }
}
