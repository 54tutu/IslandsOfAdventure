using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;//敌人造成的伤害值
    public Transform attackPoint;//敌人攻击点的位置，用于检测玩家是否在攻击范围内
    public float weaponRange;//敌人攻击范围的半径，当玩家进入这个范围时，敌人会造成伤害
    public float KnockbackForce;//敌人造成的击退力，当玩家被攻击时会被击退一定距离
    public float stunTime;//敌人造成的眩晕时间，当玩家被攻击时会被眩晕一段时间，无法移动
    public LayerMask playerLayer;//一个LayerMask，用于指定玩家所在的层，以便在攻击时检测玩家是否在攻击范围内
  


    public void Attack()
    {
        Collider2D[] hits=Physics2D.OverlapCircleAll(attackPoint.position,weaponRange,playerLayer);//在攻击点周围的攻击范围内检测玩家，返回一个Collider2D数组，包含所有在攻击范围内的玩家
        if(hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);//如果检测到玩家在攻击范围内，调用玩家的ChangeHealth方法，减少玩家的生命值
            hits[0].GetComponent<PlayerMovement>().Knockback(transform,KnockbackForce,stunTime);//调用玩家的Knockback方法，将敌人作为参数传递，造成击退效果
        }
    }
}
