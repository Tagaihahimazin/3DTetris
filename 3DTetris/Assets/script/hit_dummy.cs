using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_dummy : MonoBehaviour
{
    public GameObject field_obj;
    public GameObject piece_obj;


    private field field_script;
    private move_peace piece_script;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.gameObject.tag);
        field_script = field_obj.GetComponent<field>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        piece_obj = GameObject.Find("peace"); //変数名変えよう
        piece_script = piece_obj.GetComponent<move_peace>();
        //if (field_script.set_flag != false)
        {
            if (gameObject.tag == "floor" || gameObject.tag == "block")
            {
                Debug.Log(collider.gameObject.tag);
                if (collider.gameObject.tag == "dummy0")
                {
                     field_script.set_cube(GameObject.Find("peace"));
                }
            } else if (gameObject.tag == "kabe")
            {
                if (collider.gameObject.tag == "dummy1")
                {
                    //Debug.Log("あたった");
                    piece_script.right_move_flag = true;
                }
                //if (collider.gameObject.tag == "dummy2")
                //{
                //    piece_script.left_move_flag = true;
                //}
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (gameObject.tag == "kabe")
        {
            if (collider.gameObject.tag == "dummy1")
            {
                piece_script.right_move_flag = false;
            }
            //if (collider.gameObject.tag == "dummy2")
            //{
            //    piece_script.left_move_flag = false;
            //}
        }
    }
}
