using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab = null;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
