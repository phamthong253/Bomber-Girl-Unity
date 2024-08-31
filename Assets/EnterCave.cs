using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int sceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            Debug.Log("Da cham vao !");
        }
    }
}
