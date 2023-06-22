using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // 오브젝트를 자식으로 만들 카메라를 참조할 변수
    private GameObject objectToParent; 

    private void Start()
    {
        // 메인 카메라를 오브젝트의 자식으로 설정
        objectToParent = GameObject.Find("Cube");
        Camera.main.transform.parent = objectToParent.transform;
    }
}