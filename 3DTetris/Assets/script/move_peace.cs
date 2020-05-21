using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class move_peace : MonoBehaviour
{
    private float timeElapsed;
    private bool move_flag = false;
    private Vector3 Rotation;
    private float peace_speed;
    public GameObject floor;
    public GameObject script;

    // Start is called before the first frame update
    void Start()
    {
        peace_speed = 1.0f;
       
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Rotation = this.gameObject.transform.eulerAngles;
        if (move_flag == false)
        {
            if (timeElapsed * peace_speed >= 2)
            {
                this.gameObject.transform.position += Vector3.down;
                timeElapsed = 0.0f;
            }

            /* ------------------------------ *
             *      キー入力処理   回転       *
             * ------------------------------ */
            if (Input.GetKeyDown(KeyCode.A))                // Aキー:左に回転
            {
                Debug.Log("A");
                this.gameObject.transform.Rotate(Vector3.forward * 90, Space.World);
            }
            else if (Input.GetKeyDown(KeyCode.W))           // Wキー:奥に回転
            {
                Debug.Log("W");
                this.gameObject.transform.Rotate(Vector3.right * 90, Space.World);
            }
            else if (Input.GetKeyDown(KeyCode.S))           // Sキー:手前に回転
            {
                Debug.Log("S");
                this.gameObject.transform.Rotate(Vector3.left * 90, Space.World);
            }
            else if (Input.GetKeyDown(KeyCode.D))           // Dキー:右に回転
            {
                Debug.Log("D");
                this.gameObject.transform.Rotate(Vector3.back * 90, Space.World);
            }

            /* ------------------------------ *
             *      キー入力処理   移動       *
             * ------------------------------ */
            if (Input.GetKeyDown(KeyCode.LeftArrow))        // ←キー:左に移動
            {
                Debug.Log("←");
                this.gameObject.transform.position += Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))     // ↑キー:奥に移動
            {
                Debug.Log("↑");
                this.gameObject.transform.position += Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))  // →キー:右に移動
            {
                Debug.Log("→");
                this.gameObject.transform.position += Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))   // ↓キー:手前に移動
            {
                Debug.Log("↓");
                this.gameObject.transform.position += Vector3.back;
            }

            /* ------------------------------ *
             *      キー入力処理   スピード   *
             * ------------------------------ */
            if (Input.GetKey(KeyCode.Space))                // spaceキーを押している間:スピードアップ
            {
                Debug.Log("space now");
                peace_speed = 15.0f;
            }
            
            if (Input.GetKeyUp(KeyCode.Space))              // spaceキーを離す:通常スピード
            {
                Debug.Log("space up");
                peace_speed = 1.0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == floor.name)
        {
            move_flag = true;
            //this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //foreach (Transform child in this.gameObject.transform)
            //{
            //    //Debug.Log(child.name + child.transform.position);
            //    script.GetComponent<field>().set_cube(this.gameObject , child.transform.position);
            //}
            script.gameObject.GetComponent<field>().set_cube(this.gameObject);
            
        }
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