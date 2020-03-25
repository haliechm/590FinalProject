using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunter : MonoBehaviour
{

    // reference to inventory message
    public GameObject inventoryMessageObject;
    private TextMesh inventoryMessage;


    // reference to inventory component
    private GameObject treasureHunter;
    private TreasureHunterInventory inventory;

    // reference to win message 
    private GameObject winMessageObject;
    private TextMesh winMessage;

    // needed variables 
    private int score;
    private bool alreadyWon;
    private int numberOfItems;
    private bool inventoryMessageOn;


    void Start()
    {
        score = 0;
        alreadyWon = false;
        numberOfItems = 0;

        treasureHunter = GameObject.Find("TreasureHunter");
        inventory = treasureHunter.GetComponent<TreasureHunterInventory>();

        winMessageObject = GameObject.Find("Win Message");
        winMessage = winMessageObject.GetComponent<TextMesh>();
        winMessage.text = "You Win!";
        winMessageObject.SetActive(false);
        winMessage.color = Color.red;

        inventoryMessage = inventoryMessageObject.GetComponent<TextMesh>();
        inventoryMessageObject.SetActive(false);
        inventoryMessageOn = false;
        inventoryMessage.color = Color.black;

    }

    void Update()
    {
    // if 1 is pressed, a coin is added to the treasure hunter inventory; score is updated
        if (Input.GetKeyDown("1")) {
            Debug.Log("Key Pressed: 1");
           if (!alreadyWon) {
                GameObject coin = GameObject.Find("Coin");
                CollectibleTreasure collectible =coin.GetComponent<CollectibleTreasure>();
                inventory.collectibles[0] = collectible;
                scoreCounter();
           }
        }

    // if 2 is pressed, a treasure chest is addded to the treasure hunter inventory; score is updated
        if (Input.GetKeyDown("2")) {
            Debug.Log("Key Pressed: 2");
            if (!alreadyWon) {
                GameObject chest = GameObject.Find("Treasure Chest");
                CollectibleTreasure collectible = chest.GetComponent<CollectibleTreasure>();
                inventory.collectibles[1] = collectible;
                scoreCounter();
            }
        }

    // if 3 is pressed, a diamond is added to the treasure hunter inventory; score is updated
        if (Input.GetKeyDown("3")) {
            Debug.Log("Key Pressed: 3");
            if (!alreadyWon) {
                GameObject diamond = GameObject.Find("Diamond");
                CollectibleTreasure collectible = diamond.GetComponent<CollectibleTreasure>();
                inventory.collectibles[2] = collectible;
                scoreCounter();
            }
        }

    // if 4 is pressed, the inventory log either turns on or off
        if (Input.GetKeyDown("4")) {
            Debug.Log("Key Pressed: 4");
            setInventoryMessage();
            inventoryMessageOn = !inventoryMessageOn;
            inventoryMessageObject.SetActive(inventoryMessageOn);
        }

    }

    // determines the score from the items in the treasure hunter inventory
    // The following are the point values of each of the three items:
    // Coin: 1      Treasure Chest: 10       Diamond: 100
    void scoreCounter() {
        score = 0;
        bool full = true;

        foreach (CollectibleTreasure collectibleTreasure in inventory.collectibles) {
            if (collectibleTreasure != null) {
                score += collectibleTreasure.pointValue;
            } else {
                full = false;
            }
        }

        // win when all three objects have been collected (total points is 111)
        if (full) {
            winMessageObject.SetActive(true);
            alreadyWon = true;
        }

        setInventoryMessage();

    }

    // determines the total number of items in the inventory, and their combined total value
    void setInventoryMessage() {

          numberOfItems = 0;
            foreach (CollectibleTreasure collectibleTreasure in inventory.collectibles) {
                if (collectibleTreasure != null) {
                 numberOfItems += 1;
                }
            }
        inventoryMessage.text = "Halie \r\nScore: " + score + "\r\n# of Items: " + numberOfItems;
    }
}
