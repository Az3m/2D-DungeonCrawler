using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemImg;

}

public class CollectionController : MonoBehaviour
{
    public Item item;
    public float healAmount;
    public float healthChange;
    public float moveSpeedChange;
    public float shootDelayChange;
    public float bulletSizeChange;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImg;

        Destroy(GetComponent<PolygonCollider2D>()); //Stergem si adaugam un polygonCollider deoarece el nu se va uppdata daca daor il adaugam
        var collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {   //Aici adaug metode pentru iteme daca vreau sa le modific
            PlayerController.collectedAmount += 1;
            GameController.HealPlayer(healAmount);
            GameController.MaxHealthChange(healthChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.FireRateChange(shootDelayChange);
            GameController.BulletSizeChange(bulletSizeChange);
            Destroy(gameObject);
        }
    }
}
