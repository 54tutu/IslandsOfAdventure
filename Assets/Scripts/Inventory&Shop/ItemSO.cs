using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;//物品名称
    [TextArea] public string itemDescription;//物品描述
    public Sprite icon;//物品图标

    public bool isGold;//是否是金币
    public int stackSize=3;//物品堆叠数量上限

    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;
    public int speed;//增加的速度
    public int damage;

    [Header("For Temporary Items")]
    public float duration;//持续时间，单位为秒

}
