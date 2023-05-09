using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private SpriteRenderer bulletSprite;
    public GameObject playerBullet;
    public Transform playerPosition;
    public float timer;
    float horizontal;
    bool isLeft = false; 
    float speed = 10f;
    PlayerMovement player;
    private void Start()
    {
        bulletSprite = playerBullet.GetComponent<SpriteRenderer>();
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
        if (Input.GetButtonDown("Shoot"))
        {
            if (timer < 0)
            {
                GameObject playerBulletNew = Instantiate(playerBullet, playerPosition.position, playerPosition.rotation);
                if (horizontal < 0)
                {
                    bulletSprite.flipX = true;
                    playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                }
                else if (horizontal > 0)
                {
                    bulletSprite.flipX = true;
                    playerBulletNew.GetComponent<Rigidbody2D>().velocity =  new Vector2(speed, 0f);
                }
                else if(horizontal == 0)
                {
                    Debug.Log("Code executed");
                    if (player.facingRight == false)
                    {
                        bulletSprite.flipX = true;
                        playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
                    }
                    else if (player.facingRight == true)
                    {
                        bulletSprite.flipX = true;
                        playerBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                    }
                }
                timer = 1.0f;
            }
        }
    }
}
