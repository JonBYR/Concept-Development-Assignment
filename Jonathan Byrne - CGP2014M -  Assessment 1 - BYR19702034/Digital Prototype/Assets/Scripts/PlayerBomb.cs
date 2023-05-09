using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    private SpriteRenderer bombSprite;
    public GameObject playerBomb;
    public Transform playerPosition;
    public float timer;
    float horizontal;
    bool isLeft = false;
    float speed = 10f;
    PlayerMovement player;
    private void Start()
    {
        bombSprite = playerBomb.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0)
        {
            isLeft = true;
        }
        else if (horizontal >= 0)
        {
            isLeft = false;
        }
        timer -= Time.deltaTime;
        if (Input.GetButtonDown("Bomb"))
        {
            if (timer < 0)
            {
                GameObject playerBulletNew = Instantiate(playerBomb, playerPosition.position, playerPosition.rotation);
                if (horizontal < 0)
                {
                    bombSprite.flipX = true;
                    playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                }
                else if (horizontal > 0)
                {
                    bombSprite.flipX = true;
                    playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
                }
                else if (horizontal == 0)
                {
                    if (player.facingRight == false)
                    {
                        bombSprite.flipX = true;
                        playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
                    }
                    else if (player.facingRight == true)
                    {
                        bombSprite.flipX = true;
                        playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                    }
                }
                timer = 1.0f;
            }
        }
    }
}
