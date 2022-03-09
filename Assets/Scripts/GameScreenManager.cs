using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScreenManager : MonoBehaviour
{
    //Timer
    TMPro.TextMeshProUGUI timerText;
    public bool useTimer = false;
    public float timerInseconds = 0.0f;

    //First camera
    GameObject fps;
    //Second Camera
    Camera secondCam;
    public GameObject gameScreen;
    Text    amountCountPlayer;
    public CompanionManager companion;
    Text companionName;
    Text ammoCountCompanion;
    Slider playerHealthBar;

    WeaponSwitching weaponScript;
    GrenadeSwitching grenadeScript;

    Image grenadeImg;

    public List<WeaponSlot> weapon_holder = new List<WeaponSlot>();
    Image weaponImg;

    public GameObject craftingScreen;    
    public GameObject pauseScreen;
    Button resumeGame;
    Button restartGame;
    Button quitGame;

    public GameObject gameOverScreen;
    Button gameOverQuit;
    Button gameOverRestart;

    //crossHair
    Image crossHair;

    //on dead timer
    float deadTimer = 6.0f;
    bool openedGameOverScreen = false;
    
    void Start()
    {
        fps = Camera.main.transform.parent.gameObject;
        foreach (var cam in Camera.allCameras)
        {
            if(cam.name != Camera.main.name)
                secondCam = cam;
        }   
        //Game Screen
        playerHealthBar = gameScreen.GetComponentInChildren<Slider>();
        playerHealthBar.maxValue = JoelMeters.maxHp;

        Text[] texts = gameScreen.GetComponentsInChildren<Text>();
        foreach (Text item in texts)
        {
            if(item.name.Equals("CompanionAmmoCount")){
                ammoCountCompanion = item;
            }
            else if(item.name.Equals("PlayerAmmoCount")){
                amountCountPlayer = item;
            }
            else if(item.name.Equals("CompanionName")){
                companionName = item;
                companionName.text = companion.instance.companion.name;
            }      
        }

        

        Transform[] images = gameScreen.GetComponentsInChildren<Transform>();
        
        foreach (Transform item in images)
        {
            if(item.name.Equals("GrenadePanel")){
                grenadeImg = item.GetChild(0).GetComponent<Image>();
            }else if(item.name.Equals("WeaponPanel")){
                weaponImg = item.GetChild(0).GetComponent<Image>();
            }else if(item.name.Equals("CrossHair")){
                crossHair = item.GetComponent<Image>();
            }else if(item.name.Equals("Timer")){
                timerText = item.GetComponent<TMPro.TextMeshProUGUI>();
            }
        }

        weaponScript = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponSwitching>();
        grenadeScript = GameObject.FindGameObjectWithTag("GrenadeManager").GetComponent<GrenadeSwitching>();

        //Pause
        Button[] pauseButtons = pauseScreen.GetComponentsInChildren<Button>();
        foreach (var item in pauseButtons)
        {
            if(item.name.Equals("Resume"))
                resumeGame = item;
            else if(item.name.Equals("Restart"))
                restartGame = item;
            else if(item.name.Equals("Quit"))
                quitGame = item;
        }
        resumeGame.onClick.AddListener(ResumeGame);
        restartGame.onClick.AddListener(RestartGame);
        quitGame.onClick.AddListener(QuitGame);

        //GameOver
        Button[] gameOverButtons = gameOverScreen.GetComponentsInChildren<Button>();
        foreach (var item in gameOverButtons)
        {
            if(item.name.Equals("Restart"))
                gameOverRestart = item;
            else if(item.name.Equals("Quit"))
                gameOverQuit = item;
        }
        gameOverRestart.onClick.AddListener(RestartGame);
        gameOverQuit.onClick.AddListener(QuitGame);

        //GameOver();

        craftingScreen.gameObject.SetActive(true);

        if(!useTimer)
            timerText.gameObject.SetActive(false);
        else timerText.gameObject.SetActive(true);
    }

    void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void ResumeGame(){
        HideACursor();
        pauseScreen.SetActive(false);
    }

    void QuitGame(){
        Application.Quit();
    }

    void GameOver(){
        //Hide main camera
        fps.SetActive(false);

        Time.timeScale = 0;
        //Show Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //hide game play (health/ammo/..)
        gameScreen.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if(openedGameOverScreen) return;
        //Timer
        if(useTimer){
            if(timerInseconds > 0 && !pauseScreen.activeSelf){
            timerInseconds -= Time.deltaTime;
            timerText.text =  Mathf.FloorToInt(timerInseconds/60).ToString() +
                                "." +
                            Mathf.FloorToInt(timerInseconds%60).ToString();
            }else timerText.text = "TIMEOUT !";
        }
        //update player health
        playerHealthBar.value = JoelMeters.curHp;
        if(JoelMeters.curHp <= 0 && !openedGameOverScreen){
            if(deadTimer >= 0){
                deadTimer -= Time.deltaTime;
            }else{
                openedGameOverScreen = true;
                GameOver();
            }     
        }


        //update ammo count
        ammoCountCompanion.text = companion.instance.companionAmmo.ToString();
        
        
        
        foreach (WeaponSlot weap in weapon_holder)
        {
            //update grenade image
            if(weap.name.Equals(grenadeScript.gren.name))
                grenadeImg.sprite = weap.sprite;
            
            //update grenade image
            if(weap.name.Equals(weaponScript.weap.name)){
                weaponImg.sprite = weap.sprite;
                amountCountPlayer.text = 
                weaponScript.weap.GetComponent<Gun>().currentAmmo.ToString() +
                " / " + 
                weaponScript.weap.GetComponent<Gun>().heldAmmo.ToString();
            }
                
            
        }

        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
            //pause
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            if(pauseScreen.activeSelf)
            {                    
                    ShowACursor();
                    resumeGame.interactable = true;
                    restartGame.interactable = true;
                    quitGame.interactable = true;                
            }
            else 
            {
                HideACursor();
            }
        }
        else if(!pauseScreen.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.T)){
                craftingScreen.GetComponent<InventoryUI>().UpdateUI();
                craftingScreen.SetActive(!craftingScreen.activeSelf);
                if(craftingScreen.activeSelf) 
                {
                    Time.timeScale = 0; 
                    ShowACursor();
                }
                else 
                {
                    Time.timeScale = 1;
                    HideACursor();
                }
            }
        }
        
        
    }

    void ShowACursor(){
        //Hide main camera
        fps.SetActive(false);
        //Show second camera
        secondCam.gameObject.SetActive(true);
        //Show Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //hide game play (health/ammo/..)
        gameScreen.gameObject.SetActive(false);
    }

    void HideACursor(){
        //Hide second camera
        secondCam.gameObject.SetActive(false);

        fps.SetActive(true);         
        //Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //show game play (health/ammo/..)
        gameScreen.gameObject.SetActive(true);


    }
}
