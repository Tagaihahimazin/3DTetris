using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class move_peace : MonoBehaviour
{
    private float timeElapsed;
    public bool move_flag;
    public bool right_move_flag;
    public bool left_move_flag;
    public bool forward_move_flag;
    public bool back_move_flag;
    public bool forward_rot_flag;
    public bool right_rot_flag;
    public bool left_rot_flag;
    public bool back_rot_flag;

    private Vector3 Rotation;
    private float peace_speed;
    public GameObject script;
    public GameObject dummy_piece;
    public GameObject ghost_piece;

    private field field_script;

    public static GameObject Ghost_piece;
        
    GameObject[] dummy = new GameObject[9];
    private Vector3[] dummy_pos = { Vector3.down,
                                    Vector3.right,
                                    Vector3.left,
                                    Vector3.forward,
                                    Vector3.back,
                                    Vector3.zero,
                                    Vector3.zero,
                                    Vector3.zero,
                                    Vector3.zero     };

    private Vector3[] dummy_rot = { Vector3.zero,
                                    Vector3.zero,
                                    Vector3.zero,
                                    Vector3.zero,
                                    Vector3.zero,
                                    Vector3.forward * 90,
                                    Vector3.right * 90,
                                    Vector3.left * 90,
                                    Vector3.back * 90 };

    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("GameObject");
        field_script = script.GetComponent<field>();
        //field_script = GameObject.Find("Game Object");
        //field_script.set_flag = true;
        peace_speed = 1.0f;
        for (int i = 0; i < 9; i++)
        {   
            dummy[i] = Instantiate(dummy_piece, this.gameObject.transform.position + dummy_pos[i], Quaternion.Euler(dummy_rot[i]));
            dummy[i].name = "dummy" + i;
            //if (i == 0)
            //{
            foreach (Transform child in dummy[i].transform)
            {
                child.tag = "dummy" + i;
            }
            //}
            //dummy[i].tag = "dummy";
        }
        Ghost_piece = Instantiate(ghost_piece, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Ghost_piece.name = "ghost";
        field_script.set_flag = true;
        move_flag = false;
        right_move_flag = false;
        left_move_flag = false;
        forward_move_flag = false;
        back_move_flag = false;
        forward_rot_flag = false;
        right_rot_flag = false;
        left_rot_flag = false;
        back_rot_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Rotation = this.gameObject.transform.eulerAngles;

        if (field_script.gamestart_count == -1)
        {
            if (move_flag == false)
            {
                //Debug.Log(this.right_move_flag);
                if (timeElapsed * peace_speed >= 2)
                {
                    //Debug.Log(right_move_flag);
                    this.gameObject.transform.position += Vector3.down;
                    for (int i = 0; i < 9; i++)
                    {
                        GameObject.Find("dummy" + i).transform.position += Vector3.down;
                    }

                    timeElapsed = 0.0f;
                }

                /* ------------------------------ *
                 *      キー入力処理   回転       *
                 * ------------------------------ */
                if (Input.GetKeyDown(KeyCode.Q))                // Qキー:左に回転
                {
                    if (left_rot_flag == false)
                    {
                        Debug.Log("Q");
                        this.gameObject.transform.Rotate(Vector3.forward * 90, Space.World);
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.Rotate(Vector3.forward * 90, Space.World);
                        }
                        move_ghost_reset();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.R))           // Rキー:奥に回転
                {
                    if (back_rot_flag == false)
                    {
                        Debug.Log("R");
                        this.gameObject.transform.Rotate(Vector3.right * 90, Space.World);
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.Rotate(Vector3.right * 90, Space.World);
                        }
                        move_ghost_reset();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.F))           // Fキー:手前に回転
                {
                    if (forward_rot_flag == false)
                    {
                        Debug.Log("F");
                        this.gameObject.transform.Rotate(Vector3.left * 90, Space.World);
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.Rotate(Vector3.left * 90, Space.World);
                        }
                        move_ghost_reset();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))           // Eキー:右に回転
                {
                    if (right_rot_flag == false)
                    {
                        Debug.Log("E");
                        this.gameObject.transform.Rotate(Vector3.back * 90, Space.World);
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.Rotate(Vector3.back * 90, Space.World);
                        }
                        move_ghost_reset();
                    }
                }

                /* ------------------------------ *
                 *      キー入力処理   移動       *
                 * ------------------------------ */
                if (Input.GetKeyDown(KeyCode.A))        // Aキー:左に移動
                {
                    if (left_move_flag == false)
                    {
                        Debug.Log("←");
                        this.gameObject.transform.position += Vector3.left;
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.position += Vector3.left;
                        }
                        move_ghost_reset();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.W))     // Wキー:奥に移動
                {
                    if (back_move_flag == false)
                    {
                        Debug.Log("↑");
                        this.gameObject.transform.position += Vector3.forward;
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.position += Vector3.forward;
                        }
                        move_ghost_reset();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))  // Dキー:右に移動
                {
                    if (right_move_flag == false)
                    {
                        Debug.Log("→");
                        this.gameObject.transform.position += Vector3.right;
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.position += Vector3.right;
                        }
                        move_ghost_reset();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))   // Sキー:手前に移動
                {
                    if (forward_move_flag == false)
                    {
                        Debug.Log("↓");
                        this.gameObject.transform.position += Vector3.back;
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject.Find("dummy" + i).transform.position += Vector3.back;
                        }
                        move_ghost_reset();
                    }
                }

                /* ------------------------------ *
                 *      キー入力処理   スピード   *
                 * ------------------------------ */
                if (Input.GetKey(KeyCode.Space))                // spaceキーを押している間:スピードアップ
                {
                    //Debug.Log("space now");
                    peace_speed = 15.0f;
                }

                if (Input.GetKeyUp(KeyCode.Space))              // spaceキーを離す:通常スピード
                {
                    //Debug.Log("space up");
                    peace_speed = 1.0f;
                }
            }
        }
    }

    void move_ghost_reset()
    {
        Ghost_piece.transform.position = this.gameObject.transform.position;
        Ghost_piece.transform.rotation = this.gameObject.transform.rotation;
        Ghost_piece.GetComponent<move_ghost>().move_flag = false;
        foreach (Transform child in Ghost_piece.transform)
        {
            child.GetComponent<Renderer>().material = Ghost_piece.GetComponent<move_ghost>().Materials_list[1];
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == floor.name)
        //{
        //    move_flag = true;
        //    for (int i = 0; i < 9; i++)
        //    {
        //        Destroy(GameObject.Find("dummy" + i));
        //    }
        //    script.gameObject.GetComponent<field>().set_cube(this.gameObject);
        //}
    }  
    void OnCollisionStay(Collision collision)
    {
        //move_flag = true;
        //Debug.Log(collision.gameObject.name + "Stay"); // ログを表示する
    }
    void OnCollisionExit(Collision collision)
    {
        //move_flag = true;
        //Debug.Log(collision.gameObject.name + "Exit"); // ログを表示する
    }
}