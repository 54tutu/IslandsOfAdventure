using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;

    public List<DialogueSO> conversations;
    public DialogueSO currentConversation;//当前进行的对话




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim =GetComponentInChildren<Animator>();
    }


    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;//停止物理运动
        anim.Play("Idle");
        interactAnim.Play("Open");
    }



    private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.isKinematic=false;//恢复物理运动
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if(GameManager.Instance.DialogueManager.isDialogueActive)
            {
                GameManager.Instance.DialogueManager.AdvanceDialogue();
            }
            else
            {
                if (GameManager.Instance.DialogueManager.CanStartDialogue())
                {
                    CheckForNewConversation();
                    GameManager.Instance.DialogueManager.StartDialogue(currentConversation);
                }

            }
        }
    }
    private void CheckForNewConversation()
    {
        for(int i = 0; i < conversations.Count; i++){
            var convo = conversations[i];
            if(convo != null && convo.IsConditionMet())
            {
                currentConversation = convo;

                if (convo.removeAfterPlay)
                {
                    conversations.RemoveAt(i);
                }
                if (convo.removeTheseOnPlay != null && convo.removeTheseOnPlay.Count > 0)
                {
                    foreach (var toRemove in convo.removeTheseOnPlay)
                    {
                        conversations.Remove(toRemove);
                    }
                }
             
                break;
            } 
        }
    }
}
