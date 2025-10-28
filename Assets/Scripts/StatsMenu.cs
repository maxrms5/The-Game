using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    private bool statsMenuOpen = false;
    [SerializeField] GameObject statsMenuUI;
    [SerializeField] Inventory inventory;
    PlayerManager playerManager;

    void Start()
    {
        playerManager = GetComponentInChildren<PlayerManager>();
        HideStats(); //Starts game with stats menu closed
    }
    private void Update()        
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //Activates stats menu
        {
            if (statsMenuOpen)
            {
                HideStats();
            }
            else if (!PauseMenu.menuOpen && !(SceneManager.GetActiveScene().ToString() == "Main Menu" || SceneManager.GetActiveScene().ToString() == "Level Select"))
            {
                ShowStats();
                Debug.Log("Stats Opened");
                playerManager.UpdatePlayerUnlocks();
                Debug.Log("Unlock status updated");
                inventory.SaveToJson();
                Debug.Log("Auto Save Complete");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && statsMenuOpen)
        {
            HideStats();
        }
    }
    public void ShowStats()
    {
        statsMenuUI.SetActive(true);
        PauseMenu.menuOpen = true;
        Time.timeScale = 0f;
        statsMenuOpen = true;
    }
    public void HideStats()
    {
        statsMenuUI.SetActive(false);
        PauseMenu.menuOpen = false;
        Time.timeScale = 1f;
        statsMenuOpen = false;
    }
}