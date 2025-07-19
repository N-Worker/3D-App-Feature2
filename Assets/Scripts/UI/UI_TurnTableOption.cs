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

    //============== ฟังก์ชัน ToggleWireframe() และ ToggleBaseColor() ================
    [Header("Wireframe Option")]
    public Toggle toggleWireframe; // เพิ่ม Toggle Wireframe นี้ใน Inspector
    public Material wireframeMaterial;

    [Header("BaseColor")]
    public Toggle toggleBaseColor;
    public Shader unlitShader;

    [Space]
    public WarningMessageUI warningUI;
    private bool isUpdatingToggles = false; // ป้องกันลูปเรียกซ้ำ

    private void Start()
    {
        toggleWireframe.onValueChanged.AddListener(OnToggleWireframeChanged);
        toggleBaseColor.onValueChanged.AddListener(OnToggleBaseColorChanged);
        SyncTogglesToModel();
    }
    //============== ฟังก์ชัน ToggleWireframe() และ ToggleBaseColor() ================

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

    //============== ฟังก์ชัน ToggleWireframe() และ ToggleBaseColor() ================
    public void SyncTogglesToModel()
    {
        var model = GameManager.Instance.tableManager.GetCurrentModel();
        if (model == null) return;

        // Sync Wireframe
        model.GetComponent<WireframeToggle>()?.SetWireframe(toggleWireframe.isOn);

        // Sync BaseColor — เพิ่มบรรทัดนี้เพื่อให้โมเดลใหม่อัพเดตสถานะตาม Toggle ปัจจุบันทันที
        model.GetComponent<BaseColorModel>()?.SetBaseColor(toggleBaseColor.isOn);
    }

    private void OnToggleWireframeChanged(bool isOn)
    {
        if (isUpdatingToggles) return;

        // ถ้า BaseColor ยังเปิดอยู่ → ห้ามเปิด Wireframe
        if (isOn && toggleBaseColor.isOn)
        {
            isUpdatingToggles = true;
            toggleWireframe.isOn = false; // ปิดกลับ
            isUpdatingToggles = false;

            warningUI.ShowMessage("Close Base Color Before Open Wireframe");
            return;
        }

        SyncTogglesToModel();
    }

    private void OnToggleBaseColorChanged(bool isOn)
    {
        if (isUpdatingToggles) return;

        // ถ้า Wireframe ยังเปิดอยู่ → ห้ามเปิด BaseColor
        if (isOn && toggleWireframe.isOn)
        {
            isUpdatingToggles = true;
            toggleBaseColor.isOn = false; // ปิดกลับ
            isUpdatingToggles = false;

            warningUI.ShowMessage("Close Wireframe Before Open Base Color");
            return;
        }

        SyncTogglesToModel();
    }
    //============== ฟังก์ชัน ToggleWireframe() และ ToggleBaseColor() ================

}
