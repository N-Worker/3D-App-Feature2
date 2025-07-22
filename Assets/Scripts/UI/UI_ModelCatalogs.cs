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

    //======================ModelSearchFilter=========================
    [Header("Search")]
    public TMP_InputField searchInputField;
    //======================ModelSearchFilter=========================

    #region Variables
    private int _currentPage = 0;
    private int _modelsPerPage = 12;

    //======================ModelSearchFilter=========================
    private string _searchKeyword = "";
    private List<GameObject> _filteredModels = new List<GameObject>();
    //======================ModelSearchFilter=========================

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

        //======================ModelSearchFilter=========================
        searchInputField.onValueChanged.AddListener(OnSearchValueChanged);
        _RefreshSearchResults();
        //======================ModelSearchFilter=========================

        //ModelCatalogsUI();
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
        int totalModels = _filteredModels.Count; //เปลี่ยนจาก  _storage.models.Count;
        int startIndex = _currentPage * _modelsPerPage;
        int currentIndex = _tableManager.currentIndex;

        foreach (var button in modelCatalogButtons) button.gameObject.SetActive(false);

        for (int i = 0; i < _modelsPerPage && startIndex + i < totalModels; i++)
        {
            int modelIndex = startIndex + i;
            var storage = _filteredModels[modelIndex];//เปลี่ยนจาก _storage.models
            var button = modelCatalogButtons[i];
            button.gameObject.SetActive(true);

            var text = button.GetComponentInChildren<TMP_Text>();
            if (text != null) text.text = storage.name;

            button.onClick.RemoveAllListeners();
                                                //เปลี่ยนจาก _OnModelSelected(modelIndex));
            button.onClick.AddListener(() => _OnModelSelected(_storage.models.IndexOf(storage))); 

                                   //เปลี่ยนจาก (modelIndex != currentIndex);
            button.interactable = (_storage.models.IndexOf(storage) != currentIndex); 
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

    //======================ModelSearchFilter=========================
    private void _RefreshSearchResults()
    {
        _filteredModels = ModelSearchFilter.Filter(_storage.models, _searchKeyword);
        _currentPage = 0;
        ModelCatalogsUI();//move from Start
    }

    private void OnSearchValueChanged(string keyword)
    {
        _searchKeyword = keyword;
        _RefreshSearchResults();
    }
    //======================ModelSearchFilter=========================

}


