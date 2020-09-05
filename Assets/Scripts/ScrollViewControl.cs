using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * ゲームUI（スクロールバー）の挙動をコントロール
 */
public class ScrollViewControl : MonoBehaviour {

    GameObject obj;
    Scrollbar scroll_bar;

    float time;

    public void Start() {
        obj = this.gameObject;
        scroll_bar = obj.GetComponent<Scrollbar>();
        time = 0;
    }

    void Update()
    {
        if(time > 0) time -= Time.deltaTime;
        if (time <= 0) scroll_bar.value = 0;//他の処理が遅延するので、0.1秒遅らせてから処理を実行する
    }

    public void SetScrollbar(){
        time = 0.05f;//処理の遅延を計算する。
    }
}
