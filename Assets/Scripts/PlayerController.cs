using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;                 //Viteza cu care plaerul se va misca
    Rigidbody2D rigidbody;              //Rigidbody-ul caracterului

    public Text collectedText;          //Varaibile folosite pentru tinerea scorului
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;     //Variabile pentru proiectile
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Iau inputul de pe axele pt movement
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); //facem ca rigidbody-ul sa se miste inmultind inputul obtinut cu viteza setata

        float shootHor = Input.GetAxis("ShootHorizontal"); //Iau imputul de pe axele pt shooting
        float shootVert = Input.GetAxis("ShootVertical");

        if((shootHor != 0 || shootVert != 0) && Time.time > lastFire+fireDelay) // Verific daca se da un imput orizontal sau veritcal
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }


        collectedText.text = "Items Collecter: " + collectedAmount;

    }

    void Shoot(float x,float y)
    {
        GameObject bullet = Instantiate (bulletPrefab,transform.position,transform.rotation) as GameObject; //Initiem prefa-ul bullet cu pozitiile x y z ale playerului
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;    //Adaugam um rigid body si setam gravity scale
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,         //Face ca viteza lui bullet sa fie constanta
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,       // daca x sau y sunt mai mici decat 0 le va face mereu -1 si daca sunt mai mari le va face 1
            0
        );
    }

}
