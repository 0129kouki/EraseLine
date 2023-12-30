using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float power = 1;
    //public float powery = 1;
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //回転処理
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody.angularVelocity = new Vector3(0,0,90);
        }
        //左移動処理
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector3(-1, 0, 0) * power);
        }
        //右移動処理
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector3(1, 0, 0) * power);
        }
        //落下スピード上昇処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0, -1, 0) * power);
        }
        //下移動処理
        rigidbody.AddForce(new Vector3(0, -1, 0));
    }
}
