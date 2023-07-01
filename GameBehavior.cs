using PointClick;
using System;
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
    public static string firstSceneName = "Forest";
    private string currentSceneName;
    private SceneLayoutScriptable currentScene;
    public bool gamePaused { get; private set; } = false;
    private bool showWinScreen = false;
    private bool showLossScreen = false;
    public Inventory<Item> inventory;
    public static GameBehavior Instance { get; private set; }
    private Dictionary <string, SceneLayoutScriptable> allScenes;

    private Dictionary<string, ItemScriptable> allItems;


    private GraphicPanel backPanel, controlPanel, itemsPanel;

        
    public const int maxItems = 3;


    public void Awake()
    {
        if (Instance == null)
            Instance = this;

        currentSceneName = firstSceneName;
        /*currentScene = Resources.Load<SceneLayoutScriptable>("Places\\"+currentSceneName);
        if (currentScene == null)
            Debug.LogError($"Enable to load scriptable place {currentSceneName}");*/
            
        inventory = new Inventory<Item>();
        SceneLayoutScriptable[]  tmp = Resources.LoadAll<SceneLayoutScriptable>(FilePaths.places);
        allScenes = new Dictionary<string, SceneLayoutScriptable>();
        foreach (var scene in  tmp)
            allScenes[scene.label] = scene;
        CheckScenes();

        LoadItems();

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
        itemsPanel = GraphicPanelManager.instance.GetPanel("Items");
        if (itemsPanel == null)
            Debug.LogError("Items panel could not be found");

        ResumeGame();
        
        ChangeScene(currentSceneName);
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
        Debug.Log($"loading scene{newSceneName}");
        Globals.instance.mouseMode = Globals.MouseMode.basic;
        currentSceneName = newSceneName;
        /*        currentScene = Resources.Load<SceneLayoutScriptable>("Places\\" + currentSceneName);
                if (currentScene == null)
                    throw new ArgumentException($"Coudl not load scriptable scene {currentSceneName}");*/

        if (allScenes.ContainsKey(currentSceneName))
            currentScene = allScenes[currentSceneName];
        else
            throw new ArgumentException($"cannot find scene {currentSceneName}");

        controlPanel.GetLayer(0, true).Clear();
        DrawBackImage();
        controlPanel.GetLayer(0, true).EnableButtons(currentScene);
        GraphicLayer itemLayer = itemsPanel.GetLayer(0, true);
        
        foreach (var tmpitem in currentScene.items)
        {
            allItems[tmpitem.name].DrawItem(itemLayer.panel);
        }
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

    private void CheckScenes()
    {
        foreach(var scene in allScenes.Keys)
        {
            var check = CheckScene(allScenes[scene]);
            if (check.Count > 0)
            {
                foreach (var problemScene in check)
                    Debug.LogError($"Cannot find connection from {scene} to {problemScene}");
            }
        }

    }
    private List<string> CheckScene(SceneLayoutScriptable sc)
    {
        List<string> problemHotSpot = new();

        foreach (var spot in sc.hotSpots)
        {
            if (!allScenes.ContainsKey(spot.destination))
                problemHotSpot.Add(spot.destination);
        }

        return problemHotSpot;
    }

    private void LoadItems()
    {
        //ItemScriptable[] tmp = Resources.LoadAll<ItemScriptable>(FilePaths.itemsObjects);
        allItems = new();
        foreach (var sc in allScenes.Keys)
        {
            foreach (var tmpitem in allScenes[sc].items)
                if (!allItems.ContainsKey(tmpitem.name))
                {
                    allItems[tmpitem.name] = Resources.Load<ItemScriptable>(FilePaths.itemsObjects + tmpitem.name);
                    if (allItems[tmpitem.name] == null)
                        Debug.LogError($"cannot find the item {tmpitem.name}");
                    else
                    {
                        Debug.Log($"item {tmpitem.name} loaded successfully");
                        allItems[tmpitem.name].position.x = tmpitem.x;
                        allItems[tmpitem.name].position.y = tmpitem.y;
                    }
                }
        }

    }
}
