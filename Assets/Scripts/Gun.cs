using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject laserPrefab;
    public Vector3 offset;
    private SideScrollerController player;
    public GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<SideScrollerController>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(transform.localScale.x * player.direction.x, 0, 0);

        //make coroutine or time passed/Time.time
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position + offset, transform.parent.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sword.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
