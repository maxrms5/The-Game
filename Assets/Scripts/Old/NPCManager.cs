using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public float npcSpeed = 3f;
    public float distance = 2f; //Distance from wall before enemy changes direction
    private bool movingRight = true;
    public int npcHealth = 20;
    public Transform groundDetection; //Detection point for walls
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.Translate(Vector2.right * npcSpeed * Time.deltaTime); //NPC Movement

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance); //Checks for walls infront of NPC
        if (groundInfo.collider == false) //Changes NPC direction if it hits a wall
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
