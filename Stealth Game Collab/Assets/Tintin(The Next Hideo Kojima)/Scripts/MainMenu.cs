using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MainMenuUI;


    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)                              // I don't know if I don't want this yet
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    */
    /* public void StartGame()
     {
         MainMenuUI.SetActive(false);
         Time.timeScale = 1f;
         IsPaused = false;
     }

     void Pause()
     {
         pauseMenuUI.SetActive(true);
         Time.timeScale = 0f;
         IsPaused = true;
     }
     */
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadSettings()
    {
        //SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

