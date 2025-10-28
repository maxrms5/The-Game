using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyStatsManager : MonoBehaviour
{
    //GameObjects and scripts
    [SerializeField] EnemyCombatCollector collector;
    [SerializeField] Slider[] healthSlider = new Slider[5];
    [SerializeField] TextMeshProUGUI[] enemyName = new TextMeshProUGUI[5];
    [SerializeField] TextMeshProUGUI[] healthText = new TextMeshProUGUI[5];

    //Names and Stats
    public List<int> maxEnemyHP;
    public List<int> currentEnemyHP;
    public List<int> maxEnemyMP;
    public List<int> currentEnemyMP;
    public List<int> enemyATK;
    public List<int> currentEnemyATK;
    public List<int> enemyDEF;
    public List<int> currentEnemyDEF;
    public List<string> names;
    public List<GameObject> enemies;

    public void SetStats(int i) //Sets specific stats (used during combat ONLY)
    {
        var enemies = collector.enemiesInCombat;
        SetMaxHealth(maxEnemyHP[i], i);
        SetMaxMana(maxEnemyMP[i], i);
        SetAttack(enemyATK[i], i);
        SetDefence(enemyDEF[i], i);
        enemyName[i].text = names[i];
        Debug.Log(enemies[i].name + " Stats Set");
    }
    public void SetStats() //Sets all stat
    {
        var enemies = collector.enemiesInCombat;
        names = collector.names;
        foreach (var enemy in enemies)
        {
            var i = enemies.IndexOf(enemy); //Makes functions grab health/mana/etc from the same index in thier list as an enemy is in the enemy list iykyk
            SetMaxHealth(maxEnemyHP[i], i);
            SetMaxMana(maxEnemyMP[i], i);
            SetAttack(enemyATK[i], i);
            SetDefence(enemyDEF[i], i);
            enemyName[i].text = names[i];
            Debug.Log(enemies[i].name + " Stats Set");
        }
    }
    public void UpdateStats(int i)
    {
        var enemies = collector.enemiesInCombat;
        SetHealth(currentEnemyHP[i], i);
        SetMana(currentEnemyMP[i], i);
        SetAttack(currentEnemyATK[i], i);
        SetDefence(currentEnemyDEF[i], i);
        enemyName[i].text = names[i];
        Debug.Log(enemies[i].name + " Stats Updated");
    }
    public void UpdateStats()
    {
        var enemies = collector.enemiesInCombat;
        names = collector.names;
        foreach (var enemy in enemies)
        {
            var i = enemies.IndexOf(enemy); //Makes functions grab health/mana/etc from the same index in thier list as an enemy is in the enemy list iykyk
            SetHealth(currentEnemyHP[i], i);
            SetMana(currentEnemyMP[i], i);
            SetAttack(currentEnemyATK[i], i);
            SetDefence(currentEnemyDEF[i], i);
            enemyName[i].text = names[i];
            Debug.Log(enemies[i].name + " Stats Updated");
        }
    }
    public void SetMaxHealth(int health, int enemyNum)
    {
        currentEnemyHP[enemyNum] = health;
        healthSlider[enemyNum].maxValue = health;
        healthSlider[enemyNum].value = health;
        healthText[enemyNum].text = "HP: " + health;
    }
    public void SetHealth(int health, int enemyNum)
    {
        currentEnemyHP[enemyNum] = health;
        healthSlider[enemyNum].value = health;
        healthText[enemyNum].text = "HP: " + health;
    }
    public void ChangeHealth(int health, int enemyNum)
    {
        currentEnemyHP[enemyNum] += health;
        healthSlider[enemyNum].value = currentEnemyHP[enemyNum];
        healthText[enemyNum].text = "HP: " + currentEnemyHP[enemyNum];
    }
    public void SetMaxMana(int mana, int enemyNum)
    {
        currentEnemyMP[enemyNum] = mana;
    }   
    public void SetMana(int mana, int enemyNum)
    {
        currentEnemyMP[enemyNum] = mana;
    }
    public void ChangeMana(int mana, int playerNum)
    {
        currentEnemyMP[playerNum] += mana;
    }
    public void SetAttack(int attack, int enemyNum)
    {
        currentEnemyATK[enemyNum] = attack;
    }
    public void ChangeAttack(int attack, int enemyNum)
    {
        currentEnemyATK[enemyNum] += attack;
    }
    public void SetDefence(int defence, int enemyNum)
    {
        currentEnemyDEF[enemyNum] = defence;
    }
    public void ChangeDefence(int defence, int enemyNum)
    {
        currentEnemyDEF[enemyNum] += defence;
    }
}
