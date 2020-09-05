using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * 敵の一つ一つの動きを管理する
 */
public class EnemyScript : MonoBehaviour {

    GameObject obj;

    public GameObject PointLight;

    //public GameObject PointLight;

    enum STEP
    {
        NONE = 0,

        STOP = 1,
        MOVE = 2
    }

    STEP step;//現在のプレイヤーの状態を保存する
    float timer;//動作からの時間を計測
    float limit_timer;//動作から次の動作に移行するまでのクールタイム
    public float back_time = 0.1f;//もとの位置に戻るまでの時間
    public float input_time = 1.5f;
    public float walk_time = 0.5f;//移動アニメーションの時間

    public int search_range;

    Vector3 last_position;

    GameObject player;
    GameObject player_temp;
    PlayerStatusScript player_state;

    // Use this for initialization
    void Start () {
        obj = this.gameObject;
        timer = 0f;
        limit_timer = 0f;
        step = STEP.STOP;
        last_position = this.gameObject.transform.position;

        player = GameObject.FindGameObjectWithTag("Player");
        player_temp = GameObject.FindGameObjectWithTag("Player");
        player_state = player.GetComponent<PlayerStatusScript>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (step == STEP.MOVE && timer > limit_timer + input_time) step = STEP.STOP;

        //もしダミーがあれば計算対象をダミーに変換する
        if (GameObject.FindGameObjectWithTag("DummyPlayer") != null) {
            player = GameObject.FindGameObjectWithTag("DummyPlayer");
        }
        else
        {
            player = player_temp;
        }

        //対象との距離を計算する
        float x_distance_player_enemy = player.transform.position.x - obj.transform.position.x;
        float z_distance_player_enemy = player.transform.position.z - obj.transform.position.z;

        //対象との距離の絶対値を求める
        float x_Ads_distance = Math.Abs(x_distance_player_enemy);
        float z_Ads_distance = Math.Abs(z_distance_player_enemy);

        if (step == STEP.STOP)
        {
            last_position = this.gameObject.transform.position;

            if ((x_Ads_distance < search_range && z_Ads_distance < search_range) && player_state.GetPlayerAppearState())
            {
                int ran = UnityEngine.Random.Range(0, 2);
                //光の色を変える
                PointLight.GetComponent<Light>().color = new Color(1, 1, 0, 1);
                //Debug.Log("find");
                switch (ran) {
                    case 0:
                        if (x_distance_player_enemy > 0) RightWalk();
                        if (x_distance_player_enemy < 0) LeftWalk();
                        break;
                    case 1:
                        if (z_distance_player_enemy > 0) UpWalk();
                        if (z_distance_player_enemy < 0) DownWalk();
                        break;
                    default:
                        break;
                }
                
            }else {
                PointLight.GetComponent<Light>().color = new Color(0.8f, 0.8f, 0.8f, 1);
                int ran = UnityEngine.Random.Range(0, 4);
                switch (ran) {
                    case 0:
                        RightWalk();
                        break;
                    case 1:
                        LeftWalk();
                        break;
                    case 2:
                        UpWalk();
                        break;
                    case 3:
                        DownWalk();
                        break;
                    default:
                        break;
                }
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
        //壁に当たった時1つ前の位置に戻る
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door")
        {
            Back(last_position);
        }
        if (col.gameObject.tag == "Player") {
            Destroy(this.gameObject);
        }
    }

    void RightWalk()
    {
        transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("x", obj.transform.position.x + 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(1, 0, 0), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //値の初期化
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    void LeftWalk()
    {
        transform.rotation = Quaternion.Euler(0, -90.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("x", obj.transform.position.x - 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(-1, 0, 0), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //値の初期化
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    void UpWalk()
    {
        transform.rotation = Quaternion.Euler(0, 0.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("z", obj.transform.position.z + 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(0, 0, 1), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));


        //値の初期化
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    void DownWalk()
    {
        transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("z", obj.transform.position.z - 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(0, 0, -1), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //値の初期化
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    //1つ前の位置に戻る
    void Back(Vector3 back_position)
    {
        //iTween.MoveTo(obj, iTween.Hash("x", back_position.x, "z", back_position.z, "time", back_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", back_position, "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        timer = 0;
        limit_timer = back_time;
        step = STEP.MOVE;
    }

    void MovePlayer(Vector3 v3)
    {
        obj.transform.position = v3;
    }

    /*public void SetPlayer(GameObject temp_player) {
        player = temp_player;
    }*/
}
