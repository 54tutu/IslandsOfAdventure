using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;//一个Collider2D数组，用于存储所有山的碰撞器
    public Collider2D[] boundaryColliders;//一个Collider2D数组，用于存储所有边界的碰撞器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;//当玩家进入触发器时，启用所有山的碰撞器，使玩家无法穿过山
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = false;//当玩家进入触发器时，禁用所有边界的碰撞器，确保玩家在离开山后不再受到边界的限制
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;//将玩家的SpriteRenderer组件的sortingOrder设置为10，使玩家在山的下方显示
        }//如果碰撞的对象是玩家，遍历所有山的碰撞器并启用它们，同时将玩家的SpriteRenderer组件的sortingOrder设置为10，使玩家在山的下方显示
    }
}
