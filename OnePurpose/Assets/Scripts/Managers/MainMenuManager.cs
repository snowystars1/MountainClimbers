using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("OnePurposeGameJam1");
    }

    public void PlayMulti()
    {
        SceneManager.LoadScene("Lobby");
    }
}