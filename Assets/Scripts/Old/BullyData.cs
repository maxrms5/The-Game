using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Bully", menuName = "Enemy/Bully")]

public class BullyData : ScriptableObject
{
    public int maxHealth = 30;
    public int maxMana = 10;
    public int attack = 5;
    public int defence = 5; 

    public string atkOne = "Tickle";
    public string atkTwo = "Bite";
    public string atkThree = "Zap";
}
