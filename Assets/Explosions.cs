using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Explosions : MonoBehaviour
{
    [Header("Explosions")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer){
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }
    public void SetDirection(Vector2 direction){
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg ,Vector3.forward);
    }
    public void DestroyAfter(float sec){
        Destroy(gameObject, sec);
    }
}
