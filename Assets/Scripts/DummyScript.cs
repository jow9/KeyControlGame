using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * プレイヤーアクションの分身させる
 * プレイヤーの分身体は時間経過で消滅する
 */
public class DummyScript : MonoBehaviour {

    GameObject player;
    PlayerStatusScript player_statu;
    public int time_limit;
    float timer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        player_statu = player.GetComponent<PlayerStatusScript>();
        player_statu.AddDummyNow(1);
        timer = 0;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (timer > time_limit)
        {
            player_statu.AddDummyNow(-1);
            Destroy(this.gameObject);
        }
	}
}
