using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * プレイヤーの状態を管理
 * 利用可能コマンドや体力など
 */
public class PlayerStatusScript : MonoBehaviour {

    //自身が持つポイントライトオブジェクト
    public GameObject PointLight;

    //プレイヤーの体力
    public int player_life;

    //プレイヤーが見える状態か否か判断する(ステルス)
    bool player_appear;
    float stealth_timer;//ステルス時間を計測する
    public float limit_stealth_timer;//ステルス時間
    

    //鍵を持っているか判断する
    bool flg_get_key_a;
    bool flg_get_key_b;

    //アイテム：コマンド
    //hideコマンドが入力できる状態か判断する
    bool flg_get_hidecomand;
    bool flg_get_copycomand;

    //火の玉を打てるか判断する
    bool flg_fire_shot;

    //ダミーの出している数を管理する
    int now_dummy_num;

    int light_size;

    //ハートパネル
    HeartScript heartscipt;

    // Use this for initialization
    void Start() {
        //値が正常に入力されているかチェック。デフォルトの値を代わりに利用する。
        if (player_life <= 0) { Debug.Log("Player : Nothing Life"); player_life = 3; }
        if (player_life <= 0) { Debug.Log("Player : Limit Stealth Timer"); limit_stealth_timer = 15; }

        player_appear = true;
        flg_get_key_a = false;
        flg_get_key_b = false;
        flg_get_hidecomand = false;
        flg_fire_shot = false;
        now_dummy_num = 0;
        light_size = 5;

        //ハートの更新用スクリプト
        heartscipt = GameObject.FindGameObjectWithTag("heartpanel").GetComponent<HeartScript>();
    }

    void Update()
    {

        //プレイヤーは息絶えた。
        if (player_life <= 0) {
            //Destroy(this.gameObject);
            SceneManager.LoadScene("GameOverScene");
        }

        if (!(GetPlayerAppearState()))
        {
            stealth_timer += Time.deltaTime;
            if (stealth_timer > limit_stealth_timer)AppearPlayer();
        }
    }

    public void SetKeyState(string key_name, bool boo) {
        if (key_name == "KeyA") flg_get_key_a = boo;
        if (key_name == "KeyB") flg_get_key_b = boo;
    }

    public bool GetKeyState(string key_name)
    {
        if (key_name == "KeyA") return flg_get_key_a;
        if (key_name == "KeyB") return flg_get_key_b;

        Debug.Log("GetKeyState : This key is nothig");
        return false;
    }

    public void SetComandState(string comand_name, bool boo)
    {
        if (comand_name == "shot") flg_fire_shot = boo;
        if (comand_name == "hide") flg_get_hidecomand = boo;
        if (comand_name == "creatmycopy") flg_get_copycomand = boo;
    }

    public bool GetComandState(string  comand_name)
    {
        if (comand_name == "shot") return flg_fire_shot;
        if (comand_name == "hide") return flg_get_hidecomand;
        if (comand_name == "creatmycopy") return flg_get_copycomand;

        Debug.Log("GetKeyState : This key is nothig");
        return false;
    }

    public void DamagingToPlayer(int damage_size)
    {
        player_life -= damage_size;
        heartscipt.UpdateLife(player_life);
    }

    public void HeelToPlayer(int heel_size)
    {
        player_life += heel_size;
        heartscipt.UpdateLife(player_life);
    }

    public void SetPlayerAppearState(bool boo) {
        player_appear = boo;
        stealth_timer = 0;
    }

    //ステルス状態かどうか判断する
    public bool GetPlayerAppearState() {
        return player_appear;
    }

    public int GetNowDummyNum() {
        return now_dummy_num;
    }

    public void AddDummyNow(int add) {
        now_dummy_num += add;
    }

    //敵から見える状態になる
    void AppearPlayer()
    {
        //見えない状態にする
        SetPlayerAppearState(true);

        //光をつける
        PointLight.GetComponent<Light>().intensity = light_size;

        //初期化
        stealth_timer = 0;
    }
}
