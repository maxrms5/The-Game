using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static bool inCombat = false;
    public static bool allEnemiesDead = false;
    public int playerIndex = 0; //Active player index
    public int enemyIndex = 0; //Active enemy index
    public float enemyAtkDelay = 1f;
    public static string activePlayer; //Active player name
    public static string activeEnemy; //Active enemy name
    //Combat buttons
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject fightButtons;
    [SerializeField] GameObject itemButtons;
    [SerializeField] GameObject magicButtons;
    //Scripts
    [SerializeField] PlayerController player;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] StatsManager statsManager;
    [SerializeField] CombatMenu combatMenu;
    [SerializeField] EnemyCombatCollector collector;
    [SerializeField] EnemyStatsManager enemyStats;
    [SerializeField] SaveData saveData;
    public List<EnemyData> enemyData;
    //Player UI
    [SerializeField] GameObject[] playerUI = new GameObject[6];
    [SerializeField] Texture[] playerTexture = new Texture[6];
    [SerializeField] RawImage[] playerImage = new RawImage[6];
    //Enemy UI
    [SerializeField] GameObject[] enemyUI = new GameObject[6];
    public List<GameObject> enemies;
    //Player Attacks
    private string[] atkOne = { "Hit", "Stomp", "Sing", "Stab", "Blast", "Wet Willy" };
    private int[] atkOneDmg = { 10, 15, 5, 25, 15, 10 };
    [SerializeField] TextMeshProUGUI atkOneText;
    private string[] atkTwo = { "Punch", "Hop", "Scream", "Pierce", "Magic Missile", "Tease" };
    private int[] atkTwoDmg = { 15, 10, 5, 0, 10, 5 };
    [SerializeField] TextMeshProUGUI atkTwoText;
    private string[] atkThree = { "Slam", "Croak", "Ballad", "Puncture", "Laser", "Noogie" };
    private int[] atkThreeDmg = { 20, 10, 5, 0, 20, 15 };
    [SerializeField] TextMeshProUGUI atkThreeText;
    //Enemy Attacks
    public List<string> enemyAtkOne;
    public List<int> enemyAtkOneDmg;
    public List<string> enemyAtkTwo;
    public List<int> enemyAtkTwoDmg;
    public List<string> enemyAtkThree;
    public List<int> enemyAtkThreeDmg;
    [SerializeField] List<TextMeshProUGUI> enemyAtkText;
    //Player Spells
    private string[] spellOne = { "Fireball", "Aggro", "Uplift", "Chance", "Blind", "!@#$%" };
    private int[] spellOneDmg = { 25, 5, 0, 100, 20, 15 };
    private int[] spellOneMana = { 20, 30, 100, 100, 250, 20 };
    [SerializeField] TextMeshProUGUI spellOneText;
    private string[] spellTwo = { "Body Slam", "Ribbit", "Do Re Mi", "Backstab", "Shield", "Head Lock" };
    private int[] spellTwoDmg = { 30, 5, 5, 40, 0, 20 };
    private int[] spellTwoMana = { 10, 10, 20, 50, 20, 20 };
    [SerializeField] TextMeshProUGUI spellTwoText;
    private string[] spellThree = { "Flip", "Tongue", "Heal", "Slit Throat", "Prismatic Beam", "Swirlie" };
    private int[] spellThreeDmg = { 15, 20, 0, 30, 50, 40 };
    private int[] spellThreeMana = { 25, 20, 35, 50, 400, 50 };
    [SerializeField] TextMeshProUGUI spellThreeText;
    //Enemy Spells
    public List<string> enemySpellOne;
    public List<int> enemySpellOneDmg;
    public List<string> enemySpellTwo;
    public List<int> enemySpellTwoDmg;
    public List<string> enemySpellThree;
    public List<int> enemySpellThreeDmg;
    [SerializeField] List<TextMeshProUGUI> enemySpellText;
    //Player Items
    private string[] itemOne = { "Health Potion", "Health Potion", "Health Potion", "Health Potion", "Health Potion", "Health Potion" };
    private int hpPotionHeal = 20; //how much health a health potion restores
    [SerializeField] TextMeshProUGUI itemOneText;
    private string[] itemTwo = { "Mana Potion", "Mana Potion", "Mana Potion", "Mana Potion", "Mana Potion", "Mana Potion" };
    private int mpPotionMana = 20; //how much mana a mana potionm restores
    [SerializeField] TextMeshProUGUI itemTwoText;
    private string[] itemThree = { "Bomb", "Bomb", "Bomb", "Bomb", "Bomb", "Bomb" };
    private int[] itemThreeDmg = { 10, 10, 25, 30, 10, 50};
    [SerializeField] TextMeshProUGUI itemThreeText;

    private void Awake()
    {
        playerManager.UpdatePlayerUnlocks();
    }
    private void Start()
    {
        Debug.Log("Start Called");
        HideInactiveCharacters();
        Debug.Log("Inactive Characters Hidden");
        SetAttacks(playerIndex);
        Debug.Log("Player Attacks Set");
        SetSpells(playerIndex);
        Debug.Log("Player Spells Set");
        SetItems(playerIndex);
        Debug.Log("Player Items Set");
        ResetEnemyAttackText(enemyIndex);
        Debug.Log("Player Attacks Set");
        ResetSetEnemySpellText(enemyIndex);
        Debug.Log("Enemy Spells Set");
        CombatMenu();
        Debug.Log("Buttons Hidden");
        combatMenu.PlayerExitCombat();
        enemies = collector.enemiesInCombat;
    }
    //numbers in attack and spell lists (ex. atkOneDmg[2]) can be set playerIndex instead. Too lazy to fix rn
    public void AttackOne()
    {
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == false)
        {
            if (atkOneText.text == "Hit")
            {
                enemyStats.ChangeHealth(-atkOneDmg[0], enemyIndex);
            }
            else if (atkOneText.text == "Stomp")
            {
                enemyStats.ChangeHealth(-atkOneDmg[1], enemyIndex);
            }
            else if (atkOneText.text == "Sing")
            {
                enemyStats.ChangeHealth(-atkOneDmg[2], enemyIndex);
            }
            else if (atkOneText.text == "Backstab")
            {
                enemyStats.ChangeHealth(-atkOneDmg[3], enemyIndex);
            }
            else if (atkOneText.text == "Blast")
            {
                enemyStats.ChangeHealth(-atkOneDmg[4], enemyIndex);
            }
            else if (atkOneText.text == "Wet Willy")
            {
                enemyStats.ChangeHealth(-atkOneDmg[5], enemyIndex);
            }
            enemyData[enemyIndex].UpdateEnemyStats(enemyIndex);
            StartCoroutine(EnemyAttackOne(enemyIndex));
        }
        else if (enemyData[enemyIndex].isDead == true && statsManager.playerIsDead[playerIndex] == false)
        {
            Debug.Log("Cannot attack; " + enemies[enemyIndex].name + " is Dead");
        }
        else if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == true)
        {
            Debug.Log("Cannot attack; " + statsManager.players[playerIndex] + " is Dead");
        }
    }
    public void AttackTwo()
    {
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == false)
        {
            if (atkTwoText.text == "Punch")
            {
                enemyStats.ChangeHealth(-atkTwoDmg[0], enemyIndex);
            }
            else if (atkTwoText.text == "Hop")
            {
                enemyStats.ChangeHealth(-atkTwoDmg[1], enemyIndex);
            }
            else if (atkTwoText.text == "Scream")
            {
                enemyStats.ChangeHealth(-atkTwoDmg[2], enemyIndex);
            }
            else if (atkTwoText.text == "Pierce")
            {
                enemyStats.ChangeHealth(-atkTwoDmg[3], enemyIndex);
            }
            else if (atkTwoText.text == "Magic Missile")
            {
                enemyStats.ChangeHealth(-atkTwoDmg[4], enemyIndex);
            }
            else if (atkTwoText.text == "Tease")
            {
                enemyStats.ChangeHealth(-atkTwoDmg[5], enemyIndex);
            }
            enemyData[enemyIndex].UpdateEnemyStats(enemyIndex);
            StartCoroutine(EnemyAttackTwo(enemyIndex));
        }
        else if (enemyData[enemyIndex].isDead == true && statsManager.playerIsDead[playerIndex] == false)
        {
            Debug.Log("Cannot attack; " + enemies[enemyIndex].name + " is Dead");
        }
        else if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == true)
        {
            Debug.Log("Cannot attack; " + statsManager.players[playerIndex] + " is Dead");
        }
    }
    public void AttackThree()
    {
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == false)
        {
            if (atkThreeText.text == "Slam")
            {
                enemyStats.ChangeHealth(-atkThreeDmg[0], enemyIndex);
            }
            else if (atkThreeText.text == "Croak")
            {
                enemyStats.ChangeHealth(-atkThreeDmg[1], enemyIndex);
            }
            else if (atkThreeText.text == "Ballad")
            {
                enemyStats.ChangeHealth(-atkThreeDmg[2], enemyIndex);
            }
            else if (atkThreeText.text == "Puncture")
            {
                enemyStats.ChangeHealth(-atkThreeDmg[3], enemyIndex);
            }
            else if (atkThreeText.text == "Laser")
            {
                enemyStats.ChangeHealth(-atkThreeDmg[4], enemyIndex);
            }
            else if (atkThreeText.text == "Noogie")
            {
                enemyStats.ChangeHealth(-atkThreeDmg[5], enemyIndex);
            }
            enemyData[enemyIndex].UpdateEnemyStats(enemyIndex);
            StartCoroutine(EnemyAttackThree(enemyIndex));
        }
        else if (enemyData[enemyIndex].isDead == true && statsManager.playerIsDead[playerIndex] == false)
        {
            Debug.Log("Cannot attack; " + enemies[enemyIndex].name + " is Dead");
        }
        else if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == true)
        {
            Debug.Log("Cannot attack; " + statsManager.players[playerIndex] + " is Dead");
        }
    }
    private void SetAttacks(int i)
    {
        atkOneText.text = atkOne[i];
        atkTwoText.text = atkTwo[i];
        atkThreeText.text = atkThree[i];
    }
    public void CastSpellOne()
    {
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == false)
        {
            if (spellOneText.text == "Fireball" && statsManager.currentPlayerMP[playerIndex] >= spellOneMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellOneDmg[0], enemyIndex);
                statsManager.ChangeMana(-spellOneMana[0], playerIndex);
            }
            else if (spellOneText.text == "Aggro" && statsManager.currentPlayerMP[playerIndex] >= spellOneMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellOneDmg[1], enemyIndex);
                statsManager.ChangeMana(-spellOneMana[1], playerIndex);
            }
            else if (spellOneText.text == "Uplift" && statsManager.currentPlayerMP[playerIndex] >= spellOneMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellOneDmg[2], enemyIndex);
                statsManager.ChangeMana(-spellOneMana[2], playerIndex);
            }
            else if (spellOneText.text == "Chance" && statsManager.currentPlayerMP[playerIndex] >= spellOneMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellOneDmg[3], enemyIndex);
                statsManager.ChangeMana(-spellOneMana[3], playerIndex);
            }
            else if (spellOneText.text == "Blind" && statsManager.currentPlayerMP[playerIndex] >= spellOneMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellOneDmg[4], enemyIndex);
                statsManager.ChangeMana(-spellOneMana[4], playerIndex);
            }
            else if (spellOneText.text == "!@#$%" && statsManager.currentPlayerMP[playerIndex] >= spellOneMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellOneDmg[5], enemyIndex);
                statsManager.ChangeMana(-spellOneMana[5], playerIndex);
            }
            else
            {
                Debug.Log("Cannot cast " + spellOne[playerIndex] + "; Not enough mana");
            }
            enemyData[enemyIndex].UpdateEnemyStats(enemyIndex);
            StartCoroutine(EnemySpellOne(enemyIndex));
        }
        else if (enemyData[enemyIndex].isDead == true && statsManager.playerIsDead[playerIndex] == false)
        {
            Debug.Log("Cannot cast spell; " + enemies[enemyIndex].name + " is Dead");
        }
        else if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == true)
        {
            Debug.Log("Cannot cast spell; " + statsManager.players[playerIndex] + " is Dead");
        }
    }
    public void CastSpellTwo()
    {
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == false)
        {
            if (spellTwoText.text == "Body Slam" && statsManager.currentPlayerMP[playerIndex] >= spellTwoMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellTwoDmg[0], enemyIndex);
                statsManager.ChangeMana(-spellTwoMana[0], playerIndex);
            }
            else if (spellTwoText.text == "Ribbit" && statsManager.currentPlayerMP[playerIndex] >= spellTwoMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellTwoDmg[1], enemyIndex);
                statsManager.ChangeMana(-spellTwoMana[1], playerIndex);
            }
            else if (spellTwoText.text == "Do Re Mi" && statsManager.currentPlayerMP[playerIndex] >= spellTwoMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellTwoDmg[2], enemyIndex);
                statsManager.ChangeMana(-spellTwoMana[2], playerIndex);
            }
            else if (spellTwoText.text == "Backstab" && statsManager.currentPlayerMP[playerIndex] >= spellTwoMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellTwoDmg[3], enemyIndex);
                statsManager.ChangeMana(-spellTwoMana[3], playerIndex);
            }
            else if (spellTwoText.text == "Shield" && statsManager.currentPlayerMP[playerIndex] >= spellTwoMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellTwoDmg[4], enemyIndex);
                statsManager.ChangeMana(-spellTwoMana[4], playerIndex);
            }
            else if (spellTwoText.text == "Head Lock" && statsManager.currentPlayerMP[playerIndex] >= spellTwoMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellTwoDmg[5], enemyIndex);
                statsManager.ChangeMana(-spellTwoMana[5], playerIndex);
            }
            else
            {
                Debug.Log("Cannot cast " + spellTwo[playerIndex] + "; Not enough mana");
            }
            enemyData[enemyIndex].UpdateEnemyStats(enemyIndex);
            StartCoroutine(EnemySpellTwo(enemyIndex));
        }
        else if (enemyData[enemyIndex].isDead == true && statsManager.playerIsDead[playerIndex] == false)
        {
            Debug.Log("Cannot cast spell; " + enemies[enemyIndex].name + " is Dead");
        }
        else if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == true)
        {
            Debug.Log("Cannot cast spell; " + statsManager.players[playerIndex] + " is Dead");
        }
    }
    public void CastSpellThree()
    {
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == false)
        {
            if (spellThreeText.text == "Flip" && statsManager.currentPlayerMP[playerIndex] >= spellThreeMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellThreeDmg[0], enemyIndex);
                statsManager.ChangeMana(-spellThreeMana[0], playerIndex);
            }
            else if (spellThreeText.text == "Tongue" && statsManager.currentPlayerMP[playerIndex] >= spellThreeMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellThreeDmg[1], enemyIndex);
                statsManager.ChangeMana(-spellThreeMana[1], playerIndex);
            }
            else if (spellThreeText.text == "Heal" && statsManager.currentPlayerMP[playerIndex] >= spellThreeMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellThreeDmg[2], enemyIndex);
                statsManager.ChangeMana(-spellThreeMana[2], playerIndex);
            }
            else if (spellThreeText.text == "Slit Throat" && statsManager.currentPlayerMP[playerIndex] >= spellThreeMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellThreeDmg[3], enemyIndex);
                statsManager.ChangeMana(-spellThreeMana[3], playerIndex);
            }
            else if (spellThreeText.text == "Prismatic Beam" && statsManager.currentPlayerMP[playerIndex] >= spellThreeMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellThreeDmg[4], enemyIndex);
                statsManager.ChangeMana(-spellThreeMana[4], playerIndex);
            }
            else if (spellThreeText.text == "Swirlie" && statsManager.currentPlayerMP[playerIndex] >= spellThreeMana[playerIndex])
            {
                enemyStats.ChangeHealth(-spellThreeDmg[5], enemyIndex);
                statsManager.ChangeMana(-spellThreeMana[5], playerIndex);
            }
            else
            {
                Debug.Log("Cannot cast " + spellThree[playerIndex] + "; Not enough mana");
            }
            enemyData[enemyIndex].UpdateEnemyStats(enemyIndex);
            StartCoroutine(EnemySpellThree(enemyIndex));
        }
        else if (enemyData[enemyIndex].isDead == true && statsManager.playerIsDead[playerIndex] == false)
        {
            Debug.Log("Cannot cast spell; " + enemies[enemyIndex].name + " is Dead");
        }
        else if (enemyData[enemyIndex].isDead == false && statsManager.playerIsDead[playerIndex] == true)
        {
            Debug.Log("Cannot cast spell; " + statsManager.players[playerIndex] + " is Dead");
        }
    }
    private void SetSpells(int i)
    {
        spellOneText.text = spellOne[i];
        spellTwoText.text = spellTwo[i];
        spellThreeText.text = spellThree[i];
    }
    public void UseItemOne()
    {
        if (statsManager.playerIsDead[playerIndex] == false && statsManager.currentPlayerHP[playerIndex] < statsManager.maxPlayerHP[playerIndex])
        {
            if (itemOneText.text == "Health Potion")
            {
                statsManager.ChangeHealth(hpPotionHeal, playerIndex);
                saveData.UseItem(Item.Type.HealthPotion);
            }
            else if (itemOneText.text == "Health Potion")
            {
                statsManager.ChangeHealth(hpPotionHeal, playerIndex);
                saveData.UseItem(Item.Type.HealthPotion);
            }
            else if (itemOneText.text == "Health Potion")
            {
                statsManager.ChangeHealth(hpPotionHeal, playerIndex);
                saveData.UseItem(Item.Type.HealthPotion);
            }
            else if (itemOneText.text == "Health Potion")
            {
                statsManager.ChangeHealth(hpPotionHeal, playerIndex);
                saveData.UseItem(Item.Type.HealthPotion);
            }
            else if (itemOneText.text == "Health Potion")
            {
                statsManager.ChangeHealth(hpPotionHeal, playerIndex);
                saveData.UseItem(Item.Type.HealthPotion);
            }
            else if (itemOneText.text == "Health Potion")
            {
                statsManager.ChangeHealth(hpPotionHeal, playerIndex);
                saveData.UseItem(Item.Type.HealthPotion);
            }
        }
        else
        {
            Debug.Log("Cannot use item");
        }
    }
    public void UseItemTwo()
    {
        if (statsManager.playerIsDead[playerIndex] == false && statsManager.currentPlayerMP[playerIndex] < statsManager.maxPlayerMP[playerIndex])
        {
            if (itemTwoText.text == "Mana Potion")
            {
                statsManager.ChangeMana(mpPotionMana, playerIndex);
                saveData.UseItem(Item.Type.ManaPotion);
            }
            else if (itemTwoText.text == "Mana Potion")
            {
                statsManager.ChangeMana(mpPotionMana, playerIndex);
                saveData.UseItem(Item.Type.ManaPotion);
            }
            else if (itemTwoText.text == "Mana Potion")
            {
                statsManager.ChangeMana(mpPotionMana, playerIndex);
                saveData.UseItem(Item.Type.ManaPotion);
            }
            else if (itemTwoText.text == "Mana Potion")
            {
                statsManager.ChangeMana(mpPotionMana, playerIndex);
                saveData.UseItem(Item.Type.ManaPotion);
            }
            else if (itemTwoText.text == "Mana Potion")
            {
                statsManager.ChangeMana(mpPotionMana, playerIndex);
                saveData.UseItem(Item.Type.ManaPotion);
            }
            else if (itemTwoText.text == "Mana Potion")
            {
                statsManager.ChangeMana(mpPotionMana, playerIndex);
                saveData.UseItem(Item.Type.ManaPotion);
            }
        }
        else
        {
            Debug.Log("Cannot use item");
        }
    }
    public void UseItemThree()
    {
        Debug.Log("Cannot use item");
    }
    private void SetItems(int i)
    {
        itemOneText.text = itemOne[i];
        itemTwoText.text = itemTwo[i];
        itemThreeText.text = itemThree[i];
    }
    public IEnumerator EnemyAttackOne(int i) //i = enemy index
    {
        if (enemyData[i].isDead == false)
        {
            yield return new WaitForSeconds(enemyAtkDelay);
            enemyAtkText[i].text = enemies[i].name + " used " + enemyAtkOne[i] + "!";
            statsManager.ChangeHealth(-enemyAtkOneDmg[i], playerIndex);
        }
        else
        {
            Debug.Log(enemies[i].name + " cannot attack; " + enemies[i].name + " is dead");
        }
    }
    public IEnumerator EnemyAttackTwo(int i) //i = enemy index
    {
        if (enemyData[i].isDead == false)
        {
            yield return new WaitForSeconds(enemyAtkDelay);
            enemyAtkText[i].text = enemies[i].name + " used " + enemyAtkTwo[i] + "!";
            statsManager.ChangeHealth(-enemyAtkTwoDmg[i], playerIndex);
        }
        else
        {
            Debug.Log(enemies[i].name + " cannot attack; " + enemies[i].name + " is dead");
        }
    }
    public IEnumerator EnemyAttackThree(int i) //i = enemy index
    {
        if (enemyData[i].isDead == false)
        {
            yield return new WaitForSeconds(enemyAtkDelay);
            enemyAtkText[i].text = enemies[i].name + " used " + enemyAtkThree[i] + "!";
            statsManager.ChangeHealth(-enemyAtkThreeDmg[i], playerIndex);
        }
        else
        {
            Debug.Log(enemies[i].name + " cannot attack; " + enemies[i].name + " is dead");
        }
    }
    private void ResetEnemyAttackText(int i)
    {
        enemyAtkText[i].text = "";
    }
    public IEnumerator EnemySpellOne(int i) //i = enemy index
    {
        if (enemyData[i].isDead == false)
        {
            yield return new WaitForSeconds(enemyAtkDelay);
            enemySpellText[i].text = enemies[i].name + " used " + enemySpellOne[i] + "!";
            statsManager.ChangeHealth(-enemySpellOneDmg[i], playerIndex);
        }
        else
        {
            Debug.Log(enemies[i].name + " cannot cast spell; " + enemies[i].name + " is dead");
        }
    }
    public IEnumerator EnemySpellTwo(int i) //i = enemy index
    {
        if (enemyData[i].isDead == false)
        {
            yield return new WaitForSeconds(enemyAtkDelay);
            enemySpellText[i].text = enemies[i].name + " used " + enemySpellTwo[i] + "!";
            statsManager.ChangeHealth(-enemySpellTwoDmg[i], playerIndex);
        }
        else
        {
            Debug.Log(enemies[i].name + " cannot cast spell; " + enemies[i].name + " is dead");
        }
    }
    public IEnumerator EnemySpellThree(int i) //i = enemy index
    {
        if (enemyData[i].isDead == false)
        {
            yield return new WaitForSeconds(enemyAtkDelay);
            enemySpellText[i].text = enemies[i].name + " used " + enemySpellThree[i] + "!";
            statsManager.ChangeHealth(-enemySpellThreeDmg[i], playerIndex);
        }
        else
        {
            Debug.Log(enemies[i].name + " cannot cast spell; " + enemies[i].name + " is dead");
        }
    }
    private void ResetSetEnemySpellText(int i)
    {
        enemySpellText[i].text = "";
    }
    public void CycleCharacterRight() //Switches to next character when right button pressed
    {
        Debug.Log("Right Button Pressed");
        playerIndex++;
        Debug.Log("Player Index Increased");
        if (playerIndex > 5)
        {
            playerIndex = 0;
            Debug.Log("Index Looped");
        }
        statsManager.UpdateStats(playerIndex);
        for (int i = 0; i <= 5; i++)
        {
            if (i + playerIndex <= 5)
            {
                playerImage[i].texture = playerTexture[i + playerIndex];
            }
            else
            {
                playerImage[i].texture = playerTexture[5 - i];
            }
        }
        HideInactiveCharacters();
        Debug.Log("HUD Changed");
        SetAttacks(playerIndex);
        Debug.Log("Player Attacks Set");
        SetSpells(playerIndex);
        Debug.Log("Player Spells Set");
    }
    public void CycleCharacterLeft() //Switches to previous character when left button pressed
    {
        Debug.Log("Left Button Pressed");
        playerIndex--;
        Debug.Log("Player Index Decreased");
        if (playerIndex < 0)
        {
            playerIndex = 5;
            Debug.Log("Index Looped");
        }
        statsManager.UpdateStats(playerIndex);
        for (int i = 0; i <= 5; i++)
        {
            if (i + playerIndex <= 5)
            {
                playerImage[i].texture = playerTexture[i + playerIndex];
            }
            else
            {
                playerImage[i].texture = playerTexture[5 - i];
            }
        }
        HideInactiveCharacters();
        Debug.Log("HUD Changed");
        SetAttacks(playerIndex);
        Debug.Log("Player Attacks Set");
        SetSpells(playerIndex);
        Debug.Log("Player Spells Set");
    }
    public void CycleEnemyRight()
    {
        Debug.Log("Right Button Pressed");
        enemyIndex--;
        Debug.Log("Enemy Index Increased");
        if (enemyIndex < 0)
        {
            enemyIndex = 5;
            Debug.Log("Index Looped");
        }
        /*var enemies = collector.enemiesInCombat;
        foreach (var enemy in enemies)
        {
            var i = collector.enemiesInCombat.IndexOf(enemy); //index of each enemy (not enemy index) (enemyIndex = active enemy)
            if (i + enemyIndex <= (enemies.Count - 1)) // enemyIndex <= number of enemies
            {
                enemy.transform.position = new Vector3(collector.enemySpawn[i + enemyIndex].transform.position.x, collector.enemySpawn[i + enemyIndex].transform.position.y, collector.enemySpawn[i + enemyIndex].transform.position.z);
                Debug.Log(i + enemyIndex);
            }
            else
            {
                enemy.transform.position = new Vector3(collector.enemySpawn[(enemies.Count - 1) - i].transform.position.x, collector.enemySpawn[(enemies.Count - 1) - i].transform.position.y, collector.enemySpawn[(enemies.Count - 1) - i].transform.position.z);
                //Debug.Log(enemies.Count - 1 - i);
            }
        }*/
        HideInactiveEnemies();
        enemyStats.UpdateStats(enemyIndex);
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        Debug.Log("HUD Changed");
    }
    public void CycleEnemyLeft()
    {
        Debug.Log("Left Button Pressed");
        enemyIndex++;
        Debug.Log("Enemy Index Decreased");
        if (enemyIndex > 5)
        {
            enemyIndex = 0;
            Debug.Log("Index Looped");
        }
        /*var enemies = collector.enemiesInCombat;
        foreach (var enemy in enemies)
        {
            var i = collector.enemiesInCombat.IndexOf(enemy); //index of each enemy (not enemy index) (enemyIndex = active enemy)
            if (i + enemyIndex <= (enemies.Count - 1))
            {
                enemy.transform.position = new Vector3(collector.enemySpawn[i + enemyIndex].transform.position.x, collector.enemySpawn[i + enemyIndex].transform.position.y, collector.enemySpawn[i + enemyIndex].transform.position.z);
            }
            else
            {
                enemy.transform.position = new Vector3(collector.enemySpawn[(enemies.Count - 1) - i].transform.position.x, collector.enemySpawn[(enemies.Count - 1) - i].transform.position.y, collector.enemySpawn[(enemies.Count - 1) - i].transform.position.z);
            }
        }*/
        HideInactiveEnemies();
        enemyStats.UpdateStats(enemyIndex);
        ResetEnemyAttackText(enemyIndex);
        ResetSetEnemySpellText(enemyIndex);
        Debug.Log("HUD Changed");
    }
    public void HideInactiveEnemies() //Makes only the active character visible in combat UI
    {
        foreach (var enemy in enemies)
        {
            var i = enemies.IndexOf(enemy);
            if (enemyUI[i].name == enemies[enemyIndex].tag)
            {
                enemyUI[i].SetActive(true);
                activeEnemy = enemyUI[i].name;
                Debug.Log(enemyUI[i].name + " is Active");
            }
            else if (enemyUI[i].name != enemies[enemyIndex].tag)
            {
                enemyUI[i].SetActive(false);
                Debug.Log(enemyUI[i].name + " is Inactive");
            }
        }
    }
    private void HideInactiveCharacters() //Makes only the active character visible in combat UI
    {
        for (int i = 0; i <= 5; i++)
        {
            if (playerUI[i].tag == statsManager.playersList[playerIndex])
            {
                playerUI[i].SetActive(true);
                activePlayer = playerUI[i].tag;
                Debug.Log(playerUI[i].tag + " is Active");
            }
            else if (playerUI[i].tag != statsManager.playersList[playerIndex])
            {
                playerUI[i].SetActive(false);
                Debug.Log(playerUI[i].tag + " is Inactive");
            }
        }
    }
    public bool AllEnemiesDead() //checks is every enemy is dead
    {
        var enemies = collector.enemiesInCombat;
        foreach (var enemy in enemies)
        {
            var i = enemies.IndexOf(enemy);
            if (enemyData[i].isDead)
            {
                allEnemiesDead = true;
            }
            else //if even one enemy is not dead, the else statement will return false
            {
                allEnemiesDead = false;
                break;
            }
        }
        return allEnemiesDead;
    }
    public void OpenFightMenu() //Opens fight menu buttons
    {
        mainButtons.SetActive(false);
        fightButtons.SetActive(true);
        itemButtons.SetActive(false);
        magicButtons.SetActive(false);
    }
    public void OpenItemsMenu() //Initilizes item menu buttons
    {
        mainButtons.SetActive(false);
        fightButtons.SetActive(false);
        itemButtons.SetActive(true);
        magicButtons.SetActive(false);
    }
    public void OpenMagicMenu() //Initilizes magic menu buttons
    {
        mainButtons.SetActive(false);
        fightButtons.SetActive(false);
        itemButtons.SetActive(false);
        magicButtons.SetActive(true);
    }
    public void CombatMenu() //Initilizes combat menu buttons
    {
        mainButtons.SetActive(true);
        fightButtons.SetActive(false);
        itemButtons.SetActive(false);
        magicButtons.SetActive(false);
    }
}