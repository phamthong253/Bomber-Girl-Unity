using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Movement")]
    public float moveSpeed = 5f;
    public bool canMove = true;
    public float maxSpeed = 4f;
    public float idleFriction = 0.9f;
    public bool FacingLeft { get { return facingLeft; } }
    public bool facingLeft = false;
    public float collisionOffset = 0.05f;

    [Header("Dash")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    public HealthBar healthBar;
    [SerializeField] Transform hand;


    private Rigidbody2D rb;
    Vector2 moveInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    public Animator animator;
    public PlayerAttack attack;
    public ContactFilter2D moveFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private TrailRenderer trailRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * moveInput, ForceMode2D.Force);
            if (rb.velocity.magnitude > maxSpeed)
            {
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }
            bool success = TryMove(moveInput);
            if (!success)
            {
                success = TryMove(new Vector2(moveInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, moveInput.y));
                }
            }
            animator.SetBool("isMoving", success);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            animator.SetBool("isMoving", false);
        }

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
            // gameObject.BroadcastMessage("IsFacingRight", true);
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
            // gameObject.BroadcastMessage("IsFacingRight", false);
        }
    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                moveFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }
            animator.SetBool("isDashing", isDashing);
        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }
        if(!isDashing){
            canDash = true;
        }
    }
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void LockMovement()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }
    public void Attack()
    {
        attack.Attack();
    }
    public void EndAttack()
    {
        UnlockMovement();
        attack.StopAttack();
    }
    // void RotateHand()
    // {
    //     float angle = Utility.AngleTowardMouse(hand.position);
    //     hand.position = Quaternion.Euler(new Vector3(0f, 0f, angle));
    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosions"))
        {
            DeathSequence();
        }
    }
    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;
        Invoke(nameof(OnDeathSequenceEnded), 1f);
    }
    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }

}
