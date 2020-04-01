using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 300f;
    public int score = 10;

    private float health;
    private SpriteAnimator spriteAnimator;
    private GameManager gameManager;

    public void Setup(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    void Awake() {
        spriteAnimator = GetComponent<SpriteAnimator>();
        health = maxHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0f) {
            gameManager.AddScore(score);

            spriteAnimator.AnimateDead();
            StartCoroutine(EnemyWaitDead(3));
            Destroy(gameObject);
        }
    }

    private IEnumerator EnemyWaitDead(float seconds) {
        yield return new WaitForSeconds(seconds);
    }
}
