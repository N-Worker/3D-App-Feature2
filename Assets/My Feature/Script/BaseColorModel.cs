using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Color Nor
public class BaseColorModel : MonoBehaviour
{
    //public Material baseColorMaterial; // Mat Color
    public Shader unlitShader;

    private Material[] originalMaterials;
    private Renderer[] renderers;
    private bool isBaseColor = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();          // หาทุก Renderer ในตัวนี้และลูก ๆ
        originalMaterials = new Material[renderers.Length];        // หาทุก Renderer ในตัวนี้และลูก ๆ


        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].sharedMaterial;
        }
    }
    void Start()
    {
        // ดึง unlitShader จาก UI_TurnTableOption ถ้ายังไม่เซ็ตค่าไว้
        if (unlitShader == null)
        {
            var ui = FindObjectOfType<UI_TurnTableOption>();
            if (ui != null)
            {
                unlitShader = ui.unlitShader;
            }
        }

        // ถ้า Toggle เปิดอยู่ ก็เปิด BaseColor ให้ทันที
        var uiToggle = FindObjectOfType<UI_TurnTableOption>();
        if (uiToggle != null && uiToggle.toggleBaseColor != null && uiToggle.toggleBaseColor.isOn)
        {
            SetBaseColor(true);
        }
    }

    public void SetBaseColor(bool enable)
    {
        if (enable == isBaseColor) return;
        isBaseColor = enable;

        for (int i = 0; i < renderers.Length; i++)
        {
            // Mat Unit/Color
            //renderers[i].sharedMaterial = isBaseColor ? baseColorMaterial : originalMaterials[i]; 

            if (isBaseColor)
            {
                var shader = unlitShader ?? Shader.Find("Unlit/Texture");
                Material[] newMats = new Material[renderers[i].sharedMaterials.Length];

                for (int j = 0; j < newMats.Length; j++)
                {
                    var mat = new Material(shader);
                    var tex = originalMaterials[i].GetTexture("_MainTex");

                    if (tex != null) mat.mainTexture = tex;
                    newMats[j] = mat;
                }

                renderers[i].sharedMaterials = newMats;
            }
            else
            {
                // กลับเป็น Material เดิม
                renderers[i].sharedMaterial = originalMaterials[i];
            }
        }

        Debug.Log("Base Color Mode: " + (isBaseColor ? "ON" : "OFF"));
    }
    //void Update()
    //{
    //     //ทดสอบ: กดปุ่ม B เพื่อสลับโหมด Base Color
    //    if (UnityEngine.Input.GetKeyDown(KeyCode.B))
    //    {
    //        ToggleBaseColor();
    //    }
    //}
}

