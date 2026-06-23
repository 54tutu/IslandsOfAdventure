using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
   
    public LayerMask enemyLayer;
    public StatsUI statsUI;//这个脚本负责更新玩家属性界面上的数值显示


    public Animator anim;
    public float cooldown = 2;


    private float timer;



    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            timer = cooldown;
        }

    }

    public void DealDamage()
    {
        StatsManager.Instance.damage += 1;
        statsUI.UpdateDamage();//每次攻击时增加伤害并更新属性界面显示
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,StatsManager.Instance.weaponRange, enemyLayer);
        Debug.Log(attackPoint.position + "     " + StatsManager.Instance.weaponRange);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StatsManager.Instance.damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform,StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);//当攻击动画结束时调用这个函数
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);//在编辑器中可视化攻击范围
    }

}
