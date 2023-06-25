using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public bool gamePaused { get; private set; } = false;
    private bool showWinScreen = false;
    private bool showLossScreen = false;
    public Inventory<Item> inventory;
    public static GameBehavior Instance { get; private set; }
    private int _playerHP;
    public int playerHP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                labelText = "you lost!";
                showLossScreen = true;
                PauseGame();
            }
        }
    }

    private int _itemsCollected = 0;
    
    public const int maxItems = 3;
    private string labelText = "Collect all " + maxItems + " items and gain your freedom!";


    public void Awake()
    {
        Cursor.visible = false;
        if (Instance == null)
            Instance = this;
        inventory = new Inventory<Item>();
    }

    public void Start()
    {
        ResumeGame();
    }

    void OnGUI()
    {
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
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
