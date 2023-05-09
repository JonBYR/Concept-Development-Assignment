using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCull : MonoBehaviour
{
	void Start()
	{
		Destroy(this.gameObject, 3);
	}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && this.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            Debug.Log("Enemy Collider entered");
        }
    }
}
