using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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


    // Start is called before the first frame update
    void Start()
    {
        StageGen();
        ShowStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScenes == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ShowSelect();
                UnityEngine.Debug.Log("Return key was pressed.");
            }
        }
        if (currentScenes == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
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
                    selectUpStage.GetComponent<Image>().sprite = spriteimages[(spriteimages.Length / 2 )];
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
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
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
                    selectUpStage.GetComponent<Image>().sprite = spriteimages[(spriteimages.Length / 2)];
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                int[,] stagedata = stageinfo[currentStage];
                UnityEngine.Debug.Log("return");
                UnityEngine.Debug.Log(stagedata);
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
                                    {2,2,2,0,2,0,2,2,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,0,0,0,0,0,0,0,2},
                                    {2,2,0,0,0,0,0,2,2},
                                    {2,2,2,0,0,0,2,2,2},
                                    {2,2,2,2,0,2,2,2,2},
                                    {2,2,2,2,2,2,2,2,2} });
    }
}
