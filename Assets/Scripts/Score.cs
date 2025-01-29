using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    private Text score;
    private ChickenController chicken; // Reference to the ChickenController script


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = GetComponent<Text>();

        if (score == null)
        {
            Debug.LogError("Text component not found! Make sure this script is attached to a UI Text GameObject.");
        }

        // Find the chicken in the scene (assuming it has the "Chicken" tag)
        GameObject chickenObject = GameObject.FindWithTag("Chicken");
        if (chickenObject != null)
        {
            chicken = chickenObject.GetComponent<ChickenController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score != null)
        {
            score.text = scoreValue.ToString();
        }
        if (chicken != null && chicken.dead)
        {
            scoreValue = 0; // Reset score when chicken is dead
        }
    }
}
