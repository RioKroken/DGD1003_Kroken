using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        yield return new WaitForSeconds(0.15f); // Small delay to sync with animation
        isAttacking = true;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(enemyLayer);
        filter.useTriggers = false;

        Collider2D[] results = new Collider2D[10];

        int hitCount = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRadius,
            filter,
            results
        );

        for (int i = 0; i < hitCount; i++)
        {
            if (results[i].CompareTag("Enemy"))
            {
                Destroy(results[i].gameObject);
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

        if (other.GetComponent<Door>() != null)
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex + 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Yeniden başlat
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
