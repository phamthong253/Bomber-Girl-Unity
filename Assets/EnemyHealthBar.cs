using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    DamageableCharacter enemyDamageable;
    public float health = 1;
    private void Awake()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyDamageable = enemy.GetComponent<DamageableCharacter>();
    }
    void Start()
    {
        health = EnemyHealth(enemyDamageable.currentHealth, enemyDamageable.maxHealth);
    }
    private void OnEnable()
    {
        enemyDamageable.healthChange.AddListener(OnPlayerHealthChange);
    }
    private void OnDisable()
    {
        enemyDamageable.healthChange.RemoveListener(OnPlayerHealthChange);
    }
    private float EnemyHealth(float currentHealth, float maxHealth)
    {
        print("Enemy HP: " + currentHealth * 10 / maxHealth);
        return currentHealth * 10 / maxHealth;
    }
    private void OnPlayerHealthChange(int newHealth, int maxHealth)
    {
        health = EnemyHealth(newHealth, maxHealth);
    }
}

