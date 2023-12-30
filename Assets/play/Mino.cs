using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mino : MonoBehaviour
{
    public float previousTime;

    //�����鎞��
    public float fallTime = 1f;

    //�X�e�[�W�T�C�Y
    private static int width = 10;
    private static int height = 20;

    //��]
    public Vector3 rotationPoint;


    private static Transform[,] grid = new Transform[width, height];
    void Update()
    {
        MinoMovememt();
    }
    private void MinoMovememt()
    {
        //���ړ�����
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            //���ړ�����
            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        //�E�ړ�����
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            //�E�ړ�����
            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        //���������Ɖ����L�[���͂ɂ�闎���X�s�[�h�㏸����
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - previousTime >= fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            //���ړ�����
            if (!ValidMovement())
            {
                transform.position -= new Vector3(0, -1, 0);
                //�~�m�̐ςݏグ
                AddToGrid();
                //�~�m���C����������
                CheckLines();
                //�V�����~�m����
                this.enabled = false;
                FindObjectOfType<SpawnMino>().NewMino();
            }
            previousTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �u���b�N�̉�]
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            //�E�ǒ��˕Ԃ�
            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0); 
                if (!ValidMovement())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
            //���ǒ��˕Ԃ�
            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        }

//�~�m���C����������
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
//��1�񂻂���Ă��邩
bool HasLine(int i)
{
    for (int j = 0; j < width; j++)
    {
        if (grid[j, i] == null)
            return false;
    }
    //�X�R�A���Z
    FindObjectOfType<GameManagement>().AddScore();
    return true;
}
//���C��������
void DeleteLine(int i)
{
    for (int j = 0; j < width; j++)
    {
        Destroy(grid[j, i].gameObject);
        grid[j, i] = null;
    }
}
//���������
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
//�~�m�̐ςݏグ
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
//�ړ��͈͐���
bool ValidMovement()
{
    foreach (Transform children in transform)
    {
        int roundX = Mathf.RoundToInt(children.transform.position.x);
        int roundY = Mathf.RoundToInt(children.transform.position.y);

        //�X�e�[�W�O�ɏo�Ȃ��悤�ɐ���
        if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
        {
            return false;
        }
        //�ق��̃~�m�ɂ߂荞�܂Ȃ��悤�ɐ���
        if (grid[roundX, roundY] != null)
        {
            return false;
        }
    }
    return true;
}

}