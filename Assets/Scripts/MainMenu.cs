using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelName;

    private void Start()
    {
        levelName = gameObject.name; //Sets level to connected button name, scene name and button name MUST match in order to load correct level
    }
    public void StartNewGame() //Loads first level, CURRENTLY ONLY LOADS TEST LEVEL (SWAMP)
    {
        SceneManager.LoadScene("Swamp Level");
    }
    public void ResumeGame() //Currently resumes last saved scene DOES NOT RESUME WITH SAVED IN-SCENE PROGRESS YET
    {
        //SceneManager.LoadScene(PauseMenu.lastSavedScene);
        SceneManager.LoadScene("Presentation Level"); //LOADS FAKE LEVEL TO SHOWCASE FOR GOTTFRIED
    }
    public void SelectLevel() //Loads selected level on level select screen, tests button name and loads equivilent level
    {
        SceneManager.LoadScene(levelName);
    }
    public void LevelScreen() //Loads level select menu
    {
        SceneManager.LoadScene("Level Select");
    }
    public void LoadMenu() //Loads main menu
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame() //Alt + f4
    {
        Application.Quit();
    }
}
