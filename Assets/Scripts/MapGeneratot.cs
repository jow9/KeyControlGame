using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * スクリプトからマップ生成を行う
 */
public class MapGeneratot : MonoBehaviour {
    int dx_wall_max = 39;//x軸のマップの大きさ
    int dz_wall_max = 29;//z軸のマップの大きさ

    public GameObject wall_prefab;//マップの壁
    public GameObject door_A_prefab;//ドアA
    public GameObject door_B_prefab;//ドアB
    public GameObject item_key_A_prefab;//ドアA
    public GameObject item_key_B_prefab;//ドアB
    public GameObject enemybox_prefab;//敵
    public GameObject player;//プレイヤー
    public GameObject goal_prefab;//ゴール

    public GameObject InputField;
    public GameObject camera;

    GameObject scroll_content;
    ScrollControl scroll_control;

    // Use this for initialization
    void Start () {

        //壁を自動生成
        CreateMapWall();

        //マップの元データ
        /*
        0:空
        1:壁
        2:ドアA
        3:ドアB
        4:鍵A
        5:鍵B
        6:敵の出現ボックス
        9:プレイヤー
        */
        string map_matrix = "";
        map_matrix += "11111111111111111111111100011111111111:";//28行目
        map_matrix += "11110000000000001111111107011111111111:";//27行目
        map_matrix += "11110400000000001111111100011111111111:";//26行目
        map_matrix += "11110000000000001111111110111111111111:";//25行目
        map_matrix += "11110000090000001111111110111111111111:";//24行目
        map_matrix += "11110000000000001111111000000000000111:";//23行目
        map_matrix += "11110000000000001111111000000060000111:";//22行目
        map_matrix += "11110000200000001111111000600000000111:";//21行目
        map_matrix += "11111001111111111111111000000000000111:";//20行目
        map_matrix += "11111001111111111111111000006000000111:";//19行目
        map_matrix += "11100000001111111111111000000000000111:";//18行目
        map_matrix += "11100000000000000000011111111111100111:";//17行目
        map_matrix += "11100000000000000000011111111111100111:";//16行目
        map_matrix += "11100600000000006000011111111111100111:";//15行目
        map_matrix += "11100000000000000000011000000000000011:";//14行目
        map_matrix += "11100000000006000000000000000000006011:";//13行目
        map_matrix += "11100000000000000000011000000000000011:";//12行目
        map_matrix += "11111111111111111111111000000003000011:";//11行目
        map_matrix += "11111111111111111111111000000001111111:";//10行目
        map_matrix += "11111111111111111111111000000001111111:";//9行目
        map_matrix += "11111111111111111111111000000001111111:";//8行目
        map_matrix += "11111111111111111111111000000001111111:";//7行目
        map_matrix += "11111111111111111111111006000001111111:";//6行目
        map_matrix += "11111111111111111111111000004001111111:";//5行目
        map_matrix += "11111111111111111111111000000001111111:";//4行目
        map_matrix += "11111111111111111111111111111111111111:";//3行目
        map_matrix += "11111111111111111111111111111111111111:";//2行目
        map_matrix += "11111111111111111111111111111111111111:";//1行目


        //マップのコンテンツを自動生成
        CreateMapContent(map_matrix);

        //コマンドリストを生成する
        scroll_content = GameObject.FindGameObjectWithTag("Content");
        scroll_control = scroll_content.GetComponent<ScrollControl>();
        scroll_control.SetTextBox("コマンドリスト", "comand_list");
        scroll_control.SetTextBox("右に移動：right", "comand_right");
        scroll_control.SetTextBox("左に移動：left", "comand_left");
        scroll_control.SetTextBox("上に移動：up", "comand_up");
        scroll_control.SetTextBox("下に移動：down", "comand_down");
        scroll_control.SetTextBox("開ける：open", "comand_open");
    }

    //マップの壁を自動生成する
    void CreateMapWall() {

        //マップのz軸の上下2列に壁を作る
        for (int dx = 0; dx < dx_wall_max - 1; dx++) {
            GameObject temp1 = Instantiate(wall_prefab, new Vector3(dx + 1, 0, 0), Quaternion.identity);
            GameObject temp2 = Instantiate(wall_prefab, new Vector3(dx + 1, 0, dz_wall_max), Quaternion.identity);
            temp1.transform.parent = this.transform;
            temp2.transform.parent = this.transform;
        }
        //マップのz軸の左右2列に壁を作る
        for (int dz = 0; dz < dz_wall_max + 1; dz++)
        {
            GameObject temp1 = Instantiate(wall_prefab, new Vector3(0, 0, dz), Quaternion.identity);
            GameObject temp2 = Instantiate(wall_prefab, new Vector3(dx_wall_max, 0, dz), Quaternion.identity);
            temp1.transform.parent = this.transform;
            temp2.transform.parent = this.transform;
        }
    }

    //マップの中身を自動生成する
    void CreateMapContent(string map_matrix) {

        //「:」ごとに文字列を分割
        string[] map_matrix_array = map_matrix.Split(':');

        for (int z = 0; z < map_matrix_array.Length; z++) {

            for (int x = 0; x < map_matrix_array[z].Length; x++) {

                //x行目の文字列から1文字だけ取り出す
                int obj = int.Parse(map_matrix_array[z].Substring(x, 1));
                //int obj = int.Parse(map_matrix_array[z].Split(','));

                //「0」だったら壁を挿入する
                if(obj == 1)//壁
                {
                    GameObject temp = Instantiate(wall_prefab, new Vector3(x + 1, 0, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.transform.parent = this.transform;
                }
                if(obj == 2)//コマンドボックス:hide(shot)
                {
                    GameObject temp = Instantiate(door_A_prefab, new Vector3(x + 1, -0.5f, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.GetComponent<DoorScript>().SetItem("shot");
                    //temp.GetComponent<DoorScript>().SetItem("hide");
                    temp.transform.parent = this.transform;
                }
                if(obj == 3)//コマンドボックス:creatmycopy
                {
                    GameObject temp = Instantiate(door_A_prefab, new Vector3(x + 1, -0.5f, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.GetComponent<DoorScript>().SetItem("creatmycopy");
                    temp.transform.parent = this.transform;
                }
                if (obj == 4)//鍵A
                {
                    GameObject temp = Instantiate(item_key_A_prefab, new Vector3(x + 1, 0, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.transform.parent = this.transform;
                }
                if (obj == 5)//鍵B
                {
                    GameObject temp = Instantiate(item_key_B_prefab, new Vector3(x + 1, 0, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.transform.parent = this.transform;
                }
                if (obj == 6)//敵
                {
                    GameObject temp = Instantiate(enemybox_prefab, new Vector3(x + 1, 0, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.transform.parent = this.transform;
                }
                if (obj == 7)//ゴール
                {
                    GameObject temp = Instantiate(goal_prefab, new Vector3(x + 1, 0, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.transform.parent = this.transform;
                }
                if (obj == 9)//プレイヤー
                {
                    GameObject temp = Instantiate(player, new Vector3(x + 1, -0.5f, map_matrix_array.Length - z - 1), Quaternion.identity);
                    temp.gameObject.GetComponent<PlayerControl>().SetInputFieldr(InputField);
                    camera.gameObject.GetComponent<MainCameraScript>().SetPlayer(temp);
                }
            }
        }

    }
}
