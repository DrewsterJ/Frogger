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
    private List<VisualElement> hearts = new List<VisualElement>();
    public VisualElement root;

    [HideInInspector]
    public Label pauseMenuLabel;

    public GameObject gameManager;
    private GameManager gameManagerScript;
    
    [HideInInspector]
    public VisualElement pauseMenu;
    
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
        pauseMenuLabel = root.Query<Label>("pauseMenuLabel");
        pauseMenuLabel.visible = false;

        Button resumeButton = root.Query<Button>("resumeButton");
        Button settingsButton = root.Query<Button>("settingsButton");
        Button quitButton = root.Query<Button>("quitButton");

        gameManagerScript = gameManager.GetComponent<GameManager>();

        resumeButton.clicked += () => gameManagerScript.SwitchPause();
        quitButton.clicked += Application.Quit;
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
