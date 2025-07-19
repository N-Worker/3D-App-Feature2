using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeToggle : MonoBehaviour
{
    public Material wireframeMaterial;

    private Material[][] originalMaterials;  // เปลี่ยนจาก array เดี่ยว เป็น array ของ array
    private Renderer[] rends;
    private bool active = false;

    void Awake()
    {
        rends = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[rends.Length][]; //เพิ่ม []
        for (int i = 0; i < rends.Length; i++)
            originalMaterials[i] = rends[i].sharedMaterials; // จัดเก็บวัสดุทั้งหมดใน renderer นั้น เติม s
    }

    void Start()
    {    
        var ui = FindObjectOfType<UI_TurnTableOption>();
        wireframeMaterial = ui?.wireframeMaterial;
        SetWireframe(ui?.toggleWireframe?.isOn ?? false);
    }

    public void SetWireframe(bool enable)
    {
        active = enable;

        if (rends == null || rends.Length == 0) return;
        if (originalMaterials == null || originalMaterials.Length != rends.Length) return;

        for (int i = 0; i < rends.Length; i++)
        {
            if (active && wireframeMaterial != null)
            {
                // สร้าง array ใหม่ที่ทุกช่องเป็น wireframeMaterial
                Material[] wireframeMats = new Material[originalMaterials[i].Length];
                for (int j = 0; j < wireframeMats.Length; j++)
                {
                    wireframeMats[j] = wireframeMaterial;
                }
                rends[i].materials = wireframeMats; //wireframeMaterial เปลี่ยนเป็น wireframeMats เติมs
            }
            else
                rends[i].materials = originalMaterials[i];// เติม s
        }
    }

    public bool IsWireframeOn() => active;

    //void Update()
    //{
    //    if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
    //    {
    //        active = !active;
    //        for (int i = 0; i < rends.Length; i++)
    //            rends[i].material = active ? wireframeMaterial : original[i];
    //    }
    //}
}
