using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObj = new List<Collider2D>();
    public Collider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col.GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(tagTarget))
        {
            detectedObj.Add(collider);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(tagTarget))
        {
            detectedObj.Remove(collider);
        }
    }
}
