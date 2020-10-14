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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UP");
            selectcount = !selectcount;
            ShowCursor(selectcount);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down");
            selectcount = !selectcount;
            ShowCursor(selectcount);
        }
        if (Input.GetKeyDown(KeyCode.Return))
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
