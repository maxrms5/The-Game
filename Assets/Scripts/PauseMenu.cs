using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MainMenu
{
    public static bool gameIsPaused = false;
    public static bool menuOpen;
    public static string lastSavedScene;
    [SerializeField] GameObject pauseMenuUI;
    public Inventory inventory;

    private void Start()
    {
        Resume(); //Starts game with pause menu closed
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Activates pause menu
        {
            if (gameIsPaused) 
            {
                Resume();
            }
            else if (!menuOpen && !(SceneManager.GetActiveScene().ToString() == "Main Menu" || SceneManager.GetActiveScene().ToString() == "Level Select"))
            {
                Pause();
                Debug.Log("Paused");
                inventory.SaveToJson();
                Debug.Log("Auto Save Complete");
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        menuOpen = false;
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuOpen = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
        lastSavedScene = SceneManager.GetActiveScene().ToString();
        Debug.Log(lastSavedScene);
        Debug.Log("Scene Paused");
    }
}
