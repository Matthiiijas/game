using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    void Update() {
        if(GameObject.FindGameObjectWithTag("Player") == null) {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
