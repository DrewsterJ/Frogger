using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public int lives = 3;
    public int score;
    
    public List<VisualElement> hearts = new List<VisualElement>();
    public VisualElement root;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject camera;
    private AudioControl audioControlScript;
    
    [HideInInspector] public Label pauseMenuLabel;
    [HideInInspector] public VisualElement pauseMenu;
    [HideInInspector] public Label mainMenuLabel;
    [HideInInspector] public VisualElement mainMenu;
    [HideInInspector] public Label victoryMenuLabel;
    [HideInInspector] public VisualElement victoryMenu;
    [HideInInspector] public Label lossMenuLabel;
    [HideInInspector] public VisualElement settingsMenu;
    [HideInInspector] public Button muteUnmuteMusicButton;
    
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        hearts.Add(root.Query<VisualElement>("heartOne"));
        hearts.Add(root.Query<VisualElement>("heartTwo"));
        hearts.Add(root.Query<VisualElement>("heartThree"));
        pauseMenu = root.Query<VisualElement>("pauseMenu");
        pauseMenu.visible = false;
        mainMenu = root.Query<VisualElement>("mainMenu");
        mainMenu.visible = true;
        pauseMenuLabel = root.Query<Label>("pauseMenuLabel");
        pauseMenuLabel.visible = false;
        mainMenuLabel = root.Query<Label>("mainMenuLabel");
        mainMenuLabel.visible = false;
        victoryMenu = root.Query<VisualElement>("victoryMenu");
        victoryMenu.visible = false;
        victoryMenuLabel = root.Query<Label>("victoryMenuLabel");
        victoryMenuLabel.visible = false;
        lossMenuLabel = root.Query<Label>("lossMenuLabel");
        lossMenuLabel.visible = false;
        settingsMenu = root.Query<VisualElement>("settingsMenu");
        settingsMenu.visible = false;

        audioControlScript = camera.GetComponent<AudioControl>();
        gameManagerScript = gameManager.GetComponent<GameManager>();

        Button pauseMenuResumeButton = root.Query<Button>("pauseMenuResumeButton");
        Button pauseMenuSettingsButton = root.Query<Button>("pauseMenuSettingsButton");
        Button pauseMenuQuitButton = root.Query<Button>("pauseMenuQuitButton");
        Button mainMenuStartButton = root.Query<Button>("mainMenuStartButton");
        Button mainMenuSettingsButton = root.Query<Button>("mainMenuSettingsButton");
        Button mainMenuQuitButton = root.Query<Button>("mainMenuQuitButton");
        Button victoryRestartButton = root.Query<Button>("victoryRestartButton");
        Button victoryMainMenuButton = root.Query<Button>("victoryMainMenuButton");
        Button closeSettingsMenuButton = root.Query<Button>("closeSettingsMenuButton");
        Button playNextSongButton = root.Query<Button>("playNextSongButton");
        muteUnmuteMusicButton = root.Query<Button>("muteUnmuteMusicButton");
        
        pauseMenuResumeButton.clicked += () => gameManagerScript.SwitchPause();
        pauseMenuQuitButton.clicked += () => gameManagerScript.StopGame();
        mainMenuStartButton.clicked += () => gameManagerScript.StartGame();
        mainMenuQuitButton.clicked += Application.Quit;
        victoryRestartButton.clicked += () => gameManagerScript.RestartGame();
        victoryMainMenuButton.clicked += () => gameManagerScript.StopGame();
        pauseMenuSettingsButton.clicked += () => gameManagerScript.SwitchSettings();
        mainMenuSettingsButton.clicked += () => gameManagerScript.SwitchSettings();
        closeSettingsMenuButton.clicked += () => gameManagerScript.SwitchSettings();
        playNextSongButton.clicked += () => audioControlScript.PlayNextGameplaySong();
        muteUnmuteMusicButton.clicked += () => SwapMute();
    }

    public void UpdateScore()
    {
        score += 1;
    }

    public void UpdateLives()
    {
        if (lives <= 0) return;
        
        lives -= 1;
        hearts[lives].SetEnabled(false);
    }

    // Used to mute/unmute game music
    public void SwapMute()
    {
        if (audioControlScript.muted)
        {
            audioControlScript.UnmuteMusic();
            muteUnmuteMusicButton.text = "Mute";
        }
        else
        {
            audioControlScript.MuteMusic();
            muteUnmuteMusicButton.text = "Unmute";
        }
    }
}
