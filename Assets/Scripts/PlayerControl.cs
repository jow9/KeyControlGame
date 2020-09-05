using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * タイピングによるプレイヤーの動きを管理
 */
public class PlayerControl : MonoBehaviour {

    GameObject InputField;
    InputManager input_manager;
    PlayerStatusScript player_state;

    GameObject obj;//自分自身のゲームオブジェクトを入れる

    //string[] key_comand_list_right = {"a", "book", "car", "drive", "eco", "find", "give", "hide", "input", "join", "kind", "lot", "no", "my", "on", "pop", "quite", "right", "shop", "top", "un", "view", "word", "yellow", "zip"};
    //string[] key_comand_list_left = { "an", "bike", "cup", "door", "ear", "foud", "gave", "hot", "in", "joke", "kiss", "left", "not", "moon", "oh", "png", "rain", "short", "team", "usb", "vip", "world", "yahoo", "zone" };
    //string[] key_comand_list_up = { "apple", "body", "cake", "dog", "english", "fox", "google", "how", "into", "joker", "line", "natural", "magic", "old", "pc", "rage", "shot", "time", "up", "wave", "you", "zoo"};
    //string[] key_comand_list_down = { "able", "bady", "cafe", "down", "each", "face", "game", "hair", "if", "job", "keep", "leg", "neat", "me", "of", "page", "race", "sun", "table", "use", "very", "wait", "yes"};
    string[] key_comand_list = { "about", "above", "act", "after", "age", "ago", "air", "all", "am", "and", "angry", "bag", "bank", "ball", "bed", "beef", "boy", "bye", "call", "can", "cat", "change", "child", "city", "class", "clock", "dad", "day", "dance", "desk", "diary", "do", "does", "doll", "dream", "drink", "drive", "driver", "drop", "dry", "each", "earth", "easy", "eat", "egg", "else", "end", "enjoy", "enter", "even", "evening", "ever", "every", "eye", "face", "fact", "fall", "far", "farm", "fast", "fat", "father", "feel", "feeling", "few", "field", "fifteen", "fifty", "fight", "film", "fire", "fish", "fool", "foot", "for", "food", "four", "free", "full", "frog", "fun", "funny", "friend", "fruit", "future", "funny", "gas", "get", "glad", "go", "goal", "god", "gold", "golf", "good", "gray", "great", "grow", "gun", "hair", "half", "hand", "hard", "hat", "have", "he", "head", "hear", "hello", "her", "help", "her", "here", "hi", "high", "hill", "him", "his", "hit", "hold", "hole", "home", "honey", "hour", "house", "hurt", "ice", "idea", "if", "ill", "invite", "is", "it", "its", "jam", "job", "joy", "july", "jump", "just", "keep", "key", "kick", "kid", "kill", "king", "knee", "knife", "lady", "lake", "land", "large", "last", "late", "later", "leaf", "learn", "let", "leave", "life", "light", "like", "lion", "lip", "list", "little", "live", "long", "look", "lose", "low", "mail", "make", "man", "many", "map", "marry", "may", "meal", "mean", "meat", "meet", "men", "mile", "milk", "mind", "miss", "money", "month", "more", "most", "mouse", "most", "move", "movie", "ms", "much", "music", "must", "nail", "name", "narrow", "near", "need", "new", "next", "news", "nice", "nine", "noise", "none", "noon", "note", "now", "nurse", "off", "ok", "one", "only", "or", "our", "out", "over", "page", "pair", "park", "part", "pass", "past", "pay", "pen", "people", "pet", "piano", "pick", "pig", "pink", "pipe", "plan", "play", "point", "police", "pool", "poor", "pot", "price", "pull", "put", "push", "queen", "quite", "quick", "rabbit", "race", "read", "real", "red", "real", "rice", "rich", "ride", "rise", "road", "robot", "rock", "roof", "room", "rope", "round", "rule", "run", "sad", "safe", "sale", "salt", "same", "sand", "save", "say", "sea", "seat", "see", "seem", "sell", "send", "set", "seven", "shall", "she", "ship", "shoe", "show", "shut", "sick", "side", "sign", "since", "sing", "sit", "six", "sister", "size", "sky", "ski", "skin", "sleep", "slow", "small", "smell", "smoke", "snow", "so", "sope", "soft", "some", "son", "song", "soon", "sorry", "soup", "speak", "star", "start", "stand", "stay", "step", "still", "stop", "story", "such", "suit", "sun", "sure", "swim", "take", "talk", "tall", "tea", "teach", "tell", "ten", "tent", "test", "than", "that", "the", "thank", "then", "they", "thin", "this", "tie", "till", "to", "too", "toy", "train", "tree", "trip", "try", "turn", "two", "type", "upon", "us", "useful", "vist", "voice", "wake", "wall", "war", "was", "wash", "wave", "way", "we", "week", "wear", "well", "were", "west", "wet", "what", "who", "whom", "why", "wide", "will", "wine", "wind", "wise", "with", "work", "worst", "write", "yard", "your", "young", "year"};
    string right;
    string left;
    string up;
    string down;
    string open;
    string hide;
    string creatmycopy;
    string shot;

