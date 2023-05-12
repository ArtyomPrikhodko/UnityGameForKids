using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asnwear : MonoBehaviour
{
    public GameObject obj;
    private Vector3 whereToSpawn;
    [SerializeField] private float nextSpawn = 15.0f;
    private Transform player;
    

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Hero>().transform;
        }
    }
    private void Update()
    {
        if (Time.time > nextSpawn && nextSpawn > 1)
        {
            whereToSpawn = player.position;
            nextSpawn = 1000;
            GameObject Answears = Instantiate(obj, whereToSpawn, Quaternion.identity);
            
            
        }
    }
}
