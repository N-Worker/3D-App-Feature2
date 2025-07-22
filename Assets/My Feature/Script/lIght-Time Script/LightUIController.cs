using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUIController : MonoBehaviour
{
  
    [Header("Light")]
    public Slider sliderInner;
    public Slider sliderOuter;
    public Slider sliderIntensity;

    [Header("�Ǻ�����ͺ�ش�ٹ���ҧ")]
    public Slider sliderYaw;   // ����-���
    public Slider sliderPitch; // ���-ŧ
    public Slider sliderRange;

    public Transform centerObject; // �ش�ٹ���ҧ (���ѵ�صç��ҧ�Ƿ�)
    public float radius = 5f; // �����ǧ������俨�����ͺ

    [Header("�������")]
    public Transform target;  // Spot Light ���� Object
    private Light spotLightTarget;

    public void SetTargetLight(Light light)
    {
        if (light == null)
        {
            Debug.LogWarning("Light ������������ null!");
            return;
        }

        spotLightTarget = light;
        target = light.transform;

        // �ӹǳ����ҡ���˹觨�ԧ�ͧ�
        Vector3 direction = (target.position - centerObject.position).normalized;
        float currentRadius = Vector3.Distance(centerObject.position, target.position);

        // Pitch: ������/ŧ (�ҡ᡹ Y)
        float pitchRad = Mathf.Asin(direction.y);
        float pitchDeg = pitchRad * Mathf.Rad2Deg;

        // Yaw: �������/��� (�ҡ᡹ XZ)
        float yawRad = Mathf.Atan2(direction.z, direction.x);
        float yawDeg = yawRad * Mathf.Rad2Deg;
        if (yawDeg < 0) yawDeg += 360f;

        // ��駤�� Slider
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
        
        //  �ŧͧ�� Yaw ��� Pitch ������¹
        float yawRad = sliderYaw.value * Mathf.Deg2Rad;     // ���ͺ᡹ Y
        float pitchRad = sliderPitch.value * Mathf.Deg2Rad; // �Ǣ��-ŧ

        // �ӹǳ���˹�Ẻ�ç���
        Vector3 offset = new Vector3(
            radius * Mathf.Cos(pitchRad) * Mathf.Cos(yawRad),
            radius * Mathf.Sin(pitchRad),
            radius * Mathf.Cos(pitchRad) * Mathf.Sin(yawRad)
        );

        // ���˹������ͺ�ش��ҧ
        target.position = centerObject.position + offset;

        //����ѹ��Ѻ����ٹ���ҧ����
        target.LookAt(centerObject.position);
        
        // Spot Light Angle
        if (spotLightTarget.type == LightType.Spot)
        {
            spotLightTarget.innerSpotAngle = sliderInner.value;
            spotLightTarget.spotAngle = sliderOuter.value;
        }

        spotLightTarget.intensity = sliderIntensity.value; // �������ҧ (�ء�������ͧ Light)
        spotLightTarget.range = sliderRange.value;
    }
}