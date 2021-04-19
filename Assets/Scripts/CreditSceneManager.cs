using UnityEngine;
public class CreditSceneManager : MonoBehaviour
{
    private GameManager gm;
    public GameObject Page1, Page2;

    void Start()
    {
        gm = GameManager.Instance;
        Page1.SetActive(true);
        Page2.SetActive(false);
    }

    public void onNext()
    {
        if (Page1.activeSelf)
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
        }
        else
            gm.ToScene("MainScene");
    }
}
