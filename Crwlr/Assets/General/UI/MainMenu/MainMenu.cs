using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine() {
        GetComponent<Animator>().SetTrigger("Fadeout");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
