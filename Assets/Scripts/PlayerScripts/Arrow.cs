using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;//箭的飞行方向，默认为向右
    public float lifeSpawn = 2;//箭存在的时间，单位为秒
    public float speed;

    public LayerMask enemyLayer;//敌人所在的层，用于检测箭与敌人的碰撞
    public LayerMask obstacleLayer;//障碍物所在的层，用于检测箭与障碍物的碰撞
    public SpriteRenderer sr;//箭的SpriteRenderer组件，用于控制箭的外观
    public Sprite buriedSprite;//箭被障碍物覆盖时显示的Sprite

    public int damage;

    public float knockbackForce;//击退力量
    public float knockbackTimer;//击退持续时间
    public float stunTimer;//眩晕持续时间

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity=direction*speed;//设置箭的速度，使其沿着指定的方向飞行
        RotateArrow();//旋转箭的朝向，使其与飞行方向一致
        Destroy(gameObject, lifeSpawn);//在lifeSpawn秒后销毁箭对象，防止箭无限存在
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//计算箭的旋转角度，使其与飞行方向一致
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));//设置箭的旋转，使其朝向飞行方向
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyLayer.value&(1<<collision.gameObject.layer)) > 0)//检查碰撞的对象是否在敌人所在的层
        {
           collision.gameObject.GetComponent<Enemy_Health>().ChangeHealth(-damage);//如果碰撞对象是敌人，调用敌人的ChangeHealth方法，减少敌人的生命值
            collision.gameObject.GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTimer, stunTimer);//调用敌人的Knockback方法，施加击退效果
            AttachToTarget(collision.gameObject.transform);//调用AttachToTarget方法，使箭附着在敌人上
        }
        else if((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)//检查碰撞的对象是否在障碍物所在的层
        {
            AttachToTarget(collision.gameObject.transform);//如果碰撞对象是障碍物，调用AttachToTarget方法，使箭附着在障碍物上
        }
    }

    private void AttachToTarget(Transform target)
    {
      sr.sprite = buriedSprite;//将箭的Sprite更改为buriedSprite，表示箭被障碍物覆盖
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;//将箭的Rigidbody2D设置为Kinematic，使其不受物理影响，保持在障碍物上

        transform.SetParent(target);//将箭的父对象设置为碰撞的障碍物，使其随障碍物移动
    }
}
