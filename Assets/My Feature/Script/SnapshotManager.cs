using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SnapshotManager : MonoBehaviour
{
    [Header("Snapshot Settings")]
    //public string folderName = "Snapshots"; // ชื่อโฟลเดอร์ย่อย
    public string filePrefix = "Snapshot_"; // คำนำหน้าไฟล์
    public Button snapshotButton;
    
    public bool openFolderAfterSave = true; // เปิดโฟลเดอร์อัตโนมัติ

    private string screenshotFolder;

    void Start()
    {
        // ตั้งค่า path ให้บันทึกไว้ในโฟลเดอร์ชื่อ Snapshots ภายใต้ PersistentDataPath
        //screenshotFolder = Path.Combine(Application.persistentDataPath, folderName);
        
        screenshotFolder = Path.Combine(Application.persistentDataPath, "Snapshots");

        // สร้างโฟลเดอร์หากยังไม่มี
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }

        // เชื่อมปุ่มกับฟังก์ชันผ่านโค้ด
        if (snapshotButton != null)
        {
            snapshotButton.onClick.AddListener(TakeSnapshot);
        }
    }

    public void TakeSnapshot()
    {
        StartCoroutine(TakeSnapshotRoutine());
    }

    private IEnumerator TakeSnapshotRoutine()
    {
        // ซ่อน UI ก่อนถ่าย
        if (UIToolbarController.Instance != null)
            UIToolbarController.Instance.ShowToolbars(false);

        yield return new WaitForEndOfFrame(); // รอให้จบเฟรมก่อน

        // สร้างชื่อไฟล์จากเวลาปัจจุบัน
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); //ปี-เดือน-วัน-ชม.-นาที-วิ
        string fileName = filePrefix + timestamp + ".png";
        string fullPath = Path.Combine(screenshotFolder, fileName);

        // ถ่ายภาพ
        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("Snapshot saved to: " + fullPath);

        // รอสักพักก่อนเปิด UI กลับมา
        yield return new WaitForSeconds(0.1f);

        if (UIToolbarController.Instance != null)
            UIToolbarController.Instance.ShowToolbars(true);

        // เปิดโฟลเดอร์
        if (openFolderAfterSave)
        {
            Application.OpenURL("file://" + screenshotFolder);
        }
    }
}
