using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *カメラオブジェクトの位置を管理
 */
public class MainCameraScript : MonoBehaviour {

    GameObject ChaseObj;

    // Update is called once per frame
    void Update()
    {
        if (ChaseObj != null)
        {
            this.transform.position = ChaseObj.transform.position + new Vector3(0, 9, 3.5f);
        }
    }

    public void SetPlayer(GameObject temp_player)
    {
        ChaseObj = temp_player;
    }
}
