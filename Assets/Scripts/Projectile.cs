using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        UtilsClass.MoveRight(transform, speed);

        if(!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Destructible"))
        {
            Destroy(collision.gameObject);
        }
    }
}
