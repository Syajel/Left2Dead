using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CompanionSelectionMenu : MonoBehaviour
{
    
    public string nextSceneName;
    Animation curtain;

    //Models
    public GameObject bill;
    public GameObject zoey;
    public GameObject louis;
    public GameObject ellie;

    List<GameObject> models = new List<GameObject>();

    [HideInInspector] public static string SelectedCompanion = "bill";


    //panels
    Transform billPanel;
    Transform zoeyPanel;
    Transform louisPanel;
    Transform elliePanel;

    List<Transform> characters = new List<Transform>();
    int IndexPreview = 0;
    bool isNext = false;

    //Buttons
    Button nextChar;
    Button prevChar;

    Button startGame;
    

    void Start()
    {
        curtain = GetComponent<Animation>();
        curtain.gameObject.SetActive(false);
        //characters
        models.Add(bill);
        models.Add(zoey);
        models.Add(louis);
        models.Add(ellie);
        foreach (var model in models)
            model.SetActive(false);
        //panels
        billPanel = GameObject.Find("Bill_Panel").GetComponent<Transform>();
        characters.Add(billPanel);
        bill.SetActive(true);
        zoeyPanel = GameObject.Find("Zoey_Panel").GetComponent<Transform>();
        zoeyPanel.gameObject.SetActive(false);
        characters.Add(zoeyPanel);
        louisPanel = GameObject.Find("Louis_Panel").GetComponent<Transform>();
        louisPanel.gameObject.SetActive(false);
        characters.Add(louisPanel);
        elliePanel = GameObject.Find("Ellie_Panel").GetComponent<Transform>();
        elliePanel.gameObject.SetActive(false);
        characters.Add(elliePanel);

        //buttons
        nextChar = GameObject.Find("Next-Char").GetComponent<Button>();
        nextChar.onClick.AddListener(NextChar);

        prevChar = GameObject.Find("Prev-Char").GetComponent<Button>();
        prevChar.onClick.AddListener(PrevChar);
        prevChar.interactable = true;

        startGame = GameObject.Find("Choose-Companion-Button").GetComponent<Button>();
        startGame.onClick.AddListener(StartGame);
        startGame.interactable = true;

        //nextSceneName
    }

    void StartGame(){
        BackGroundMusic.Scene1();
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    void Fade(){
        curtain.gameObject.SetActive(true);
        curtain.Play();
    }

    void NextChar(){
        Fade();
        models[IndexPreview].SetActive(false);
        characters[IndexPreview].gameObject.SetActive(false);
        isNext = true;
    }

    void PrevChar(){
        Fade();
        models[IndexPreview].SetActive(false);
        characters[IndexPreview].gameObject.SetActive(false);
        isNext = false;
    }

    void ShowChar(int index){
        nextChar.interactable = true;
        prevChar.interactable = true;
        startGame.interactable = true;
        if(index < 0)
            index = characters.Count -1;
        index = index % characters.Count;        
        characters[index].gameObject.SetActive(true);
        models[index].SetActive(true);
        SelectedCompanion = models[index].name;
        IndexPreview = index;

    }

   void OnFadeEnd(){
        curtain.gameObject.SetActive(false);
        if(isNext)
            ShowChar(IndexPreview+1);
        else ShowChar(IndexPreview-1);
    }
}
