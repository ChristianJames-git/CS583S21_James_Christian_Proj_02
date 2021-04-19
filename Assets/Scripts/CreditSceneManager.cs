using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditSceneManager : MonoBehaviour
{
    public GameObject Page1, Page2;

    void Start()
    {
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
            SceneManager.LoadScene("MainScene");
    }
}
