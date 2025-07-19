using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Random Color
public class BaseMapColorChanger : MonoBehaviour
{
    private Renderer[] renderers;

    void Start()
    {
        // ���١� ����� Renderer ������
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // ���ͺ: �� C ��������¹ Albedo �բͧ�١ �
        if (UnityEngine.Input.GetKeyDown(KeyCode.C))
        {
            ChangeAlbedoColors();
        }
    }

    void ChangeAlbedoColors()
    {
        foreach (Renderer rend in renderers)
        {
            // ���� Material ��������з� Material ��ѡ� Project
            Material mat = rend.material;

            // ����¹ Albedo Color ��������
            mat.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.6f, 1f); // ������
        }

        Debug.Log("Changed Albedo Colors!");
    }
}

