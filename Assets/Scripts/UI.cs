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
    
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        scoreText = root.Query<Label>("scoreText");
        hearts.Add(root.Query<VisualElement>("heartOne"));
        hearts.Add(root.Query<VisualElement>("heartTwo"));
        hearts.Add(root.Query<VisualElement>("heartThree"));
        scoreText.text = "Score: " + score;
    }

    public void updateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void updateLives()
    {
        if (lives <= 0) return;
        
        lives -= 1;
        hearts[lives].SetEnabled(false);
    }
}
