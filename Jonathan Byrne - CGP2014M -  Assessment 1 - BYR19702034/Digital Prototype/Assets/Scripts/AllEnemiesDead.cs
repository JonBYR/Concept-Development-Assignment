using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AllEnemiesDead : MonoBehaviour
{
    public List<GameObject> listOfEnemies = new List<GameObject>();
    int enemiesInScene;
    public PlayerMovement player;
    private IEnumerator dead;
    // Start is called before the first frame update
    void Start()
    {
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Debug.Log(listOfEnemies.Count);
        dead = AllDead();
        enemiesInScene = listOfEnemies.Count;
    }
    void Update()
    {
        if (player.playerDead == true) StopCoroutine(dead);
    }
    public void deadEnemy()
    {
        enemiesInScene = enemiesInScene - 1;
        Debug.Log(enemiesInScene);
        if (enemiesInScene == 0)
        {
            StartCoroutine(dead);
        }
    }
    public IEnumerator AllDead()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("BossScene");
    }
}
