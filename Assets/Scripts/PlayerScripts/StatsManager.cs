using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;//引用StatsUI组件以更新UI显示
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public int speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//如果没有StatsManager实例存在，就将当前对象设为实例
        }
        else
        {
            Destroy(gameObject);//确保只有一个StatsManager实例存在
        }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text= "HP: " + currentHealth + "/" + maxHealth;//更新UI显示当前生命值和最大生命值
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth>=maxHealth)
        {
            currentHealth = maxHealth;//确保当前生命值不会超过最大生命值
        }
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateAllStats();
    }
}
