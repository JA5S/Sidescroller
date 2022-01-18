using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerController : MonoBehaviour
{
    //Variables
    public Enemy enemy;
    public BattleManager battleManager;
    private Rigidbody2D playerRb;
    public Vector3 direction;

    public float jumpForce = 275f;
    public float speed = 6f;
    private float rotSpeed = 180;

    private float horizontalInput;
    private float verticalInput;

    public bool isFacingRight = true;
    public bool isOnGround;
    public bool canMove = true;
    public bool battleHasStarted;
    public Vector3 battleStartPos;
    public Vector3 enemyBattleStartPos;

    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MovePlayer();
        }

        if(transform.position.y < -7.5)
        {
            transform.position = startPos;
        }
    }

    void FixedUpdate()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            enemy = collision.collider.GetComponent<Enemy>();
            Debug.Log("Combat has started");
            //Load combat positions
            battleHasStarted = true;
            battleStartPos = transform.position;
            enemyBattleStartPos = enemy.transform.position;
            if (battleHasStarted)
            {
                canMove = false;
                enemy.canMove = false;
            }
            //Win
            //or Lose
            //Debug.Log("Player has been destroyed");
        }
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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
