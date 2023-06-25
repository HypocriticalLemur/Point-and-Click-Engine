using PointClick;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VNENGINE;

public class GameBehavior : MonoBehaviour
{
    public static string firstSceneName = "Porog";
    private string currentSceneName;
    private SceneLayoutScriptable currentScene;
    public bool gamePaused { get; private set; } = false;
    private bool showWinScreen = false;
    private bool showLossScreen = false;
    public Inventory<Item> inventory;
    public static GameBehavior Instance { get; private set; }


    private GraphicPanel backPanel;
    private GraphicPanel controlPanel;


    private int _itemsCollected = 0;
    
    public const int maxItems = 3;
    private string labelText = "Collect all " + maxItems + " items and gain your freedom!";


    public void Awake()
    {
        if (Instance == null)
            Instance = this;

        string currentSceneName = firstSceneName;
        currentScene = Resources.Load<SceneLayoutScriptable>("Places\\"+currentSceneName);
        if (currentScene == null)
            Debug.LogError($"Enable to load scriptable place {currentSceneName}");

        inventory = new Inventory<Item>();
    }

    void Start()
    {
        StartCoroutine(Running());
    }
    IEnumerator Running()
    {
        backPanel    = GraphicPanelManager.instance.GetPanel("Background");
        if (backPanel == null)
            Debug.LogError("Background panel could not be found");
        controlPanel = GraphicPanelManager.instance.GetPanel("Controls");
        if (controlPanel == null)
            Debug.LogError("Control panel could not be found");
        ResumeGame();
        DrawBackImage();
        controlPanel.GetLayer(0,true).EnableButtons(currentScene);
        UnityEngine.Cursor.visible = true;
        yield return null;
    }

    private void DrawBackImage()
    {
        GraphicLayer layer = backPanel.GetLayer(0, true);
        layer.SetTexture(currentScene.image);
    }
    /*
    void OnGUI()
    {
        foreach (var s in currentScene.hotSpots)
        {
            if(GUI.Button(new Rect(s.xmin, s.ymin, s.xsize, s.ysize), ""))
            {
                ChangeScene(s.destination);
            }
        }
    }*/


    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void ChangeScene(string newSceneName)
    {
        currentSceneName = newSceneName;
        currentScene = Resources.Load<SceneLayoutScriptable>("Places\\" + currentSceneName);
        if (currentScene == null)
            Debug.LogError($"Coudl not load scriptable scene {currentSceneName}");
        controlPanel.GetLayer(0, true).Clear();
        DrawBackImage();
        controlPanel.GetLayer(0, true).EnableButtons(currentScene);
    }

    private int OnHotSpot(SceneLayoutScriptable sc)
    {
        int spot = -1;


        return spot;
    }
    void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }
    private void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }
}
