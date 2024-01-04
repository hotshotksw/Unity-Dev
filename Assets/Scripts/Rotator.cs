using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField][Range(-360, 360)] float angle;
    float speed = 5;

    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.up);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
