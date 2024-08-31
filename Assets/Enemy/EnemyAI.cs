using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Collections;
public class EnemyAI : MonoBehaviour
{
    [Header("Movement Patrol")]
    NavMeshAgent agent;
    Vector3 target;
    public Transform[] waypoints;
    public float posTime;
    int waypointIndex;

    [Header("Info Enemy")]
    private Animator animator;
    public float knockbackForce = 10f;
    private int damage = 1;
    EnemyHealthBar health;

    [Header("Spawn Tile")]
    public GameObject[] gameObjects;
    public GameObject enemy;
    readonly GameObject[] enemises;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = this.GetComponent<EnemyHealthBar>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(GoPosPoint());
        if (enemises.Length >= 0 && enemises.Length <= 4)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(gameObjects[Random.Range(0, gameObjects.Length)], transform.position, Quaternion.identity);
        }

    }
    void Update()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = GetComponent<IDamageable>();
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosions"))
        {
            Vector2 direction = (transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageable.OnHit(damage, knockback);
            animator.SetTrigger("hasAttacked");
            if (health.health <= 0)
            {
                animator.SetBool("Defeated", true);
                enabled = false;
            }
            // DeathSequence();
        }
    }

    IEnumerator GoPosPoint()
    {
        yield return new WaitForSeconds(posTime);
        waypointIndex = Random.Range(0, waypoints.Length);
        StartCoroutine(GoPosPoint());
    }
    private void DeathSequence()
    {
        enabled = false;
        Invoke(nameof(OnDeathSequenceEnded), 1f);
    }
    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}
