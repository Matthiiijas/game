using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    InputMaster controls;
    void Awake() {
        controls = new InputMaster();
        controls.Player.Pause.started += ctx => PauseGame();
    }

    void Update() {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>().healthPoints == 0) {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    void PauseGame() {
        Debug.Log("pause");
        GameObject.Find("InGameOverlay").SetActive(false);
        GameObject.Find("PauseMenu").SetActive(true);
    }
}
