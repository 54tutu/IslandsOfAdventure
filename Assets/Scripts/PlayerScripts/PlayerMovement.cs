using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int facingDirection = 1;//玩家面朝的方向，1表示向右，-1表示向左
    public Rigidbody2D rb;//玩家的刚体组件
    public Animator anim;//动画控制器变量
    public Player_Combat player_Combat;//玩家的攻击组件变量


    private bool isKnockback;//一个布尔变量，用于跟踪玩家是否正在被击退
    public bool isShooting;//一个布尔变量，用于跟踪玩家是否正在射击

    private void Update()
    {
        if (Input.GetButtonDown("Slash")&& player_Combat.enabled==true)
        {
             player_Combat.Attack();//如果玩家按下攻击按钮（Slash），则调用Player_combat组件中的Attack函数来执行攻击动作
        }
    }


    // FixedUpdate is called 50x frame
    void FixedUpdate()
    {
        if(isShooting == true)
        {
            rb.velocity = Vector2.zero;//如果玩家正在射击，将玩家的速度设置为零，确保玩家在射击时不会移动
        }
        else if (isKnockback == false)
        {
            float horizontal = Input.GetAxis("Horizontal");//获取水平输入
            float vertical = Input.GetAxis("Vertical");//获取垂直输入
                                                       //根据输入调整玩家的面朝方向
            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }//如果玩家正在向右移动但面朝左，或者正在向左移动但面朝右，则调用Flip函数来翻转玩家的面朝方向

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));//设置动画参数，使用绝对值确保动画在任何方向上都能正确播放
            anim.SetFloat("vertical", Mathf.Abs(vertical));//设置动画参数，使用绝对值确保动画在任何方向上都能正确播放
            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;//根据输入设置玩家的速度，乘以speed来调整移动速度
        }
    }

    void Flip()
    {
        facingDirection *= -1;//翻转面朝方向
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);//通过改变玩家的局部缩放来实现翻转
    }

    public void Knockback(Transform enemy,float force,float stunTime) {
    isKnockback = true;//设置isKnockback为true，表示玩家正在被击退
        Vector2 direction=(transform.position-enemy.position).normalized;//计算玩家和敌人之间的方向向量
        rb.velocity = direction*force;
        StartCoroutine(KnockbackCounter(stunTime));//调用协程来处理击退的持续时间
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);//等待stunTime秒后继续执行下面的代码
        rb.velocity = Vector2.zero;
        isKnockback = false;//将玩家的速度设置为零，并将isKnockback设置为false，表示玩家不再被击退
    }
}
