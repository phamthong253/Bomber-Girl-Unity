using UnityEngine;
using UnityEngine.AI;

public class Boss : EnemyAI
{
    NavMeshAgent agent;
    EnemyHealthBar health;
    public GameObject healthBar;
    private int damage;
    private Animator animator;
    private Quaternion initialRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<GameObject>();
        initialRotation = transform.rotation;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        BeginStage();
    }

    // Update is called once per frame
    void Update()
    {
        // if (agent.velocity != Vector3.zero)
        // {
        //     // Apply the initial rotation
        //     transform.rotation = initialRotation;
        // }
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     IDamageable damageable = GetComponent<IDamageable>();
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Explosions"))
    //     {
    //         Vector2 direction = (transform.position - transform.position).normalized;
    //         Vector2 knockback = direction * knockbackForce;
    //         damageable.OnHit(damage, knockback);
    //         animator.SetTrigger("hasAttacked");
    //         if (health.health <= 0)
    //         {
    //             animator.SetBool("Defeated", true);
    //         }
    //         // Debug.Log("Enemy Died");
    //         // DeathSequence();
    //     }
    // }
    private void BeginStage()
    {
        Debug.Log("Health: " + health);
        Debug.Log("Begin Stage");

    }
}
