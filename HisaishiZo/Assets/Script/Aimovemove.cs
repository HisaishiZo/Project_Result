using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Aimovemove : MonoBehaviour
{
    public Transform pr;
    private float randx, randz;
    public bool isMove;
    private float timer, interval;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        isMove = true;
        interval = Random.Range(1f, 2f);
        target = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(isMove)
        {
            Move();
        }
        else
        {
            Rotate();
        }

        if(isMove && timer >= interval)
        {
            randx = pr.transform.position.x + Random.Range(-5f, 5f);
            randz = pr.transform.position.z + Random.Range(-5f, 5f);
            target = new Vector3(randx, 0, randz);
            isMove = false;
            timer = 0;
        }

        if (!isMove && timer >= interval)
        {
            isMove = true;
            interval = Random.Range(1f, 2f);
            timer = 0;
        }
    }

    void Move()
    {
        transform.position = Vector3.Lerp(pr.position, target, 0.003f);
    }

    void Rotate()
    {
        Vector3 dir = target - pr.transform.position;
        dir.y = 0f;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        // 현재 회전 값을 목표 회전 값으로 부드럽게 전환합니다.
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.005f);
    }
}
