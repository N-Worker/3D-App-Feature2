using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SnapshotManager : MonoBehaviour
{
    [Header("Snapshot Settings")]
    //public string folderName = "Snapshots"; // ��������������
    public string filePrefix = "Snapshot_"; // �ӹ�˹�����
    public Button snapshotButton;
    
    public bool openFolderAfterSave = true; // �Դ�������ѵ��ѵ�

    private string screenshotFolder;

    void Start()
    {
        // ��駤�� path ���ѹ�֡������������� Snapshots ����� PersistentDataPath
        //screenshotFolder = Path.Combine(Application.persistentDataPath, folderName);
        
        screenshotFolder = Path.Combine(Application.persistentDataPath, "Snapshots");

        // ���ҧ�������ҡ�ѧ�����
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }

        // ����������Ѻ�ѧ��ѹ��ҹ��
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
        // ��͹ UI ��͹����
        if (UIToolbarController.Instance != null)
            UIToolbarController.Instance.ShowToolbars(false);

        yield return new WaitForEndOfFrame(); // ����騺�����͹

        // ���ҧ�������ҡ���һѨ�غѹ
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); //��-��͹-�ѹ-��.-�ҷ�-��
        string fileName = filePrefix + timestamp + ".png";
        string fullPath = Path.Combine(screenshotFolder, fileName);

        // �����Ҿ
        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("Snapshot saved to: " + fullPath);

        // ���ѡ�ѡ��͹�Դ UI ��Ѻ��
        yield return new WaitForSeconds(0.1f);

        if (UIToolbarController.Instance != null)
            UIToolbarController.Instance.ShowToolbars(true);

        // �Դ������
        if (openFolderAfterSave)
        {
            Application.OpenURL("file://" + screenshotFolder);
        }
    }
}
