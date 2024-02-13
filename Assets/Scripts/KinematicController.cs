using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KinematicController : MonoBehaviour
{
    [SerializeField, Range(0, 40)] float speed = 1;
    [SerializeField] float maxDistance = 5;
    [SerializeField] float rotationAngle = 10;

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        Vector3 force = direction * speed * Time.deltaTime;
        transform.localPosition += force;

        transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, maxDistance);
        Quaternion qyaw = Quaternion.AngleAxis(direction.x * rotationAngle, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(-direction.y * rotationAngle, Vector3.right);

        Quaternion rotation = qyaw * qpitch;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime);
    }
}
