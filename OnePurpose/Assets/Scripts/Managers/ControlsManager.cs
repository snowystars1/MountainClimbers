using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
}
