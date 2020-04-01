using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    public float damage = 100f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 1f;
    public float explosionForce = 5f;

    public void Start() {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D targetRigidBody = other.GetComponent<Rigidbody2D>();
        
        if (targetRigidBody){
            var dir = (targetRigidBody.transform.position - transform.position);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            targetRigidBody.AddForce(dir.normalized * explosionForce * wearoff);

            EnemyHealth targetHealth = targetRigidBody.GetComponent<EnemyHealth>();

            if (targetHealth) {
                targetHealth.TakeDamage(damage);
                
            }
        }

        Destroy(gameObject);
    }
    
}
