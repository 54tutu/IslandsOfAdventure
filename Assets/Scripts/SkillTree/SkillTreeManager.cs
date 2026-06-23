using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{

    public SkillSlot[] skillSlots;//技能槽数组
    public TMP_Text pointsText;//技能点文本UI组件
    public int availablePoints;//可用技能点


    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;//订阅技能点消耗事件，当技能点被消耗时调用HandleAbilityPointsSpent方法
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;//订阅技能满级事件，当技能达到满级时调用HandleSkillMaxed方法
        ExpManager.OnLevelUp += UpdateAbilityPoints;//订阅角色升级事件，当角色升级时调用UpdateAbilityPoints方法，增加可用技能点
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;//取消订阅技能点消耗事件，避免内存泄漏
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;//取消订阅技能满级事件
        ExpManager.OnLevelUp -= UpdateAbilityPoints;
    }
    private void Start()
    {
       foreach(SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(()=>CheckAvailablePoints(slot));//为每个技能槽的按钮添加点击事件监听器，当按钮被点击时调用CheckAvailablePoints方法，传入对应的技能槽作为参数
        }
       UpdateAbilityPoints(0);//初始化技能点显示
    }

    private void CheckAvailablePoints(SkillSlot slot)
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill();//如果有可用技能点，尝试升级技能
        }
    }

    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if (availablePoints > 0)
        {
            UpdateAbilityPoints(-1);//扣除一个技能点
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach(SkillSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlocked();
            }
           
        }
    }//当技能满级时，遍历所有技能槽，如果有未解锁但满足解锁条件的技能槽，则解锁它们

    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;//更新可用技能点数量
        pointsText.text = "Points: " + availablePoints;//更新技能点文本显示
    }

}
