using UnityEngine;

public class DestroyBrick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float destructibleTime = 1f;
    [Range(0.1f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;
    private void Start(){
        Destroy(gameObject, destructibleTime);
    }
    private void OnDestroy(){
        if(spawnableItems.Length > 0 && Random.value < itemSpawnChance){
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
