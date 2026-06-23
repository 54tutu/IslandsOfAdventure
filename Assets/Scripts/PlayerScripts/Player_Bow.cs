using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{

    public Transform launchPoint;//箭的发射点
    public GameObject arrowPrefab;//箭的预制体
    private Vector2 aimDirection= Vector2.right;//玩家瞄准的方向，默认为向右


    public float shootCooldown = .5f;//射击冷却时间，单位为秒
    private float shootTimer;//射击计时器，用于跟踪冷却时间

    public Animator anim;
    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        shootTimer-= Time.deltaTime;//更新射击计时器，减少时间
        HandleAiming();//处理玩家的瞄准输入
        if (Input.GetButtonDown("Shoot")&& shootTimer<=0)
        {
            playerMovement.isShooting = true;//当玩家按下射击按钮时，设置PlayerMovement组件中的isShooting变量为true，表示玩家正在射击
            anim.SetBool("isShooting", true);//当玩家按下射击按钮时，设置动画参数isShooting为true，触发射击动画

        }

    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);//启用玩家弓箭时，将动画层0的权重设置为0，确保弓箭动画不会干扰其他动画
        anim.SetLayerWeight(1, 1);//将动画层1的权重设置为1，启用弓箭动画
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);//禁用玩家弓箭时，将动画层0的权重设置为1，恢复其他动画的正常播放
        anim.SetLayerWeight(1, 0);//将动画层1的权重设置为0，禁用弓箭动画
    }

    private void HandleAiming()
    {
        float horizontal=Input.GetAxisRaw("Horizontal");//获取玩家水平输入
        float vertical=Input.GetAxisRaw("Vertical");//获取玩家垂直输入

        if(horizontal!= 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;//根据输入计算瞄准方向，并归一化
            anim.SetFloat("aimX", aimDirection.x);//设置动画参数aimX为瞄准方向的x分量
            anim.SetFloat("aimY", aimDirection.y);//设置动画参数aimY为瞄准方向的y分量
        }
    }

    private void shoot()
    {
        if (shootTimer <= 0)
        {
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();//实例化箭的预制体，并获取其Arrow实例
            arrow.direction = aimDirection;//设置箭的飞行方向为玩家当前的瞄准方向
            shootTimer = shootCooldown;//重置射击计时器，开始冷却
        }//防止调用过快，使其一下射出多支箭
       
        anim.SetBool("isShooting", false);//射击后，设置动画参数isShooting为false，结束射击动画
        playerMovement.isShooting = false;//设置PlayerMovement组件中的isShooting变量为false，表示玩家不再射击
    }
}
