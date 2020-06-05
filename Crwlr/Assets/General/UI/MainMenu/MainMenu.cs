using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Awake() {
        Time.timeScale = 1;
    }

    public void StartGame() {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine() {
        GetComponent<Animator>().SetTrigger("Fadeout");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
