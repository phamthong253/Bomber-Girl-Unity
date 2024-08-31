using System.Drawing;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
public class PlayerAttack : MonoBehaviour
{
    public float knockbackForce = 2f;
    public int damage = 3;
    public Vector3 faceRight = new Vector3(0.32f, 3.82f, 0);
    public Vector3 faceLeft = new Vector3(-1.97f, 3.79f, 0);
    public float swordAttackCD = 0.5f;
    private bool attackButtonDown, isAttacking = false;
    public Collider2D attackCollider;
    PlayerControls playerControls;
    Animator animator;
    Player player;
    private void Awake()
    {
        playerControls = new PlayerControls();
        player = GetComponentInParent<Player>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        playerControls.Player.Attack.started += _ => StartAttacking();
        playerControls.Player.Attack.canceled += _ => StopAttacking();
    }
    private void Update()
    {
        if(attackButtonDown && !isAttacking){
            Attack();
            animator.SetTrigger("Hit");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
        StopAttack();
    }
    public void IsFacingRight()
    {
        if (player.FacingLeft)
        {
            gameObject.transform.localPosition = faceLeft;
        }
        else
        {
            gameObject.transform.localPosition = faceRight;
        }
    }

    public void Attack()
    {
            player.LockMovement();
            isAttacking = true;
            attackCollider.gameObject.SetActive(true);
            player.animator.SetTrigger("Hit");
            StartCoroutine(AttackCDRoutine());
    }
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
        StopAttack();
    }
    public void StopAttack()
    {
        player.UnlockMovement();
        isAttacking = false;
        attackCollider.gameObject.SetActive(false);
        Debug.Log("Stop Attacking");
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IDamageable>(out var damageObject))
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2)(collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageObject.OnHit(damage, knockback);
        }
    }
}
