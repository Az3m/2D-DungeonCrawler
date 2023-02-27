using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime;

    public bool isEnemyBullet = false;

    private Vector2 lastPosition;
    private Vector2 curPosition;
    private Vector2 playerPosition;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet) 
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyBullet) 
        {
            curPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 5f * Time.deltaTime);

            if(curPosition == lastPosition ) 
            {
                Destroy(gameObject); 
            }
            lastPosition = curPosition;
        }
    }

    public void GetPlayer(Transform player)
    {
        playerPosition = player.position;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !isEnemyBullet )
        {
            collision.gameObject.GetComponent<EnemyController>().EnemyTakeDamage(GameController.PlayerDamage);
            Destroy(gameObject);
        }

        if(collision.tag == "Player" && isEnemyBullet)
        {
            GameController.DamagePlayer(GameController.RangedEnemyDamage);
            Destroy(gameObject);
        }
    }
}