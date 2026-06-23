using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;


    public bool canBePickedUp = true;

    public int quantity;//数量
    public static event Action<ItemSO, int> OnItemLooted;//当物品被拾取时触发的事件，传递物品数据和数量


    private void OnValidate()
    {
        if (itemSO == null)
        {
            return;
        }
        UpdateAppearance();

    }//当玩家进入触发器时


    public void Initialize(ItemSO itemSO,int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickedUp=false;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        sr.sprite = itemSO.icon;
        this.name = itemSO.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&canBePickedUp==true)
        {
            anim.Play("LootPickUp");//播放拾取动画
            OnItemLooted?.Invoke(itemSO, quantity);//触发物品被拾取事件，传递物品数据和数量
            Destroy(gameObject, 0.5f);//销毁物品对象
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickedUp=true;
        }
    }
}
