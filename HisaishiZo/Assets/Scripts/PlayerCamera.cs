using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // ������Ʈ�� �ڽ����� ���� ī�޶� ������ ����
    private GameObject objectToParent; 

    private void Start()
    {
        // ���� ī�޶� ������Ʈ�� �ڽ����� ����
        objectToParent = GameObject.Find("Cube");
        Camera.main.transform.parent = objectToParent.transform;
    }
}