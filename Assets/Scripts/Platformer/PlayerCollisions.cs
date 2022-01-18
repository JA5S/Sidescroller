using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    private PlayerController2D pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = gameObject.GetComponent<PlayerController2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                pc.setOnGround(true);
                break;
            case "Enemy":
                Debug.Log("Collided with an enemy!");
                pc.KillPlayer();
                pc.Respawn();
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Platform":
                pc.setOnGround(true);
                pc.MoveWithPlatform(collision.gameObject.GetComponent<AutoMove>());
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Collectable":
                pc.Collect(collision);
                break;
            case "Killbox":
                Debug.Log("Player fell from the world!");
                pc.KillPlayer();
                pc.Respawn();
                break;
            case "Checkpoint":
                Debug.Log("Reached Checkpoint");
                pc.setSpawnPos(collision.transform.position);
                break;
            default:
                break;
        }
    }
}
