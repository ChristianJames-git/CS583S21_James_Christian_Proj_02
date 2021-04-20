using UnityEngine;
public class CreditSceneManager : MonoBehaviour
{
    private GameManager gm;
    public GameObject Page1, Page2, Page3;

    void Start()
    {
        gm = GameManager.Instance;
        Page1.SetActive(true);
        Page2.SetActive(false);
        Page3.SetActive(false);
    }

    public void onNext()
    {
        if (Page1.activeSelf)
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
        }
        else if (Page2.activeSelf)
        {
            Page2.SetActive(false);
            Page3.SetActive(true);
        }
        else
            gm.ToScene("MainScene");
    }
}
