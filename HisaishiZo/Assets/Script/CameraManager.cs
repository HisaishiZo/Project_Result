using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject objectToParent; // ������Ʈ�� �ڽ����� ���� ī�޶� ������ ����

    private void Start()
    {
        // ���� ī�޶� ������Ʈ�� �ڽ����� ����
        objectToParent = GameObject.Find("Cube");
        Camera.main.transform.parent = objectToParent.transform;
    }
}
