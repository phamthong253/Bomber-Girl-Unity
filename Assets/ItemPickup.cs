using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum ItemType
    {
        ExtraBomb,
        ExtraExplosion,
        BoostSpeed,
        type
    }
    public ItemType type;
    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.ExtraExplosion:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.BoostSpeed:
                player.GetComponent<Player>().moveSpeed++;
                break;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }
}
