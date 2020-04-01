using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float jumpSpeed = 16f;
    
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private SpriteAnimator spriteAnimator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask platformLayerMask;
    private Func<Vector2> GetPlayerPosition;

    public void Setup(Func<Vector2> GetPlayerPosition) {
        this.GetPlayerPosition = GetPlayerPosition;
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteAnimator = GetComponent<SpriteAnimator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        rigidbody.isKinematic = false;
    }

    private void OnDisable() {
        rigidbody.isKinematic = true;
    }

    public void Update() {
        Move();
    }

    public void Move() {
        Vector2 playerPosition = GetPlayerPosition();

        // move towards player
        // flip sprite to the right (original orientation) 
        // if player X is greater than enemy X position
        if (transform.position.x < playerPosition.x - 0.5f) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            spriteAnimator.AnimateRun();

            rigidbody.velocity = new Vector2(movementSpeed, rigidbody.velocity.y);
        } else if (playerPosition.x + 0.5f < transform.position.x) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            spriteAnimator.AnimateRun();

            rigidbody.velocity = new Vector2(-movementSpeed, rigidbody.velocity.y);
        } else {
            spriteAnimator.AnimateIdle();
            rigidbody.velocity = new Vector2(0, 0);
        }

        
        
    }

    private bool IsGrounded() {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        return raycast.collider != null;
    }

    private void Attack() {
        Debug.Log("Enemy attacks you");
    }
}
