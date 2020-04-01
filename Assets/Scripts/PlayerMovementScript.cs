using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float m_MovementSpeed = 5f;
    public float m_JumpSpeed = 16f;
    public float m_MidAirSpeed = 1f;

    private Rigidbody2D m_Rigidbody2d;
    private BoxCollider2D m_BoxCollider2d;
    [SerializeField] private LayerMask m_PlatformLayerMask;
    [SerializeField] private LayerMask m_EnemyLayerMask;
    private SpriteAnimator m_SpriteAnimator;
    private SpriteRenderer m_SpriteRenderer;

    private void Awake(){
        m_Rigidbody2d = GetComponent<Rigidbody2D>();
        m_BoxCollider2d = GetComponent<BoxCollider2D>();
        m_SpriteAnimator = GetComponent<SpriteAnimator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable(){
        m_Rigidbody2d.isKinematic = false;
    }

    private void OnDisable(){
        m_Rigidbody2d.isKinematic = true;
        m_SpriteAnimator.AnimateDead();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {   
            transform.rotation = Quaternion.Euler(0, 180, 0);

            if (IsGrounded())
            {
                m_SpriteAnimator.AnimateRun();
                m_Rigidbody2d.velocity = new Vector2(-m_MovementSpeed, m_Rigidbody2d.velocity.y);
            } else 
            {
                m_Rigidbody2d.velocity += new Vector2(-m_MovementSpeed * m_MidAirSpeed * Time.deltaTime, 0);
                m_Rigidbody2d.velocity = new Vector2(Mathf.Clamp(m_Rigidbody2d.velocity.x, -m_MovementSpeed, m_MovementSpeed), m_Rigidbody2d.velocity.y);
            }
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (IsGrounded())
            {
                m_SpriteAnimator.AnimateRun();
                m_Rigidbody2d.velocity = new Vector2(m_MovementSpeed, m_Rigidbody2d.velocity.y);
            } else 
            {
                m_Rigidbody2d.velocity += new Vector2(m_MovementSpeed * m_MidAirSpeed * Time.deltaTime, 0);
                m_Rigidbody2d.velocity = new Vector2(Mathf.Clamp(m_Rigidbody2d.velocity.x, -m_MovementSpeed, m_MovementSpeed), m_Rigidbody2d.velocity.y);
            }
        } else 
        {
            if (IsGrounded()) 
            {
                m_SpriteAnimator.AnimateIdle();
                m_Rigidbody2d.velocity = new Vector2(0, m_Rigidbody2d.velocity.y);

            }
        }
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.W))
        {
            m_SpriteAnimator.AnimateJump();
            m_Rigidbody2d.velocity = new Vector2(m_Rigidbody2d.velocity.x, m_JumpSpeed);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastPlatform = Physics2D.BoxCast(m_BoxCollider2d.bounds.center, m_BoxCollider2d.bounds.size, 0f, Vector2.down, .1f, m_PlatformLayerMask);
        RaycastHit2D raycastEnemy = Physics2D.BoxCast(m_BoxCollider2d.bounds.center, m_BoxCollider2d.bounds.size, 0f, Vector2.down, .1f, m_EnemyLayerMask);
        return raycastPlatform.collider != null || raycastEnemy.collider != null;
    }
}
