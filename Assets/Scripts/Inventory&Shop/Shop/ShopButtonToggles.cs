using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonToggles : MonoBehaviour
{
    
    public void OpenItemShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenItemShop();
        }
    }//打开物品商店
    
    public void OpenWeaponShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenWeaponShop();
        }
    }

    public void OpenArmorShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenArmorShop();
        }
    }



}
