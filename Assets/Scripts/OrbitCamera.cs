using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField][Range(20, 90)] float defaultPitch = 40;
    [SerializeField][Range(2, 8)] float distance = 5;
    [SerializeField][Range(0.1f, 2.0f)] float sensitivity = 0.5f;

    [SerializeField] GameManager gameManager;

    float yaw = 0;
    float pitch = 0;

    private void Start()
    {
        pitch = defaultPitch;
        //transform.rotation = target.transform.rotation;
    }

    void Update()
    {
        if(gameManager.state == GameManager.State.PLAY_GAME)
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;

            Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
            Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
            Quaternion rotation = qyaw * qpitch;

            transform.position = target.position + (rotation * Vector3.back * distance);
            transform.rotation = rotation;
        }
    }

    public void OnRespawn(GameObject respawn)
    {
        transform.rotation = respawn.transform.rotation;
    }
}
