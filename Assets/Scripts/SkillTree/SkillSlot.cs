using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;//前置技能槽列表，解锁当前技能需要满足这些技能槽的条件

    public SkillSO skillSO;//技能数据对象

    public int currentLevel;//当前技能等级
    public bool isUnlocked;//技能是否解锁

    public Image skillIcon;//技能图标UI组件
    public Button skillButton;//技能按钮UI组件
    public TMP_Text skillLevelText;//技能等级文本UI组件

    public static event Action<SkillSlot> OnAbilityPointSpent;//当技能点被使用时触发的事件，传递当前技能槽作为参数
    public static event Action<SkillSlot> OnSkillMaxed;//当技能达到最大等级时触发的事件，传递当前技能槽作为参数

    private void OnValidate()
    {
        //确保技能数据对象和技能等级文本组件不为null
        if (skillSO != null&&skillLevelText!=null)
        {
            UpdateUI();
        }
    }
    //这个方法在编辑器中被调用，当我们修改skillSO字段时，
    //OnValidate会自动更新技能图标UI组件的图片为新的技能图标。这使得我们在编辑器中更方便地设置和预览技能槽的外观。

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;//如果技能已解锁且当前等级小于最大等级，升级技能
            OnAbilityPointSpent?.Invoke(this);//触发技能点使用事件，传递当前技能槽作为参数
            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);//触发技能达到最大等级事件，传递当前技能槽作为参数
            }
            UpdateUI();//升级后更新UI显示
        }
    }

    public bool CanUnlockSkill()
    {
        foreach(SkillSlot slot in prerequisiteSkillSlots)
        {
            if(!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;//如果有任何一个前置技能槽未解锁或等级未满，返回false
            }
        }
        return true;//如果所有前置技能槽都满足条件，返回true
    }

    public void Unlocked()
    {
        isUnlocked = true;//解锁技能
        UpdateUI();
    }

    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;//如果技能解锁，按钮可交互
            skillLevelText.text=currentLevel.ToString()+"/"+skillSO.maxLevel.ToString();//显示当前等级和最大等级
            skillIcon.color = Color.white;//解锁状态下图标显示白色
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";//如果未解锁，状态显示"Locked"
            skillIcon.color = Color.gray;//未解锁状态下图标显示灰色
        }
    }
}
