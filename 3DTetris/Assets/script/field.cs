using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class field : MonoBehaviour
{
    public List<Material> Materials_list = new List<Material>();

    // fieldサイズ
    private const int field_x = 5;
    private const int field_y = 10;
    private const int field_z = 5;

    // fieldの状態
    private const int _NON = 0;
    private const int _BLOCK = 1;
    private const int _KABE = 2;


    public GameObject[, ,] field_cube = new GameObject[field_z+2, field_y+2, field_x+2];
    public int[, ,] field_array = new int[field_z+2, field_y+2, field_x+2];

    // Start is called before the first frame update
    void Start()
    {
        /* --------------------------------------- *
         *         fieldを配列で生成               *
         *                                         *
         *         0 _NON   : 何もなし             *
         *         1 _BLOCK : 配置済みブロック     *
         *         2 _KABE  : 壁                   *
         * --------------------------------------- */
        for (var z = 0; z < field_cube.GetLength(0); z++)
        {
            for (var y = 0; y < field_cube.GetLength(1); y++)
            {
                for (var x = 0; x < field_cube.GetLength(2); x++)
                {
                    if (x == 0 || x == field_x + 1
                        || y == 0 || y == field_y + 1
                        || z == 0 || z == field_z + 1)
                    {
                        field_array[z, y, x] = _KABE;
                    }
                    else
                    {
                        field_array[z, y, x] = _NON;
                    }

                }
            }
        }

        /* --------------------------------------- *
         *         配列からfield生成               *
         *         cube name：field_z_y_x          *
         *                                         *
         *         0 _NON   -> 何もなし            *
         *         1 _BLOCK -> 配置済みブロック    *
         *         2 _KABE  -> 壁                  *
         * --------------------------------------- */
        for (var z = 0; z < field_cube.GetLength(0); z++)
        {
            for (var y = 0; y < field_cube.GetLength(1); y++)
            {
                for (var x = 0; x < field_cube.GetLength(2); x++)
                {
                    field_cube[z, y, x] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    string text = $"field_{z}_{y}_{x}";
                    field_cube[z, y, x].name = text;
                    field_cube[z, y, x].transform.position = new Vector3(x, y, z);
                    field_cube[z, y, x].GetComponent<Collider>().isTrigger = true;
                    field_cube[z, y, x].GetComponent<Renderer>().material = Materials_list[field_array[z,y,x]];

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_cube(GameObject self_peace)
    {
        foreach (Transform child in self_peace.transform)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);
            int z = Mathf.RoundToInt(child.transform.position.z);
            GameObject change_cube = GameObject.Find($"field_{z}_{y}_{x}");
            field_array[z, y, x] = _BLOCK;
            Debug.Log($"{change_cube.transform.position}&{z},{y},{x}&{child.transform.position}");
            change_cube.GetComponent<Renderer>().material = Materials_list[field_array[z, y, x]];
            change_cube.GetComponent<Collider>().isTrigger = false;
        }
        Destroy(self_peace.gameObject);
    }
    private void test(GameObject[, ,] testtest)
    {
        foreach (GameObject i in testtest)
        {
            Debug.Log(i.transform.position);
        }
        for (var z = 0; z < field_cube.GetLength(0); z++)
        {
            for (var y = 0; y < field_cube.GetLength(1); y++)
            {
                for (var x = 0; x < field_cube.GetLength(2); x++)
                {
                    Debug.Log(field_cube[z, y, x].transform.position);
                }
            }
        }
    }
}
