using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyCull : MonoBehaviour
{
    AllEnemiesDead dead;
    public AudioManager audioManager;
    public Animator enemyAnim;
    public bool deadEnemy = false;
    bool wallTrigger = false;
    private void Start()
    {
        dead = FindObjectOfType<AllEnemiesDead>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            foreach (BoxCollider2D enemyCollider in GetComponents<BoxCollider2D>()) enemyCollider.enabled = false;
            dead.deadEnemy();
            deadEnemy = true;
            audioManager.PlayDeath();
            enemyAnim.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
        if (collision.gameObject.tag == "Death")
        {
            if (!wallTrigger)
            {
                foreach (BoxCollider2D enemyCollider in GetComponents<BoxCollider2D>()) enemyCollider.enabled = false;
                dead.deadEnemy();
                deadEnemy = true;
                audioManager.PlayDeath();
                enemyAnim.SetTrigger("Death");
                Destroy(gameObject, 1f);
                Debug.Log("Collider entered");
                wallTrigger = true;
            }
        }
    }
}
