using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight = 3;
    public float gravity = -9.8f;
    public Transform groundCheckPos;
    public LayerMask groundMask;

    private CharacterController characterController;
    private bool isGrounded = false;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckPos.position, 0.4f, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = 0;

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        // jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // add gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
