using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWalls : MonoBehaviour
{
    public AudioManager audioManager;
    Rigidbody2D enemyRb;
    public Animator enemyAnim;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            audioManager.PlayDeath();
            enemyAnim.SetTrigger("Death");
            enemyRb.constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(gameObject, 1f);
            this.GetComponent<Collider2D>().enabled = !this.GetComponent<Collider2D>().enabled;
            Debug.Log("Collider entered");
        }
    }
}
