using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private GameManager gm;
    public GameObject mainPanel;

    private void Start()
    {
        gm = GameManager.Instance;
        AudioManager.instance.Play("MenuMusic");
    }

    public void onSecretClicked()
    {
        gm.secretClicked = true;
    }

    public void onPlayClicked()
    {
        AudioManager.instance.Stop("MenuMusic");
        gm.ToScene("PlayScene");
    }

    public void onHowToPlayClicked()
    {
        AudioManager.instance.Stop("MenuMusic");
        gm.ToScene("HowToPlayScene");
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }
}
