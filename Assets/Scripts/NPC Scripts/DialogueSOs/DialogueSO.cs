using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]//创建一个新的菜单项来创建DialogueSO对象

public class DialogueSO : ScriptableObject
{

    public DialogueLine[] lines;//对话行数组，每个元素包含说话者和文本
    public DialogueOption[] options;//对话选项数组，每个元素包含选项文本和对应的下一个对话
    [Header("Conditional Requirements (Optional)")]
    public ActorSO[] requiredNPCs;
    public LocationSO[] requiredLocations;//条件地点
    public ItemSO[] requiredItems;
    
    [Header("Control Flags")]
    public bool removeAfterPlay;
    public List<DialogueSO> removeTheseOnPlay;



    public bool IsConditionMet()
    {
        if(requiredNPCs.Length > 0)
        {
            foreach(var npc in requiredNPCs)
            {
                if (!GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(npc)) 
                    return false;
            }
        }
        if(requiredLocations.Length > 0)
        {
            foreach(var location in requiredLocations)
            {
                if(!GameManager.Instance.LocationHistoryTracher.HasVisited(location))
                    return false;
            }
        }

        if(requiredItems.Length > 0)
        {
            foreach (var item in requiredItems)
            {
                if(!InventoryManager.Instance.HasItem(item))
                    return false;
            }
        }
        return true;
    }
}


[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3, 5)] public string text;


}//DialogueSO类是一个ScriptableObject，用于存储对话数据。它包含一个DialogueLine数组，每个DialogueLine表示对话中的一行文本和说话者。
 //ActorSO类是另一个ScriptableObject，用于存储角色数据，包括角色名称和头像。


[System.Serializable]
public class DialogueOption
{
    public string optionText;//选项文本
    public DialogueSO nextDialogue;//选项对应的下一个对话
}