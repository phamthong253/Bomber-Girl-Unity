using System.Collections;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Tilemap tilemap;
    public Transform playerTransform;
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Q;
    public float bombFuse = 3f;
    public int bombAmount = 5;
    public int bombRemaining;

    [Header("Explosions")]
    public Explosions explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructibles")]
    public Tilemap destructibleTiles;
    public DestroyBrick destructiblePrefab;

    private void OnEnable()
    {
        bombRemaining = bombAmount;
    }
    private void Update()
    {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        // position.x = Mathf.Round(position.x);
        // position.y = Mathf.Round(position.y);
        Vector3Int cell = tilemap.WorldToCell(playerTransform.position);
        Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);


        GameObject bomb = Instantiate(bombPrefab, cellCenter, Quaternion.identity);
        bombRemaining--;
        
        yield return new WaitForSeconds(bombFuse);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        Explosions explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }
        Explosions explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);
        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }
    public void AddBomb()
    {
        bombAmount++;
        bombRemaining++;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = true;
        }
    }
}