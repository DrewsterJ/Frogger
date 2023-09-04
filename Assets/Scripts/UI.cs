using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    private Label livesText;
    private Label scoreText;
    public VisualElement root;
    
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        livesText = root.Query<Label>("livesText");
        scoreText = root.Query<Label>("scoreText");
        lives = 3;
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    public void updateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void updateLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;
    }
}
