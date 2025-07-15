using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightToggleController : MonoBehaviour
{
    [Header("�ǧ俷����� (���§���ç�Ѻ��õ�駤��)")]
    public List<Light> allLights = new List<Light>();

    [Header("�Դ���ͻԴ俴ǧ�˹�͹�������")]
    public List<bool> lightEnabledAtStart = new List<bool>();

    [Header("UI Toggle �Ǻ���")]
    public List<Toggle> lightToggles = new List<Toggle>();

    void Start()
    {
        // ��Ѻ��Ҵ list �����ҡѹ (�ѹ��Ҵ)
        while (lightEnabledAtStart.Count < allLights.Count)
        {
            lightEnabledAtStart.Add(false); // ��� default = �Դ
        }

        for (int i = 0; i < allLights.Count; i++)
        {
            int index = i; // ��ͧ�ѹ closure
            bool isOnAtStart = lightEnabledAtStart[i];

            // �Դ/�Դ�͹�����
            allLights[index].enabled = isOnAtStart;

            // ����� UI Toggle �Ǻ�������
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
