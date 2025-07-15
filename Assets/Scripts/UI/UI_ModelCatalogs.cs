using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ModelCatalogs : MonoBehaviour
{
    [Header("ModelCatalogButtons")]
    public List<Button> modelCatalogButtons;

    [Header("SwitchPage")]
    public Button nextPageButton;
    public TMP_Text currentPageText;
    public Button prevPageButton;

    #region Variables
    private int _currentPage = 0;
    private int _modelsPerPage = 12;
    #endregion

    #region References
    private GameManager _gameManager;
    private TableManager _tableManager;
    private ModelStorage _storage;
    #endregion

    void Start()
    {
        _InitializeReferences();

        _ButtonSetUp();

        ModelCatalogsUI();
    }
    private void _InitializeReferences()
    {
        _gameManager = GameManager.Instance;
        _tableManager = _gameManager.tableManager;
        _storage = _gameManager.modelStorage;
    }
    private void _ButtonSetUp()
    {
        nextPageButton.onClick.AddListener(_NextPage);
        prevPageButton.onClick.AddListener(_PreviousPage);

        currentPageText.text = (_currentPage + 1).ToString();
    }
    public void ModelCatalogsUI()
    {
        int totalModels = _storage.models.Count;
        int startIndex = _currentPage * _modelsPerPage;
        int currentIndex = _tableManager.currentIndex;

        foreach (var button in modelCatalogButtons) button.gameObject.SetActive(false);

        for (int i = 0; i < _modelsPerPage && startIndex + i < totalModels; i++)
        {
            int modelIndex = startIndex + i;
            var storage = _storage.models[modelIndex];
            var button = modelCatalogButtons[i];
            button.gameObject.SetActive(true);

            var text = button.GetComponentInChildren<TMP_Text>();
            if (text != null) text.text = storage.name;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _OnModelSelected(modelIndex));

            button.interactable = (modelIndex != currentIndex);
        }

        prevPageButton.interactable = _currentPage > 0;
        nextPageButton.interactable = (_currentPage + 1) * _modelsPerPage < totalModels;
    }
    private void _OnModelSelected(int index)
    {
        _tableManager.LoadSpecificModel(index);
    }
    private void _NextPage()
    {
        _currentPage++;
        currentPageText.text = (_currentPage + 1).ToString();
        ModelCatalogsUI();
    }
    private void _PreviousPage()
    {
        _currentPage--;
        currentPageText.text = (_currentPage + 1).ToString();
        ModelCatalogsUI();
    }
}


