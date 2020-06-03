using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class field : MonoBehaviour
{
    public List<Material> Materials_list = new List<Material>();

    // fieldサイズ
    private const int field_x = 10;
    private const int field_y = 20;
    private const int field_z = 10;

    // fieldの状態
    private const int _NON = 0;
    private const int _BLOCK = 1;
    private const int _KABE = 2;
    private const int _FLOOR = 3;

    public GameObject field_obj;
    public GameObject peace_obj;

    public bool set_flag = true;

    public GameObject[,,] field_cube = new GameObject[field_z + 2, field_y + 2, field_x + 2];
    public int[,,] field_array = new int[field_z + 2, field_y + 2, field_x + 2];

    // Start is called before the first frame update
    void Start()
    {
        /* --------------------------------------- *
         *         fieldを配列で生成               *
         *                                         *
         *         0 _NON   : 何もなし             *
         *         1 _BLOCK : 配置済みブロック     *
         *         2 _KABE  : 壁                   *
         *         3 _FLOOR : 床                   *
         * --------------------------------------- */
        for (var z = 0; z < field_cube.GetLength(0); z++)
        {
            for (var y = 0; y < field_cube.GetLength(1); y++)
            {
                for (var x = 0; x < field_cube.GetLength(2); x++)
                {
                    if (x == 0 || x == field_x + 1
                        || z == 0 || z == field_z + 1)
                    {
                        field_array[z, y, x] = _KABE;
                    }
                    else if (y == 0)
                    {
                        field_array[z, y, x] = _FLOOR;
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
         *         3 _FLOOR -> 床                   *
         * --------------------------------------- */
        for (var z = 0; z < field_cube.GetLength(0); z++)
        {
            for (var y = 0; y < field_cube.GetLength(1); y++)
            {
                for (var x = 0; x < field_cube.GetLength(2); x++)
                {
                    field_cube[z, y, x] = GameObject.Instantiate<GameObject>(field_obj, new Vector3(0, 0, 0), Quaternion.identity);
                    string text = $"field_{z}_{y}_{x}";
                    field_cube[z, y, x].name = text;
                    field_cube[z, y, x].transform.position = new Vector3(x, y, z);
                    field_cube[z, y, x].GetComponent<Collider>().isTrigger = true;
                    field_cube[z, y, x].GetComponent<Renderer>().material = Materials_list[field_array[z, y, x]];
                    if (field_array[z, y, x] == _FLOOR)
                    {
                        field_cube[z, y, x].tag = "floor";
                    }
                    if (field_array[z, y, x] == _KABE)
                    {
                        field_cube[z, y, x].tag = "kabe";
                    }
                    if (field_array[z, y, x] == _NON)
                    {
                        field_cube[z, y, x].tag = "non";
                    }
                }
            }
        }

        Create_piece();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_cube(GameObject self_peace)
    {
        if (set_flag == true)
        {
            set_flag = false;
            foreach (Transform child in self_peace.transform)
            {
                int x = Mathf.RoundToInt(child.transform.position.x);
                int y = Mathf.RoundToInt(child.transform.position.y);
                int z = Mathf.RoundToInt(child.transform.position.z);
                if (x >= field_x) x = field_x;
                if (y >= field_y) y = field_y;
                if (z >= field_z) z = field_z;
                GameObject change_cube = GameObject.Find($"field_{z}_{y}_{x}");
                field_array[z, y, x] = _BLOCK;
                //Debug.Log($"{change_cube.transform.position}&{z},{y},{x}&{child.transform.position}");
                change_cube.GetComponent<Renderer>().material = child.GetComponent<Renderer>().material;
                change_cube.tag = "block";
            }
            GameObject[] dummy_Objects = GameObject.FindGameObjectsWithTag("dummy");

            for (int i = 0; i < dummy_Objects.Length; i++)
            {
                //Debug.Log("削除");
                Destroy(dummy_Objects[i]);
            }
            Destroy(self_peace.gameObject);
            //set_flag = false;
            Create_piece();

        }
    }
    private void test(GameObject[,,] testtest)
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

    public int Get(string name)
    {
        if (name == "_BLOCK") { return _BLOCK; }
        if (name == "_FLOOR") { return _FLOOR; }
        if (name == "_KABE") { return _KABE; }
        if (name == "_NON") { return _NON; }

        return -1;
    }

    private void Create_piece()
    {
        GameObject obj = GameObject.Instantiate<GameObject>(peace_obj, new Vector3(3, 10, 3), Quaternion.identity);
        obj.name = "peace";
    }
}