using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HTPSceneManager : MonoBehaviour
{
    public List<GameObject> inputPanels;
    public GameObject mainPanel; //panels
    private int currentPanel;

    // Start is called before the first frame update
    void Start()
    {
        inputPanels.Add(mainPanel);
        resetPanels();
    }

    public void nextPage()
    {
        inputPanels[currentPanel++].SetActive(false);
        if (currentPanel < inputPanels.Count)
            inputPanels[currentPanel].SetActive(true);
        else
            SceneManager.LoadScene("MainScene");
    }

    private void resetPanels()
    {
        for (int i = 1; i < inputPanels.Count; i++)
        {
            inputPanels[i].SetActive(false);
        }
        currentPanel = 0;
    }
}
