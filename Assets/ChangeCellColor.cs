using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeCellColor : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap tilemap_1;
    public Transform playerTransform;
    public Color color;
    public Tile borderTile;
    private Color originalColor;
    private Dictionary<Vector3Int, bool> borderStates = new Dictionary<Vector3Int, bool>();
    private Vector3Int lastPlayerCellPosition;

    private void Update()
    {

        // Kiểm tra xem Tilemap Renderer đã được kích hoạt hay chưa
        if (!tilemap.GetComponent<TilemapRenderer>().enabled)
        {
            Debug.LogWarning("Tilemap Renderer is not enabled. Color changes will not be displayed.");
            return;
        }
        //Add border cho cell

        // Lấy vị trí cell từ playerTransform
        Vector3Int cellPosition = tilemap.WorldToCell(playerTransform.position);
        TileBase tile = tilemap.GetTile(cellPosition);
        // Kiểm tra xem có TileBase tại vị trí cell hay không
        if (cellPosition != lastPlayerCellPosition)
        {
            if (tile != null)
            {
                lastPlayerCellPosition = cellPosition;
                StartCoroutine(TransitionColor(cellPosition, color, originalColor, 2f));
            }
        }
    }
    // Đặt màu sắc cho cell
    private IEnumerator TransitionColor(Vector3Int cellPosition, Color colorA, Color colorB, float pingPongDuration)
    {
        float timer = 0f;
        bool pingPongDirection = true; // true: A -> B, false: B -> A
        while (true)
        {
            float t = timer / pingPongDuration;
            if (pingPongDirection)
            {
                tilemap.SetTileFlags(cellPosition, TileFlags.None);
                tilemap.SetColor(cellPosition, Color.Lerp(colorA, colorB, t));
                AddBorderToCell(cellPosition);
            }
            timer += Time.deltaTime;

            if (t >= 1f)
            {
                pingPongDirection = !pingPongDirection;
                timer = 0f;
            }
            yield return null;
        }

    }
    private void AddBorderToCell(Vector3Int cellPosition)
    {
        if (cellPosition == tilemap.WorldToCell(playerTransform.position))
        {
            // Thêm border cho cell tại vị trí của người chơi
            tilemap_1.SetTile(cellPosition, borderTile);
            borderStates[cellPosition] = true; // Đánh dấu cell đã có border
        }
        else
        {
            // Xóa border cho các cell khác
            originalColor = new(143f / 255f, 143f / 255f, 143f / 255f, 1f);
            tilemap.SetColor(cellPosition, originalColor);
            tilemap_1.SetTile(cellPosition, null);
            borderStates[cellPosition] = false; // Đánh dấu cell không có border
        }
    }
}