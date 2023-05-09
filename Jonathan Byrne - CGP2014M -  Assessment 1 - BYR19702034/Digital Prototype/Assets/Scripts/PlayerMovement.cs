using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    float horizontalDirection = 0;
    bool goingUp = false; //bool to see if the player has pressed the up arrow
    public float playerSpeed = 40f;
    private Rigidbody2D playerRb;
    internal bool facingRight = true;
    public bool playerDead = false;
    private Vector3 playerVector = Vector3.zero;
    public float upwardsForce = 400f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; //determines how much to smooth the movement
    public Animator animator;
    public AudioManager audioManager;
    public PlayerBomb bomb;
    public PlayerBullet bullet;
    public ChangeGravity gravity;
    private int triggerEntered = 1;
    BoxCollider2D playerCollider;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal") * playerSpeed;
        if (Input.GetButtonDown("MoveUp"))
        {
            goingUp = true;
            animator.SetBool("IsGoingUp", true);
        }
        if (triggerEntered == 0) StartCoroutine(GameOver());
    }
    public void UpButtonReleased()
    {
        animator.SetBool("IsGoingUp", false);
    }
    private void FixedUpdate()
    {
        Move(horizontalDirection * Time.fixedDeltaTime); //determines the player movement
    }
    public void Move(float movementX)
    {
        Vector3 newVelocity = new Vector2(movementX * 10f, playerRb.velocity.y); //determines the velocity so the player moves
        playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, newVelocity, ref playerVector, movementSmoothing);
        if (movementX < 0 && !facingRight)
        {
            Flip(); //if the player is moving left but the sprites is currently facing left
        }
        else if (movementX > 0 && facingRight)
        {
            Flip(); //if the player is moving right but the player is facing right
        }
        if (goingUp)
        {
            playerRb.AddForce(new Vector2(0f, 100f));
            goingUp = false;
            Invoke("UpButtonReleased", 0.5f); //method gets called each time the bool is changed, but with a delay
        }
    }
    private void Flip()
    {
        facingRight = !facingRight; //switches the bool that determines where the player is facing
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; //switches where the player is facing
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WaypointTop" || collision.gameObject.tag == "WayPointBottom")
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Death")
        {
            triggerEntered = 0;
            audioManager.PlayDeath();
            animator.SetTrigger("Death");
            playerDead = true;
            playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
            bomb.enabled = false;
            bullet.enabled = false;
            playerCollider.enabled = false;
            Debug.Log("Enemy Collider entered");
        }
    }
    IEnumerator GameOver()
    {
        Debug.Log("Coroutined");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOverScene");
    }
}
