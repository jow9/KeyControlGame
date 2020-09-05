using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * プレイヤーの攻撃：ファイアーショットオブジェクトを管理
 */
public class ArrowScript : MonoBehaviour {

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Goal")
        {
            if (col.gameObject.tag == "Enemy") {
                Destroy(col.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
