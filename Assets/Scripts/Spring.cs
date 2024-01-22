using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] AudioSource springAudio = null;
    [SerializeField] bool keepMomentum = false;

    private void Start()
    {
        springAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!keepMomentum && other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(force * transform.rotation.x, force, force * transform.rotation.z), ForceMode.Impulse);
        } else if (keepMomentum && other.gameObject.TryGetComponent<Player>(out player))
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(force * transform.rotation.x, force, force * transform.rotation.z), ForceMode.Impulse);
        }

        if (springAudio != null)
        {
            springAudio.PlayOneShot(springAudio.clip);
        }
    }
}
