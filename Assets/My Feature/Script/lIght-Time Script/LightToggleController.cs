using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightToggleController : MonoBehaviour
{
    [Header("ดวงไฟทั้งหมด (เรียงให้ตรงกับการตั้งค่า)")]
    public List<Light> allLights = new List<Light>();

    [Header("เปิดหรือปิดไฟดวงไหนตอนเริ่มเกม")]
    public List<bool> lightEnabledAtStart = new List<bool>();

    [Header("UI Toggle ควบคุม")]
    public List<Toggle> lightToggles = new List<Toggle>();

    void Start()
    {
        // ปรับขนาด list ให้เท่ากัน (กันพลาด)
        while (lightEnabledAtStart.Count < allLights.Count)
        {
            lightEnabledAtStart.Add(false); // ค่า default = ปิด
        }

        for (int i = 0; i < allLights.Count; i++)
        {
            int index = i; // ป้องกัน closure
            bool isOnAtStart = lightEnabledAtStart[i];

            // เปิด/ปิดตอนเริ่ม
            allLights[index].enabled = isOnAtStart;

            // ถ้ามี UI Toggle ควบคุมด้วย
            if (i < lightToggles.Count && lightToggles[i] != null)
            {
                lightToggles[i].isOn = isOnAtStart;

                lightToggles[i].onValueChanged.AddListener((isOn) =>
                {
                    allLights[index].enabled = isOn;
                });
            }
        }
    }
}
