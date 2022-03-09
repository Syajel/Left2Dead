using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public GameObject joel;
    Scene currentScene;
    public GameObject gameOverScreen;
    //public CompanionManager companionController;
    //public CompanionController companion;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        //companion = companionController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        string sceneName = currentScene.name;
        if (GameObject.ReferenceEquals(other.gameObject, joel))
        {
            if (sceneName.Equals("LevelOne"))
            {
                SceneManager.LoadScene(sceneName: "LevelTwo");
                BackGroundMusic.Scene2();
            }
            else if (sceneName.Equals("LevelTwo"))
            {
                SceneManager.LoadScene(sceneName: "LevelThree");
                //companion.GetComponent<GameObject>().transform.position = new Vector3(-17.49464f, 6.38463f, -88.98f);

            }
            else if (sceneName.Equals("LevelThree"))
            {
                gameOverScreen.SetActive(true);
            }
        }
    }
}
