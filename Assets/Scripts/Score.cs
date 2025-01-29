using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    private Text score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = GetComponent<Text>();

        if (score == null)
        {
            Debug.LogError("Text component not found! Make sure this script is attached to a UI Text GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score != null)
        {
            score.text = scoreValue.ToString();
        }
    }
}
