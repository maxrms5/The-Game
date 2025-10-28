using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool allCharsUnlocked = false;
    public bool brawlerUnlocked = true;
    public bool frogUnlocked = false;
    public bool bardUnlocked = false;
    public bool rougeUnlocked = false;
    public bool crabUnlocked = false;
    public bool bullyUnlocked = false;
    [SerializeField] GameObject[] playerUI = new GameObject[5];
    [SerializeField] GameObject statsScreen;

    public void UpdatePlayerUnlocks()
    {
        if (statsScreen != null)
        {
            if (brawlerUnlocked || allCharsUnlocked)
            {
                playerUI[0].SetActive(true);
                Debug.Log("Brawler Unlocked");
            }
            else
            {
                playerUI[0].SetActive(false);
                Debug.Log("Brawler Locked");
            }
            
            if (frogUnlocked || allCharsUnlocked)
            {
                playerUI[1].SetActive(true);
                Debug.Log("Frog Unlocked");
            }
            else
            {
                playerUI[1].SetActive(false);
                Debug.Log("Frog Locked");
            }

            if (bardUnlocked || allCharsUnlocked)
            {
                playerUI[2].SetActive(true);
                Debug.Log("Bard Unlocked");
            }
            else
            {
                playerUI[2].SetActive(false);
                Debug.Log("Bard Locked");
            }

            if (rougeUnlocked || allCharsUnlocked)
            {
                playerUI[3].SetActive(true);
                Debug.Log("Rouge Unlocked");
            }
            else
            {
                playerUI[3].SetActive(false);
                Debug.Log("Rouge Locked");
            }

            if (crabUnlocked || allCharsUnlocked)
            {
                playerUI[4].SetActive(true);
                Debug.Log("Crab Unlocked");
            }
            else
            {
                playerUI[4].SetActive(false);
                Debug.Log("Crab Locked");
            }

            if (bullyUnlocked || allCharsUnlocked)
            {
                playerUI[5].SetActive(true);
                Debug.Log("Bully Unlocked");
            }
            else
            {
                playerUI[5].SetActive(false);
                Debug.Log("Bully Locked");
            }
        }
        else
        {
            Debug.Log("UI Disabled");
        }
    }
}
