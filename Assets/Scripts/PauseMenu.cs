using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused = false;
    public GameObject player1;
    public GameObject player2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        player1.GetComponent<PlayerMovement>().canMove = false;
        player2.GetComponent<PlayerMovement>().canMove = false;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        player1.GetComponent<PlayerMovement>().canMove = true;
        player2.GetComponent<PlayerMovement>().canMove = true;
    }
}
