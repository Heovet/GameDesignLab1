using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public Transform gameCamera;

    public GameEvents GoombaDies;
    public GameEvents MarioDies;

    public Animator marioAnimator;

    // for audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;

    [Header("Death Velocity")]
    public Vector2 deathImpulse;

    // state
    [System.NonSerialized]
    public bool alive = true;

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    // Start is called before the first frame update

    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);

    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
/*        Debug.Log("Touch Ground");*/

        if (col.gameObject.CompareTag("Ground"))
        {
/*            Debug.Log("Sending animator: Mario on ground");*/
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && onGroundState == true)
        {
            Debug.Log("Collided with goomba!");
            // play death animation
            marioAnimator.Play("Mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
/*            Time.timeScale = 0.0f;*/
        }
/*        else if (other.gameObject.CompareTag("Enemy") && onGroundState == false)
        {
*//*            Debug.Log("Goomba Stomp");*//*
            GoombaDies.Raise(this, true);
        }*/

    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {

        if (alive)
        {

            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            // other code
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                // check if it doesn't go beyond maxSpeed
                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }

            // stop
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                // stop
                marioBody.velocity = Vector2.zero;
            }

            // jump instruction
            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                marioAnimator.SetBool("onGround", onGroundState);
            }
        }
    }

    public void RestartButtonCallback(int input)
    {
/*        Debug.Log("Restart!");*/
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-24.11f, -5.96f, 0.0f);

        // reset camera position
        gameCamera.position = new Vector3(-20, -2, -100);

        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        // reset score
        jumpOverGoomba.score = 0;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene
        MarioDies.Raise(this, true);
    }

}