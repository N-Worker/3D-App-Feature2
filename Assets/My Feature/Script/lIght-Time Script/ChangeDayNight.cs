using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDayNight : MonoBehaviour
{
    public Light directionalLight;

    [Header("Preset Lighting")]
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.1f, 0.1f, 0.4f);
    public Color sunsetColor = new Color(1f, 0.5f, 0.2f); // สีส้ม

    public float dayIntensity = 1f;
    public float nightIntensity = 0.2f;
    public float sunsetIntensity = 0.6f;

    public Vector3 dayRotation = new Vector3(50f, 30f, 0f);
    public Vector3 nightRotation = new Vector3(-30f, 0f, 0f);
    public Vector3 sunsetRotation = new Vector3(20f, 15f, 0f);

    [Header("Skybox (ไม่จำเป็น)")]
    public Material skyboxDay;
    public Material skyboxNight;
    public Material skyboxSunset;

    private int currentState = 0; // 0 = Day, 1 = Dusk, 2 = Night

    //private bool isNight = false;

    void Start()
    {
        // ตั้งค่าเริ่มต้นเป็น Day
        if (directionalLight != null)
        {
            directionalLight.color = dayColor;
            directionalLight.intensity = dayIntensity;
            directionalLight.transform.rotation = Quaternion.Euler(dayRotation);
        }

        if (skyboxDay != null)
        {
            RenderSettings.skybox = skyboxDay;
        }
    }

    public void ButtonDayNight()
    {
        //isNight = !isNight;
        currentState = (currentState + 1) % 3;

        if (directionalLight == null)
        {
            Debug.LogWarning("ยังไม่ได้ใส่ Directional Light!");
            return;
        }

        switch (currentState)
        {
            case 0: // Day
                directionalLight.color = dayColor;
                directionalLight.intensity = dayIntensity;
                directionalLight.transform.rotation = Quaternion.Euler(dayRotation);
                if (skyboxDay != null) RenderSettings.skybox = skyboxDay;
                break;

            case 1: // Dusk
                directionalLight.color = sunsetColor;
                directionalLight.intensity = sunsetIntensity;
                directionalLight.transform.rotation = Quaternion.Euler(sunsetRotation);
                if (skyboxSunset != null) RenderSettings.skybox = skyboxSunset;
                break;

            case 2: // Night
                directionalLight.color = nightColor;
                directionalLight.intensity = nightIntensity;
                directionalLight.transform.rotation = Quaternion.Euler(nightRotation);
                if (skyboxNight != null) RenderSettings.skybox = skyboxNight;
                break;
        }

        //if (isNight)
        //{
        //    directionalLight.color = nightColor;
        //    directionalLight.intensity = nightIntensity;
        //    directionalLight.transform.rotation = Quaternion.Euler(nightRotation);
        //}
        //else
        //{
        //    directionalLight.color = dayColor;
        //    directionalLight.intensity = dayIntensity;
        //    directionalLight.transform.rotation = Quaternion.Euler(dayRotation);
        //}
    }
}
