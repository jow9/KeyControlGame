using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 指定時間でオブジェクトを消滅させる
 */
public class DestroyCountdown : MonoBehaviour {

    public float destroy_count;

	// Use this for initialization
	void Start () {
        if (destroy_count == 0) Debug.Log("Error : destroy_count is Null");
	}
	
	// Update is called once per frame
	void Update () {
		destroy_count -= Time.deltaTime;
        if (destroy_count < 0) Destroy(this.gameObject);
    }
}