    string now_move;

    //プレイヤーの今の状態を管理する
    enum STEP {
        NONE = 0,

        STOP = 1,
        MOVE = 2
    }

    STEP step;//現在のプレイヤーの状態を保存する

    float timer;//動作からの時間を計測
    float limit_timer;//動作から次の動作に移行するまでのクールタイム


    public float input_time = 0.1f;
    public float walk_time = 0.5f;//移動アニメーションの時間
    public float default_time = 1f;//それ以外のクールタイム
    public float back_time = 0.1f;//もとの位置に戻るまでの時間
    public float shot_time = 0.5f;

    Vector3 last_position;

    public GameObject KeyA;
    public GameObject KeyB;
    public GameObject PointLight;
    public GameObject player_dummy;
    public GameObject FireShot;

    public float shot_speed;

    GameObject scroll_content;
    ScrollControl scroll_control;

    Text right_arrow_text;
    Text down_arrow_text;
    Text left_arrow_text;
    Text up_arrow_text;
    Text fire_shot_text;

    // Use this for initialization
    void Start () {
        timer = 0f;
        limit_timer = 0f;
        step = STEP.STOP; 
        obj = this.gameObject;
        last_position = this.gameObject.transform.position;
        input_manager = InputField.GetComponent<InputManager>();
        player_state = this.gameObject.GetComponent<PlayerStatusScript>();

        right = "right";
        left = "left";
        up = "up";
        down = "down";
        open = "open";
        hide = "hide";
        creatmycopy = "creatmycopy";
        shot = "shot";

        scroll_content = GameObject.FindGameObjectWithTag("Content");
        scroll_control = scroll_content.GetComponent<ScrollControl>();

        right_arrow_text = GameObject.FindGameObjectWithTag("rightarrowtext").GetComponent<Text>();
        down_arrow_text = GameObject.FindGameObjectWithTag("downarrowtext").GetComponent<Text>();
        left_arrow_text = GameObject.FindGameObjectWithTag("leftarrowtext").GetComponent<Text>();
        up_arrow_text = GameObject.FindGameObjectWithTag("uparrowtext").GetComponent<Text>();
        fire_shot_text = GameObject.FindGameObjectWithTag("fireshottext").GetComponent<Text>();
    }


	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        //プレイヤーの動作をコントロール
        if (step == STEP.MOVE && timer > limit_timer + input_time) step = STEP.STOP;

        //入力コマンドを取得する
        string key_comand = input_manager.GetComandKey();

        //コマンドに合わせて動作を割り当てる
        if (step == STEP.STOP) {
            last_position = this.gameObject.transform.position;
            if (key_comand == "") now_move = "stop";

            if (key_comand == right || now_move == "right") RightWalk();
            if (key_comand == left || now_move == "left") LeftWalk();
            if (key_comand == up || now_move == "up") UpWalk();
            if (key_comand == down || now_move == "down") DownWalk();
            if (key_comand == open && player_state.GetKeyState("KeyA")) UseKey("KeyA");
            if (key_comand == open && player_state.GetKeyState("KeyB")) UseKey("KeyB");
            if (key_comand == hide && player_state.GetComandState("hide")) StealthPlayer();
            if (key_comand == creatmycopy && player_state.GetComandState("creatmycopy") && player_state.GetNowDummyNum() < 1) CreatDummy();
            if (key_comand == shot && player_state.GetComandState("shot") || now_move == "shot") ShotFire();
        }

