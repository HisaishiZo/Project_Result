using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class characterMove : MonoBehaviour
{
    GameObject player;
    private float time = 90;
    private float timer = 0;
    private bool isMove = false;
    private int result;
    private int speed = 5;
    private Rigidbody rigd;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        moving();
    }



    void moving()
    {
        result = Random.Range(0, 4);
        print(result);
        if (result == 0)//¾Õ
        {
            player.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (result == 1)//¿À¸¥
        {
            player.transform.Rotate(Vector3.right*Time.deltaTime * speed);
        }
        else if (result == 2)//¿Þ
        {
            player.transform.Rotate(Vector3.left * Time.deltaTime * speed);
        }
        else if (result == 3)//µÚ
        {
            player.transform.Rotate(Vector3.back * Time.deltaTime * speed);
        }

    }
}