using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject score_obj;
    public GameObject high_score_obj;
    public GameObject Retry_obj;
    public GameObject StageSelect_obj;
    int score,highscore;
    bool selectcount;

    private GameObject Retry_cursor;
    private GameObject Select_cursor;

    private int count_potepo;

    // Start is called before the first frame update
    void Start()
    {
        score = field.score;
        highscore = field.highscore;
        selectcount = false;

        if (score > highscore)
        {
            highscore = score;
        }
        score_obj.GetComponent<Text>().text = score.ToString();
        high_score_obj.GetComponent<Text>().text = highscore.ToString();
        field.highscore = highscore;

        Retry_cursor = Retry_obj.transform.GetChild(0).gameObject;
        Select_cursor = StageSelect_obj.transform.GetChild(0).gameObject;

        ShowCursor(selectcount);
    }

    // Update is called once per frame
    void Update()
    {
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");

        if (lsh == 0 && lsv == 0 && dph == 0 && dpv == 0)
        {
            count_potepo = 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || ((lsv > 0) && (lsv > lsh) && (count_potepo == 1)) || ((dpv > 0) && (dpv > dph) && (count_potepo == 1)))
        {
            count_potepo = 0;
            Debug.Log("UP");
            selectcount = !selectcount;
            ShowCursor(selectcount);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || ((lsv < 0) && (lsv < lsh) && (count_potepo == 1)) || ((dpv < 0) && (dpv < dph) && (count_potepo == 1)))
        {
            count_potepo = 0;
            Debug.Log("Down");
            selectcount = !selectcount;
            ShowCursor(selectcount);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 0"))
        {
            if (selectcount == false)
                SceneManager.LoadScene("tagaitest");
            else
                SceneManager.LoadScene("StageSelect");
        }
    }

    void ShowCursor(bool select)
    {
        if (select == false)
        {
            Retry_cursor.GetComponent<Image>().enabled = true;
            Select_cursor.GetComponent<Image>().enabled = false;
        }
        else
        {
            Retry_cursor.GetComponent<Image>().enabled = false;
            Select_cursor.GetComponent<Image>().enabled = true;
        }
    }
}
