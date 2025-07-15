using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSelector : MonoBehaviour
{
    public Light[] lightList; // ดวงไฟทั้งหมด
    public HSVColorPicker hsvColorPicker;
    public LightUIController lightUIController; // อ้างอิงตัวควบคุม

    [Header("SetUpแรก")]
    public Transform centerObject; // จุดศูนย์กลางของวงกลม
    public float radius = 5f; // รัศมีวงกลม
    public float heightY = 2f; // เพิ่มที่ด้านบนของสคริปต์

    void Start()
    {
        ArrangeLightsInCircle();

        // เลือกดวงแรกโดยอัตโนมัติ
        SelectLightByIndex(0);
    }

    public void ArrangeLightsInCircle()
    {
        if (centerObject == null) return;

        int lightCount = lightList.Length;

        for (int i = 0; i < lightCount; i++)
        {
            float angle = i * Mathf.PI * 2f / lightCount;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            
            pos.y = heightY; // เพิ่มความสูงของไฟ
            
            Transform lightTransform = lightList[i].transform;
            lightTransform.position = centerObject.position + pos;

            // ให้ไฟหันเข้าศูนย์กลาง
            lightTransform.LookAt(centerObject.position);

            // กำหนดค่าเริ่มต้น: สีขาว
            lightList[i].color = Color.white;
            lightList[i].intensity = 10f;
            lightList[i].range = 10f;
            lightList[i].spotAngle = 45f;
        }
    }

    public void SetLightsToColorLoop()
    {
        Color[] rgbColors = new Color[]
        {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow
        };

        for (int i = 0; i < lightList.Length; i++)
        {
            if (lightList[i] != null)
            {
                lightList[i].color = rgbColors[i % rgbColors.Length];
            }
        }
    }

    public void SelectLightByIndex(int index)
    {
        if (index >= 0 && index < lightList.Length)
        {
            Light selected = lightList[index];

            // ควบคุม Position/Rotation
            lightUIController.SetTargetLight(selected);

            // ควบคุมสีผ่าน HSV
            if (hsvColorPicker != null)
            {
                hsvColorPicker.targetLight = selected;

                // โหลดค่าสีจาก Light ไปยัง Slider HSV
                Color color = selected.color;
                Color.RGBToHSV(color, out float h, out float s, out float v);

                hsvColorPicker.sliderHue.value = h;
                hsvColorPicker.sliderSaturation.value = s;
                hsvColorPicker.sliderValue.value = v;
            }
        }
    }
    //public void ToggleLightOnOff(int index)
    //{
    //    if (index >= 0 && index < lightList.Length)
    //    {
    //        lightList[index].enabled = !lightList[index].enabled;
    //    }
    //}
}
