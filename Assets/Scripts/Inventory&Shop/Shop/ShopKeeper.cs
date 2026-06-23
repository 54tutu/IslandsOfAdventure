using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    public static ShopKeeper currentShopKeeper;//当前交互的商店老板实例

    public Animator anim;
    public CanvasGroup shopCanvasGroup;//商店UI的CanvasGroup组件
    public ShopManager shopManager;
    [SerializeField] private List<ShopItems> shopItems;//商店物品列表
    [SerializeField] private List<ShopItems> shopWeapons;//商店武器列表
    [SerializeField] private List<ShopItems> shopArmor;//商店护甲列表
    public static event Action<ShopManager, bool> OnShopStateChanged;//商店状态改变事件，参数为商店管理器和商店是否打开的布尔值

    [SerializeField] private Camera shopkeeperCam;//商店老板专用相机
    [SerializeField] private Vector3 cameraOffset=new Vector3(0,0,-1);//相机偏移量

    private bool playerInRange;//玩家是否在范围内
    private bool isShopOpen;//商店是否打开


    void Update()
    {
        if (playerInRange)
        {
            if(Input.GetButtonDown("Interact"))
            {
                if(!isShopOpen)
                {
                    //Time.timeScale = 0;//暂停游戏
                    isShopOpen = true;
                    currentShopKeeper = this;//设置当前交互的商店老板实例为当前对象
                    OnShopStateChanged?.Invoke(shopManager, true);//触发商店状态改变事件，传递商店管理器和商店打开的状态
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;//允许UI接收点击事件
                    shopCanvasGroup.interactable = true;

                    shopkeeperCam.transform.position=transform.position+cameraOffset;//将相机位置设置为商店老板位置加上偏移量
                    shopkeeperCam.gameObject.SetActive(true);//激活商店老板专用相机

                    OpenItemShop();//默认打开物品商店
                }
                else
                {
                    //Time.timeScale = 1;//恢复游戏
                    currentShopKeeper = null;//重置当前交互的商店老板实例
                    isShopOpen = false;
                    OnShopStateChanged?.Invoke(shopManager, false);
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;//允许UI接收点击事件
                    shopCanvasGroup.interactable = false;
                    shopkeeperCam.gameObject.SetActive(false);//关闭商店老板专用相机
                }
             
            }
        }
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);//填充商店物品
    }

    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopWeapons);//填充商店武器
    }

    public void OpenArmorShop()
    {
        shopManager.PopulateShopItems(shopArmor);//填充商店护甲
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}
