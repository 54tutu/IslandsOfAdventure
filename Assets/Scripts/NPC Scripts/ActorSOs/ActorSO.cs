using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ActorSO", menuName = "Dialogue/NPC")]//创建一个新的菜单项来创建ActorSO对象
public class ActorSO : ScriptableObject//角色数据类
{

    public string actorName;
    public Sprite portrait;//角色头像

}
