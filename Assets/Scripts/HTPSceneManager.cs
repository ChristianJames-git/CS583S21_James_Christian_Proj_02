using System.Collections.Generic;
using UnityEngine;

public class HTPSceneManager : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> inputPanels;
    public GameObject Panel1, Panel2, Panel3; //panels
    private int currentPanel;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        inputPanels = new List<GameObject> { Panel1, Panel2, Panel3 };
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
