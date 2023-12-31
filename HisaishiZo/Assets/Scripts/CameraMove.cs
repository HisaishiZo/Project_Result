using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    private float rotateSpeed = 1500.0f;

    void Update()
    {
        if (Input.GetMouseButton(0)) // 클릭한 경우
        {
             yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;

            transform.eulerAngles = new Vector3(0, yRotate, 0);
        }
    }
}