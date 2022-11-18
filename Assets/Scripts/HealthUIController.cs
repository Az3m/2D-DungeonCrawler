using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject heartContainer;
    private float fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fill = (float)GameController.Health;
        fill = (fill / GameController.MaxHealth); //Pune valoarea fill-ului intre 0-1

        heartContainer.GetComponent<Image>().fillAmount = fill;


    }
}
