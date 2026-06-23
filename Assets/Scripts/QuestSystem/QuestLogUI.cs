using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{

    [SerializeField] private QuestManager questManager;

    public void HandleQuestClicked(QuestSO questSO)
    {
        Debug.Log($"===Clicked Quest: {questSO.questName}===");

        foreach (var objective in questSO.objectives)
        {
            questManager.UpdateObjectiveProgress(questSO, objective);
            Debug.Log($"Objective: {objective.description}=>{questManager.GetProgressText(questSO,objective)}");
        }
    }

}
