using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{


    private readonly HashSet<ActorSO> spokenNPCs=new HashSet<ActorSO>();//已说过话的NPC列表 并设为只读



    public void RecordNPC(ActorSO actorSO)//记录与NPC的对话
    {
        spokenNPCs.Add(actorSO);
        Debug.Log($"Recorded NPC: {actorSO.actorName}");
    }
    public bool HasSpokenWith(ActorSO actorSO)//检查是否与NPC说过话
    {
        return spokenNPCs.Contains(actorSO);
    }

}
