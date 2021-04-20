using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Sprite mainChar;
    public bool secretClicked;
    public static GameManager Instance { get; private set; }
    private AudioManager audioMan;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        playerStats = new PlayerStats();
        playerStats.playerSprite = mainChar;
        audioMan = AudioManager.instance;
        audioMan.Play("MenuMusic");
    }

    public void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.hello";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerStats);
        stream.Close();
    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.hello";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStats stats = formatter.Deserialize(stream) as PlayerStats;
            stream.Close();

            playerStats = stats;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
        }
    }

    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

public class Item
{
    public int itemID;
    public string itemName;
    public int itemCost;
    public int index;
}