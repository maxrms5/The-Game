using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI goldText;
    [SerializeField] AudioSource healthCollectSound;
    [SerializeField] AudioSource manaCollectSound;
    [SerializeField] AudioSource goldCollectSound;
    [SerializeField] SaveData saveData;

    private void Awake()
    {
        healthCollectSound = GetComponent<AudioSource>();
        manaCollectSound = GetComponent<AudioSource>();
        goldCollectSound = GetComponent<AudioSource>();
        saveData = GetComponent<SaveData>();
    }
    private void Start()
    {
        healthText.text = "HP Potions: " + saveData.ItemCount(Item.Type.HealthPotion);
        manaText.text = "MP Potions: " + saveData.ItemCount(Item.Type.ManaPotion);
        goldText.text = "Gold: " + saveData.ItemCount(Item.Type.GoldCoin);
    }
    private void OnTriggerEnter2D(Collider2D collision) //Collects items when player collides with item, uses tags to detect
    {
        if (collision.gameObject.CompareTag("healthPotion")) //Collects health potions
        {
            //healthCollectSound.Play();
            Destroy(collision.gameObject);
            Debug.Log("Item Destroyed");
            saveData.PickupItem(Item.Type.HealthPotion);
            healthText.text = "HP Potions: " + saveData.ItemCount(Item.Type.HealthPotion);
            Debug.Log("Item Picked Up");
        }
        if (collision.gameObject.CompareTag("manaPotion")) //Collects mana potions
        {
            //manaCollectSound.Play();
            Destroy(collision.gameObject);
            Debug.Log("Item Destroyed");
            saveData.PickupItem(Item.Type.ManaPotion);
            manaText.text = "MP Potions: " + saveData.ItemCount(Item.Type.ManaPotion);
            Debug.Log("Item Picked Up");
        }
        if (collision.gameObject.CompareTag("goldCoin")) //Collects mana potions
        {
            //goldCollectSound.Play();
            Destroy(collision.gameObject);
            saveData.PickupItem(Item.Type.GoldCoin);
            goldText.text = "Gold: " + saveData.ItemCount(Item.Type.GoldCoin);
            Debug.Log("Item Picked Up");
        }
    }
}
