using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range(1, 100)] float maxForce = 5;
    [SerializeField][Range(1, 100)] float jumpForce = 5;
    [SerializeField][Range(1, 2)] float turnSpeed = 1;
    [SerializeField] Transform view;
    [SerializeField] AudioSource jumpSound;
    [Header("Collision")]
    [SerializeField][Range(0, 5)] float rayLength = 1;
    [SerializeField] LayerMask groundLayerMask;

    Rigidbody rb;
    Vector3 force = Vector3.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical") * 1.5f;

        Quaternion yrotation = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = yrotation * direction * maxForce;

        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            force *= 0.25f;
            if(jumpSound.clip != null)
            {
                jumpSound.PlayOneShot(jumpSound.clip);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            force *= 3f;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, ForceMode.Force);
    }

    public bool CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayerMask);
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
