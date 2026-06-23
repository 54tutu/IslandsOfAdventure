using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 3;//击败敌人后给予玩家的经验值奖励
    public delegate void MonsterDefeated(int exp);//定义一个委托，用于在敌人被击败时传递经验值奖励
    public static event MonsterDefeated OnMonsterDefeated;//定义一个事件，当敌人被击败时触发

   public int currentHealth;//当前生命值
    public int maxHealth;//最大生命值

    private void Start()
    {
        currentHealth = maxHealth;//在游戏开始时，将当前生命值设置为最大生命值
    }
    public void ChangeHealth(int amount)
    {
               currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;//如果当前生命值超过最大生命值，将当前生命值设置为最大生命值
        }
        else if (currentHealth<=0)
        {
            OnMonsterDefeated(expReward);//触发敌人被击败事件，并传递经验值奖励
            Destroy(gameObject);
        }
    }
}
