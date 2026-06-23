using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引入UI命名空间以使用Slider组件
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;//当前经验值
    public int expToLevel = 10;//升级所需经验值
    public float expGrowthMultiplier = 1.2f;//经验值增长倍率
    public Slider expSlider;//经验值进度条
    public TMP_Text currentLevelText;//当前等级文本

    public static event Action<int> OnLevelUp;//当玩家升级时触发的事件，传递当前等级作为参数

    public void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }//测试用，按下回车键时增加经验值

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;//订阅敌人被击败事件，当敌人被击败时调用GainExperience方法
    }
    private void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;//取消订阅事件，避免内存泄漏
    }

    public void GainExperience(int amout)
    {
        currentExp += amout;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }
    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
        OnLevelUp?.Invoke(1);//触发升级事件

    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;//设置Slider的最大值为升级所需经验值
        expSlider.value= currentExp;//设置Slider的当前值为当前经验值
        currentLevelText.text = "Lv: " + level;//更新等级文本显示
    }
}
