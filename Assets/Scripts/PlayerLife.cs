using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public int maxPlayerHealth = 20;
    public int currentPlayerHealth;
    public bool isDead;

    [SerializeField] private AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPlayerHealth = maxPlayerHealth;
        //healthBar.SetMaxHealth(maxPlayerHealth);
        isDead = false;
}
    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;
        //healthBar.SetHealth(currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            Die();
            Debug.Log("Death Triggered");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ttky"))
        {
            Debug.Log("Collided");
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        StartCoroutine(RestartLevel());
       // healthBar.SetHealth(0);
        deathSound.Play();
        Debug.Log("Death Sound Triggered");
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("RB Static");
    }
    IEnumerator RestartLevel()
    {
        Debug.Log("Level Reset Triggered");
        anim.SetTrigger("death");
        Debug.Log("Anim Triggered");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log("Anim Wait Over Triggered");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
