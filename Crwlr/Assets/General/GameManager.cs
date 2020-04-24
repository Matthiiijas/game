using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update() {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>().healthPoints == 0) {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
