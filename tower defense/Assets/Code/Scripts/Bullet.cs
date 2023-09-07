using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int bulletDamage = 1;


    private Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void FixedUpdate() {
        if (!target) return;

        Vector3 targetPosition = target.position + (Vector3)(target.GetComponent<Rigidbody2D>().velocity * Time.fixedDeltaTime);

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

        // rotate vertical projectile to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; 
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Take health from enemy
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);

    }

}
