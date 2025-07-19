using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningMessageUI : MonoBehaviour
{
    public GameObject messagePanel; // ���ͧ��ͤ���
    public TMP_Text messageText;    // ��ͤ���㹡��ͧ

    public float displayTime = 2f;  // �������ҷ���ʴ�

    private Coroutine currentRoutine;

    public void ShowMessage(string message)
    {

        // �Դ GameObject ��͹����� Coroutine
        if (!messagePanel.activeSelf)
            messagePanel.SetActive(true);

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(Show(message));
    }

    private IEnumerator Show(string msg)
    {
        messagePanel.SetActive(true);
        messageText.text = msg;

        yield return new WaitForSeconds(displayTime);

        messagePanel.SetActive(false);
        messageText.text = "";
    }
}

