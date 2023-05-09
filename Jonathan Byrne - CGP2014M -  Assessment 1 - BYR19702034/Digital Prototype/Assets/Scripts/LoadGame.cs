using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void StartPrototype()
    {
        SceneManager.LoadScene("PrototypeScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartPrototype", 8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
