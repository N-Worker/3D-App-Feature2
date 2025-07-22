using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ซ่อน tool bar
public class UIToolbarController : MonoBehaviour
{
    public static UIToolbarController Instance;

    [Header("UI Toolbar Panels")]
    public GameObject[] toolbarPanels;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowToolbars(bool show)
    {
        foreach (var panel in toolbarPanels)
        {
            if (panel != null) panel.SetActive(show);
        }
    }
}
