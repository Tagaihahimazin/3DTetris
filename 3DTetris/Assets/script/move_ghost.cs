using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_ghost : MonoBehaviour
{
    public bool move_flag;
    public List<Material> Materials_list = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        move_flag = false;
        foreach (Transform child in this.gameObject.transform)
        {
            child.GetComponent<Renderer>().material = Materials_list[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(move_flag != true)
        {
            this.gameObject.transform.position += Vector3.down;
        }
    }
}
