using System.Collections.Generic;
using UnityEngine;

public class ModelStorage : MonoBehaviour
{
    public List<GameObject> models = new List<GameObject>();

    public void LoadModels()
    {
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("");

        foreach (GameObject prefab in allPrefabs)
        {
            if (prefab.GetComponent<ModelProperties>() != null && !models.Contains(prefab))
            {
                models.Add(prefab);
            }
        }

        models.Sort((a, b) =>
        {
            int idA = a.GetComponent<ModelProperties>().studenID;
            int idB = b.GetComponent<ModelProperties>().studenID;
            return idA.CompareTo(idB);
        });
    }
}