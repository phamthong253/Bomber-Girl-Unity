using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public EnemyHealthBar healthBar;
    public EnemyAttack enemyAttack;
    public GameObject player;
    public int damage = 1;
    public float knockbackForce = 10f;
    public float moveSpeed = 200f;
    Rigidbody2D rb;
    public DetectionZone detectionZone;
    bool hasTarget = true;
    bool canDame = true;
    public bool flip;
    public Animator animator;
    DamageableCharacter damageableCharacter;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }
    void Start()
    {
        damageableCharacter = GetComponent<DamageableCharacter>();
        damageableCharacter.currentHealth = 10;
    }
    void FixedUpdate()
    {
        if (damageableCharacter.Targetable && detectionZone.detectedObj.Count > 0)
        {
            Vector3 scale = transform.localScale;
            Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
            // Follow the object target
            if (player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                animator.SetBool("hasTarget", hasTarget);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }
            transform.localScale = scale;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("hasTarget", false);
        }
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        healthBar.transform.position = screenPos + new Vector3(0, 80, 0);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (collider.CompareTag("Enemy"))
        {
            // Ignore the collision between the enemy and the player
            Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
            canDame = false;
        }
        else if (collider.CompareTag("Player"))
        {
            if (damageable != null)
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;
                damageable.OnHit(damage, knockback);
                Invoke(nameof(EnableDamage), 1f);
            }
        }
    }
    void EnableDamage()
    {
        canDame = true;
    }
}
