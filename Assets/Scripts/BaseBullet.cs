using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 25f;

    private Transform target;

    public void SetTarget(Transform l_target)
    {
        this.target = l_target;
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
