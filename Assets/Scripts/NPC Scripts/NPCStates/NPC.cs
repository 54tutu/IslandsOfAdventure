using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public enum NPCState
    {
        Default,
        Idle,
        Patrol,
        Wander,
        Talk
    }
    public NPCState currrentState= NPCState.Patrol;
    private NPCState defaultState;

    public NPC_Patrol patrol;
    public NPC_Wander wander;
    public NPC_Talk talk;

    void Start()
    {
        defaultState = currrentState;//设置默认状态
        SwitchState(currrentState);
    }


    public void SwitchState(NPCState newState)
    {
        currrentState = newState;
        patrol.enabled=newState == NPCState.Patrol;
        wander.enabled = newState == NPCState.Wander;
        talk.enabled = newState == NPCState.Talk;
    }//当玩家进入NPC的触发器时，切换到对话状态；当玩家离开时，切换回默认状态

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(NPCState.Talk);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }
}
