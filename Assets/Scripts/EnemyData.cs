using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //Scripts
    [SerializeField] EnemyCombatCollector collector;
    [SerializeField] EnemyStatsManager enemyStatMan;
    [SerializeField] CombatManager combatManager;
    [SerializeField] CombatMenu combatMenu;
    //Stats
    [HideInInspector] public int maxHealth;
    public int currentHealth;
    [HideInInspector] public int maxMana;
    public int currentMana;
    [HideInInspector] public int attack;
    public int currentAttack;
    [HideInInspector] public int defence;
    public int currentDefence;
    public string atkOne;
    public int atkOneDmg;
    public string atkTwo;
    public int atkTwoDmg;
    public string atkThree;
    public int atkThreeDmg;
    public string spellOne;
    public int spellOneDmg;
    public string spellTwo;
    public int spellTwoDmg;
    public string spellThree;
    public int spellThreeDmg;
    public bool isDead = false;

    private void Awake() //Checks if enemy is a type, then sets stats for that enemy
    {
        if (gameObject.name.Contains("Pixie"))
        {
            Debug.Log("Enemy is Pixie");
            maxHealth = 30;
            currentHealth = 30;
            maxMana = 50;
            currentMana = 50;
            attack = 10;
            currentAttack = 10;
            defence = 5;
            currentDefence = 5;
            atkOne = "Zap";
            atkOneDmg = 15;
            atkTwo = "Taunt";
            atkTwoDmg = 5;
            atkThree = "Bite";
            atkThreeDmg = 10;
            spellOne = "Invisibility";
            spellOneDmg = 0;
            spellTwo = "Computer Bug";
            spellTwoDmg = 15;
            spellThree = "Alan Turing";
            spellThreeDmg = 50;
        }
        else if (gameObject.name.Contains("Bully"))
        {
            Debug.Log("Enemy is Bully");
            maxHealth = 100;
            currentHealth = 100;
            maxMana = 0;
            currentMana = 0;
            attack = 20;
            currentAttack = 20;
            defence = 10;
            currentDefence = 10;
            atkOne = "Face Punch";
            atkOneDmg = 25;
            atkTwo = "Tease";
            atkTwoDmg = 5;
            atkThree = "Anger";
            atkThreeDmg = 15;
            spellOne = "Rage";
            spellOneDmg = 25;
            spellTwo = "Kick";
            spellTwoDmg = 15;
            spellThree = "!@#$%";
            spellThreeDmg = 20;
        }
        else if (gameObject.name.Contains("Slime"))
        {
            Debug.Log("Enemy is Slime");
            maxHealth = 20;
            currentHealth = 20;
            maxMana = 25;
            currentMana = 25;
            attack = 20;
            currentAttack = 20;
            defence = 0;
            currentDefence = 0;
            atkOne = "Splooge";
            atkOneDmg = 10;
            atkTwo = "Slime";
            atkTwoDmg = 5;
            atkThree = "Splat";
            atkThreeDmg = 20;
            spellOne = "Engulf";
            spellOneDmg = 15;
            spellTwo = "Squish";
            spellTwoDmg = 20;
            spellThree = "Stickify";
            spellThreeDmg = 10;
        }
        else if (gameObject.name.Contains("Book"))
        {
            Debug.Log("Enemy is Book");
            maxHealth = 30;
            currentHealth = 30;
            maxMana = 100;
            currentMana = 100;
            attack = 10;
            currentAttack = 10;
            defence = 10;
            currentDefence = 10;
            atkOne = "Read";
            atkOneDmg = 20;
            atkTwo = "Define";
            atkTwoDmg = 10;
            atkThree = "Cover";
            atkThreeDmg = 5;
            spellOne = "Ancient Knowledge";
            spellOneDmg = 25;
            spellTwo = "Book Worm";
            spellTwoDmg = 15;
            spellThree = "Paper Cut";
            spellThreeDmg = 5;
        }
        else if (gameObject.name.Contains("Demon"))
        {
            Debug.Log("Enemy is Demon");
            maxHealth = 150;
            currentHealth = 150;
            maxMana = 50;
            currentMana = 50;
            attack = 25;
            currentAttack = 25;
            defence = 15;
            currentDefence = 15;
            atkOne = "Fireball";
            atkOneDmg = 25;
            atkTwo = "Curse";
            atkTwoDmg = 15;
            atkThree = "Torture";
            atkThreeDmg = 40;
            spellOne = "Scorching Ray";
            spellOneDmg = 20;
            spellTwo = "Inferno";
            spellTwoDmg = 30;
            spellThree = "Damnation";
            spellThreeDmg = 50;
        }
        else if (gameObject.name.Contains("Angel"))
        {
            Debug.Log("Enemy is Angel");
            maxHealth = 200;
            currentHealth = 200;
            maxMana = 125;
            currentMana = 125;
            attack = 20;
            currentAttack = 20;
            defence = 10;
            currentDefence = 10;
            atkOne = "Bless";
            atkOneDmg = 40;
            atkTwo = "Pray";
            atkTwoDmg = 5;
            atkThree = "Convert";
            atkThreeDmg = 20;
            spellOne = "Ascend";
            spellOneDmg = 50;
            spellTwo = "Invisibility";
            spellTwoDmg = 0;
            spellThree = "Smite";
            spellThreeDmg = 35;
        }
    }
    public void AddStatsToLists()
    {
        enemyStatMan.maxEnemyHP.Add(maxHealth);
        enemyStatMan.currentEnemyHP.Add(currentHealth);
        enemyStatMan.maxEnemyMP.Add(maxMana);
        enemyStatMan.currentEnemyMP.Add(currentMana);
        enemyStatMan.enemyATK.Add(attack);
        enemyStatMan.currentEnemyATK.Add(currentAttack);
        enemyStatMan.enemyDEF.Add(defence);
        enemyStatMan.currentEnemyDEF.Add(currentDefence);
        combatManager.enemyAtkOne.Add(atkOne);
        combatManager.enemyAtkOneDmg.Add(atkOneDmg);
        combatManager.enemyAtkTwo.Add(atkTwo);
        combatManager.enemyAtkTwoDmg.Add(atkTwoDmg);
        combatManager.enemyAtkThree.Add(atkThree);
        combatManager.enemyAtkThreeDmg.Add(atkThreeDmg);
        combatManager.enemySpellOne.Add(spellOne);
        combatManager.enemySpellOneDmg.Add(spellOneDmg);
        combatManager.enemySpellTwo.Add(spellTwo);
        combatManager.enemySpellTwoDmg.Add(spellTwoDmg);
        combatManager.enemySpellThree.Add(spellThree);
        combatManager.enemySpellThreeDmg.Add(spellThreeDmg);
    }
    public void ClearStatsFromLists()
    {
        enemyStatMan.maxEnemyHP.Clear();
        enemyStatMan.currentEnemyHP.Clear();
        enemyStatMan.maxEnemyMP.Clear();
        enemyStatMan.currentEnemyMP.Clear();
        enemyStatMan.enemyATK.Clear();
        enemyStatMan.currentEnemyATK.Clear();
        enemyStatMan.enemyDEF.Clear();
        enemyStatMan.currentEnemyDEF.Clear();
        combatManager.enemyAtkOne.Clear();
        combatManager.enemyAtkOneDmg.Clear();
        combatManager.enemyAtkTwo.Clear();
        combatManager.enemyAtkTwoDmg.Clear();
        combatManager.enemyAtkThree.Clear();
        combatManager.enemyAtkThreeDmg.Clear();
        combatManager.enemySpellOne.Clear();
        combatManager.enemySpellOneDmg.Clear();
        combatManager.enemySpellTwo.Clear();
        combatManager.enemySpellTwoDmg.Clear();
        combatManager.enemySpellThree.Clear();
        combatManager.enemySpellThreeDmg.Clear();
    }
    public void UpdateEnemyStats(int i)
    {
        currentHealth = enemyStatMan.currentEnemyHP[i];
        currentMana = enemyStatMan.currentEnemyMP[i];
        currentAttack = enemyStatMan.currentEnemyATK[i];
        currentDefence = enemyStatMan.currentEnemyDEF[i];

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collector.EnemyEnterCombat();
        }
    }
    private void Die()
    {
        isDead = true;
        currentHealth = 0;
        enemyStatMan.SetHealth(0, combatManager.enemyIndex);

        if (combatManager.AllEnemiesDead())
        {
            combatMenu.PlayerExitCombat(combatManager.AllEnemiesDead());
        }
    }
}