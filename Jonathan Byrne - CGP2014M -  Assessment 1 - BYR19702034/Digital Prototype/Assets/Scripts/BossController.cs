using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer bossSprite;
    private Rigidbody2D bossRb;
    public float speed;
    public float timer;
    bool facingRight = true;
    bool goingDown = true;
    bool rightSide = false;
    bool arrived = false;
    public GameObject rightWaypoint;
    public GameObject leftWaypoint;
    public Animator animator;
    public BossHealth health;
    void Start()
    {
        bossSprite = GetComponent<SpriteRenderer>();
        bossRb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WaypointTop")
        {
            goingDown = true;
        }
        else if (collision.gameObject.tag == "WayPointBottom")
        {
            goingDown = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (health.getHealth() > 0)
        {
            if (!rightSide)
            {
                Vector3 movement = new Vector3(rightWaypoint.transform.position.x, this.transform.position.y, 0f);
                this.transform.position = Vector3.MoveTowards(this.transform.position, movement, speed * Time.deltaTime);
                if ((transform.position.x == rightWaypoint.transform.position.x) && arrived == true)
                {
                    Flip();
                    animator.SetBool("GoingLeft", false);
                    animator.SetBool("GoingRight", false);
                    arrived = false;
                }
            }
            if (rightSide)
            {
                Vector3 movement = new Vector3(leftWaypoint.transform.position.x, this.transform.position.y, 0f);
                this.transform.position = Vector3.MoveTowards(this.transform.position, movement, speed * Time.deltaTime);
                if ((transform.position.x == leftWaypoint.transform.position.x) && arrived == true)
                {
                    Flip();
                    animator.SetBool("GoingLeft", false);
                    animator.SetBool("GoingRight", false);
                    arrived = false;
                }
            }
            timer -= Time.deltaTime;
            if (goingDown == true) this.transform.Translate(Vector3.down * speed * Time.deltaTime);
            else if (goingDown == false) this.transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (timer < 0)
            {
                if (rightSide == false)
                {
                    rightSide = true;
                    arrived = true;
                    if (this.transform.position.x == rightWaypoint.transform.position.x)
                    {
                        animator.SetBool("GoingRight", false);
                        animator.SetBool("GoingLeft", true);
                    }
                    timer = 7.5f;
                }
                else if (rightSide == true)
                {
                    rightSide = false;
                    arrived = true;
                    if (this.transform.position.x == leftWaypoint.transform.position.x)
                    {
                        animator.SetBool("GoingLeft", false);
                        animator.SetBool("GoingRight", true);
                    }
                    timer = 7.5f;
                }

            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
