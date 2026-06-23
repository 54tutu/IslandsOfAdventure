using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//这个脚本负责更新玩家属性界面上的数值显示

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;//属性界面上每个属性的显示位置，按照顺序分别是伤害、速度等
    public CanvasGroup statsCanvas;//这个脚本负责更新玩家属性界面上的数值显示


    private bool statsOpen=false;//属性界面是否打开的标志

    private void Start()
    {
      UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;//恢复游戏
                UpdateAllStats();//每次关闭属性界面时更新属性界面上的数值显示，确保下次打开时显示的是最新的数值
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;//关闭属性界面上的交互
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;//暂停游戏
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }

        }//按下切换属性界面的按钮时，切换属性界面的显示状态
    }
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;//更新伤害显示
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;//更新速度显示
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed(); 
    }
}
