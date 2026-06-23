using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;//敌人的移动速度
    public float attackRange = 2;//敌人攻击范围的半径，当玩家进入这个范围时，敌人会开始攻击
    public float attackCooldown = 1;//敌人攻击的冷却时间，单位为秒
    public float playerDetectRange = 5;//敌人检测玩家的范围，当玩家进入这个范围时，敌人会开始追逐玩家
    public Transform detectionPoint;//敌人检测点的位置，用于检测玩家是否在检测范围内
    public LayerMask playerLayer;//一个LayerMask，用于指定玩家所在的层，以便在检测时判断玩家是否在检测范围内

    private float attackCooldownTimer;//一个计时器变量，用于跟踪敌人攻击的冷却时间，初始值为0，当敌人攻击后将其设置为attackCooldown，并在Update方法中逐渐减少，直到再次可以攻击
    private int facingDirection = -1;//敌人面朝的方向，1表示向右，-1表示向左
    private EnemyState enemyState;//一个枚举变量，表示敌人的当前状态，可以是Idle（闲置）或Chasing（追逐）
    
    private Rigidbody2D rb;
    private Transform player;//敌人的Transform组件，用于获取玩家的位置
    private Animator anim;//动画控制器变量

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();//在Start方法中获取敌人的Rigidbody2D组件和Animator组件，以便在Update方法中控制敌人的移动和动画
        ChangeState(EnemyState.Idle);//在Start方法中将敌人的状态设置为Idle（闲置），表示敌人初始时不追逐玩家
    }
    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;//如果attackCooldownTimer大于0，则将其减少Time.deltaTime（每帧的时间），直到它变为0
            }
            if (enemyState == EnemyState.Chasing)
            {
                Chase();//如果敌人的状态是Chasing（追逐），则调用Chase函数来控制敌人朝向玩家移动
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;//如果敌人的状态是Attacking（攻击），则将敌人的速度设置为零，停止移动
            }
        }
    }

    void Chase()
    {
        if(Vector2.Distance(transform.position,player.transform.position)<=attackRange&& attackCooldownTimer<=0)
        {
            attackCooldownTimer = attackCooldown;//重置冷却时间 
            ChangeState(EnemyState.Attacking);//如果敌人和玩家之间的距离小于或等于攻击范围，并且攻击冷却时间已经结束，则将敌人的状态设置为Attacking（攻击）
        }
        else if (player.position.x > transform.position.x && facingDirection == -1 || player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();//如果玩家在敌人右侧但敌人面朝左，或者玩家在敌人左侧但敌人面朝右，则调用Flip函数来翻转敌人的面朝方向
        }
        Vector2 direction = (player.position - transform.position).normalized;//计算敌人朝向玩家的方向，并将其归一化为单位向量
        rb.velocity = direction * speed;//设置敌人的速度，使其朝向玩家移动，乘以speed来调整移动速度
    }
    void Flip()
    {
        facingDirection *= -1;//翻转面朝方向
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);//通过改变敌人的局部缩放来实现翻转
    }
    private void CheckForPlayer()
    {
        //使用Physics2D.OverlapCircleAll函数在敌人检测点周围创建一个圆形区域，并检查是否有玩家在这个区域内。
        //这个函数返回一个Collider2D数组，包含所有与圆形区域重叠的碰撞体。
        //如果数组长度大于0，说明有玩家在检测范围内，将玩家的Transform组件赋值给player变量，并将敌人的状态设置为Chasing（追逐）
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position,playerDetectRange,playerLayer);
        if(hits.Length > 0)
        {
            player = hits[0].transform;
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown; 
                ChangeState(EnemyState.Attacking);
            }
            else if(Vector2.Distance(transform.position, player.position) > attackRange&& enemyState!=EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);//如果敌人和玩家之间的距离大于攻击范围，则将敌人的状态设置为Chasing（追逐）
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);//如果没有玩家在检测范围内
        }
    }
   
    public void ChangeState(EnemyState newState)
    {
        //根据当前状态设置相应的动画参数
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);//如果当前状态是Idle（闲置），则将动画参数isIdle设置为false，表示敌人不再处于闲置状态
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);//如果当前状态是Chasing（追逐），则将动画参数isChasing设置为false，表示敌人不再处于追逐状态
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);//如果当前状态是Attacking（攻击），则将动画参数isAttacking设置为false，表示敌人不再处于攻击状态
        }

        enemyState = newState;//将敌人的状态更新为新的状态

        //根据新的状态设置相应的动画参数
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);//如果新的状态是Idle（闲置），则将动画参数isIdle设置为true，表示敌人处于闲置状态
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);//如果新的状态是Chasing（追逐），则将动画参数isChasing设置为true，表示敌人处于追逐状态
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);//如果当前状态是Attacking（攻击），则将动画参数isAttacking设置为true，表示敌人处于攻击状态
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position,playerDetectRange);//在Scene视图中绘制一个红色的线框圆，表示敌人的检测范围
    }
}

public enum EnemyState
{
       Idle,
    Chasing,
    Attacking,
    Knockback,
}//定义一个枚举类型EnemyState，包含Idle（闲置）、Chasing（追逐）和Attacking（攻击）三个状态，用于表示敌人的当前状态