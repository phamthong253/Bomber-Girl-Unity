using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider2D attackCollider;
    public float knockbackForce = 2f;
    private int damage = 1;
    // Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackCollider = GetComponent<Collider2D>();
        // animator = GetComponent<Animator>();
    }
    public void AttackRight()
    {
        attackCollider.enabled = true;
    }
    public void AttackLeft()
    {
        attackCollider.enabled = true;
    }
    public void StopAttack()
    {
        attackCollider.enabled = false;
    }
    // Update is called once per frame
    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageableCharacter damageable = collision.GetComponent<DamageableCharacter>();
        if (collision.CompareTag("Enemy"))
        {
            // Ignore the collision between the enemy and the player
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
            // enemy.EnableDamage() = false;
        }
        else
        {
            if (damageable != null)
            {
                damageable.OnHit(damage);
                // Invoke(nameof(EnableDamage), 1f);
                attackCollider.enabled = true;
            }
        }
    }
}
