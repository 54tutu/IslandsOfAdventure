using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//引入TextMeshPro命名空间，用于显示玩家的生命值

public class PlayerHealth : MonoBehaviour
{


    public TMP_Text healthText;//显示生命值的文本组件
    public Animator healthTextAnim;//文本更新动画组件

    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;//在游戏开始时，显示玩家的当前生命值和最大生命值
    }
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;//改变当前生命值，amount可以是正数（增加生命）或负数（减少生命）
        healthTextAnim.Play("TextUpdate");//播放文本更新动画
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
        if (StatsManager.Instance.currentHealth <= 0){
            gameObject.SetActive(false);
        }//如果当前生命值小于或等于0，让玩家对象消失
    }
    

    
}
