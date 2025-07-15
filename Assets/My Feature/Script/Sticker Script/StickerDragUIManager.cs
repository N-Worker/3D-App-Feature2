using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerDragUIManager : MonoBehaviour
{
    [System.Serializable]
    public class StickerSettings
    {
        public Sprite sprite;
        public string stickerName;
    }

    [Header("Sticker Settings")]
    public List<StickerSettings> sticker = new List<StickerSettings>();

    [Header("UI Prefabs")]
    public GameObject stickerUIPrefab; //  prefab ของ StickeriDragUI (มี Image + StickerDragUI.cs)

    [Header("Parent UI")]
    public Transform parentContainer;

    public static StickerDragUIManager Instance;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        foreach (var sticker in sticker)
        {
            GameObject obj = Instantiate(stickerUIPrefab, parentContainer);

            // หาภายใน CommentImage หรือ Image ที่อยู่ลึก
            Image img = obj.GetComponent<Image>();
            StickerDragUI drag = obj.GetComponent<StickerDragUI>();

            if (img != null && sticker.sprite != null)
                img.sprite = sticker.sprite;

            if (drag != null)
                drag.stickerName = string.IsNullOrEmpty(sticker.stickerName) ? sticker.sprite.name : sticker.stickerName;
        }
    }
    
}
