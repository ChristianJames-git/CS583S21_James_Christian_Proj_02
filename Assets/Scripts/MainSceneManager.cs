using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private GameManager gm;
    public GameObject mainPanel;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void onSecretClicked()
    {
        gm.secretClicked = true;
    }

    public void onPlayClicked()
    {
        gm.ToScene("PlayScene");
    }

    public void onHowToPlayClicked()
    {
        gm.ToScene("HowToPlayScene");
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }
}
