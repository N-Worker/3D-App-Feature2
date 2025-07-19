using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningMessageUI : MonoBehaviour
{
    public GameObject messagePanel; // กล่องข้อความ
    public TMP_Text messageText;    // ข้อความในกล่อง

    public float displayTime = 2f;  // ระยะเวลาที่แสดง

    private Coroutine currentRoutine;

    public void ShowMessage(string message)
    {

        // เปิด GameObject ก่อนเริ่ม Coroutine
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

