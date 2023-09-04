using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    private Label livesText;
    private Label scoreText;
    private List<VisualElement> hearts = new List<VisualElement>();
    public VisualElement root;
    
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        livesText = root.Query<Label>("livesText");
        scoreText = root.Query<Label>("scoreText");
        GroupBox heartsContainer = root.Query<GroupBox>("heartsGroup");
        hearts.Add(root.Query<VisualElement>("heartOne"));
        hearts.Add(root.Query<VisualElement>("heartTwo"));
        hearts.Add(root.Query<VisualElement>("heartThree"));
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
        Debug.Log(hearts.Count);
        lives -= 1;
        hearts[lives].SetEnabled(false);
        hearts.RemoveAt(lives);
        livesText.text = "Lives: " + lives;
    }
}
