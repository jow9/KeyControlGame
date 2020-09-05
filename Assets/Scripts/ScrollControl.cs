using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * ゲームUI（コマンドリスト）を管理
 * ゲーム中に手に入れたアクションをリストで表示する
 * リストはマウスのスクロール操作で確認でき、またアクションを入手した際は自動でスクロールさせる
 */
public class ScrollControl : MonoBehaviour {

    public GameObject Node;
    public GameObject Scroll;
    List<string> text_box_list = new List<string>();
    List<string> text_tag_list = new List<string>();
    Color default_color;
    Color pic_color;

    void Start()
    {
        default_color = new Color(1, 1, 1, 0.3F);
        pic_color = new Color(1, 1, 1, 0.8F);

        ChangeViewTextBox("list");
    }

    void Update() {
    }

    public void SetTextBox(string str, string comand_tag) {
        text_box_list.Add(str);
        text_tag_list.Add(comand_tag);

        //テキストのボックスを生成する
        GameObject node_temp = Instantiate(Node);
        node_temp.transform.SetParent(transform, false);

        //タグを設定する
        node_temp.tag = comand_tag;

        //テキストの中身を変更する
        Text text = node_temp.GetComponentInChildren<Text>();
        text.text = str;

        //スクロールの位置を一番下に移動させる。
        Scroll.GetComponent<ScrollViewControl>().SetScrollbar();

    }

    //選択したノードのテキスト情報を変更する
    public void ChangeText(string comand, string new_comand)
    {
        string tag_name = "comand_" + comand;
        bool boo = false;
        if (tag_name == text_tag_list[0]) boo = true;//例外処理

        //タグがあるか確認する
        for (int i = 1; i < text_tag_list.Count; i++)
        {
            if (text_tag_list[i] == tag_name) boo = true;
        }

        //タグがあった場合、選択したノードのテキストを変更する
        if (boo)
        {
            GameObject node = GameObject.FindGameObjectWithTag(tag_name);
            Text text = node.GetComponentInChildren<Text>();
            text.text = new_comand;
        }
    }

    //選択したノードの見た目を変更する
    public void ChangeViewTextBox(string comand) {

        string tag_name = "comand_" + comand;
        bool boo = false;
        if(tag_name == text_tag_list[0]) boo = true;

        //一度すべてのノードの色をリセットする
        for (int i = 1; i < text_tag_list.Count; i++)
        {
            GameObject temp = GameObject.FindGameObjectWithTag(text_tag_list[i]);
            temp.gameObject.GetComponent<Image>().color = default_color;

            //タグがあるか確認する
            if (text_tag_list[i] == tag_name) boo = true;
        }

        //タグがあった場合、選択したノードの色を変更する
        if (boo) {
            GameObject node = GameObject.FindGameObjectWithTag(tag_name);
            node.gameObject.GetComponent<Image>().color = pic_color;
        }
    }
}
