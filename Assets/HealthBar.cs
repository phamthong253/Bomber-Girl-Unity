using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    DamageableCharacter playerDamageable;
    public Slider slider;
    public TMP_Text healthBarText;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<DamageableCharacter>();
    }
    void Start()
    {
        slider.value = CalculateSliderPercentage(playerDamageable.currentHealth, playerDamageable.maxHealth);
        healthBarText.text = "HP: " + playerDamageable.currentHealth + "/" + playerDamageable.maxHealth;
    }
    private void OnEnable()
    {
        playerDamageable.healthChange.AddListener(OnPlayerHealthChange);
    }
    private void OnDisable()
    {
        playerDamageable.healthChange.RemoveListener(OnPlayerHealthChange);
    }
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        print("HP: " + currentHealth * 10 / maxHealth);
        return currentHealth * 10 / maxHealth;
    }
    private void OnPlayerHealthChange(int newHealth, int maxHealth)
    {
        slider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP: " + newHealth + "/" + maxHealth;
    }
}

