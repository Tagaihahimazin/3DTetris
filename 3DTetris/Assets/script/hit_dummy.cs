using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hit_dummy : MonoBehaviour
{
    private GameObject field_obj;
    public GameObject piece_obj;
    public GameObject ghost_obj;
    public GameObject gameover_canvas_obj;

    private field field_script;
    private move_peace piece_script;
    private move_ghost ghost_script;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.gameObject.tag);
        field_obj = GameObject.Find("GameObject");
        field_script = field_obj.GetComponent<field>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        piece_obj = GameObject.Find("peace"); //変数名変えよう
        ghost_obj = GameObject.Find("ghost");
        piece_script = piece_obj.GetComponent<move_peace>();
        if (ghost_obj != null) {
            ghost_script = ghost_obj.GetComponent<move_ghost>();
        }
        //if (field_script.set_flag != false)
        {
            if (gameObject.tag == "floor" || gameObject.tag == "block")
            {
                //Debug.Log(collider.gameObject.tag);
                if (collider.gameObject.tag == "dummy0")
                {
                    piece_script.move_flag = true;
                    field_script.set_cube(GameObject.Find("peace"));
                }
                if (ghost_obj != null)
                {
                    if (collider.gameObject.tag == "ghost" && ghost_script.move_flag == false)
                    {
                        foreach (Transform child in ghost_obj.transform)
                        {
                            child.GetComponent<Renderer>().material = ghost_script.Materials_list[0];
                        }
                        ghost_script.move_flag = true;
                        ghost_obj.transform.position += Vector3.up;
                    }
                }
            } else if (gameObject.tag == "kabe")
            {
                if (collider.gameObject.tag == "dummy1")
                {
                    //Debug.Log("あたった");
                    piece_script.right_move_flag = true;
                }
                if (collider.gameObject.tag == "dummy2")
                {
                    piece_script.left_move_flag = true;
                }
                if (collider.gameObject.tag == "dummy3")
                {
                    //Debug.Log("あたった");
                    piece_script.back_move_flag = true;
                }
                if (collider.gameObject.tag == "dummy4")
                {
                    piece_script.forward_move_flag = true;
                }
                if (collider.gameObject.tag == "dummy5")
                {
                    piece_script.left_rot_flag = true;
                }
                if (collider.gameObject.tag == "dummy6")
                {
                    piece_script.back_rot_flag = true;
                }
                if (collider.gameObject.tag == "dummy7")
                {
                    piece_script.forward_rot_flag = true;
                }
                if (collider.gameObject.tag == "dummy8")
                {
                    piece_script.right_rot_flag = true;
                }
            }
            else if (gameObject.tag == "gen")
            {
                if (collider.gameObject.tag == "piece")
                {
                    Debug.Log("gen 通過");
                    if (piece_script.move_flag == true)
                    {
                        if (field_script.create_flag == false) {
                            field_script.create_flag = true;
                            Debug.Log("Gameover");
                            SceneManager.LoadScene("GameOver");
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (gameObject.tag != "non")
        {
            if (collider.gameObject.tag == "dummy1")
            {
                piece_script.right_move_flag = false;
            }
            if (collider.gameObject.tag == "dummy2")
            {
                piece_script.left_move_flag = false;
            }
            if (collider.gameObject.tag == "dummy3")
            {
                piece_script.back_move_flag = false;
            }
            if (collider.gameObject.tag == "dummy4")
            {
                piece_script.forward_move_flag = false;
            }
            if (collider.gameObject.tag == "dummy5")
            {
                piece_script.left_rot_flag = false;
            }
            if (collider.gameObject.tag == "dummy6")
            {
                piece_script.back_rot_flag = false;
            }
            if (collider.gameObject.tag == "dummy7")
            {
                piece_script.forward_rot_flag = false;
            }
            if (collider.gameObject.tag == "dummy8")
            {
                piece_script.right_rot_flag = false;
            }
        }
    }
}
