using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravityMultiplier = 2f;
    public float rotationSpeed = 200f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Vertical");
        float rotateY = Input.GetAxis("Horizontal");

        // Move forward and backward
        Vector3 move = transform.forward * moveZ * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // Rotate left and right
        transform.Rotate(0, rotateY * rotationSpeed * Time.deltaTime, 0);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
