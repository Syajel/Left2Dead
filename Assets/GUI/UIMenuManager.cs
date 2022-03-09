using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public GameObject companionsMenu;
    List<GameObject> menus = new List<GameObject>();
    public GameObject startMenu;  
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject howtoplayMenu;

    Button playButton;
    Button optionsButton;
    Button quitButton;
    Button creditsButton;
    Button howButton;

    Slider musicSlider;

    // Start is called before the first frame update
    void Awake()
    {
        menus.Add(startMenu);
        menus.Add(pauseMenu);               
        menus.Add(optionsMenu);                
        menus.Add(creditsMenu);                        
        menus.Add(howtoplayMenu); 

        playButton = GameObject.Find("Play-Button").GetComponent<Button>();
        playButton.onClick.AddListener(Play);
        optionsButton = GameObject.Find("Options-Button").GetComponent<Button>();
        optionsButton.onClick.AddListener(OptionsMenu);
        quitButton = GameObject.Find("Quit-Button").GetComponent<Button>();
        quitButton.onClick.AddListener(Quit);    
        creditsButton = GameObject.Find("Credits-Button").GetComponent<Button>();
        creditsButton.onClick.AddListener(CreditsMenu);  
        howButton = GameObject.Find("HowToPlay-Button").GetComponent<Button>();
        howButton.onClick.AddListener(HowToPlayMenu);  
        
        GameObject[] backButtongo = GameObject.FindGameObjectsWithTag("BackButton");
        foreach (var button in backButtongo)
            button.GetComponent<Button>().onClick.AddListener(OnBackButton);
        
        musicSlider =  GameObject.Find("Music-Slider").GetComponent<Slider>();
        musicSlider.onValueChanged.AddListener(delegate { MusicVolumeChanged(); });

        StartMenu();
    }
    

    void StartMenu(){
        startMenu.SetActive(true);
        foreach (var menu in menus)
            if(menu != startMenu)
                menu.SetActive(false);
        
    }

    void Play(){
        companionsMenu.gameObject.SetActive(true);
        startMenu.SetActive(false);        
    }

    void Quit(){
        Application.Quit();
    }

    void OptionsMenu(){
        optionsMenu.SetActive(true);
        foreach (var menu in menus)
            if(menu != optionsMenu)
                menu.SetActive(false);
    }

    void CreditsMenu(){
        creditsMenu.SetActive(true);
        foreach (var menu in menus)
            if(menu != creditsMenu)
                menu.SetActive(false);
    }

    void HowToPlayMenu(){
        howtoplayMenu.SetActive(true);
        foreach (var menu in menus)
            if(menu != howtoplayMenu)
                menu.SetActive(false);
    }   
/*
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if(pauseMenu.activeSelf) Time.timeScale = 0; else Time.timeScale = 1;
        }
    }
*/
    void PauseMenu(){
        pauseMenu.SetActive(true);
        foreach (var menu in menus)
            if(menu != pauseMenu)
                menu.SetActive(false);
    }

    void OnBackButton(){
        StartMenu();
    }

    public void MusicVolumeChanged()
    {
         AudioListener.volume = musicSlider.value;
    }
}
