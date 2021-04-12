using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject mainPanel;

    public void onPlayClicked()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void onHowToPlayClicked()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void onQuitClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
