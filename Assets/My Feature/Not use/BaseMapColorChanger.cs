using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Random Color
public class BaseMapColorChanger : MonoBehaviour
{
    private Renderer[] renderers;

    void Start()
    {
        // หาลูกๆ ที่มี Renderer ทั้งหมด
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // ทดสอบ: กด C เพื่อเปลี่ยน Albedo สีของลูก ๆ
        if (UnityEngine.Input.GetKeyDown(KeyCode.C))
        {
            ChangeAlbedoColors();
        }
    }

    void ChangeAlbedoColors()
    {
        foreach (Renderer rend in renderers)
        {
            // สำเนา Material เพื่อไม่กระทบ Material หลักใน Project
            Material mat = rend.material;

            // เปลี่ยน Albedo Color เป็นสีสุ่ม
            mat.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.6f, 1f); // สีสวยๆ
        }

        Debug.Log("Changed Albedo Colors!");
    }
}

