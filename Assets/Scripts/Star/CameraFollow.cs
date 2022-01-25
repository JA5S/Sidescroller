using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; //change to reference prefab?
    [SerializeField] private Vector3 offset = new Vector3(3, 0, -10);
    [SerializeField] private Vector3 altOffset = new Vector3(0, -5, -10);
    [SerializeField] private float cameraLeftBounds = 1.25f;

    public float speed = 5f;

    public bool cameraMoveTowards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraMoveTowards)
        {
            //if(transform.position.x > cameraLeftBounds)
            {
                if(player.transform.position.x + offset.x >= cameraLeftBounds)
                {
                    transform.position = new Vector3(player.transform.position.x + offset.x, offset.y, offset.z);
                }
                else if(transform.position.x > cameraLeftBounds)
                {
                    transform.position = new Vector3(cameraLeftBounds, offset.y, offset.z);
                }  
            }
                
        }
        if (cameraMoveTowards)
        {
            if (Camera.main.transform.position.x > cameraLeftBounds || player.transform.position.x + offset.x >= cameraLeftBounds)
                transform.position = Vector3.MoveTowards(Camera.main.transform.position, player.transform.position + altOffset, speed * Time.deltaTime);
        }
    }

    public Vector3 getOffset()
    {
        return offset;
    }

    public Vector3 getAltOffset()
    {
        return altOffset;
    }

    public float getCamLeftBounds()
    {
        return cameraLeftBounds;
    }
}
