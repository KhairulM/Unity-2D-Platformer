using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 1000f;
    [HideInInspector] public float health;
    private SpriteAnimator spriteAnimator;

    private void Awake() {
        spriteAnimator = GetComponent<SpriteAnimator>();
        health = maxHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log(health);

        if (health <= 0f) {
            spriteAnimator.AnimateDead();
        }
    }
}
