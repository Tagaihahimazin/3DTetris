using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class field : MonoBehaviour
{
    public List<Material> Materials_list = new List<Material>();

    // fieldサイズ
    private const int field_x = 6;
    private const int field_y = 12;
    private const int field_z = 6;

    // fieldの状態
    private const int _NON = 0;
    private const int _BLOCK = 1;
    private const int _KABE = 2;
    private const int _FLOOR = 3;
    private const int _GEN = 4;

    public GameObject field_obj;
    public GameObject GUI_obj;
    private GameObject GUI_countdown;
    public GameObject[] pieces = new GameObject[5];
    private GameObject[] pieceObj = new GameObject[6];
    private GameObject[] mino = new GameObject[6];
    private GameObject[] nono = new GameObject[6];
    private GameObject maincamera,subcamera;
    private float rotSpeed = 2.0f;
    public GameObject playerObject;
    private Vector3 defAngle;
    private Vector3 newAngle;
    private Vector3 defPos;

    public bool set_flag = true;
    public bool create_flag = false;
    int count = 0;
    public float gamestart_count = 0;

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
         *         4 _GEN   : 生成位置             *
         * --------------------------------------- */
        for (var z = 0; z < field_cube.GetLength(0); z++)
        {
            for (var y = 0; y < field_cube.GetLength(1); y++)
            {
                for (var x = 0; x < field_cube.GetLength(2); x++)
                {
                    if ((x == field_x / 2 + 1 || x == field_x / 2 + 2) &&(z == field_z / 2 + 1 || z == field_x / 2 + 2) &&(y == field_cube.GetLength(1)-1))
                    {
                        field_array[z, y, x] = _GEN;
                    }
                    else if (x == 0 || x == field_x + 1
                        || z == 0 || z == field_z + 1)
                    {
                        field_array[z, y, x] = _KABE;
                    }
                    else if (y == 0)
                    {
                        field_array[z, y, x] = _FLOOR;
                    }
                    else if(y==1&&(0<x&&x<field_x+1)&&(0<z && z < field_z + 1))
                    {
                        field_array[z, y, x] = _BLOCK;
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
         *         3 _FLOOR -> 床                  *
         *         4 _GEN   -> 生成位置            *
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
                    if (field_array[z, y, x] == _GEN)
                    {
                        field_cube[z, y, x].tag = "gen";
                    }
                    if (field_array[z, y, x] == _BLOCK)
                    {
                        field_cube[z, y, x].tag = "block";
                    }
                }
            }
        }
        create_flag = false;
        maincamera = GameObject.Find("Main Camera");
        subcamera = GameObject.Find("Sub Camera");
        //playerObject = field_cube[6,11,6].gameObject;
        defAngle = maincamera.transform.localEulerAngles;
        defPos = maincamera.transform.localPosition;

        GUI_countdown = GUI_obj.transform.Find("CountDown").gameObject;

        for (int k = 0; k < 6; k++)
        {
            pieceObj[k] = pieces[Random.Range(0, pieces.Length)];
        }
        if (create_flag == false)
        {
            Create_piece();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gamestart_count == -1)
        {
            rotateCamera();
        }
        else if (gamestart_count < 4)
        {
            if (gamestart_count < 1)
            {
                GUI_countdown.GetComponent<Text>().text = "3";
            }
            else if (gamestart_count < 2)
            {
                GUI_countdown.GetComponent<Text>().text = "2";
            }
            else if (gamestart_count < 3)
            {
                GUI_countdown.GetComponent<Text>().text = "1";
            }
            else
            {
                GUI_countdown.GetComponent<Text>().text = "START";
            }
            gamestart_count += Time.deltaTime;
        }
        else
        {
            GUI_countdown.GetComponent<Text>().enabled = false;
            GUI_obj.transform.Find("StartPanel").gameObject.GetComponent<Image>().enabled = false;
            gamestart_count = -1;
        }
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
            Destroy(GameObject.Find("ghost"));
            Destroy(self_peace.gameObject);
            //set_flag = false;
            Delete_surface();
            if (create_flag == false)
            {
                Create_piece();
            }

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
        if (count != 0)
        {
            for (int i = 0; i < 6; i++)
            {
                Destroy(mino[i]);
            }
            for (int k = 0; k < 5; k++)
            {
                pieceObj[k] = pieceObj[k + 1];
            }
            pieceObj[5] = pieces[Random.Range(0, pieces.Length)];
        }
        count++;
        int posy = -300;
        for (int k = 1; k < 6; k++)
        {
            mino[k] = GameObject.Instantiate<GameObject>(pieceObj[k], new Vector3(25, posy, 20), Quaternion.identity);
            posy -= 5;
            mino[k].name = "nextpiece";
        }
        mino[0] = GameObject.Instantiate<GameObject>(pieceObj[0], new Vector3(field_x / 2 + 1, field_y, field_z / 2 + 1), Quaternion.identity);
        mino[0].GetComponent<move_peace>().enabled = true;
        mino[0].name = "peace";
    }

    private void Delete_surface()
    {
        int x, y, z;
        int CN;     //ブロックの数計算用
        for(y = field_cube.GetLength(1)-3; y > 0; y--){
            CN = 0;
            for (x = 1; x < field_cube.GetLength(2) - 1; x++){
                for(z = 1; z < field_cube.GetLength(0) - 1; z++){
                    if (field_array[z, y, x] != _BLOCK)     break;
                    CN++;
                }

                if (CN != x * (field_cube.GetLength(0)-2))      break;
            }

            if (CN == (field_cube.GetLength(2) - 2) * (field_cube.GetLength(0) - 2)) {   //もしも、面が揃っていたら
                for(int ty = y; ty < field_cube.GetLength(1)-2; ty++){
                    for(x=1;x < field_cube.GetLength(2) - 1; x++){
                        for (z = 1; z < field_cube.GetLength(0) - 1; z++){
                            field_array[z, ty, x] = field_array[z, ty + 1, x];
                            Destroy(field_cube[z, ty, x]);
                        }
                    }
                }
                //再描画
                for (var zz = 1; zz < field_cube.GetLength(0)-1; zz++)
                {
                    for (var yy = y; yy < field_cube.GetLength(1)-2; yy++)
                    {
                        for (var xx = 1; xx < field_cube.GetLength(2)-1; xx++)
                        {
                            field_cube[zz, yy, xx] = GameObject.Instantiate<GameObject>(field_obj, new Vector3(0, 0, 0), Quaternion.identity);
                            string text = $"field_{zz}_{yy}_{xx}";
                            field_cube[zz, yy, xx].name = text;
                            field_cube[zz, yy, xx].transform.position = new Vector3(xx, yy, zz);
                            field_cube[zz, yy, xx].GetComponent<Collider>().isTrigger = true;
                            field_cube[zz, yy, xx].GetComponent<Renderer>().material = Materials_list[field_array[zz, yy, xx]];
                            if (field_array[zz, yy, xx] == _FLOOR)
                            {
                                field_cube[zz, yy, xx].tag = "floor";
                            }
                            if (field_array[zz, yy, xx] == _KABE)
                            {
                                field_cube[zz, yy, xx].tag = "kabe";
                            }
                            if (field_array[zz, yy, xx] == _NON)
                            {
                                field_cube[zz, yy, xx].tag = "non";
                            }
                            if (field_array[zz, yy, xx] == _GEN)
                            {
                                field_cube[zz, yy, xx].tag = "gen";
                            }
                            if (field_array[zz, yy, xx] == _BLOCK)
                            {
                                field_cube[zz, yy, xx].tag = "block";
                            }
                        }
                    }
                }
            }
        }
    }

    private void rotateCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスクリック開始(マウスダウン)時にカメラの角度を保持(Z軸には回転させないため).
            newAngle = defAngle;
        }
        else if (Input.GetMouseButton(0))
        {
            //Debug.Log(Input.GetAxis("Mouse X"));
            //Vector3でX,Y方向の回転の度合いを定義
            newAngle = new Vector3(Input.GetAxis("Mouse X") * rotSpeed, Input.GetAxis("Mouse Y") * rotSpeed * -1, 0);

            //transform.RotateAround()をしようしてメインカメラを回転させる
            maincamera.transform.RotateAround(playerObject.transform.position, Vector3.up, newAngle.x);
            maincamera.transform.RotateAround(playerObject.transform.position, transform.right, newAngle.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //transform.RotateAround()をしようしてメインカメラを回転させる
            maincamera.transform.rotation = Quaternion.Euler(defAngle);
            maincamera.transform.position = defPos;
        }
    }
}

