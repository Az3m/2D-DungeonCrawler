using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static int health = 10;
    public static int Health { get => health;set => health = value; }
    public Text healthText;

    private static int maxHealth = 10;
    public static int MaxHealth { get => maxHealth;set => maxHealth = value; }

    private static float moveSpeed = 5f;
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private static float fireRate = 0.5f;
    public static float FireRate { get => fireRate; set => fireRate = value; }

    private static float bulletSpeed = 7f;
    public static float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    private static float bulletLifetime = 1f;
    public static float BulletLifetime { get => bulletLifetime; set => bulletLifetime = value; }


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public static void DamagePlayer (int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            KillPlayer();
        }



    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void KillPlayer()
    {

    }


}
