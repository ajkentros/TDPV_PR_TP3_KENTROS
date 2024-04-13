using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private RouteSpawner ruteSpawner;


    // Start is called before the first frame update
    void Start()
    {
        ruteSpawner = GetComponent<RouteSpawner>();    
    }

 

    public void SpawnTriggerEntered()
    {
       
        
 
    }
}
