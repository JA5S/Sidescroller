using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum BattleState {OFF, START, PLAYERTURN, ENEMYTURN, WIN, LOSE}

    public BattleState state;
    public SideScrollerController controller;
    public Sword player;
    private Enemy enemy;
    private Vector3 battlePosPlayer;
    private Vector3 battlePosEnemy;


    // Update is called once per frame
    void Update()
    {
        if (controller.battleHasStarted)
        {
            enemy = controller.enemy;
            SetBattlePositions();
            if (state == BattleState.OFF)
            {
                StartBattle();
            }
            if(state == BattleState.ENEMYTURN)
            {
                enemy.Attack();
            }
            if(state == BattleState.PLAYERTURN)
            {
                player.Attack();
            }
            if (enemy.health == 0 || !enemy.gameObject.activeSelf)
            {
                state = BattleState.WIN;
                controller.battleHasStarted = false;
                controller.canMove = true;
            }
            if(state == BattleState.WIN || state == BattleState.LOSE)
            {
                state = BattleState.OFF;
            }
        }
    }

    void StartBattle()
    {
        state = BattleState.START;
        if (player.speedStat >= enemy.speedStat && state == BattleState.START)
        {
            state = BattleState.PLAYERTURN;
            Debug.Log("Switched to Player Turn");
        }
        else if(state == BattleState.START)
        {
            state = BattleState.ENEMYTURN;
            Debug.Log("Switched to Player Turn");
        }
    }

    void SetBattlePositions()
    {
        //set battle positions
        battlePosPlayer = new Vector3(controller.enemyBattleStartPos.x - 2.5f, controller.transform.position.y, controller.battleStartPos.z);
        controller.transform.position = Vector3.MoveTowards(controller.transform.position, battlePosPlayer, Time.deltaTime * controller.speed);
        battlePosEnemy = controller.enemyBattleStartPos + Vector3.right;
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, battlePosEnemy, Time.deltaTime * enemy.speed);
    }
}
