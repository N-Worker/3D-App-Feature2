using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StickerDropSlot : MonoBehaviour, IDropHandler
{
    [System.Serializable]
    public class StickerLine
    {
        public Image stickerImage;
        public TextMeshProUGUI stickerText;
    }

    public static StickerDropSlot Instance; //เพิ่ม

    private UIManager _uiManager;

    [Header("UI")]
    public TMP_Text modelNameText;          // Text สำหรับชื่อโมเดล
    public Transform stickerListParent;  // พ่อของ sticker list
    public GameObject stickerLinePrefab; // Prefab Text 1 บรรทัด เช่น: <3 x2

    private Dictionary<string, TextMeshProUGUI> stickerTextLines = new();

    void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        _uiManager = GameManager.Instance.uiManager;

        if (_uiManager != null && _uiManager.currentModelName != null)
        {
            string modelName = _uiManager.currentModelName.text;
            UpdateModelName(modelName);
        }
    }

    public void UpdateModelName(string newName)
    {
        if (modelNameText != null)
            modelNameText.text = newName;

        UpdateUI(); // รีเฟรช emoji ด้วย
    }

    public void OnDrop(PointerEventData eventData)
    {
        StickerDragUI drag = eventData.pointerDrag?.GetComponent<StickerDragUI>();

        if (drag != null && _uiManager != null)
        {
            string modelName = _uiManager.currentModelName.text;
            if (string.IsNullOrEmpty(modelName)) return;

            StickerTracker.Instance.AddSticker(modelName, drag.stickerName);

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (_uiManager == null) return;

        string modelName = _uiManager.currentModelName.text;

        if (string.IsNullOrEmpty(modelName)) return;

        if (modelNameText != null)
            modelNameText.text = modelName;

        // ดึงข้อมูล sticker ของโมเดลนี้
        Dictionary<string, int> stats = StickerTracker.Instance.GetStickerStatsForModel(modelName);

        // ลบ sticker เดิมที่มีอยู่
        foreach (Transform child in stickerListParent)
        {
            Destroy(child.gameObject);
        }

        stickerTextLines.Clear();

        // เพิ่มใหม่ทั้งหมด
        foreach (var pair in stats)
        {
            GameObject line = Instantiate(stickerLinePrefab, stickerListParent);

            // Text
            var text = line.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                //text.text = $"{pair.Key} x{pair.Value}"; //ชื่อ+จำนวน
                text.text = $"x{pair.Value}";//จำนวน 

            // Image
            var image = line.GetComponentInChildren<Image>();
            if (image != null)
            {
                Sprite sprite = FindStickerSpriteByName(pair.Key);
                if (sprite != null)
                    image.sprite = sprite;
            }


            stickerTextLines[pair.Key] = text;
        }
    }

    private Sprite FindStickerSpriteByName(string name)
    {
        foreach (var sticker in StickerDragUIManager.Instance.sticker)
        {
            if (sticker.stickerName == name || sticker.sprite.name == name)
                return sticker.sprite;
        }
        return null;
    }
}
