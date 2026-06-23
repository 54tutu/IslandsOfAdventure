using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//注册场景加载事件，当场景加载完成后调用OnSceneLoaded方法
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;//取消注册场景加载事件
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();//获取CinemachineConfiner2D组件
        confiner.m_BoundingShape2D = GameObject.FindWithTag("Confiner").GetComponent<PolygonCollider2D>();
        //将CinemachineConfiner2D组件的m_BoundingShape2D属性设置为场景中标签为"Confiner"的游戏对象的PolygonCollider2D组件
    }//当场景加载完成后，找到标签为"Confiner"的游戏对象，并将其PolygonCollider2D组件设置为CinemachineConfiner2D组件的m_BoundingShape2D属性，以实现相机的边界限制
}
