using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using UnityEngine;

/* Connects PlayerCombat with Enemy actions by allowing player to damage enemy. Also allows enemy to patrol between ledges and walls.
 * Combat on enemy code by Brackeys: https://www.youtube.com/watch?v=sPiVz1k-fEs&t=678s
 * Patrol code by Wild Cockatiel Games: https://www.youtube.com/watch?v=7mkD9K2nwDM&t=544s, https://www.youtube.com/watch?v=XnoKMNdH-HU
 * Script completed by Danica
 */

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    
    //enemy movement/patrol
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;
    [SerializeField] private bool stayOnLedges = true;
    private int currentDirection;
    private float halfWidth;
    private float halfHeight;
    private UnityEngine.Vector2 movement;
    private bool isGrounded = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentHealth = maxHealth;
        animator.SetBool("IsRunning", true);
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
        currentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false : true;
    }

    private void FixedUpdate()
    {
        movement.x = speed * currentDirection;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
        SetDirection();
    }


    private void onCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void onCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    //Changes direction after hitting a wall or ledge
    private void SetDirection()
    {
        if(!isGrounded) return;   //stops sprite from spinning when not grounded

        UnityEngine.Vector2 rightPos = transform.position;
        UnityEngine.Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;

        if(rb.linearVelocity.x > 0)
        {
            if (Physics2D.Raycast(transform.position, UnityEngine.Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Default")))
            {
                //  Draw a ray starting at the center of our enemy and point it to the right
                //  check to see if the raycast is intersecting with a wall
                //  Also check to make sure our enemy is actually WALKING right
                //  If we don't do this check the enemy will get stuck moving constantly back and forth
                currentDirection *= -1;
                spriteRenderer.flipX = true;
            }
            else if (stayOnLedges && !Physics2D.Raycast(rightPos, UnityEngine.Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Default")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = true;
            }
        }
        else if(rb.linearVelocity.x < 0)
        {
            if (Physics2D.Raycast(transform.position, UnityEngine.Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Default")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = false;
            }
            else if (stayOnLedges && !Physics2D.Raycast(leftPos, UnityEngine.Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Default")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = false;
            }
        }

        // shows the raycast physically (easier to see what it hits)
        //UnityEngine.Debug.DrawRay(transform.position, UnityEngine.Vector2.right * (halfWidth + 0.1f), UnityEngine.Color.red);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
