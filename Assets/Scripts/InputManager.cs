using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *　ユーザのタイピング入力を管理
 */
public class InputManager : MonoBehaviour {

    InputField inputField;

    //GameObject scroll_content;
    //ScrollControl scroll_control;

    //public bool flg = false;

    string comand_key;

    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start()
    {
        /*if (flg)
        {
            scroll_content = GameObject.FindGameObjectWithTag("Content");
            scroll_control = scroll_content.GetComponent<ScrollControl>();
        }*/

        inputField = this.GetComponent<InputField>();

        InitInputField();

        InitComandKey();
    }


    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>
    public void InputLogger()
    {

        string inputValue = inputField.text;

        //if (flg)if (inputValue != null) scroll_control.ChangeViewTextBox(inputValue);

        SetComandKey(inputValue);

        InitInputField();
    }


    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>
    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }


    /// <summary>
    /// 入力したコマンドをセットする
    /// </summary>
    void SetComandKey(string text) {

        comand_key = text;

    }


    public string GetComandKey()
    {

        return comand_key;

    }

    /// <summary>
    /// コマンドをリセットする
    /// </summary>
    void InitComandKey() {

        comand_key = "";

    }
}
