using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Movement")]
    public float speed;
    [Header("Jumping")]
    public float flapForce;

    [Header("Animation")]
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteCollision;


    private Rigidbody2D chickenRigidBody;
    private AudioSource _chickenAudioSource;
    private SpriteRenderer _chickenSpriteRenderer;

    private bool dead = false;


    public AudioClip[] chickenSounds = new AudioClip[5];
    


    private void HandleJump()
    {
        if (dead)
        {
            return;

        }

        Vector2 velocity = chickenRigidBody.linearVelocity;
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            velocity.y = flapForce;
            chickenRigidBody.linearVelocity = velocity;
        }

    }

    private void PlayFlappingSound()
    {
        if (dead)
        {
            return;

        }

        AudioClip clip = chickenSounds[Random.Range(0, chickenSounds.Length)];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _chickenAudioSource.PlayOneShot(clip);
        }

    }

    private void MoveChickenForward()
    {
        Vector2 velocity = chickenRigidBody.linearVelocity;

        if (dead)
        {
            velocity.x = 0;
            chickenRigidBody.linearVelocity = velocity;
            return;

        }

        velocity.x = speed;
        chickenRigidBody.linearVelocity = velocity;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dead = false;

    }

    private void AnimateChickeSprite()
    {
     
        if (dead)
        {
            return;

        }
        if (chickenRigidBody.linearVelocity.y > 0)
        {
            _chickenSpriteRenderer.sprite = spriteUp;
        }
        else
        {
            _chickenSpriteRenderer.sprite = spriteDown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with obstacle detected!");
            dead = true;
            _chickenSpriteRenderer.sprite = spriteCollision;

            Invoke("ReloadScene", 1f);
        }
    }
    void Start()
    {
        chickenRigidBody = GetComponent<Rigidbody2D>();
        _chickenAudioSource = GetComponent<AudioSource>();
        _chickenSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 normalizedDir = chickenRigidBody.linearVelocity.normalized;
        transform.right = normalizedDir;
        HandleJump();
        MoveChickenForward();
        PlayFlappingSound();
        AnimateChickeSprite();

     
    }
}
