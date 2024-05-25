using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static Action<bool> RestrictPlayerMovementAndShooting;

    private Vector3 velocity;
    private bool isGrounded;
    private bool disableMovement = false;
    public CharacterController controller;
    [SerializeField ] private float speed = 12f;
    [SerializeField ] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private GameObject damageTakenEffect;
    
    [SerializeField] private Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public int playerHealth = 100;


    private void Start()
    {
        RestrictPlayerMovementAndShooting += SetPlayerMovement;
    }


    private void SetPlayerMovement(bool p)
    {
        disableMovement = p;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (disableMovement)
            return;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer)=="EnemyBullet")
        {
            damageTakenEffect.SetActive(true);
            StartCoroutine(StopShowingEffect());
        }
    }

    private IEnumerator StopShowingEffect()
    {
        yield return new WaitForEndOfFrame();
        damageTakenEffect.SetActive(false);
    }

}
