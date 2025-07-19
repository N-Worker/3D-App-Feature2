using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeRenderer : MonoBehaviour
{
    //public Color lineColor = Color.white;
    //public bool showWireframe = false;

    //private Material lineMaterial;
    //private List<MeshFilter> meshFilters = new List<MeshFilter>();

    //void Start()
    //{
    //    // เตรียม material
    //    Shader shader = Shader.Find("Unlit/Color");
    //    lineMaterial = new Material(shader);
    //    lineMaterial.color = lineColor;

    //    // หาทุก MeshFilter ในลูก ๆ
    //    meshFilters.AddRange(GetComponentsInChildren<MeshFilter>());
    //}

    //void Update()
    //{
    //    //// toggle ด้วยปุ่ม F1
    //    //if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
    //    //{
    //    //    showWireframe = !showWireframe;
    //    //}
    //}

    //void OnRenderObject()
    //{
    //    if (!showWireframe || !gameObject.activeInHierarchy || !gameObject.activeSelf) return;

    //    //Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main); //ระยะกล้องเห็น Wireframe

    //    lineMaterial.SetPass(0);

    //    foreach (MeshFilter mf in meshFilters)
    //    {
    //        //        //================== ระยะกล้องเห็น Wireframe ===========================
    //        //        //if (!GeometryUtility.TestPlanesAABB(cameraPlanes, mf.mesh.bounds))
    //        //        //    continue;

    //        //        //if (ScanZone.Instance != null && !ScanZone.Instance.IsWithinScanZone(mf.transform.position))
    //        //        //    continue;
    //        //        //================== ระยะกล้องเห็น Wireframe ===========================

    //        Mesh mesh = mf.sharedMesh;
    //        if (mesh == null) continue;

    //        Transform tf = mf.transform;
    //        Vector3[] vertices = mesh.vertices;
    //        int[] triangles = mesh.triangles;

    //        GL.PushMatrix();
    //        GL.MultMatrix(tf.localToWorldMatrix);
    //        GL.Begin(GL.LINES);
    //        GL.Color(lineColor);

    //        for (int i = 0; i < triangles.Length; i += 3)
    //        {
    //            //================== ระยะกล้องเห็น Wireframe ===========================

    //            //  Vector3 worldA = tf.TransformPoint(vertices[triangles[i]]);
    //            //  Vector3 worldB = tf.TransformPoint(vertices[triangles[i + 1]]);
    //            //  Vector3 worldC = tf.TransformPoint(vertices[triangles[i + 2]]);

    //            // กรอง triangle ถ้าอยู่นอก scan zone ทั้ง 3 จุด
    //            // if (!IsInScanZone(worldA) && !IsInScanZone(worldB) && !IsInScanZone(worldC))
    //            //     continue;
    //            //================== ระยะกล้องเห็น Wireframe ===========================

    //            DrawEdge(vertices[triangles[i]], vertices[triangles[i + 1]]);
    //            DrawEdge(vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
    //            DrawEdge(vertices[triangles[i + 2]], vertices[triangles[i]]);
    //        }

    //            GL.End();
    //            GL.PopMatrix();
    //    }
    //}

    //    private void DrawEdge(Vector3 a, Vector3 b)
    //    {
    //        GL.Vertex(a);
    //        GL.Vertex(b);
    //    }

        //================== ระยะกล้องเห็น Wireframe ===========================

        //private bool IsInScanZone(Vector3 worldPos)
        //{
        //    if (ScanZone.Instance == null) return true;
        //    return ScanZone.Instance.IsWithinScanZone(worldPos);
        //}
        //================== ระยะกล้องเห็น Wireframe ===========================

}
