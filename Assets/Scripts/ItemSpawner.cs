using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weigth;
    }


    public List<Spawnable> items = new List<Spawnable>();
    float totalWeigth;

    private void Awake()
    {
        totalWeigth = 0;

        foreach (var spawnable in items)
        {
            totalWeigth += spawnable.weigth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        float pick = UnityEngine.Random.value * totalWeigth;
        int chosenIndex = 0;
        float cumulativeWeigth = items[0].weigth;

        while (pick > cumulativeWeigth && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeigth += items[chosenIndex].weigth;
        }

        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;




    }

    // Update is called once per frame
    void Update()
    {
       

        //pentru a evita o eroare monstrii vor spawna "nothing" ca item spawn si acesta va fi sters, dropurile de la monstrii se vor duce in enemyDrops
        if (GameObject.FindWithTag("Nothing") != null) 
        {
            Debug.Log("Am sters toate nothingurile");
            GameObject[] nothings = GameObject.FindGameObjectsWithTag("Nothing");
            foreach (GameObject nothing in nothings)
            {
                GameObject.Destroy(nothing);
            }
        }


    }


}
