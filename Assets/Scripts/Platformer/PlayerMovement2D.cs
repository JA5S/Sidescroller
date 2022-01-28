using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour, IMove
{
    //Variable Declaration
    private float xInput;
    private float rotSpeed = 180f;
    private Vector3 direction = Vector3.right;
    private bool isFacingRight = true;

    public void Move(float moveSpeed)
    {
        xInput = Input.GetAxis("Horizontal");
        transform.Translate(direction * xInput * moveSpeed * Time.deltaTime);

        if (xInput < 0 && isFacingRight)
        {
            FlipPlayer();
        }

        if (xInput > 0 && !isFacingRight)
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        transform.Rotate(Vector3.up * rotSpeed);
        direction *= -1;
        isFacingRight = !isFacingRight;
    }

    public void MoveWith(IMove other)
    {
        transform.Translate(-direction * other.getMoveSpeed() * other.getDirection() * Time.deltaTime);
    }

    public float getMoveSpeed()
    {
        throw new System.NotImplementedException();
    }

    public float getDirection()
    {
        return direction.x;
    }
}
