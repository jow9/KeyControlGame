using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ドアや宝箱の開閉処理
 * 開閉には「鍵」の所持が求められる
 */
public class DoorScript : MonoBehaviour {

    GameObject player;

    public GameObject open_box;

    GameObject scroll_content;
    ScrollControl scroll_control;

    PlayerStatusScript player_status;
    public string[] key;
    string Item;
    bool[] key_flg;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player_status = player.GetComponent<PlayerStatusScript>();
        scroll_content = GameObject.FindGameObjectWithTag("Content");
        scroll_control = scroll_content.GetComponent<ScrollControl>();

        key_flg = new bool[key.Length];
        for (int i = 0; i < key_flg.Length; i++) {
            key_flg[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        int count = 0;
        for (int i = 0; i < key_flg.Length; i++){
            if (!(key_flg[i])) break;
            count++;
        }
        if (count == key_flg.Length)
        {
            Destroy(this.gameObject);

            //コマンドを設定する
            player_status.SetComandState(Item, true);
            //scroll_control.SetTextBox("Get New Comand : " + Item);
            switch (Item) {
                case "shot":
                    scroll_control.SetTextBox("火の玉を打つ：shot", "comand_shot");
                    GameObject.FindGameObjectWithTag("fireshottext").GetComponent<Text>().text = "shot";
                    break;

                case "hide":
                    scroll_control.SetTextBox("隠れる：hide", "comand_hide");
                    break;

                case "creatmycopy":
                    scroll_control.SetTextBox("分身する：creatmycopy", "comand_creatmycopy");
                    break;

                default:
                    break;
            }
        }
	}

    void OnCollisionStay(Collision col)
    {
        //Debug.Log(col.gameObject.tag);
        for (int i = 0; i < key.Length; i++) {
            if (col.gameObject.tag == key[i])
            {
                key_flg[i] = true;
                player_status.SetKeyState(key[i], false);
                Instantiate(open_box, transform.position, Quaternion.identity);
                Destroy(col.gameObject);
            }
        }
    }

    public void SetItem(string Item_name) {
        Item = Item_name;
    }
}
