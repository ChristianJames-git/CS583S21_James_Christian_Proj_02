using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerSystem : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameSceneManager gsm;
    [SerializeField] private UI_Shop uiShop;
    [SerializeField] private InventoryManager invMan;
    public GameObject Potion2;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            //Movements
            case "toShop":
                gsm.Teleport(25, 0);
                break;
            case "fromShop":
                gsm.Teleport(10.5f, 1);
                break;
            case "mineEntrance":
                gsm.Teleport(-52, 1);
                if (!gsm.mineComplete)
                    gsm.LockAll();
                break;
            case "mineExit":
                gsm.Teleport(-7, 0);
                break;
            case "toFloor1":
                gsm.Teleport(-25, 28);
                gsm.ChestPuzzleReset();
                break;
            case "backToTutorial":
                gsm.Teleport(-43.5f, -5.5f);
                break;
            case "PrisonTeleporters":
                gsm.Teleport(54.5f, 15.5f);
                Potion2.SetActive(true);
                break;
            case "toFloor2":
                gsm.Teleport(33.5f, 23f);
                uiShop.CreateButton(invMan.spikeRing, 5);
                break;
            case "backToFloor1":
                gsm.Teleport(-15, 20.5f);
                gsm.ChestPuzzleReset();
                break;
            case "toExit":
                gsm.Teleport(8.5f, -14f);
                gsm.exitRock.SetActive(false);
                gsm.mineComplete = true;
                break;
            case "backToFloor2":
                gsm.Teleport(8.5f, 41f);
                break;
            case "toEnd":
                gm.ToScene("Credits");
                break;
            //Shop
            case "shopArea":
                uiShop.Show();
                break;
            case "healArea":
                gm.playerStats.playerHP = gm.playerStats.playerMaxHP;
                break;
            //Chests
            case "Chest1":
                collision.gameObject.SetActive(false);
                gm.playerStats.purse += 50;
                break;
            case "Chest2":
                collision.gameObject.SetActive(false);
                invMan.AddItem(invMan.gem);
                invMan.AddItem(invMan.potion);
                break;
            case "Chest3A":
                gsm.ChestPuzzle(0);
                break;
            case "Chest3B":
                gsm.ChestPuzzle(1);
                break;
            case "Chest3C":
                gsm.ChestPuzzle(2);
                break;
            case "Chest3D":
                gsm.ChestPuzzle(3);
                break;
            //Riddles
            case "riddle1Chest":
                gsm.ShowRiddle(0);
                break;
            case "riddle2Doors":
                gsm.ShowRiddle(1);
                break;
            case "riddle3ToFloor1":
                gsm.ShowRiddle(2);
                break;
            case "riddle4AtFloor1":
                gsm.ShowRiddle(3);
                break;
            case "riddle5ToChestRoom":
                gsm.ShowRiddle(4);
                break;
            case "riddle6ToChestRoom":
                gsm.ShowRiddle(5);
                break;
            case "riddle7LeftRoom":
                gsm.ShowRiddle(6);
                break;
            case "riddle8ToFloor2":
                gsm.ShowRiddle(7);
                break;
            case "riddle9":
                gsm.ShowRiddle(8);
                break;
            case "riddle10":
                gsm.ShowRiddle(9);
                break;
            case "riddle11":
                gsm.ShowRiddle(10);
                break;
            case "riddle12":
                gsm.ShowRiddle(11);
                break;
            case "riddle13":
                gsm.ShowRiddle(12);
                break;
            //Tips
            case "tip1Start":
                gsm.TipText.text = "Welcome to my little 'town'! The shop is to your right. Hope you like it since you're stuck here as long as I please. Ignore those sounds coming from the grate to the left.";
                gsm.Tip.SetActive(true);
                break;
            case "tip2Shop":
                gsm.TipText.text = "Here's my humble shop. I don't know how you could get injured here, but if you do, that heart will heal you right up";
                gsm.Tip.SetActive(true);
                break;
            case "tip3Mine":
                gsm.TipText.text = "Well it seems you investigated that noise even though I told you not to...though why did I think you'd trust me. I did trap you here. Good think I prepared some riddles for you";
                gsm.Tip.SetActive(true);
                break;
            case "tip4Trap":
                gsm.TipText.text = "Traps do damage or otherwise negatively effect your wellbeing. I've left a few of them around for you. Enjoy :)";
                gsm.Tip.SetActive(true);
                break;
            case "tip5Puzzle":
                gsm.TipText.text = "Good luck with this puzzle. Think Z's and you might survive";
                gsm.Tip.SetActive(true);
                break;
            case "tip6Teleporters":
                gsm.TipText.text = "I'd avoid the purple if I were you. Prison is never a fun place to go. Also a ring in shop may help with the puzzle from the last level";
                gsm.Tip.SetActive(true);
                break;
            case "tip7Prison":
                gsm.TipText.text = "You didn't listen did you. Have fun getting out of here. That trap is there for an easy escape";
                gsm.Tip.SetActive(true);
                break;
            case "tip8Ring":
                gsm.TipText.text = "You seem to be close to beating my dungeon. Here's my ring of fire resistance if you want it";
                gsm.Tip.SetActive(true);
                break;
            case "tip9End":
                gsm.TipText.text = "You did it! I didn't think you would but people continue to surprise me. Hope you enjoyed my small town, now go return to yours with your loot";
                gsm.Tip.SetActive(true);
                break;
            //Traps
            case "Spike":
                gsm.TrapDamage(0);
                break;
            case "Fire":
                gsm.TrapDamage(1);
                break;
            case "BearTrap":
                gsm.TrapDamage(2);
                break;
            //Items
            case "Potion":
                collision.gameObject.SetActive(false);
                invMan.AddItem(invMan.potion);
                break;
            case "FireRing":
                collision.gameObject.SetActive(false);
                invMan.AddItem(invMan.fireRing);
                break;
            case "Backpack":
                collision.gameObject.SetActive(false);
                invMan.AddItem(invMan.gem);
                invMan.AddItem(invMan.potion);
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "shopArea":
                uiShop.Hide();
                break;
            case "riddle1Chest":
            case "riddle2Doors":
            case "riddle3ToFloor1":
            case "riddle4AtFloor1":
            case "riddle5ToChestRoom":
            case "riddle6ToChestRoom":
            case "riddle7LeftRoom":
            case "riddle8ToFloor2":
            case "riddle9":
            case "riddle10":
            case "riddle11":
            case "riddle12":
            case "riddle13":
                gsm.Riddle.SetActive(false);
                gsm.RiddleAnswer.text = "";
                break;
            case "tip1Start":
            case "tip2Shop":
            case "tip3Mine":
            case "tip4Trap":
            case "tip5Puzzle":
            case "tip6Teleporters":
            case "tip7Prison":
            case "tip8Ring":
            case "tip9End":
                gsm.Tip.SetActive(false);
                break;
        }
    }
}
