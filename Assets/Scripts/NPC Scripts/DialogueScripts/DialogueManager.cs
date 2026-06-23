using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{



    [Header("UI References")]//UI引用
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;//对话选项按钮数组

    private DialogueSO currentDialogue;//当前对话数据
    public bool isDialogueActive;//对话是否激活
    private int dialogueIndex;//当前对话索引


    private float lastDialogueEndTime;//对话结束时间
    private float dialogueCooldown = .1f;//对话冷却时间


    private void Awake()
    {
       
        canvasGroup.alpha = 0;//初始隐藏对话UI
        canvasGroup.interactable = false;//初始不可交互
        canvasGroup.blocksRaycasts = false;//初始不阻挡射线

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }//隐藏所有对话选项按钮                   


    }

    public bool CanStartDialogue()
    {
        return Time.unscaledTime - lastDialogueEndTime >= dialogueCooldown;
    }

    
    public void StartDialogue(DialogueSO dialogueSO)
    {

        currentDialogue= dialogueSO;
        dialogueIndex = 0;//重置对话索引
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
            ShowDialogue();
        else
            ShowChoice();
    }//推进对话

    private void ShowDialogue()
    {
        DialogueLine line=currentDialogue.lines[dialogueIndex];//获取当前对话行
        GameManager.Instance.DialogueHistoryTracker.RecordNPC(line.speaker);//记录与NPC的对话历史
        portrait.sprite=line.speaker.portrait;
        actorName.text=line.speaker.actorName;
        dialogueText.text = line.text;//更新UI显示

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;//使对话UI可交互
        canvasGroup.blocksRaycasts = true;//使对话UI阻挡射线

        dialogueIndex++;
    }


    private void ShowChoice()
    {
            ClearChoices();//清除之前的选项
        if (currentDialogue.options.Length > 0)
        {
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option=currentDialogue.options[i];//获取当前选项
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;//设置选项文本
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));//为选项按钮添加点击事件
            }
        }
        else
        {
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "End";//如果没有选项，显示结束按钮
            choiceButtons[0].onClick.AddListener(EndDialogue);//为结束按钮添加点击事件
            choiceButtons[0].gameObject.SetActive(true);//显示结束按钮
        }

        EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);//将结束按钮绑定到键盘操作
    }//显示对话选项


    private void ChooseOption(DialogueSO dialogueSO)
    {

        if (dialogueSO == null)
        {
            EndDialogue();
        }
        else
        {
            ClearChoices();
            StartDialogue(dialogueSO);
        }
    }


    private void EndDialogue()
    {
        dialogueIndex= 0;
        isDialogueActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        lastDialogueEndTime = Time.unscaledTime;
    }

    private void ClearChoices()//清除对话选项
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();//清除按钮的所有点击事件监听器
        }
    }
}
