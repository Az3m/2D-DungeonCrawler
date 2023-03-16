using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Progress;

public enum EnemyState
{
    Idle,
    Follow,
    Attack,
    Die
};

public enum EnemyType
{
    Mele,
    Ranged
};


public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weigth;
    }

    public List<Spawnable> enemyDrops = new List<Spawnable>();

    float totalEnemyDropsWeigth;



    GameObject player;
    public EnemyState currentState = EnemyState.Idle; //Default enemy state
    public EnemyType enemyType;

    public Rigidbody2D rb;
    public float health;
    public float range; //Range and speed of the enemy
    public float speed;
    public float attackRange;
    public float attackCooldown;
    public int enemyDamage;
    public bool notInRoom = false;

    private bool chooseDir = false;
    private bool cooldownAttack = false;
    private bool dead = false;

    private Vector3 randomDir;

    public GameObject bulletPrefab;

    private void Awake()
    {
        totalEnemyDropsWeigth = 0;
        foreach (var spawnable in enemyDrops)
        {
            totalEnemyDropsWeigth += spawnable.weigth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyDamage = GameController.EnemyDamage;

        switch (currentState) //finite state machine
        {
            case (EnemyState.Idle):
                //Idle();
            break;

            break;

            case (EnemyState.Follow):
                Follow();
            break;

            case (EnemyState.Die):
                
            break;

            case (EnemyState.Attack):
                Attack();
            break;
        }

        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currentState != EnemyState.Die)
            {
                currentState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currentState != EnemyState.Die)
            {
                currentState = EnemyState.Idle;

            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currentState = EnemyState.Attack;
            }
        }else
        {
            currentState = EnemyState.Idle;
        }


    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range; //Verifica daca playrul este in range-ul inamicului
    }


    private IEnumerator ChooseDir()
    {
        
        chooseDir = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 8f)); // asteapta intre 2 - 8 sec
        randomDir = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));//Un vector care ne da o directie random in care se va rotii obiectul
        Quaternion nextRotaton = Quaternion.Euler(randomDir); //Quaternion se foloseste pentru rotatiile obiectelor
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotaton, UnityEngine.Random.Range(0.5f, 2.5f));//O tranzitie intre cele 2 rotatii intr-un timp random
        chooseDir = false;
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed*Time.deltaTime);

    }

    void Attack()
    {
        if (!cooldownAttack)
        {
            switch (enemyType)
            {
                case EnemyType.Mele:

                    GameController.DamagePlayer(enemyDamage);
                    StartCoroutine(Cooldown());
                break;

                case EnemyType.Ranged:

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(Cooldown());
                break;
            }
            
        }
        
    }
    public void EnemyTakeDamage(float playerDmg)
    {
        health -= playerDmg;
        if(health <= 0)
        {
            Death();
        }
    }

    private IEnumerator Cooldown()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(attackCooldown);
        cooldownAttack = false;
    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
        SpawnItemOnEnemyDeath();
        
    }

    public void SpawnItemOnEnemyDeath()
    {
        float pick = UnityEngine.Random.value * totalEnemyDropsWeigth;
        int chosenIndex = 0;
        float cumulativeWeigth = enemyDrops[0].weigth;

        while (pick > cumulativeWeigth && chosenIndex < enemyDrops.Count - 1)
        {
            chosenIndex++;
            cumulativeWeigth += enemyDrops[chosenIndex].weigth;
        }

        GameObject i = Instantiate(enemyDrops[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;

    }
}
