using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景切换器，挂载在场景切换点上，当玩家进入触发器时加载指定的场景

public class SceneChanger : MonoBehaviour
{

    public string sceneToLoad;//要加载的场景名称
    public Animator fadeAnim;
    public float fadeTime = .5f;//淡入淡出动画的持续时间
    public Vector2 newPlayerPosition;
    private Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            player = collision.transform;
            fadeAnim.Play("FadeToWhite");//播放动画
           StartCoroutine(DelayFade());//等待动画播放完毕后加载场景
        }
    }

    IEnumerator DelayFade()//协程，等待动画播放完毕后加载场景
    {
        yield return new WaitForSeconds(fadeTime);//等待动画播放完毕
        player.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);//加载指定的场景
    }

}
