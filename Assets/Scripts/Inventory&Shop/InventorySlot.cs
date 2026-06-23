using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;//用于处理点击事件
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour,IPointerClickHandler//实现IPointerClickHandler接口以处理点击事件
{

    public ItemSO itemSO;//物品数据
    public int quantity;//物品数量

    public Image itemImage;//物品图像组件
    public TMP_Text quantityText;

    private InventoryManager inventoryManager;
    private ShopManager activeShop;

    private void Start()
    {
        inventoryManager=GetComponentInParent<InventoryManager>();//获取父对象上的InventoryManager组件
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }

    private void HandleShopStateChanged(ShopManager shopManager,bool isOpen)
    {
        activeShop=isOpen?shopManager:null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {

                if (activeShop != null)
                {
                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }
                else
                {
                    if (itemSO.currentHealth > 0 && StatsManager.Instance.currentHealth >= StatsManager.Instance.maxHealth)
                    {
                        return;//如果当前生命值已经满了，就不使用物品
                    }
                    inventoryManager.ApplyUseItem(this);//使用物品
                }
                
            }//可以在这里添加左键点击的逻辑，例如显示物品详情等
            else if (eventData.button == PointerEventData.InputButton.Right)//右键丢弃
            {
                inventoryManager.DropItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if (quantity<=0)
        {
            itemSO = null;
        }
        if (itemSO != null)
        {
            itemImage.sprite = itemSO.icon;//设置物品图像
            itemImage.gameObject.SetActive(true);//显示物品图像
            quantityText.text=quantity.ToString();//设置数量文本
        }
        else
        {
            itemImage.gameObject.SetActive(false);//隐藏物品图像
            quantityText.text = "";//清空数量文本
        }
    }

}
