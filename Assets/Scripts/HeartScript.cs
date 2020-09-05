using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *ゲームUI（体力値の表示）の管理
 */
public class HeartScript : MonoBehaviour
{

    public GameObject[] heart_icon;

    public void UpdateLife(int life) {

        for (int i = 0; i < heart_icon.Length; i++) {

            if (i < life) heart_icon[i].SetActive(true);
            else heart_icon[i].SetActive(false);

        }
    }
}
