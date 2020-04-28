using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject inGameOverlay, pauseMenu;
    public bool gamePaused;

    InputMaster controls;
    void Awake() {
        controls = new InputMaster();
        controls.Player.Pause.started += ctx => PauseGame();
    }

    void Update() {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>().healthPoints == 0) {
            SceneManager.LoadScene(0);
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

    public void FinishLevel() {
        SceneManager.LoadScene(1);
    }

    void OnEnable() {
        controls.Enable();
    }

    void OnDisable() {
        controls.Disable();
    }
}
