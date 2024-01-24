using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
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
        print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.AddPoints(points);
        }

        if (pickupPrefab != null)
        {
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        }

        //if (pickupAudio!= null)
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, player.transform.position);
            //pickupAudio.PlayOneShot(pickupAudio.clip);
        }

        gameObject.SetActive(false);
    }
}
