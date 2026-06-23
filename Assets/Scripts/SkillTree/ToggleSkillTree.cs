using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{

    public CanvasGroup statsCanvas;//技能树UI的CanvasGroup组件引用，用于控制UI的显示和隐藏
    private bool skillTreeOpen = false;//技能树当前的显示状态，初始为隐藏

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;//恢复游戏
                statsCanvas.alpha = 0;//隐藏技能树UI
               statsCanvas.blocksRaycasts= false;//关闭技能树UI的交互，防止玩家在技能树关闭时仍然能够点击UI元素
                skillTreeOpen = false;//更新技能树的显示状态为关闭
            }
            else
            {
                Time.timeScale = 0;//暂停游戏
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }

        }
    }
}
