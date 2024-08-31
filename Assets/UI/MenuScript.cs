using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPause = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     pauseMenu.SetActive(false);
    // }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(!isPause){
                PauseGame();
            }else{
                ResumeGame();
            }
        }
    }
    public void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    public void MainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    public void Quit(){
        Application.Quit();
    }

}
