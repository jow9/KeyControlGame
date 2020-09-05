using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * スタートメニューの動きを管理
 * 入力したコマンドによりシーンを切り替える
*/
public class ChangeSceneScript : MonoBehaviour {

    public GameObject InputField;
    InputManager input_manager;

    // Use this for initialization
    void Start () {
        input_manager = InputField.GetComponent<InputManager>();
    }
	
	// Update is called once per frame
	void Update () {

        string key_comand = input_manager.GetComandKey();

        switch (key_comand) {
            case "retry":
                SceneManager.LoadScene("main");
                break;

            case "yes":
                SceneManager.LoadScene("main");
                break;

            case "no":
                SceneManager.LoadScene("GameOverScene");
                break;

            case "help":
                SceneManager.LoadScene("HelpScene");
                break;
            default:
                break;
        }

        if(Input.GetKeyDown(KeyCode.Space))SceneManager.LoadScene("StartScene");

    }
}
