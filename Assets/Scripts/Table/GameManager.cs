using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ModelStorage modelStorage;
    public UIManager uiManager { get; private set; }
    public InputManager inputManager { get; private set; }
    public TableManager tableManager { get; private set; }

    private void Awake()
    {
        Instance = this;
        uiManager = GetComponentInChildren<UIManager>();
        inputManager = GetComponentInChildren<InputManager>();
        tableManager = GetComponentInChildren<TableManager>();

        modelStorage.LoadModels();
    }
}