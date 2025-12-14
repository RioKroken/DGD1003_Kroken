using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayer;
    public float attackDuration = 0.3f;
    private bool isAttacking = false;

    private Rigidbody2D rb;
    private float horizontal;
    private bool isGrounded;
    private bool facingRight = true;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Smooth horizontal movement (A / D)
        horizontal = Input.GetAxis("Horizontal");

        // W = Jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isAttacking)
        {
            Jump();
        }

        // Left Click = Attack
        if (Input.GetMouseButtonDown(0) && isGrounded && !isAttacking)
        {
            Attack();
        }

        FlipCheck();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(horizontal));
        animator.SetFloat("yVelocity", (rb.linearVelocityY));
        CheckGround();
        animator.SetBool("isJumping", !isGrounded);
    }

    void FlipCheck()
    {
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Attack()
    {
        if (isAttacking) return;

        animator.SetTrigger("Attack");
        isAttacking = true;
        StartCoroutine(AttackCoroutine());
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        // Düþman kontrolü
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
            }
        }

        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();

        if (collectable != null)
        {
            collectable.Collect();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
