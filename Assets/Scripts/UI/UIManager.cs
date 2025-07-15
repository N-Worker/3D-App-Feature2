using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject modelCatalogsPanel;
    public UI_ModelCatalogs modelCatalogs;
    private bool toggle_modelCatalogs;

    [Space]
    public GameObject currentModelPanel;
    public TMP_Text currentModelName;

    [Space]
    public GameObject turnTableOptionPanel;
    public UI_TurnTableOption turnTableOption { get; private set; }

    private void Awake()
    {
        turnTableOption = turnTableOptionPanel.gameObject.GetComponent<UI_TurnTableOption>();
    }

    public void ToggleModelSelect()
    {
        toggle_modelCatalogs = !toggle_modelCatalogs;
        modelCatalogsPanel.SetActive(toggle_modelCatalogs);
        currentModelPanel.SetActive(!toggle_modelCatalogs);
    }
}
