using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventorySlot[] itemSlots;//物品槽数组
    public UseItem useItem;
    public int gold;//金币数量
    public TMP_Text goldText;//显示金币数量的文本组件
    public GameObject lootPrefab;//丢弃物品的预制体
    public Transform player;//玩家位置，用于丢弃物品时设置物品的初始位置


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach(var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }

    private void OnEnable()//当脚本启用时订阅物品被拾取事件
    {
        Loot.OnItemLooted += AddItem;
    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }


    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;//增加金币数量
            goldText.text = gold.ToString();//更新金币数量显示
            return;
        }
      
        foreach(var slot in itemSlots)//首先尝试将物品添加到已有的相同物品槽中
        {
            if (slot.itemSO==itemSO&&slot.quantity<itemSO.stackSize)
            {
                int availableSpace = itemSO.stackSize - slot.quantity;//计算当前物品槽中剩余的空间
                int amountToAdd = Mathf.Min(quantity, availableSpace);//计算实际要添加的数量，不能超过剩余空间

                slot.quantity += amountToAdd;//增加物品数量
                quantity -= amountToAdd;//减少剩余数量
                slot.UpdateUI();//更新物品槽显示
                if(quantity <= 0)
                {
                    return;//如果所有物品都添加完了，退出方法
                }
            }
        }

            foreach (var slot in itemSlots)//如果还有剩余数量，尝试将物品添加到空的物品槽中
            {
                if (slot.itemSO == null)
                {
                int amountToAdd = Mathf.Min(itemSO.stackSize, quantity);//计算实际要添加的数量，不能超过物品的最大堆叠数量
                slot.itemSO = itemSO;//在这里设置物品槽的物品信息
                    slot.quantity = quantity;//在这里设置物品槽的数量
                    slot.UpdateUI();//在这里设置更新所有物品槽的逻辑
                    return;
                }

            }

        if (quantity>0)
        {
          DropLoot(itemSO, quantity);
        }
        
    }


    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itemSO, 1);
        slot.quantity--;
        if(slot.quantity <= 0)
        {
            slot.itemSO = null;
        }
        slot.UpdateUI() ;
    }
    


    private void DropLoot(ItemSO itemSO, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, quantity);
    }

    public void ApplyUseItem(InventorySlot slot)
    {
        if(slot.itemSO != null && slot.quantity >= 0)
        {
            useItem.ApplyItemEffects(slot.itemSO);
            slot.quantity--;//使用物品后数量减少
            if(slot.quantity <= 0)
            {
                slot.itemSO = null;//如果数量小于等于0，清空物品槽
            }
            slot.UpdateUI();
        }
    }
    public bool HasItem(ItemSO itemSO)
    {
        foreach (var slot in itemSlots)
        {
            if(slot.itemSO==itemSO&&slot.quantity>0)
                return true;
        }
        return false;
    }

    public int GetItemQuantity(ItemSO itemSO)
    {
        int total= 0;
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO = itemSO)
            {
                total += slot.quantity;
            }
        }
        return total;
    }
}
