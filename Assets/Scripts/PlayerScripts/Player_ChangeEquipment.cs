using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeEquipment : MonoBehaviour
{

    public Player_Combat combat;//玩家战斗脚本组件
    public Player_Bow bow;//玩家弓箭脚本组件

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("ChangeEquipment"))
        {
           
                bow.enabled = !bow.enabled;//启用玩家弓箭脚本
            
        }
    }
}
