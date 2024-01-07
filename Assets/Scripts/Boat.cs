using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 5f;

    private void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
