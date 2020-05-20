using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field : MonoBehaviour
{
    public Material set_material;
    private const int field_x = 5;
    private const int field_y = 10;
    private const int field_z = 5;

    public GameObject[, ,] cube = new GameObject[field_z, field_y, field_x];

    // Start is called before the first frame update
    void Start()
    {
        for (var z = 0; z < field_z; z++)
        {
            for (var y = 0; y < field_y; y++)
            {
                for (var x = 0; x < field_x; x++)
                {
                    cube[z,y,x] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube[z, y, x].transform.position = new Vector3(x, y, z);
                    cube[z, y, x].GetComponent<Renderer>().material = set_material;
                    cube[z, y, x].GetComponent<Collider>().isTrigger = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
