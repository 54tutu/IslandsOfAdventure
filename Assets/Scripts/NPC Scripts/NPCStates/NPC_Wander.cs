using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Wander : MonoBehaviour
{

    [Header("Wander Area")]
    public float wanderWidth = 5;
    public float wanderHeight = 5;
    public Vector2 startingPosition;//起始位置

    public float speed = 2;
    public float pauseDuration = 1;
    public Vector2 target;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isPause;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNewDestination());
    }

    private void Update()
    {
        if (isPause)
        {
            rb.velocity=Vector2.zero;
            return;
        }
        if (Vector2.Distance(transform.position, target) < .1f)
        {
            StartCoroutine(PauseAndPickNewDestination());
        }
        Move();
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.velocity = direction * speed;
    }


    IEnumerator PauseAndPickNewDestination()
    {
        isPause = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pauseDuration);
        target = GetRandomTarget();
        isPause = false;
        anim.Play("Walk");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!enabled) return;
        StartCoroutine(PauseAndPickNewDestination());
        
    }


    private Vector2 GetRandomTarget()//生成随机目标点
    {
        float halfWidth = wanderWidth/2;
        float halfHeight = wanderHeight/2;//计算边界
        int edge = Random.Range(0, 4);//随机选择边界
        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),//左边界
            1 => new Vector2(startingPosition.x + halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),//右边界
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y - halfHeight),//下边界
            _ => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y + halfHeight),//上边界
        };//返回随机目标点
    }//使用switch表达式根据随机边界生成目标点

    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireCube(startingPosition,new Vector3(wanderWidth, wanderHeight, 0));
    }

}
