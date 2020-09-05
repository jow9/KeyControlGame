using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * マップ上の敵全体の管理
 * 敵のリスポーン処理
 */
public class EnemysBoxScript : MonoBehaviour {
    public GameObject enemy_prefab;

    public int generation_enemy_range_x;//敵を出現させる範囲:x
    public int generation_enemy_range_z;//敵を出現させる範囲:z
    public int max_enemy_sum;//一度に出現させることができる敵の数
    public int generation_probability;//敵が出現する確率
    public float limit_timer_generation_enemy;//敵の生成の抽選をおこなう間隔

    float timer_generation_enemy;//敵の生成の抽選をおこなうための時間管理

    // Use this for initialization
    void Start() {
        //値が正常に入力されているかチェック。デフォルトの値を代わりに利用する。
        if (generation_enemy_range_x <= 0) { Debug.Log("EnemyBox : Nothing generation_enemy_range"); generation_enemy_range_x = 1; }
        if (generation_enemy_range_z <= 0) { Debug.Log("EnemyBox : Nothing generation_enemy_range"); generation_enemy_range_z = 1; }
        if (max_enemy_sum <= 0) { Debug.Log("EnemyBox : Nothing max_enemy_sum"); max_enemy_sum = 3; }
        if (max_enemy_sum <= 0) { Debug.Log("EnemyBox : Nothing generation_probability"); generation_probability = 10; }
        if (limit_timer_generation_enemy <= 0) { Debug.Log("EnemyBox : Nothing limit_timer_generation_enemy"); limit_timer_generation_enemy = 5.0f; }
        

        //初期化
        timer_generation_enemy = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //今生きている敵の数
        int now_enemy_sum = this.transform.childCount;

        //敵の生成
        timer_generation_enemy += Time.deltaTime;
        if (timer_generation_enemy > limit_timer_generation_enemy && now_enemy_sum < max_enemy_sum)
        {
            CreatEnemy();
            
            //値の初期化
            timer_generation_enemy = 0;
        }
    }

    void CreatEnemy() {
        //敵を出現させて良いか判定する
        bool flg_generation_enemy = false;
        int ran = UnityEngine.Random.Range(0, 100);
        if (ran < generation_probability) flg_generation_enemy = true;

        //敵を生成する
        if (flg_generation_enemy)
        {
            //出現ポイントを計算する
            int x_ran = UnityEngine.Random.Range(-(generation_enemy_range_x), generation_enemy_range_x + 1);
            int z_ran = UnityEngine.Random.Range(-(generation_enemy_range_z), generation_enemy_range_z + 1);

            GameObject temp = Instantiate(enemy_prefab, transform.position + new Vector3(x_ran, -0.5f, z_ran), Quaternion.identity);
            temp.transform.parent = this.transform;
        }
    }
}
