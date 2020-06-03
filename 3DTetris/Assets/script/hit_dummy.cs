using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_dummy : MonoBehaviour
{
    public GameObject field_obj;

    private field field_script;

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
        //if (field_script.set_flag != false)
        {
            if (gameObject.tag == "floor" || gameObject.tag == "block")
            {
                Debug.Log(collider.gameObject.tag);
                if (collider.gameObject.tag == "dummy0")
                {
                     field_script.set_cube(GameObject.Find("peace"));
                }
            }
        }
    }
}
