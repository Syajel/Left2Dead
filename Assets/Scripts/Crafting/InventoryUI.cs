using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    /* ***Selected Item *** */
    public Item selectedItem;
    /* ***Header*** */
    private Text itemTitle;
    private Image itemIcon;
    private Text itemDescription;

    /* ***Selection Panel*** */
    private GameObject selectionPanel;
    private Text[] selectionSlots;
    private Button[] selectionButtons;

    /* ***Check-list Panel*** */
    private GameObject CheckListPanel;
    private List<Image> isAvailableSlots = new List<Image>();
    private List<Text> supplyCount = new List<Text>();

    /* ***Crafting *** */
    private Button craftingButton;

    /* ***Resume Button*** */
    private Button resumeButton;

    //CraftingRecipes
    CraftingRecipes craftingRecipes = new CraftingRecipes();

    //Grenade Script
    Grenade [] grenadeScript;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        //reference header's components
        string headerPath = "Info-Panel/Header/";
        itemTitle = GameObject.Find(headerPath+"Item-Name").GetComponent<Text>();
        itemIcon = GameObject.Find(headerPath+"Slot/Icon").GetComponent<Image>();
        itemDescription = GameObject.Find(headerPath+"Slot/Item-Description").GetComponent<Text>();
        
        //reference selection panel's components
        selectionPanel = GameObject.Find("Selection-Panel");
        selectionSlots = selectionPanel.GetComponentsInChildren<Text>();
        selectionButtons = selectionPanel.GetComponentsInChildren<Button>();
        foreach (Button button in selectionButtons)        
            button.onClick.AddListener( () => OnSelectedItem(button) );

        //reference checklist panel's components
        CheckListPanel = GameObject.Find("Info-Panel/Check-List");

        Image [] isAvailableSlotsTemp = CheckListPanel.GetComponentsInChildren<Image>();
        foreach (var slot in isAvailableSlotsTemp)        
            if(slot.name.Equals("available-1") || slot.name.Equals("available-2"))
                isAvailableSlots.Add(slot);           
        
        Text [] supplyCountTemp = CheckListPanel.GetComponentsInChildren<Text>();
        foreach (var count in supplyCountTemp)        
            if(count.name.Equals("number"))
                supplyCount.Add(count);
            

        //craftingButton
        craftingButton = GameObject.Find("Info-Panel/bar-line/Craft-Button").GetComponent<Button>();
        craftingButton.onClick.AddListener(OnCrafting);
        craftingButton.enabled = false;

        //resumeButton
