using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{

    public CanvasGroup infoPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("属性文本")]
    public TMP_Text[] statTexts;//显示属性的文本数组

    private RectTransform infoPanelRect;//信息面板的RectTransform组件

    private void Awake()
    {
        infoPanelRect=GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;

        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();

        if (itemSO.currentHealth > 0)
            stats.Add("Health: " + itemSO.currentHealth.ToString());
        if (itemSO.damage > 0)
            stats.Add("Damage: " + itemSO.damage.ToString());
        if (itemSO.speed > 0)
            stats.Add("Speed: " + itemSO.speed.ToString());
        if (itemSO.duration > 0)
            stats.Add("Duration: " + itemSO.duration.ToString());

        if(stats.Count <= 0)
        {
            return;
        }
        for(int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }

    }//显示物品信息的方法，接受一个ItemSO对象作为参数，并将其属性显示在UI上

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }//隐藏物品信息的方法，将信息面板的透明度设置为0，并清空文本内容

    public void FollowMouse()//使信息面板跟随鼠标位置的方法
    {
        Vector3 mousePosition = Input.mousePosition;//获取鼠标位置
        Vector3 offset = new Vector3(10, -10, 0);
        infoPanelRect.position= mousePosition + offset;//将信息面板的位置设置为鼠标位置加上一个偏移量，使其不会遮挡鼠标
    }

}
