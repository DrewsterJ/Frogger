using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;
    
    private Label scoreText;
    public List<VisualElement> hearts = new List<VisualElement>();
    public VisualElement root;

    [HideInInspector]
    public Label pauseMenuLabel;

    public GameObject gameManager;
    private GameManager gameManagerScript;
    
    [HideInInspector]
    public VisualElement pauseMenu;
    
    [HideInInspector]
    public Label mainMenuLabel;
    
    [HideInInspector]
    public VisualElement mainMenu;
    
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        scoreText = root.Query<Label>("scoreText");
        hearts.Add(root.Query<VisualElement>("heartOne"));
        hearts.Add(root.Query<VisualElement>("heartTwo"));
        hearts.Add(root.Query<VisualElement>("heartThree"));
        pauseMenu = root.Query<VisualElement>("pauseMenu");
        pauseMenu.visible = false;
        scoreText.text = "Score: " + score;
        mainMenu = root.Query<VisualElement>("mainMenu");
        mainMenu.visible = true;
        pauseMenuLabel = root.Query<Label>("pauseMenuLabel");
        pauseMenuLabel.visible = false;
        mainMenuLabel = root.Query<Label>("mainMenuLabel");
        mainMenuLabel.visible = false;

        Button pauseMenuResumeButton = root.Query<Button>("pauseMenuResumeButton");
        Button pauseMenuSettingsButton = root.Query<Button>("pauseMenuSettingsButton");
        Button pauseMenuQuitButton = root.Query<Button>("pauseMenuQuitButton");
        Button mainMenuStartButton = root.Query<Button>("mainMenuStartButton");
        Button mainMenuSettingsButton = root.Query<Button>("mainMenuSettingsButton");
        Button mainMenuQuitButton = root.Query<Button>("mainMenuQuitButton");

        gameManagerScript = gameManager.GetComponent<GameManager>();

        pauseMenuResumeButton.clicked += () => gameManagerScript.SwitchPause();
        pauseMenuQuitButton.clicked += () => gameManagerScript.StopGame();
        mainMenuStartButton.clicked += () => gameManagerScript.StartGame();
        mainMenuQuitButton.clicked += Application.Quit;
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        if (lives <= 0) return;
        
        lives -= 1;
        hearts[lives].SetEnabled(false);
    }
}
