using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D enemyRb;
    public float enemySpeed = 400f;
    private Vector2 movement;
    private bool facingRight = true;
    private float flipDirection;
    public bool gravity = false;
    EnemyCull enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = this.GetComponent<Rigidbody2D>();
        enemy = this.GetComponent<EnemyCull>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.deadEnemy)
        {
            Vector3 direction = player.position - transform.position; //checks the distance of the player from the enemies position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Maths function that will determine the angle the player is from the enemy
            direction.Normalize();
            flipDirection = direction.x;
            movement = direction;
        }
    }
    private void FixedUpdate()
    {
        if (!enemy.deadEnemy)
        {
            if (gravity == false)
            {
                Move(movement, flipDirection);
            }
        }
    }
    void Move(Vector2 enemyMovement, float flip)
    {
        enemyRb.MovePosition((Vector2)transform.position + (enemyMovement * enemySpeed * Time.deltaTime));
        if (flipDirection < 0 && !facingRight)
        {
            Flip();
        }
        else if (flipDirection > 0 && facingRight)
        {
            Flip();
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
