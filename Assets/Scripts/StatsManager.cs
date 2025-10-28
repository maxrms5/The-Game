using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    //Scripts
    //[SerializeField] CombatMenu combatMenu;
    //Game Objects
    [SerializeField] GameObject[] player = new GameObject[5];
    [SerializeField] Slider[] healthSlider = new Slider[5];
    [SerializeField] Slider[] manaSlider = new Slider[5];
    //Text and Images
    [SerializeField] TextMeshProUGUI[] playerName = new TextMeshProUGUI[5];
    [SerializeField] TextMeshProUGUI[] attackText = new TextMeshProUGUI[5];
    [SerializeField] TextMeshProUGUI[] defenceText = new TextMeshProUGUI[5];
    [SerializeField] TextMeshProUGUI[] healthText = new TextMeshProUGUI[5];
    [SerializeField] TextMeshProUGUI[] manaText = new TextMeshProUGUI[5];
    [SerializeField] RawImage[] playerImage = new RawImage[5];
    [SerializeField] Texture[] playerTextures = new Texture[5];
    //Names and Stats
    public string[] players = { "Brawler", "Frog", "Bard", "Rouge", "Crab", "Bully" };
    public bool[] playerIsDead = { false, false, false, false, false, false };
    public int[] maxPlayerHP = { 50, 35, 10, 55, 70, 25 };
    public int[] currentPlayerHP = { 50, 35, 10, 55, 70, 25};
    public int[] maxPlayerMP = { 125, 100, 110, 130, 500, 105 };
    public int[] currentPlayerMP = { 125, 100, 110, 130, 500, 105};
    public int[] playerATK = { 12, 14, 16, 18, 20, 22 };
    public int[] currentPlayerATK = { 12, 14, 16, 18, 20, 22};
    public int[] playerDEF = { 5, 6, 7, 8, 9, 10 };
    public int[] currentPlayerDEF = { 5, 6, 7, 8, 9, 10 };
    public List<string> playersList;

    //Awake(); is called whenever the gameobject the script is attatched to becomes active
    private void Awake() //Calls multiple times, first when scene is loaded; after, anytime combat is entered
    {
        playerImage[0].texture = playerTextures[0];
        playersList = new List<string>(players); //Adds players to list
        SetStats();
        Debug.Log("Stats Set Successfully");
    }
    public void SetStats(int i) //Sets specific stats (used during combat ONLY)
    {
        playerImage[i].texture = playerTextures[i]; //Player image icon
        Debug.Log(players[i] + " Image Changed");
        SetMaxHealth(maxPlayerHP[i], i);
        SetMaxMana(maxPlayerMP[i], i);
        SetAttack(playerATK[i], i);
        SetDefence(playerDEF[i], i);
        playerName[i].text = players[i]; //Player name
        Debug.Log(players[i] + " Stats Set");
    }
    public void SetStats() //Sets all stats
    {
        for (int i = 0; i <= 5; i++)
        {
            playerImage[i].texture = playerTextures[i]; //Player image icon
            Debug.Log(players[i] + " Image Changed");
            SetMaxHealth(maxPlayerHP[i], i);
            SetMaxMana(maxPlayerMP[i], i);
            SetAttack(playerATK[i], i);
            SetDefence(playerDEF[i], i);
            playerName[i].text = players[i]; //Player name
            Debug.Log(players[i] + " Stats Set");
        }
    }
    public void UpdateStats(int i) //Sets specific stats (used during combat ONLY)
    {
        playerImage[i].texture = playerTextures[i]; //Player image icon
        Debug.Log(players[i] + " Image Changed");
        SetHealth(currentPlayerHP[i], i);
        SetMana(currentPlayerMP[i], i);
        SetAttack(currentPlayerATK[i], i);
        SetDefence(currentPlayerDEF[i], i);
        playerName[i].text = players[i]; //Player name
        Debug.Log(players[i] + " Stats Set");
    }
    public void UpdateStats() //Sets all stats
    {
        for (int i = 0; i <= 5; i++)
        {
            playerImage[i].texture = playerTextures[i]; //Player image icon
            Debug.Log(players[i] + " Image Changed");
            SetHealth(currentPlayerHP[i], i);
            SetMana(currentPlayerMP[i], i);
            SetAttack(currentPlayerATK[i], i);
            SetDefence(currentPlayerDEF[i], i);
            playerName[i].text = players[i]; //Player name
            Debug.Log(players[i] + " Stats Set");
        }
    }
    public void SetMaxHealth(int health, int playerNum)
    {
        healthSlider[playerNum].maxValue = health;
        healthSlider[playerNum].value = health;
        healthText[playerNum].text = "HP: " + health;
        currentPlayerHP[playerNum] = health;
    }
    public void SetHealth(int health, int playerNum)
    {
        healthSlider[playerNum].value = health;
        healthText[playerNum].text = "HP: " + health;
        currentPlayerHP[playerNum] = health;
    }
    public void ChangeHealth(int health, int playerNum)
    {
        if (currentPlayerHP[playerNum] + health > maxPlayerHP[playerNum]) //if player hp goes over max hp, player hp = max hp
        {
            Debug.Log("+ " + (maxPlayerHP[playerNum] - currentPlayerHP[playerNum]) + " health");
            currentPlayerHP[playerNum] = maxPlayerHP[playerNum];
        }
        else if (currentPlayerHP[playerNum] + health <= 0) //if player hp drops to negatives, player hp = 0 (prevents neg hp)
        {
            Debug.Log(health + " health");
            currentPlayerHP[playerNum] = 0;
            playerIsDead[playerNum] = true;
        }
        else //adds/subs hp from current player hp
        {
            currentPlayerHP[playerNum] += health;
            Debug.Log("+ " + health + " health");
        }

        healthSlider[playerNum].value = currentPlayerHP[playerNum];
        Debug.Log("Health Slider Updated");
        healthText[playerNum].text = "HP: " + currentPlayerHP[playerNum];
        Debug.Log("Health Text Updated");
    }
    public void SetMaxMana(int mana, int playerNum)
    {
        manaSlider[playerNum].maxValue = mana;
        manaSlider[playerNum].value = mana;
        manaText[playerNum].text = "MP: " + mana;
        currentPlayerMP[playerNum] = mana;
    }
    public void SetMana(int mana, int playerNum)
    {
        manaSlider[playerNum].value = mana;
        manaText[playerNum].text = "MP: " + mana;
        currentPlayerMP[playerNum] = mana;
    }
    public void ChangeMana(int mana, int playerNum)
    {
        if (currentPlayerMP[playerNum] + mana > maxPlayerMP[playerNum]) //if player hp goes over max hp, player hp = max hp
        {
            Debug.Log("+ " + (maxPlayerMP[playerNum] - currentPlayerMP[playerNum]) + " health");
            currentPlayerMP[playerNum] = maxPlayerMP[playerNum];
        }
        else if (currentPlayerMP[playerNum] + mana <= 0) //if player hp drops to negatives, player hp = 0 (prevents neg hp)
        {
            Debug.Log(mana + " health");
            currentPlayerMP[playerNum] = 0;
        }
        else //adds/subs hp from current player hp
        {
            currentPlayerMP[playerNum] += mana;
            Debug.Log("+ " + mana + " health");
        }
        manaSlider[playerNum].value = currentPlayerMP[playerNum];
        Debug.Log("Mana Slider Updated");
        manaText[playerNum].text = "MP: " + currentPlayerMP[playerNum];
        Debug.Log("Mana Text Updated");
    }
    public void SetAttack(int attack, int playerNum)
    {
        currentPlayerATK[playerNum] = attack;
        attackText[playerNum].text = "ATK: " + attack;
    }
    public void ChangeAttack(int attack, int playerNum)
    {
        currentPlayerATK[playerNum] += attack;
        attackText[playerNum].text = "HP: " + currentPlayerATK[playerNum];
    }
    public void SetDefence(int defence, int playerNum)
    {
        currentPlayerDEF[playerNum] = defence;
        defenceText[playerNum].text = "DEF: " + defence;
    }
    public void ChangeDefence(int defence, int playerNum)
    {
        currentPlayerDEF[playerNum] += defence;
        defenceText[playerNum].text = "HP: " + currentPlayerDEF[playerNum];
    }
}