using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomInputKey;
using CustomInputJoyStickKey;

public class SceneManagerScript : MonoBehaviour
{
	[SerializeField] GameObject startPanel;
	[SerializeField] GameObject selectPanel;
    [SerializeField] GameObject selectStage;
    [SerializeField] GameObject selectUpStage;
    [SerializeField] GameObject selectDownStage;
    public Sprite[] spriteimages = new Sprite[4];
    public List<int[,]> stageinfo = new List<int[,]>();

    private int currentScenes;
    private int currentStage;

    // Scenesの状態
    private const int _START = 0;
    private const int _SELECT = 1;

    // fieldの状態
    private const int _NON = 0;
    private const int _BLOCK = 1;
    private const int _KABE = 2;
    private const int _FLOOR = 3;
    private const int _GEN = 4;

    private int count_potepo;


    public List<AudioClip> soundlist = new List<AudioClip>();

    private AudioSource audioSource;

    GameObject ManageObject;
    SceneFadeManager fadeManager;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //SceneFadeManagerがアタッチされているオブジェクトを取得
        ManageObject = GameObject.Find("GameController");
        //オブジェクトの中のSceneFadeManagerを取得
        fadeManager = ManageObject.GetComponent<SceneFadeManager>();
        StageGen();
        ShowStart();
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
        
        //UnityEngine.Debug.Log(currentScenes);
        if (currentScenes == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 0"))
            {
                audioSource.PlayOneShot(soundlist[1]);
                ShowSelect();
                UnityEngine.Debug.Log("Return key was pressed.");
            }
            currentStage = 0;
            selectUpStage.GetComponent<Image>().sprite = spriteimages[(spriteimages.Length-2)];
            selectDownStage.GetComponent<Image>().sprite = spriteimages[2];
        }
        else if (currentScenes == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || ((lsv > 0) && (lsv > lsh) && (count_potepo == 1)) || ((dpv > 0) && (dpv > dph) && (count_potepo == 1)))
            {
                count_potepo = 0;
                audioSource.PlayOneShot(soundlist[0]);
                if (currentStage == 0)
                {
                    currentStage = spriteimages.Length / 2 - 1;
                }
                else
                {
                    // 一周回るようにしたい
                    currentStage--;
                }
                UnityEngine.Debug.Log("上矢印が押されました");
                UnityEngine.Debug.Log(currentStage);
                selectStage.GetComponent<Image>().sprite = spriteimages[(currentStage + 1) * 2 - 1];
                if (currentStage == 0)
                {
                    selectUpStage.GetComponent<Image>().sprite = spriteimages[(spriteimages.Length - 2)];
                }
                else
                {
                    selectUpStage.GetComponent<Image>().sprite = spriteimages[(currentStage - 1) * 2];
                }
                if (currentStage == spriteimages.Length / 2 - 1)
                {
                    selectDownStage.GetComponent<Image>().sprite = spriteimages[0];
                }
                else
                {
                    selectDownStage.GetComponent<Image>().sprite = spriteimages[(currentStage + 1) * 2];
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || ((lsv < 0) && (lsv < lsh) && (count_potepo == 1)) || ((dpv < 0) && (dpv < dph) && (count_potepo == 1)))
            {
                count_potepo = 0;
                audioSource.PlayOneShot(soundlist[0]);
                if (currentStage == spriteimages.Length / 2 - 1)
                {
                    currentStage = 0;
                }
                else
                {
                    // 一周回るようにしたい
                    currentStage++;
                }
                UnityEngine.Debug.Log("下矢印が押されました");
                UnityEngine.Debug.Log(currentStage);
                selectStage.GetComponent<Image>().sprite = spriteimages[(currentStage + 1) * 2 - 1];
                if (currentStage == 0)
                {
                    selectUpStage.GetComponent<Image>().sprite = spriteimages[(spriteimages.Length - 2)];
                }
                else
                {
                    selectUpStage.GetComponent<Image>().sprite = spriteimages[(currentStage - 1) * 2];
                }
                if (currentStage == spriteimages.Length / 2 - 1)
                {
                    selectDownStage.GetComponent<Image>().sprite = spriteimages[0];
                }
                else
                {
                    selectDownStage.GetComponent<Image>().sprite = spriteimages[(currentStage + 1) * 2];
                }
            }
            else if (CustomInput.Interval_InputKeydown(KeyCode.Return, 3) || CustomInputJoyStick.Interval_InputKeydown("joystick button 1",3) || CustomInputJoyStick.Interval_InputKeydown("joystick button 0", 3))
            {
                audioSource.PlayOneShot(soundlist[1]);
                int[,] stagedata = stageinfo[currentStage];
                UnityEngine.Debug.Log("return");
                MainGameController.setStageData(stagedata);
                UnityEngine.Debug.Log(MainGameController.getStageData());
                //SceneManager.LoadScene("tagaitest");
                //SceneFadeManagerの中のフェードアウト開始関数を呼び出し
                fadeManager.fadeOutStart(0, 0, 0, 0, "tagaitest");
            }
        }
    }

	void ShowStart()
	{
        currentScenes = _START;
        currentStage = 0;
		startPanel.SetActive(true);
        selectPanel.SetActive(false);
	}
	
	void ShowSelect()
	{
        currentScenes = _SELECT;
        currentStage = 1;
		startPanel.SetActive(false);
        selectPanel.SetActive(true);
	}

    void StageGen()
    {
        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,2,2,0,2,2,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,2,0,2,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2,2,2},
                                    {2,2,0,0,2,0,0,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,2,0,2,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,2,2,0,2,2,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,0,0,2,0,0,2,2},
                                    {2,2,0,2,2,2,0,2,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,2,2,2},
                                    {2,0,0,0,0,0,2,2,2},
                                    {2,0,0,0,0,0,0,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,0,0,2,0,0,2,2},
                                    {2,2,0,2,2,2,0,2,2},
                                    {2,2,0,0,2,0,0,2,2},
                                    {2,2,0,2,0,2,0,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,2,0,0,0,0,2,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,0,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,0,0,0,0,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });

        stageinfo.Add(new int[9, 9]{{2,2,2,2,2,2,2,2,2},
                                    {2,0,2,2,2,0,0,2,2},
                                    {2,0,0,2,2,0,0,0,2},
                                    {2,2,0,2,0,0,0,2,2},
                                    {2,2,2,0,0,0,0,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,2,2,2},
                                    {2,0,0,2,0,2,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });
    }
}
