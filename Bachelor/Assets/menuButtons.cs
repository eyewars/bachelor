using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour{
    public void restartGame() {
        SceneManager.LoadScene(1);
    }

    public void mainMenu() {
        SceneManager.LoadScene(0);
    }

    public void quitGame() {
        Application.Quit();
        Debug.Log("Nå quitta spillet!");
    }

    public void settingsMenu() {
        SceneManager.LoadScene(3);
    }
}