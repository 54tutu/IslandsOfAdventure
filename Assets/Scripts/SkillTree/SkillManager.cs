using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_Combat combat;//玩家战斗组件引用，用于修改玩家的战斗属性

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;//订阅技能点消耗事件，当技能点被消耗时调用HandleAbilityPointsSpent方法
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;//取消订阅技能点消耗事件，避免内存泄漏
    }

    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        string skillName = skillSlot.skillSO.skillName;//获取被升级的技能名称

        switch (skillName)
        {
            case "Max Health Boost":
                StatsManager.Instance.UpdateMaxHealth(1);//调用StatsManager的UpdateMaxHealth方法，增加最大生命值
                break;

                case "Sword Slash":
                    combat.enabled = true;//启用玩家的战斗组件，允许玩家使用剑击技能
                break;

            default:
                Debug.LogWarning("未知技能被升级: " + skillName);//如果技能名称不匹配任何已知技能，输出警告信息
                break;
        }
    }
}
