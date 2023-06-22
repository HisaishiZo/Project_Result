using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle,
    Walk,
}

public class AIMove : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private States state;    // 상태
    private bool isIdlestate;
    private bool isWalkState;
    private bool isMove;
    private bool isRotate;

    [Header("Speed")]
    [SerializeField] private float SPEED_MIN;    // 속력
    [SerializeField] private float SPEED_MAX;    // 속력
    [Range(0.0f, 100.0f)]
    [SerializeField] private float speed;

    private float probability;
    private Quaternion rotation;

    [Header("Time")]
    [SerializeField] private float DURATION_MIN; // 시간
    [SerializeField] private float DURATION_MAX; // 시간
    [Range(0.0f, 10.0f)]
    [SerializeField] private float duration;

    void Start()
    {
        StartCoroutine(FSM());
    }

    private IEnumerator FSM()
    {
        while (true)
        {
            switch (state)
            {
                case States.Idle:
                    break;

                case States.Walk:
                    if (isWalkState == false)
                    {
                        isWalkState = true;
                        OnTransferWalkEnter();
                        StartCoroutine(Walk(speed, rotation, duration));
                    }
                    break;

                default:
                    break;
            }

            yield return null;
        }
    }

    private void OnTransferWalkEnter()
    {
        speed = Random.Range(SPEED_MIN, SPEED_MAX);
        rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        duration = Random.Range(DURATION_MIN, DURATION_MAX);
    }

    private IEnumerator Walk(float speed, Quaternion rotation, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;

             rigidbody.velocity = (new Vector3(1f, 0f, 1f) * speed);

            // probability = 20%
            if (isRotate == false)
            {
                probability = Random.Range(0, 10);
                if ((probability == 0) || (probability == 1))
                {
                    isRotate = true;
                    StartCoroutine(Rotate(rotation));
                }
            }

            yield return null;
        }
    }

    private IEnumerator Rotate(Quaternion rotation)
    {
        while(true)
        {
            rigidbody.MoveRotation(rotation);
            yield return null;
        }

        isRotate = false;
        yield break;
    }

    private void TransferStateToIdle()
    {

    }

    private void TransferStateToWalk()
    {

    }
}
