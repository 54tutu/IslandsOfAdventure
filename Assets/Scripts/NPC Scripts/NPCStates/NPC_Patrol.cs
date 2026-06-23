using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{

    public Vector2[] patrolPoints;//巡逻点数组
    public Vector2 target;//当前目标点
    public float speed=2;//巡逻速度
    public float pauseDuration=1.5f;//在每个巡逻点停留的时间


    private Rigidbody2D rb;
    private int currentPatrolIndex;//当前巡逻点索引
    private bool isPaused;//是否正在停留

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(SetPatrolPoint());
    }

    void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector3)target - transform.position).normalized;//计算朝向目标点的方向，并归一化

        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, target) <.1f )
        {
            StartCoroutine(SetPatrolPoint());
        }
    }
    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pauseDuration);//等待停留时间
        currentPatrolIndex= (currentPatrolIndex + 1) % patrolPoints.Length;//更新巡逻点索引，使用模运算确保索引在数组范围内循环
        target= patrolPoints[currentPatrolIndex];//将下一个巡逻点设置为新的目标点
        isPaused = false;
        anim.Play("Walk");
    }
}
