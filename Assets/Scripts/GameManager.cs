using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;//单例实例

    public DialogueManager DialogueManager;
    public DialogueHistoryTracker DialogueHistoryTracker;
    public LocationHistoryTracher LocationHistoryTracher;

    [Header("Persitent Objects")]
    public GameObject[] persistentObjects;//需要在场景切换时保持不销毁的对象数组



    private void Awake()
    {
        if (Instance != null)
        {
            CleanUpAndDestroy();
            return;
        }
        else
        {
            Instance = this;//设置单例实例为当前对象
            DontDestroyOnLoad(gameObject);//在加载新场景时不销毁当前对象
            MarkPersistentObjects();
        }
        
    }


    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
           if(obj != null)
            {
                DontDestroyOnLoad(obj);//标记对象在加载新场景时不销毁
            }
        }   
    }//标记需要在场景切换时保持不销毁的对象

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);//销毁持久化对象
        }
        Destroy(gameObject);//销毁当前对象
    }//当有新的GameManager实例被创建时，销毁旧的实例和持久化对象，确保只有一个GameManager实例存在
}
