using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float runSpeed = 30f;
    public float gravity = -9.81f;
    public float jumpHeight = 0.0f;

    private float currentSpeed;

    private CharacterController controller;
    private Vector3 velocity;

    //public AudioClip soundClip;
    //private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning) {
            currentSpeed = runSpeed;
        } else {
            currentSpeed = speed;
        }

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        /*if (Input.GetButtonDown("Jump") && controller.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/
    }
}
