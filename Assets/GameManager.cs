using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winnerUI;
    public bool isWinner = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] players;
    public void CheckWinState()
    {
        int aliveCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }
        if (aliveCount <= 1)
        {
            DisplayWinner();
            Invoke(nameof(NewRound), 3f);
        }
    }
    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void DisplayWinner()
    {
        if (!isWinner)
        {
            isWinner = true;
            winnerUI.SetActive(true);
        }
    }
}
