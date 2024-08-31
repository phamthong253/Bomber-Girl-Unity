using System;
using System.ComponentModel;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatsUI : MonoBehaviour
{
    public TMP_Text bombText;
    public TMP_Text explosionText;
    public TMP_Text speedText;
    public TMP_Text heartText;

    private int heartAmount;
    private int bombAmount;
    private int explosionAmount;
    private float speedAmount;


    private void Start()
    {
        heartAmount = 0;
        bombAmount = GetComponent<BombController>().bombAmount;
        explosionAmount = GetComponent<BombController>().explosionRadius;
        speedAmount = GetComponent<Player>().moveSpeed;

        bombText.text = $"{bombAmount}";
        explosionText.text = $"{explosionAmount}";
        speedText.text = $"{speedAmount}";
        heartText.text = $"{heartAmount}";
    }
    public void IncreaseStats(ItemPickup.ItemType type)
    {
        switch (type)
        {
            case ItemPickup.ItemType.ExtraBomb:
                bombAmount++;
                bombText.text = $"{bombAmount}";
                Debug.Log("Bomb Amount: " + bombAmount);
                break;
            case ItemPickup.ItemType.ExtraExplosion:
                explosionAmount++;
                explosionText.text = $"{explosionAmount}";
                Debug.Log("Explosion Amount: " + explosionAmount);
                break;
            case ItemPickup.ItemType.BoostSpeed:
                speedAmount++;
                speedText.text = $"{speedAmount}";
                Debug.Log("Speed Amount: " + speedAmount);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (other.gameObject.TryGetComponent<ItemPickup>(out var itemPickup))
            {
                IncreaseStats(itemPickup.type);
            }
        }
    }
}