//        resumeButton = GameObject.Find("Exit-Button").GetComponent<Button>();
        //resumeButton.onClick.AddListener(HideInventory);

        grenadeScript = GameObject.FindGameObjectWithTag("GrenadeManager").transform.GetComponentsInChildren<Grenade>(true);
        
        
        UpdateUI();

       //Hide UI
       HideInventory();
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        UpdateHeaderUI();
        UpdateItemCountUI();
        UpdateSupplyCountUI();
        UpdateRecipeInfo();
    }

    /*Update Header*/
    void UpdateHeaderUI(){
        itemTitle.text = selectedItem.name;
        itemIcon.sprite = selectedItem.icon;
        itemDescription.text = selectedItem.description;
    }

    /*Update Item Count (Selection Panel)*/
    void UpdateItemCountUI(){
        for(int i=0; i<5; i++){
            selectionSlots[i].text = inventory.items[i].itemCount.ToString();
            
            if(inventory.items[i].itemCount >= inventory.items[i].maxAmmount){
                selectionSlots[i].color = Color.red;
            }else selectionSlots[i].color = Color.yellow;
        }  
    }

    /*Update Supply Count (Checklist Panel)*/
    void UpdateSupplyCountUI(){
        for(int i=0; i<6; i++){
            supplyCount[i].text = inventory.supplies[i].count.ToString();   
        }  
    }

    #region SLOTS
    private void AlcoholSlots(bool slot1, bool slot2){
            isAvailableSlots[0].enabled = slot1;
            isAvailableSlots[1].enabled = slot2;
    }
    private void RagsSlots(bool slot1, bool slot2){
            isAvailableSlots[2].enabled = slot1;
            isAvailableSlots[3].enabled = slot2;
    }
    private void SugarSlots(bool slot1, bool slot2){
            isAvailableSlots[4].enabled = slot1;
            isAvailableSlots[5].enabled = slot2;
    }
    private void GunPowderSlots(bool slot1, bool slot2){
            isAvailableSlots[6].enabled = slot1;
            isAvailableSlots[7].enabled = slot2;
    }
    private void CanisterSlots(bool slot1, bool slot2){
            isAvailableSlots[8].enabled = slot1;
            isAvailableSlots[9].enabled = slot2;
    }
    private void BileSlots(bool slot1, bool slot2){
            isAvailableSlots[10].enabled = slot1;
            isAvailableSlots[11].enabled = slot2;
    }
    #endregion
    /*Update Requirements according to item been selected*/
    void UpdateRecipeInfo(){
        if(selectedItem.name.Equals("Molotov Cocktail") || selectedItem.name.Equals("Health Pack")){
            AlcoholSlots(true, true);
            RagsSlots(true, true);
            SugarSlots(false, false);
            GunPowderSlots(false, false);
            CanisterSlots(false, false);
            BileSlots(false, false);
        }else if(selectedItem.name.Equals("Stun Grenade")){
            AlcoholSlots(false, false);
            RagsSlots(false, false);
            SugarSlots(true, false);
            GunPowderSlots(true, true);
            CanisterSlots(false, false);
            BileSlots(false, false);
        }else if(selectedItem.name.Equals("Pipe Bomb")){
            AlcoholSlots(true, false);
            RagsSlots(false, false);
            SugarSlots(false, false);
            GunPowderSlots(true, false);
            CanisterSlots(true, false);
            BileSlots(false, false);
        }
        else if(selectedItem.name.Equals("Bile Bomb")){
            AlcoholSlots(false, false);
            RagsSlots(false, false);
            SugarSlots(false, false);
            GunPowderSlots(true, false);
            CanisterSlots(true, false);
            BileSlots(true, false);
        }
    }

    Item getItemByName(string name){
        foreach (Item item in inventory.items)
        {
            if(item.name.Contains(name))
                return item;
        }
        return null;
    }

    bool hasItemRecipe(Item item){
        Recipe recipe = null;
        if(item.name.Equals("Molotov Cocktail")){
            recipe = craftingRecipes.molotov_recipe;
        }else if(item.name.Equals("Stun Grenade")){   
            recipe = craftingRecipes.stun_recipe;
        }else if(item.name.Equals("Health Pack")){
            recipe = craftingRecipes.health_recipe;
        }else if(item.name.Equals("Pipe Bomb")){
            recipe = craftingRecipes.pipe_recipe;
        }else if(item.name.Equals("Bile Bomb")){
            recipe = craftingRecipes.bile_recipe;
        }
        if(recipe != null && craftingRecipes.hasRecipe(recipe))
            return true;

        return false;
    }
    void OnSelectedItem(Button item){
      selectedItem = getItemByName(item.name);
      UpdateUI();

      //check if has ingredients
      //if yes, enable craft button
      //if no, disable craft button
      if(hasItemRecipe(selectedItem) && selectedItem.itemCount < selectedItem.maxAmmount){
          craftingButton.enabled = true;
      }else craftingButton.enabled = false;      
    }

    //Crafting
    Recipe ItemRecipe(Item item){
        Recipe recipe = null;
        if(item.name.Equals("Molotov Cocktail")){
            recipe = craftingRecipes.molotov_recipe;
        }else if(item.name.Equals("Stun Grenade")){   
            recipe = craftingRecipes.stun_recipe;
        }else if(item.name.Equals("Health Pack")){
            recipe = craftingRecipes.health_recipe;
        }else if(item.name.Equals("Pipe Bomb")){
            recipe = craftingRecipes.pipe_recipe;
        }else if(item.name.Equals("Bile Bomb")){
            recipe = craftingRecipes.bile_recipe;
        }
        return recipe;
    }
    void OnCrafting()
    {
        Recipe recipe = ItemRecipe(selectedItem);
        //reduce player's used supplies
        List<Ingredient> playerIng = new List<Ingredient>();
        foreach(Ingredient ingredient in recipe.ingredients){
            foreach(Supply supply in inventory.supplies){
                if(ingredient.name.Equals(supply.name)){
                    supply.count -= ingredient.quantity;
                }                    
            }
        }
        //increase player's items
        foreach(Item item in inventory.items){
            if(selectedItem == item){
                item.itemCount++;
            }                    
        }
        //increase player's item in the other script
        //loop on children
        //from item name get the correspondant child
        //change its gun script (ammo)
        string maptoScript = "";
        if(selectedItem.name.Equals("Pipe Bomb")){
            maptoScript = "explosive_1";
        }
        else if(selectedItem.name.Equals("Bile Bomb")){
            maptoScript = "explosive_2";
        }
        else if(selectedItem.name.Equals("Stun Grenade")){
            maptoScript = "explosive_3";
        }
        else if(selectedItem.name.Equals("Molotov Cocktail")){
            maptoScript = "explosive_4";
        }


        foreach (Grenade item in grenadeScript)
        {
            if(item.gameObject.name.Equals(maptoScript)){
               item.addAmmo();
            }
        }




        UpdateUI();

        if(hasItemRecipe(selectedItem) && selectedItem.itemCount < selectedItem.maxAmmount){
          craftingButton.enabled = true;
      }else craftingButton.enabled = false;
    }

    void HideInventory(){
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
