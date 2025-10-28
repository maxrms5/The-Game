using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyCombatCollector : MonoBehaviour
{
    public GameObject[] enemies;
    public List<GameObject> enemiesInCombat;
    public List<string> names;
    public GameObject[] enemySpawn = new GameObject[6];
    [SerializeField] CombatMenu combatMenu;
    [SerializeField] CombatManager combatManager;
    [SerializeField] EnemyStatsManager enemyStatMan;

    private void OnEnable()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }
    public void EnemyEnterCombat()
    {
        if (combatMenu.InCombat() == false)
        {
            foreach (var enemy in enemies)
            {
                var distanceToPlayer = Vector3.Distance(gameObject.transform.position, enemy.transform.position); //distance from enemy to player
                if (distanceToPlayer < 5f)
                {
                    enemy.GetComponent<AIPath>().canMove = false;
                    enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    enemy.GetComponent<SpriteRenderer>().sortingOrder = 101;
                    enemy.GetComponent<EnemyData>().AddStatsToLists();
                    combatManager.enemyData.Add(enemy.GetComponent<EnemyData>());
                    enemiesInCombat.Add(enemy);
                    names.Add(enemy.name);

                    var i = enemiesInCombat.IndexOf(enemy);
                    enemy.tag = "Enemy " + (i+1); //Tags enemies 1 - 6
                    enemy.transform.position = new Vector3(enemySpawn[i].transform.position.x, enemySpawn[i].transform.position.y, enemySpawn[i].transform.position.z);
                    //enemyStatMan.SetStats(i);
                    Debug.Log("Enemy '" + enemy.name + "' is at " + enemySpawn[i].name);
                    Debug.Log("Enemy '" + enemy.name + "' is in combat");
                }
                else //stops all other enemies in scene from moving, basiclly pausing game
                {
                    enemy.GetComponent<AIPath>().canMove = false;
                }
            }
        }
        combatMenu.PlayerEnterCombat();
        Debug.Log("Player in combat");
        enemyStatMan.SetStats();
        Debug.Log("All enemy stats set");
        combatManager.HideInactiveEnemies();
        Debug.Log("Enemies hidden");
    }
    public void EnemyExitCombat()
    {
        if (combatMenu.InCombat() == true)
        {
            Debug.Log("Enemies Exiting Combat");
            foreach (var enemy in enemiesInCombat)
            {
                enemy.GetComponent<AIPath>().canMove = true;
                enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                enemy.GetComponent<SpriteRenderer>().sortingOrder = 10;
                enemy.GetComponent<EnemyData>().ClearStatsFromLists();
                enemy.tag = "enemy";
                Debug.Log("Enemy '" + enemy.name + "' is out of combat");
            }
            foreach (var enemy in enemies) //other enemies can move again
            {
                enemy.GetComponent<AIPath>().canMove = true;
            }
            enemiesInCombat.Clear();
            combatManager.enemyData.Clear();
            names.Clear();
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            Debug.Log("Combat Exited");
        }
    }
    public void EnemyExitCombat(bool allEnemiesDead)
    {
        if (allEnemiesDead == true)
        {
            if (combatMenu.InCombat() == true)
            {
                Debug.Log("Enemies Being Destoryed");
                foreach (var enemy in enemiesInCombat)
                {
                    Destroy(enemy);
                }
                foreach (var enemy in enemies) //other enemies can move again
                {
                    enemy.GetComponent<AIPath>().canMove = true;
                }
                enemiesInCombat.Clear();
                combatManager.enemyData.Clear();
                names.Clear();
                enemies = GameObject.FindGameObjectsWithTag("enemy");
                Debug.Log("Combat Exited");
            }
        }
        else
        {
            if (combatMenu.InCombat() == true)
            {
                Debug.Log("Enemies Exiting Combat");
                foreach (var enemy in enemiesInCombat)
                {
                    enemy.GetComponent<AIPath>().canMove = true;
                    enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    enemy.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    enemy.GetComponent<EnemyData>().ClearStatsFromLists();
                    combatManager.enemyData.Clear();
                    names.Clear();
                    enemy.tag = "enemy";
                    Debug.Log("Enemy '" + enemy.name + "' is out of combat");
                }
                foreach (var enemy in enemies) //other enemies can move again
                {
                    enemy.GetComponent<AIPath>().canMove = true;
                }
                enemiesInCombat.Clear();
                enemies = GameObject.FindGameObjectsWithTag("enemy");
                Debug.Log("Combat Exited");
            }
        }
    }
}