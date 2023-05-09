using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    float gravityShiftBegin;
    float gravityShiftEnds;
    float originalScale;
    private Rigidbody2D itemRb;
    bool affectGravity = false;
    public bool isEnemy = false;
    public PlayerMovement player;
    public CameraShake shake;
    // Start is called before the first frame update
    void Awake()
    {
        itemRb = GetComponent<Rigidbody2D>();
        originalScale = itemRb.gravityScale; //sets the original gravity scale to a variable
    }
    // Update is called once per frame
    void Update()
    {
        if (player.playerDead == true)
        {
            this.enabled = false;
            shake.enabled = false;
        }
            if (Input.GetButtonDown("ShiftGravity"))
            {
                gravityShiftBegin = Time.time; //starts a timer for when gravity is being affected
                shake.enabled = true;
            }
            if (Input.GetButtonUp("ShiftGravity"))
            {
                gravityShiftEnds = (Time.time - gravityShiftBegin);
                affectGravity = true;
            }
            if (affectGravity == true)
            {
                if (isEnemy == true)
                {
                    GetComponent<EnemyController>().gravity = true;
                }
                itemRb.gravityScale = 0.25f * gravityShiftEnds * 5f;

                gravityShiftEnds -= Time.deltaTime;
                if (gravityShiftEnds < 0)
                {
                    itemRb.gravityScale = originalScale;
                    affectGravity = false;
                    itemRb.velocity = new Vector3(0f, 0f, 0f);
                    if (isEnemy == true)
                    {
                        GetComponent<EnemyController>().gravity = false;
                    }
                }
            }
        
    }
}
