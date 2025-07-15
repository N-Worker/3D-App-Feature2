using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerTracker : MonoBehaviour
{
    public static StickerTracker Instance;

    private Dictionary<string, Dictionary<string, int>> data = new Dictionary<string, Dictionary<string, int>>();

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddSticker(string modelName, string stickerName)
    {
        if (!data.ContainsKey(modelName))
            data[modelName] = new Dictionary<string, int>();

        if (!data[modelName].ContainsKey(stickerName))
            data[modelName][stickerName] = 0;

        data[modelName][stickerName]++;

        Debug.Log($"Model: {modelName} now has {stickerName} x{data[modelName][stickerName]}");
    }

    // ดึงข้อมูล (เผื่อเอาไปแสดงใน UI)
    public Dictionary<string, int> GetStickerStatsForModel(string modelName)
    {
        if (data.ContainsKey(modelName))
        return data[modelName];
        return new Dictionary<string, int>();
    }

    public int GetTotalStickerCount(string modelName)
    {
        if (!data.ContainsKey(modelName)) return 0;

        int total = 0;
        foreach (var pair in data[modelName])
        {
            total += pair.Value;
        }
        return total;
    }
}
