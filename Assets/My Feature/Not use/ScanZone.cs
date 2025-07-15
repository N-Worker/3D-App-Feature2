using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//================== ระยะกล้องเห็น Wireframe ===========================
public class ScanZone : MonoBehaviour
{
//    public static ScanZone Instance;

//    [Header("Scan Settings")]
//    public Camera scanCamera;
//    public float distance = 2f; // ความลึกของ ScanZone (ระยะหน้า -> หลังกล้อง)

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public bool IsWithinScanZone(Vector3 worldPos)
//    {
//        if (scanCamera == null) return true;

//        Vector3 viewportPos = scanCamera.WorldToViewportPoint(worldPos);

//        // ถ้าอยู่นอกระยะกล้อง หรืออยู่ด้านหลังกล้อง
//        if (viewportPos.z < 0 || viewportPos.z > distance) return false;

//        // ตรวจสอบว่าอยู่ในระยะมองเห็นของกล้อง (viewport 0..1)
//        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
//               viewportPos.y >= 0 && viewportPos.y <= 1;
//    }

//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        if (scanCamera == null) return;

//        Gizmos.color = new Color(1f, 0.5f, 0f, 0.1f); // สีส้มอ่อน
//        Matrix4x4 temp = Gizmos.matrix;

//        // เปลี่ยน Gizmo ให้แสดงตามมุมมองกล้อง
//        Gizmos.matrix = Matrix4x4.TRS(scanCamera.transform.position, scanCamera.transform.rotation, Vector3.one);

//        Vector3 center = new Vector3(0, 0, distance * 0.5f);
//        Vector3 size = new Vector3(
//            2f * Mathf.Tan(scanCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * distance * scanCamera.aspect,
//            2f * Mathf.Tan(scanCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * distance,
//            distance
//        );

//        Gizmos.DrawCube(center, size);
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireCube(center, size);

//        Gizmos.matrix = temp;
//    }
//#endif
}
