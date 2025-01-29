using UnityEngine;

public class ScoringTrigger : MonoBehaviour
{
    private bool _scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_scored && other.CompareTag("Chicken"))
        {
            _scored = true;
            Score.scoreValue += 1; // Increment score
            Destroy(gameObject, 1f); // Destroy the trigger after scoring
        }
    }
}
