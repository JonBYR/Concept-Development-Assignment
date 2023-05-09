using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int bossHealth;
    public Color flash;
    public Color normal;
    public float invincibility;
    public int numberOfFlashes;
    public Collider2D bossCollider;
    public SpriteRenderer bossSprite;
    private Rigidbody2D bossRb;
    public Animator bossAnim;
    public AudioManager audioManager;
    public PlayerMovement player;
    private IEnumerator vic;
    private void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        vic = Victory();
    }
    private void Update()
    {
        if (player.playerDead == true) StopCoroutine(vic);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            bossHealth -= 1;
            audioManager.PlayHit();
            StartCoroutine(invincible());
            if (bossHealth == 0)
            {
                audioManager.PlayDeath();
                Destroy(bossCollider);
                bossAnim.SetTrigger("Death");
                StartCoroutine(vic);
            }
            Debug.Log(bossHealth);
        }
    }
    public int getHealth()
    {
        return bossHealth;
    }
    private IEnumerator invincible()
    {
        int temp = 0;
        bossCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            bossSprite.color = flash;
            yield return new WaitForSeconds(invincibility);
            bossSprite.color = normal;
            yield return new WaitForSeconds(invincibility);
            temp++;
        }
        bossCollider.enabled = true;
    }
    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("VictoryScene");
    }
}
