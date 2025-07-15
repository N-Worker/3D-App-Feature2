using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_TurnTableOption : MonoBehaviour
{
    public Toggle autoResetCamera;
    public Toggle toggleRotate;
    public Toggle flipRotate;
    public Toggle autoSwitchModel;
    [Space]
    public GameObject rotatePerModelGO;
    public Slider rotatePerModel;
    public TMP_Text rotatePerModelValueText;
    [Space]
    public GameObject rotateSpeedGO;
    public Slider rotateSpeed;
    public TMP_Text rotateSpeedValueText;

    //============== Wireframe UI ================
    [Header("Wireframe Option")]
    public Toggle toggleWireframe; // เพิ่ม Toggle Wireframe นี้ใน Inspector
    public Material wireframeMaterial;

    private void Start()
    {
        toggleWireframe.onValueChanged.AddListener(delegate { ToggleWireframe(); });
        ToggleWireframe();
    }
    //============== Wireframe UI ================

    public void RotatePerModelValueChanged()
    {
        rotatePerModelValueText.text = rotatePerModel.value.ToString();
    }
    public void RotateSpeedValueChanged()
    {
        rotateSpeedValueText.text = rotateSpeed.value.ToString();
    }

    public void ToggleRotate()
    {
        flipRotate.gameObject.SetActive(toggleRotate.isOn);
        autoSwitchModel.gameObject.SetActive(toggleRotate.isOn);
        rotatePerModelGO.SetActive(toggleRotate.isOn);
        rotateSpeedGO.SetActive(toggleRotate.isOn);
    }

    //============== Wireframe UI ================
    public void ToggleWireframe()
    {
        //============== Add WireframeRenderer (GL.Line) ===============
        //    bool enable = toggleWireframe.isOn;
        //    WireframeRenderer[] wireframes = FindObjectsOfType<WireframeRenderer>();
        //    foreach (var wf in wireframes) wf.showWireframe = enable;
        //============== Add WireframeRenderer (GL.Line) ===============

        var tableManager = GameManager.Instance.tableManager;
        if (tableManager == null) return;

        GameObject currentModel = tableManager.modelInfo.Find(i => i.index == tableManager.currentIndex).model;
        if (currentModel == null) return;

        WireframeToggle toggle = currentModel.GetComponent<WireframeToggle>();
        if (toggle != null)
        {
            toggle.SetWireframe(toggleWireframe.isOn);
        }
    }
    //============== Wireframe UI ================

}
