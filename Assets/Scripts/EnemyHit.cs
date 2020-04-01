using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float damage = 50f;
    public LayerMask playerMask;
    public PlayerHealth playerHealth;
    private SpriteAnimator spriteAnimator;
    private Collider2D collider;
    private ContactFilter2D filter2D;
    private float delay, timer;

    private void Start() {
        spriteAnimator = GetComponent<SpriteAnimator>();
        collider = GetComponent<Collider2D>();
        filter2D.SetLayerMask(playerMask);
        delay = 1f;
    }

    void Update() {
        timer += Time.deltaTime;
        EnemyAttack();    
    }

    private void EnemyAttack() {
        if (collider.IsTouching(filter2D)) {
            spriteAnimator.AnimateAttack();

            if (timer >= delay && playerHealth.health >= 0f) {
                timer = 0f;
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
