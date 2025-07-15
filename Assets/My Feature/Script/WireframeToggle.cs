using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeToggle : MonoBehaviour
{
    public Material wireframeMaterial;
    private Material[] originalMaterials;
    private Renderer[] rends;
    private bool active = false;

    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[rends.Length];
        for (int i = 0; i < rends.Length; i++)
            originalMaterials[i] = rends[i].sharedMaterial;
    }

    public void SetWireframe(bool enable)
    {
        active = enable;

        if (rends == null || rends.Length == 0) return;
        if (originalMaterials == null || originalMaterials.Length != rends.Length) return;

        for (int i = 0; i < rends.Length; i++)
        {
            if (active && wireframeMaterial != null)
                rends[i].material = wireframeMaterial;
            else
                rends[i].material = originalMaterials[i];
        }
    }

    public bool IsWireframeOn() => active;

    //void Update()
    //{
    //    if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
    //    {
    //        active = !active;
    //        for (int i = 0; i < rends.Length; i++)
    //            rends[i].material = active ? wireframeMaterial : original[i];
    //    }
    //}
}
