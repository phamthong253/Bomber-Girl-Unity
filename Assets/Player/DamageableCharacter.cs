using UnityEngine;
using UnityEngine.Events;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public UnityEvent<int, int> healthChange;
    public GameObject healthText;
    public bool disabledSimulation = false;
    public bool canturnInvisible = false;
    public float invicibilityTime = 0.25f;
    private float invicibleTimeElapse = 0f;
    public int maxHealth = 20;
    public int currentHealth;
    // public HealthBar healthBar;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool Defeated = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    public int Health
    {
        set
        {
            if (value < currentHealth)
            {
                // animator.SetTrigger("Hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }
            currentHealth = value;
            healthChange?.Invoke(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                animator.SetBool("Defeated", Defeated);
                Targetable = false;
                // ObjectDestroy();
            }
        }
        get
        {
            return currentHealth;
        }
    }

    public bool Targetable
    {
        get { return targetable; }
        set
        {
            targetable = value;
            physicsCollider.enabled = value;
            if (disabledSimulation)
            {
                rb.simulated = false;
            }
        }
    }

    public bool Invicible
    {
        get
        {
            return invicible;
        }
        set
        {
            invicible = value;
            if (Invicible == true)
            {
                invicibleTimeElapse = 0f;
            }
        }
    }

    public bool invicible = false;
    bool targetable = true;
    private void Start()
    {
        currentHealth = maxHealth;
        // healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        animator.SetBool("Defeated", false);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }
    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

    [System.Obsolete]
    public void OnHit(int damage, Vector2 knockback)
    {
        if (!Invicible)
        {
            currentHealth -= damage;
            // healthBar.SetHealth(currentHealth);
            Health -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            animator.SetTrigger("hasAttacked");
            if (canturnInvisible)
            {
                Invicible = true;
            }
        }
    }

    [System.Obsolete]
    public void OnHit(int damage)
    {
        if (!Invicible)
        {
            currentHealth -= damage;
            // healthBar.SetHealth(currentHealth);
            Health -= damage;
            if (canturnInvisible)
            {
                Invicible = true;
                animator.SetTrigger("hasAttacked");
            }
        }
    }
    void FixedUpdate()
    {
        if (Invicible)
        {
            invicibleTimeElapse += Time.deltaTime;
            if (invicibleTimeElapse > invicibilityTime)
            {
                Invicible = false;
            }
        }
    }
}
