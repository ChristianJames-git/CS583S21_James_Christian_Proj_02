using System.Collections.Generic;
using UnityEngine;

public class HTPSceneManager : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> inputPanels;
    public GameObject mainPanel; //panels
    private int currentPanel;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        inputPanels.Add(mainPanel);
        resetPanels();
    }

    public void nextPage()
    {
        inputPanels[currentPanel++].SetActive(false);
        if (currentPanel < inputPanels.Count)
            inputPanels[currentPanel].SetActive(true);
        else
            gm.ToScene("MainScene");
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
