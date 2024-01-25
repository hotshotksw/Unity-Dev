using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab = null;
    [SerializeField] AudioClip clip = null;
    [SerializeField] int points = 0;
    //[SerializeField] AudioSource pickupAudio = null;

    private void Start()
    {
        //pickupAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("Found Player");
            player.GetComponent<PhysicsCharacterController>().GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            if (player.GetComponent<PhysicsCharacterController>().CheckGround() == false)
            {
                Debug.Log("Found Character");
                player.AddPoints(points);

                if (pickupPrefab != null)
                {
                    Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                }

                //if (pickupAudio!= null)
                if (clip != null)
                {
                    AudioSource.PlayClipAtPoint(clip, transform.position);
                    //pickupAudio.PlayOneShot(pickupAudio.clip);
                }

                gameObject.SetActive(false);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("Found Player");
            player.GetComponent<PhysicsCharacterController>().GetComponent<Rigidbody>().AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
            if (player.GetComponent<PhysicsCharacterController>().CheckGround() == false) 
            {
                Debug.Log("Found Character");
                player.AddPoints(points);

                if (pickupPrefab != null)
                {
                    Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                }

                //if (pickupAudio!= null)
                if (clip != null)
                {
                    AudioSource.PlayClipAtPoint(clip, transform.position);
                    //pickupAudio.PlayOneShot(pickupAudio.clip);
                }

                gameObject.SetActive(false);
            }
            
        }
        
    }
}
