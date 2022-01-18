using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public int health = 2;
    public int speedStat = 3;
    public int attackStat = 1;
    public bool canMove = true;
    public BattleManager battleManager;
    public Sword player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            UtilsClass.MoveLeft(transform, speed);
        }

        if(health == 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            Debug.Log(this.name + " has been defeated!");
        }
    }

    public void Attack()
    {
        Debug.Log(name + " has attacked the Player");
        player.health -= attackStat;
        Debug.Log(name + " dealt " + attackStat + " damage to the Player");
        battleManager.state = BattleManager.BattleState.PLAYERTURN;
        Debug.Log("Now Player Turn");
    }
}
