using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private SpriteRenderer bulletSprite;
    public GameObject enemyBullet;
    public Transform enemyPosition;
    public float timer;
    public Animator animator;
    float speed = 10f;
    public EnemyCull enemy;
    // Start is called before the first frame update
    void Start()
    {
        bulletSprite = enemyBullet.GetComponent<SpriteRenderer>();
    }
    public void ResetBool()
    {
        animator.SetBool("IsShoot", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!enemy.deadEnemy)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                animator.SetBool("IsShoot", true);
                if (enemyPosition.name == "EnemyProjectilePositionA")
                {
                    GameObject enemyBulletNew = Instantiate(enemyBullet, enemyPosition.position, enemyPosition.rotation);
                    bulletSprite.flipX = true;
                    enemyBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                }
                else if (enemyPosition.name == "EnemyProjectilePositionB")
                {
                    GameObject enemyBulletNew = Instantiate(enemyBullet, enemyPosition.position, enemyPosition.rotation);
                    bulletSprite.flipX = false;
                    enemyBulletNew.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
                }
                timer = 10.0f;
                Invoke("ResetBool", 0.5f);
            }
        }
    }
}
