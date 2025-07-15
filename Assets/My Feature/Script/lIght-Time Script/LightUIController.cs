using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUIController : MonoBehaviour
{
    //[Header("Rot")]
    //public Slider sliderRotX;
    //public Slider sliderRotY;

    [Header("Light")]
    public Slider sliderInner;
    public Slider sliderOuter;
    public Slider sliderIntensity;

    //[Header("Pos")]
    //public Slider sliderPosX;
    //public Slider sliderPosY;
    //public Slider sliderPosZ;

    [Header("ควบคุมไฟรอบจุดศูนย์กลาง")]
    public Slider sliderYaw;   // ซ้าย-ขวา
    public Slider sliderPitch; // ขึ้น-ลง
    public Slider sliderRange;

    public Transform centerObject; // จุดศูนย์กลาง (เช่นวัตถุตรงกลางเวที)
    public float radius = 5f; // รัศมีวงกลมที่ไฟจะวิ่งรอบ

    [Header("เป้าหมาย")]
    public Transform target;  // Spot Light หรือ Object
    private Light spotLightTarget;

    public void SetTargetLight(Light light)
    {
        if (light == null)
        {
            Debug.LogWarning("Light ที่ส่งเข้ามาเป็น null!");
            return;
        }

        spotLightTarget = light;
        target = light.transform;

        // โหลดค่าเริ่มต้นจาก Light ไปยัง UI
        //Vector3 rot = target.eulerAngles;
        //sliderRotX.value = rot.x;
        //sliderRotY.value = rot.y;

        //sliderPosX.value = target.position.x;
        //sliderPosY.value = target.position.y;
        //sliderPosZ.value = target.position.z;

        // คำนวณมุมจากตำแหน่งจริงของไฟ
        Vector3 direction = (target.position - centerObject.position).normalized;
        float currentRadius = Vector3.Distance(centerObject.position, target.position);

        // Pitch: มุมขึ้น/ลง (จากแกน Y)
        float pitchRad = Mathf.Asin(direction.y);
        float pitchDeg = pitchRad * Mathf.Rad2Deg;

        // Yaw: มุมซ้าย/ขวา (จากแกน XZ)
        float yawRad = Mathf.Atan2(direction.z, direction.x);
        float yawDeg = yawRad * Mathf.Rad2Deg;
        if (yawDeg < 0) yawDeg += 360f;

        // ตั้งค่า Slider
        sliderYaw.value = yawDeg;
        sliderPitch.value = pitchDeg;
        radius = currentRadius;

        if (spotLightTarget.type == LightType.Spot)
        {
            sliderInner.value = spotLightTarget.innerSpotAngle;
            sliderOuter.value = spotLightTarget.spotAngle;

        }

        sliderIntensity.value = spotLightTarget.intensity;
        sliderRange.value = spotLightTarget.range;
    }

    void Update()
    {
        if (target == null || spotLightTarget == null || centerObject == null) return;
        
        //  แปลงองศา Yaw และ Pitch เป็นเรเดียน
        float yawRad = sliderYaw.value * Mathf.Deg2Rad;     // แนวรอบแกน Y
        float pitchRad = sliderPitch.value * Mathf.Deg2Rad; // แนวขึ้น-ลง

        // คำนวณตำแหน่งแบบทรงกลม
        Vector3 offset = new Vector3(
            radius * Mathf.Cos(pitchRad) * Mathf.Cos(yawRad),
            radius * Mathf.Sin(pitchRad),
            radius * Mathf.Cos(pitchRad) * Mathf.Sin(yawRad)
        );

        // ตำแหน่งใหม่รอบจุดกลาง
        target.position = centerObject.position + offset;

        //ให้หันกลับเข้าศูนย์กลางเสมอ
        target.LookAt(centerObject.position);
        
        /*
        // หมุน
        float x = sliderRotX.value;
        float y = sliderRotY.value;
        target.rotation = Quaternion.Euler(x, y, 0f);

        // คำนวณตำแหน่งใหม่แบบหมุนรอบศูนย์กลาง
        float angleDeg = sliderAngleAroundCenter.value;
        float angleRad = angleDeg * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad)) * radius;

        target.position = centerObject.position + offset;

        // หันกลับเข้าจุดศูนย์กลาง
        //target.LookAt(centerObject.position);

        // ตำแหน่ง XYZ
        Vector3 pos = target.position;
        pos.x = sliderPosX.value;
        pos.y = sliderPosY.value;
        pos.z = sliderPosZ.value;
        target.position = pos;       
        */

        // Spot Light Angle
        if (spotLightTarget.type == LightType.Spot)
        {
            spotLightTarget.innerSpotAngle = sliderInner.value;
            spotLightTarget.spotAngle = sliderOuter.value;
        }

        spotLightTarget.intensity = sliderIntensity.value; // ความสว่าง (ทุกประเภทของ Light)
        spotLightTarget.range = sliderRange.value;
    }
}