using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HordeTrigger : MonoBehaviour
{
    public GameObject joel;
    public GameObject infected;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {

        string sceneName = currentScene.name;
        
        if (GameObject.ReferenceEquals(other.gameObject, joel)) {
            for (int i = 0; i <25; i++)
            {
                if (sceneName.Equals("LevelOne"))
                    Instantiate(infected, new Vector3(-70, 5.610f, 38.22f), Quaternion.identity);
                else if(sceneName.Equals("LevelTwo"))
                    Instantiate(infected, new Vector3(-97.28448f, 5f, -91.03821f), Quaternion.identity);
            }
        }
    }
}
