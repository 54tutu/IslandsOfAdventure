using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemy_Movement;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime, stunTime));//开始眩晕计时器
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.velocity = direction * knockbackForce;//将敌人击退

    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);//等待knockbackTime秒后继续执行下面的代码
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);//等待stunTime秒后继续执行下面的代码
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
