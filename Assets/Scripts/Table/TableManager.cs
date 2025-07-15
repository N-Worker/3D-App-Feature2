using System.Collections.Generic;
using UnityEngine;
public class TableManager : MonoBehaviour
{
    #region Variables
    public bool toggleRotate { get; set; } = true;
    public int currentIndex { get; set; } = -1;
    public float totalRotation { get; set; }

    private const int _maxModelInstances = 5;
    private List<int> _modelQueue = new List<int>();
    private ModelStorage _storage;
    public List<ModelInfo> modelInfo = new List<ModelInfo>();
    #endregion

    #region References
    private GameManager _gameManager;
    private UIManager _uIManager;
    private UI_ModelCatalogs _uI_ModelCatalogs;
    private UI_TurnTableOption _uI_TurnTableOption;
    #endregion

    #region Unity Main Methods
    void Start()
    {
        _InitializeReferences();
    }
    void Update()
    {
        _HandleRotateTable();
    }
    #endregion
    private void _InitializeReferences()
    {
        _gameManager = GameManager.Instance;
        _storage = _gameManager.modelStorage;
        _uIManager = _gameManager.uiManager;
        _uI_ModelCatalogs = _uIManager.modelCatalogs;
        _uI_TurnTableOption = _uIManager.turnTableOption;
    }
    private void _HandleRotateTable()
    {
        if (currentIndex == -1) return;

        if (!toggleRotate || !_uI_TurnTableOption.toggleRotate.isOn) return;

        float rotationSpeed = _uI_TurnTableOption.rotateSpeed.value * Time.deltaTime;
        float direction = _uI_TurnTableOption.flipRotate.isOn ? -1f : 1f;
        transform.Rotate(Vector3.up, rotationSpeed * direction);

        if (_uI_TurnTableOption.autoSwitchModel.isOn)
        {
            totalRotation += rotationSpeed;
            if (totalRotation >= 360f * _uI_TurnTableOption.rotatePerModel.value)
            {
                totalRotation = 0f;
                NextModel();
            }
        }
        else totalRotation = 0f;
    }

    #region LoadModel
    private void _CreateInstanceModel(int index)
    {
        _HideAllModels();

        if (_ShowModelInfo(index)) return;

        GameObject instance = _InstantiateNewModel(index);
        _AddModelInfo(instance, index);
        _modelQueue.Add(index);

        if (_modelQueue.Count > _maxModelInstances) _TrimOldModels();
    }
    private void _HideAllModels()
    {
        foreach (var info in modelInfo) info.model.SetActive(false);
    }
    private bool _ShowModelInfo(int index)
    {
        foreach (var info in modelInfo)
        {
            if (info.index == index)
            {
                info.model.SetActive(true);
                _uIManager.currentModelName.text = info.name;
                _modelQueue.Remove(index);
                _modelQueue.Add(index);
                //_uI_MaterialParts.currentPage = 0;
                //_uI_MaterialParts.MaterialPartsUI();
                return true;
            }
        }
        return false;
    }
    private GameObject _InstantiateNewModel(int index)
    {
        GameObject prefab = _storage.models[index].gameObject;
        GameObject instance = Instantiate(prefab, transform.position, transform.rotation, transform);
        instance.name = prefab.name;
        instance.SetActive(true);

        _uIManager.currentModelName.text = instance.name;
        _AutoScaleModel(instance);
        _CenterModel(instance);

        //============== Add WireframeRenderer (GL.Line) ===============
        // ใส่ WireframeDrawer.cs อัตโนมัติ
        //if (instance.GetComponent<WireframeRenderer>() == null)
        //{
        //    WireframeRenderer drawer = instance.AddComponent<WireframeRenderer>();
        //    drawer.lineColor = Color.white; // หรือปรับสีตามต้องการ
        //}
        //============== Add WireframeRenderer ===============

        //============== Add WireframeToggle ===============
        if (instance.GetComponent<WireframeToggle>() == null)
        {
            WireframeToggle toggle = instance.AddComponent<WireframeToggle>();
            toggle.wireframeMaterial = _uI_TurnTableOption.wireframeMaterial; // ดึงจาก UI
            toggle.SetWireframe(_uI_TurnTableOption.toggleWireframe.isOn); // เปิด/ปิดตาม UI
            
            // ป้องกัน null
            if (_uI_TurnTableOption != null && _uI_TurnTableOption.wireframeMaterial != null)
                toggle.wireframeMaterial = _uI_TurnTableOption.wireframeMaterial;
        }
        //============== Add WireframeToggle ===============

        return instance;
    }
    private void _TrimOldModels()
    {
        int removeIndex = _modelQueue[0];
        var info = modelInfo.Find(i => i.index == removeIndex);
        if (info.model != null)
        {
            Destroy(info.model);
            modelInfo.Remove(info);
        }
        _modelQueue.RemoveAt(0);
    }
    private void _AddModelInfo(GameObject instance, int index)
    {
        ModelInfo info = new ModelInfo
        {
            index = index,
            name = instance.name,
            model = instance,
        };

        modelInfo.Add(info);

        modelInfo.Sort((a, b) => a.index.CompareTo(b.index));
    }
    #endregion

    #region AdjustModel
    private void _AutoScaleModel(GameObject model, float targetSize = 1f)
    {
        Bounds bounds = _GetBounds(model);
        float maxDim = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        if (maxDim == 0) return;
        float scale = targetSize / maxDim;
        model.transform.localScale *= scale;
    }
    private void _CenterModel(GameObject model)
    {
        Bounds bounds = _GetBounds(model);
        Vector3 offset = bounds.center - model.transform.position;
        model.transform.position -= offset;
    }
    private Bounds _GetBounds(GameObject model)
    {
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(model.transform.position, Vector3.zero);
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
        return bounds;
    }
    #endregion

    #region SwitchModel
    public void NextModel()
    {
        if (_storage.models.Count <= 1) return;
        currentIndex = (currentIndex + 1) % _storage.models.Count;
        _SwitchModel();
    }
    public void PreviousModel()
    {
        if (_storage.models.Count <= 1) return;
        currentIndex = (currentIndex - 1 + _storage.models.Count) % _storage.models.Count;
        _SwitchModel();
    }
    public void LoadSpecificModel(int index)
    {
        currentIndex = index;
        _SwitchModel();
    }
    private void _SwitchModel()
    {
        totalRotation = 0f;
        transform.eulerAngles = Vector3.up * 180f;
        _CreateInstanceModel(currentIndex);
        _uI_ModelCatalogs.ModelCatalogsUI();

        //============== UpdateModelName Text ================
        // อัปเดตชื่อโมเดลใน StickerDropSlot
        if (StickerDropSlot.Instance != null)
        {
            string modelName = _uIManager.currentModelName.text;
            StickerDropSlot.Instance.UpdateModelName(modelName);
        }
        //============== UpdateModelName Text ================

        //============== Wireframe UI ================
        //เพิ่มตรงนี้ให้ UI ควบคุม Wireframe ใหม่ด้วย
        var turnTableUI = GameManager.Instance.uiManager.turnTableOption;
        if (turnTableUI != null)
        {
            turnTableUI.ToggleWireframe(); // sync toggle กับโมเดลใหม่
        }
        //============== Wireframe UI ================

    }
    #endregion
}
#region Stucture
[System.Serializable]
public struct ModelInfo
{
    public string name;
    public int index;
    public GameObject model;
}
#endregion