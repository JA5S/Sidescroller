using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    //Variable declarations
    private float dir = 1;
    [SerializeField] private float moveSpd = 5f;
    [SerializeField] private float leftBounds = -2;
    [SerializeField] private float rightBounds = 2;
    [SerializeField] private float centerX = 3;
    [SerializeField] private bool startMovingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        if(!startMovingLeft)
        {
            dir *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - centerX <= leftBounds)
            dir *= -1;
        if (transform.position.x - centerX >= rightBounds)
            dir *= -1;

        transform.Translate(Vector3.left * moveSpd * dir * Time.deltaTime);
    }

    public float getDirection()
    {
        return dir;
    }

    public float getMoveSpeed()
    {
        return moveSpd;
    }
}
