using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 6;
    public static float Health { get => health;set => health = value; }

    private static float maxHealth = 6;
    public static float MaxHealth { get => maxHealth;set => maxHealth = value; }

    private static float moveSpeed = 5f;
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private static float playerDamage, playerDamageDefault = 1;
    public static float PlayerDamage { get=> playerDamage; set => playerDamage = value; }

    private static int enemyDamage = 1;
    public static int EnemyDamage { get => enemyDamage; set => enemyDamage = value; }

    private static int rangedEnemyDamage = 2;
    public static int RangedEnemyDamage { get => rangedEnemyDamage; set => rangedEnemyDamage = value; }

    private static float fireRate, fireRateDefault = 0.5f;
    public static float FireRate { get => fireRate; set => fireRate = value; }

    private static float bulletSpeed = 7f;
    public static float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    private static float bulletSize = 0.5f;
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    private static float bulletLifetime = 1f;
    public static float BulletLifetime { get => bulletLifetime; set => bulletLifetime = value; }


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        health = 6;
        moveSpeed = 5f;
        playerDamage = 1;
        fireRate = 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DamagePlayer (int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        if(health + healAmount > MaxHealth) 
        {
            health = maxHealth;
        }
        else
        {
            health += healAmount;
        }
    }

    public static void DamageChange(float damage)
    {
        playerDamage += damage;
    }

    public static void MoveSpeedChange (float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate) //Tine minte : rate negativ creste dellay-ul(tragi mai incet) si rate pozitiv il scare (tragi mai repede)
    {                                          
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    internal static void MaxHealthChange(float health)
    {
        maxHealth += health;
    }

    public static void KillPlayer()
    {
        SceneManager.LoadScene(2);
    }


}
