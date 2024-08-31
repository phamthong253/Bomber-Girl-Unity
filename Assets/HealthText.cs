using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 300;
    public Vector3 floatDirection = new(0, 1, 0);
    public TextMeshProUGUI textMesh;
    float timeElapse = 0.0f;
    RectTransform rTransform;
    Color staringColor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        staringColor = textMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapse += Time.deltaTime;
        rTransform.position += floatDirection * floatSpeed * Time.deltaTime;
        textMesh.color = new Color(staringColor.r, staringColor.g, staringColor.b, 1 - (timeElapse / timeToLive));
        if (timeElapse > timeToLive)
        {
            Destroy(gameObject);
        }
    }

}
