using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMenu : MonoBehaviour
{
    public static bool inCombat = false;
    [SerializeField] GameObject combatMenu;
    [SerializeField] PlayerController player;
    [SerializeField] EnemyCombatCollector collector;

    public void PlayerEnterCombat()
    {
        inCombat = true;
        player.rb.bodyType = RigidbodyType2D.Static;
        player.anim.SetBool("inCombat", true);
        combatMenu.SetActive(true);
    }
    public void PlayerExitCombat()
    {
        player.rb.bodyType = RigidbodyType2D.Dynamic;
        player.anim.SetBool("inCombat", false);
        collector.EnemyExitCombat();
        inCombat = false;
        combatMenu.SetActive(false);
    }
    public void PlayerExitCombat(bool allEnemiesDead)
    {
        player.rb.bodyType = RigidbodyType2D.Dynamic;
        player.anim.SetBool("inCombat", false);
        collector.EnemyExitCombat(allEnemiesDead);
        inCombat = false;
        combatMenu.SetActive(false);
    }
    public bool InCombat()
    {
        return inCombat;
    }
}