        //デバッグ用
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("space");
            iTween.MoveBy(gameObject, iTween.Hash("x", 2, "time", 0.6f, "easetype", "linear"));
        }*/
    }

    //衝突判定
    void OnCollisionEnter(Collision col)
    {
        //壁に当たった時1つ前の位置に戻る
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door")
        {
            Back(last_position);
        }
        //アイテム：鍵Aをゲット
        if (col.gameObject.tag == "ItemKeyA") {
            player_state.SetKeyState("KeyA", true);
            Destroy(col.gameObject);
        }
        //アイテム：鍵Bをゲット
        if (col.gameObject.tag == "ItemKeyB")
        {
            player_state.SetKeyState("KeyB", true);
            Destroy(col.gameObject);
        }
        //敵との衝突：ダメージを受ける
        if (col.gameObject.tag == "Enemy")
        {
            player_state.DamagingToPlayer(1);
        }
        if (col.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("ClearScene");
        }
    }

    //歩く動作関数
    void RightWalk() {
        transform.rotation = Quaternion.Euler(0, -90.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("x", obj.transform.position.x - 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(-1, 0, 0), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //Debug.Log(obj.name);

        //値の初期化
        if (now_move != "right")
        {
            right = ChangeComandKey();
            string new_text = "右に移動：" + right;
            scroll_control.ChangeText("right", new_text);
            right_arrow_text.text = right;
            now_move = "right";
        }
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    void LeftWalk()
    {
        transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("x", obj.transform.position.x + 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(+1, 0, 0), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //値の初期化
        if (now_move != "left") {
            left = ChangeComandKey();
            string new_text = "左に移動：" + left;
            scroll_control.ChangeText("left", new_text);
            left_arrow_text.text = left;
            now_move = "left";
        }
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    void UpWalk()
    {
        transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("z", obj.transform.position.z - 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(0, 0, -1), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //値の初期化
        if (now_move != "up") {
            up = ChangeComandKey();
            string new_text = "上に移動：" + up;
            scroll_control.ChangeText("up", new_text);
            up_arrow_text.text = up;
            now_move = "up";
        }

        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    void DownWalk()
    {
        transform.rotation = Quaternion.Euler(0, 0.0f, 0);
        //iTween.MoveTo(obj, iTween.Hash("z", obj.transform.position.z + 1, "time", walk_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", obj.transform.position + new Vector3(0, 0, 1), "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        //値の初期化
        if (now_move != "down") {
            down = ChangeComandKey();
            string new_text = "下に移動：" + down;
            scroll_control.ChangeText("down", new_text);
            down_arrow_text.text = down;
            now_move = "down";
        }
        timer = 0;
        limit_timer = walk_time;
        step = STEP.MOVE;
    }

    //1つ前の位置に戻る
    void Back(Vector3 back_position) {
        //iTween.MoveTo(obj, iTween.Hash("x", back_position.x, "z", back_position.z, "time", back_time, "easetype", "linear"));
        iTween.ValueTo(obj, iTween.Hash("from", obj.transform.position, "to", back_position, "time", walk_time, "easetype", "linear", "onupdate", "MovePlayer"));

        timer = 0;
        limit_timer = back_time;
        step = STEP.MOVE;
        now_move = "back";
    }

    void MovePlayer(Vector3 v3) {
        obj.transform.position = v3;
    }

    //keyを使用する
    void UseKey(string key_type) {
        switch (key_type) {
            case "KeyA":
                Instantiate(KeyA, this.transform.position + (this.transform.forward) + new Vector3(0, 1f, 0), Quaternion.identity);
                break;
            case "KeyB":
                Instantiate(KeyB, this.transform.position + (this.transform.forward) + new Vector3(0, 1f, 0), Quaternion.identity);
                break;
        }

        //値の初期化
        if (now_move != "open") {
            open = ChangeComandKey();
            string new_text = "開ける：" + open;
            scroll_control.ChangeText("open", new_text);
            now_move = "open";
        }
        timer = 0;
        limit_timer = default_time;
        step = STEP.MOVE;
    }

    //敵から見えない状態になる
    void StealthPlayer()
    {
        //見えない状態にする
        player_state.SetPlayerAppearState(false);

        //光を消す
        PointLight.GetComponent<Light>().intensity = 0.3f;

        //値の初期化
        if (now_move != "hide") {
            hide = ChangeComandKey();
            string new_text = "隠れる：" + hide;
            scroll_control.ChangeText("hide", new_text);
            now_move = "hide";
        }
        timer = 0;
        limit_timer = default_time;
        step = STEP.MOVE;
    }

    //ダミーをその場に生成する
    void CreatDummy() {
        Instantiate(player_dummy, this.transform.position + new Vector3(1, 0, 0), Quaternion.identity);

        //値の初期化
        if (now_move != "creatmycopy") {
            creatmycopy = ChangeComandKey();
            string new_text = "分身する：" + creatmycopy;
            scroll_control.ChangeText("creatmycopy", new_text);
            now_move = "creatmycopy";
        }
        timer = 0;
        limit_timer = default_time;
        step = STEP.MOVE;
    }

    void ShotFire() {
        GameObject arrow_temp = Instantiate(FireShot, this.transform.position + transform.forward, Quaternion.identity);
        arrow_temp.GetComponent<Rigidbody>().AddForce(transform.forward * shot_speed, ForceMode.VelocityChange);

        //値の初期化
        if (now_move != "shot")
        {
            shot = ChangeComandKey();
            string new_text = "火の玉を打つ：" + shot;
            scroll_control.ChangeText("shot", new_text);
            fire_shot_text.text = shot;
            now_move = "shot";
        }
        timer = 0;
        limit_timer = shot_time;
        step = STEP.MOVE;
    }

    //文字入力を行うオブジェクトをアタッチする
    public void SetInputFieldr(GameObject temp_InputField) {
        InputField = temp_InputField;
    }

    //タイプしなければいけないコマンドを変更する
    string ChangeComandKey() {
        int random_number = Random.Range(0, key_comand_list.Length);
        return key_comand_list[random_number];
    }
}