using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    //这个脚本负责处理物品的使用效果，应用到玩家的属性上
    public void ApplyItemEffects(ItemSO itemSO)
    {
        
        if (itemSO.currentHealth > 0)
        {
            StatsManager.Instance.UpdateHealth(itemSO.currentHealth);
        }
        if (itemSO.maxHealth > 0)
        {
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth);
        }
        if(itemSO.speed > 0)
        {
            StatsManager.Instance.UpdateSpeed(itemSO.speed);
        }
        if (itemSO.duration > 0)
        {
            StartCoroutine(EffectTimer(itemSO,itemSO.duration));
        }
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (itemSO.currentHealth > 0)
        {
            StatsManager.Instance.UpdateHealth(-itemSO.currentHealth);
        }
        if (itemSO.maxHealth > 0)
        {
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth);
        }
        if (itemSO.speed > 0)
        {
            StatsManager.Instance.UpdateSpeed(-itemSO.speed);
        }
    }//这个协程负责在持续时间结束后撤销物品的效果
}
