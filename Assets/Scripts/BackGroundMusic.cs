using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().play("StartScreen");
    }

    public static void Scene1(){
        FindObjectOfType<AudioManager>().stop("StartScreen");
        FindObjectOfType<AudioManager>().play("Level_Design_1");
    }

    public static void Scene2(){
        FindObjectOfType<AudioManager>().stop("Level_Design_1");
        FindObjectOfType<AudioManager>().play("Level_Design_2");
    }

   /* public static void Scene3(){
        FindObjectOfType<AudioManager>().stop("Level_Design_2");
        FindObjectOfType<AudioManager>().play("Level_Design_3");
    }*/
}
