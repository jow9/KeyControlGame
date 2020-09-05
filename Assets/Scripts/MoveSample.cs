using UnityEngine;
using System.Collections;


/*
 *　実験用コード
 *　ゲームには利用しない
 */
public class MoveSample : MonoBehaviour
{	
	void Update(){
        //iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            iTween.MoveBy(gameObject, iTween.Hash("x", 2, "time", 0.6f, "easetype", "linear"));
        }
    }
}

