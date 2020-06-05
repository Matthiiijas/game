using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject inGameOverlay, pauseMenu, gameOverScreen;
    public bool gamePaused;

    InputMaster controls;
    void Awake() {
        Time.timeScale = 1;
        controls = new InputMaster();
        controls.Player.Pause.started += ctx => PauseGame();
    }

    void Update() {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>().healthPoints <= 0) {
            GameOver();
        }
    }

    void PauseGame() {
        if(gamePaused) gamePaused = false;
        else gamePaused = true;
        inGameOverlay.SetActive(!gamePaused);
        pauseMenu.SetActive(gamePaused);
        if(gamePaused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    void GameOver() {
        inGameOverlay.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.001f;
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnEnable() {
        controls.Enable();
    }

    void OnDisable() {
        controls.Disable();
    }
}
