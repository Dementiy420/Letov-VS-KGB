using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public AudioSource mainmusic;
    public bool pause;
    public GameObject pauseGame;
    public GameObject loseGame;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
                Resume();
            else
                Pause();
        }
        if (loseGame.activeSelf) 
        {
            mainmusic.Stop();
        }
    }

    public void Resume() 
    {
        mainmusic.volume = 0.5f;
        pauseGame.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void Pause() 
    {
        mainmusic.volume = 0.1f;
        pauseGame.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }
    public void Exit() 
    {
        KGBCount.count = 0;
        Letov.countVin = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Restart() 
    {
        KGBCount.count = 0;
        Letov.countVin = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        pause = false;
    }
}