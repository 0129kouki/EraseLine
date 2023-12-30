using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mino : MonoBehaviour
{
    public float previousTime;

    //落ちる時間
    public float fallTime = 1f;

    //ステージサイズ
    private static int width = 10;
    private static int height = 20;

    //回転
    public Vector3 rotationPoint;


    private static Transform[,] grid = new Transform[width, height];
    void Update()
    {
        MinoMovememt();
    }
    private void MinoMovememt()
    {
        //左移動処理
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            //左移動制限
            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        //右移動処理
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            //右移動制限
            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        //自動落下と下矢印キー入力による落下スピード上昇処理
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - previousTime >= fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            //下移動制限
            if (!ValidMovement())
            {
                transform.position -= new Vector3(0, -1, 0);
                //ミノの積み上げ
                AddToGrid();
                //ミノライン消し処理
                CheckLines();
                //新しいミノ生成
                this.enabled = false;
                FindObjectOfType<SpawnMino>().NewMino();
            }
            previousTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ブロックの回転
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            //右壁跳ね返し
            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0); 
                if (!ValidMovement())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
            //左壁跳ね返し
            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        }

//ミノライン消し処理
public void CheckLines()
{
    for (int i = height - 1; i >= 0; i--)
    {
        if (HasLine(i))
        {
            DeleteLine(i);
            RowDown(i);
        }
    }
}
//横1列そろっているか
bool HasLine(int i)
{
    for (int j = 0; j < width; j++)
    {
        if (grid[j, i] == null)
            return false;
    }
    //スコア加算
    FindObjectOfType<GameManagement>().AddScore();
    return true;
}
//ラインを消す
void DeleteLine(int i)
{
    for (int j = 0; j < width; j++)
    {
        Destroy(grid[j, i].gameObject);
        grid[j, i] = null;
    }
}
//列を下げる
public void RowDown(int i)
{
    for (int y = i; y < height; y++)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, y] != null)
            {
                grid[j, y - 1] = grid[j, y];
                grid[j, y] = null;
                grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
            }
        }
    }
}
//ミノの積み上げ
void AddToGrid()
{
    foreach (Transform children in transform)
    {
        int roundX = Mathf.RoundToInt(children.transform.position.x);
        int roundY = Mathf.RoundToInt(children.transform.position.y);

        Debug.Log(roundX + " " + roundY);

        grid[roundX, roundY] = children;

        if (roundY >= height - 1)
        {
            FindObjectOfType<GameManagement>().GameOver();
        }
    }
}
//移動範囲制限
bool ValidMovement()
{
    foreach (Transform children in transform)
    {
        int roundX = Mathf.RoundToInt(children.transform.position.x);
        int roundY = Mathf.RoundToInt(children.transform.position.y);

        //ステージ外に出ないように制限
        if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
        {
            return false;
        }
        //ほかのミノにめり込まないように制限
        if (grid[roundX, roundY] != null)
        {
            return false;
        }
    }
    return true;
}

}