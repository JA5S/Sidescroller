using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController2D : MonoBehaviour
{
    //Variable Declarations
    [SerializeField] private float moveSpd = 5f;
    private float xInput;
    private float rotSpeed = 180f;
    private Vector3 direction;
    [SerializeField]private bool isFacingRight = true;
    private bool canMove = true;

    [SerializeField] private float jumpFrc = 350f;
    private float jumpInput;
    private float startHeight;
    private bool isJumping;
    private bool isFalling;
    private bool onGround;

    [SerializeField] private float dashFrc = 350f;
    [SerializeField] private int maxDashes = 2;
    [SerializeField] private int numDashes;
    private float timePassed;
    private float startTime;
    private bool isDashing;

    private Rigidbody2D rBody;

    private TextMeshProUGUI clctText;
    private int collectCnt;

    [SerializeField] private Vector3 spawnPos = new Vector3(0, 0, 0);

    private CameraFollow cameraFollow;
    private bool checkCamPos;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        //Find a safer way to do this??
        clctText = GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>();

        direction = Vector3.right;

        numDashes = maxDashes;
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Movement
        if(canMove)
            Move();

        //Debug.Log(collectCnt.ToString("D3"));
        clctText.text = collectCnt.ToString("D3");

        if (checkCamPos)
            StartCoroutine(enableMovement());
    }

    // Physics update once per fixed frame
    private void FixedUpdate()
    {
        //Jump and Dash Physics
        if (canMove)
        {
            Jump();
            if (Input.GetKey(KeyCode.H))//&& numDashes > 0
                Dash();
        }

        if (isDashing)
            stopDash();
    }

    private void Move()
    {
        xInput = Input.GetAxis("Horizontal");
        transform.Translate(direction * xInput * moveSpd * Time.deltaTime);

        if (xInput < 0 && isFacingRight)
        {
            Flip();
        }

        if (xInput > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    public void MoveWithPlatform(AutoMove platform)
    {
        transform.Translate(-direction * platform.getMoveSpeed() * platform.getDirection() * Time.deltaTime);
    }

    private void Flip()
    {
        transform.Rotate(Vector3.up * rotSpeed);
        direction *= -1;
        isFacingRight = !isFacingRight;
    }

    public float getDir()
    {
        return direction.x;
    }

    private void Jump()
    {
        jumpInput = Input.GetAxisRaw("Vertical");
        if (onGround && jumpInput > 0)
        {
            startHeight = transform.position.y;
            rBody.AddForce(Vector2.up * jumpInput * jumpFrc * Time.fixedDeltaTime, ForceMode2D.Impulse);
            onGround = false;
            isJumping = true;
        }
        if(transform.position.y >= (startHeight + 2.1f) && isJumping && !isFalling)
        {
            isFalling = true;
            rBody.gravityScale *= 2;
        }
        if (onGround)
        {
            resetGravity();
            isJumping = false;
            isFalling = false;
        }
    }

    private void Dash()
    {
        Debug.Log("Player has dashed!");
        isDashing = true;
        startTime = Time.time;
        numDashes -= 1;
        rBody.AddForce(direction * dashFrc * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void stopDash()
    {
        timePassed = Time.time - startTime;
        if (timePassed >= 0.2f)
        {
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = 0;
            isDashing = false;
            Debug.Log("Finished Dash");
        }
    }

    //Collect trigger collectable and destroy/set inactive 
    public void Collect(Collider2D collision)
    {
        collectCnt++;
        Debug.Log(collectCnt + " collectable(s) collected!");

        collision.gameObject.SetActive(false);
        //Destroy(collision.gameObject);
        Debug.Log("Collectable destroyed!");
    }

    public int getCollectCnt()
    {
        return collectCnt;
    }

    public void KillPlayer()
    {
        Debug.Log("Player was killed!");
        gameObject.SetActive(false);
        canMove = false;
        //rBody.isKinematic = true; //turn off gravity?
    }

    public Vector3 getSpawnPos()
    {
        return spawnPos;
    }

    public void setSpawnPos(Vector3 newSpawnPos)
    {
        spawnPos = newSpawnPos;
    }

    public void setOnGround(bool isOnGround)
    {
        onGround = isOnGround;
    }

    public void setGravity(float gravityModifier)
    {
        rBody.gravityScale *= gravityModifier;
    }

    private void resetGravity()
    {
        rBody.gravityScale = 1f;
    }

    public void Respawn() //make a subroutine for timing
    {
        Debug.Log("Player respawned at start.");
        transform.position = spawnPos;
        gameObject.SetActive(true);
        checkCamPos = true;
    }

    IEnumerator enableMovement()
    {
        if(camPosCheck())
        {
            Debug.Log("Camera reset to player's position.");
            canMove = true;
            checkCamPos = false;
            StopCoroutine("enableMovement");
        }
        yield return new WaitForSeconds(0.2f);
    }

    private bool camPosCheck()
    {
        if(Camera.main.transform.position == (transform.position + cameraFollow.getAltOffset()) ||
            Camera.main.transform.position.x == (transform.position.x + cameraFollow.getOffset().x) ||
            Camera.main.transform.position.x <= cameraFollow.getCamLeftBounds())
        {
            return true;
        }

        return false;
    }
}
