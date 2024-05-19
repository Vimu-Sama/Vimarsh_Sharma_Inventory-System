using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 newVelocity = move * speed;
        newVelocity.y = rb.velocity.y; // Preserve the current y velocity (gravity)
        rb.velocity = newVelocity;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the player is touching the ground
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        // When the player is not touching the ground, they are not grounded
        isGrounded = false;
    }
}
