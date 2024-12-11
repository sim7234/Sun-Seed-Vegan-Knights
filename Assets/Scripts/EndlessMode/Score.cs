using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

   [HideInInspector] public int score;

    static int highScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ("Score: " + score).ToString();

        if (score > highScore)
        {
            highScore = score;
        }
    }
}
