using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorwayManager : MonoBehaviour
{
    private bool levelComplete = false;
    private Animator anim;
    [SerializeField] GameObject cam;
    [SerializeField] PlayerController player;
    public int preScene = 1; //Set to build index of test level
    private string combatScene = "Combat Menu";
    [SerializeField] private AudioSource levelEndSound;
    //[SerializeField] GameObject playerSpawn;
    [SerializeField] LoadMenu loadMenu;

    void Start()
    {
        anim = GetComponent<Animator>();
        levelEndSound = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D  collision) //Moves to next level when player collides with checkpoint
    {
        //PlayerController player = GetComponent<PlayerController>();
        if (collision.gameObject.name == "level name here please fix!!!!" && !levelComplete)
        {
            levelComplete = true;
            anim.SetTrigger("levelComplete");
            levelEndSound.Play();
            //player.isMovable = false;
            Invoke("CompleteLevel", 2.5f);
            //rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (collision.gameObject.CompareTag("enemy")) //Switches to combat screen when player collides with enemy
        {
            //EnterCombat();
        }
    }
    private void CompleteLevel() //loads next level in scene build order
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void EndCombat() //allows player to move and returns player to previous scene
    {
        CombatManager.inCombat = false;
        SceneManager.LoadScene(preScene);
        cam.SetActive(true);
    }
    public void EnterCombat() //Switches scene to comabt menu, brings player along
    {
        preScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Previous Scene Index Saved");
        CombatManager.inCombat = true;
        Debug.Log("In Combat");
        player.rb.bodyType = RigidbodyType2D.Static;
        player.anim.SetBool("inCombat", true);
        cam.SetActive(false);
        SceneManager.LoadScene(combatScene);
        //loadMenu.LoadScene(combatScene);
        Debug.Log("Combat Scene Loaded");
        //transform.position = playerSpawn.transform.position;
        //transform.localScale = new Vector3(13f, 13f, 1f);
    }
}