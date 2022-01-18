using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //declare variables
    private Rigidbody2D playerRb;

    private Vector3 direction;

    private bool isOnGround;

    public float jumpForce = 5;
    public float speed = 5;
    private float rotSpeed = 180f;

    private float horizontalInput;
    private float verticalInput;

    private bool isFacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        //initialize references and variables
        playerRb = GetComponent<Rigidbody2D>();
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
    void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //playerRb.MovePosition(direction * horizontalInput * speed * Time.deltaTime);
        transform.Translate(direction * horizontalInput * speed * Time.deltaTime);

        if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(Vector3.up * rotSpeed);
        direction *= -1;
        isFacingRight = !isFacingRight;
    }

    void Jump()
    {
        if (isOnGround && verticalInput > 0)
        {
            playerRb.AddForce(Vector2.up * verticalInput * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }
}
