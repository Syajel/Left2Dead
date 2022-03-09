using System.Collections.Generic;
public class CraftingRecipes
{

    //Molotov Cocktail
    //2 alcohol + 2 rags
    public Recipe molotov_recipe = new Recipe("molotov_recipe", 
                            new Ingredient("alcohol", 2),  
                            new Ingredient("rags", 2)
                            );

    //Stun Grenade
    //1 Sugar + 2 Gunpowder
    public Recipe stun_recipe = new Recipe("stun_recipe", 
                            new Ingredient("sugar", 1),  
                            new Ingredient("gunpowder", 2)
                            );

    //Health Pack
    //2 Alcohol + 2 Rags
    public Recipe health_recipe = new Recipe("health_recipe", 
                            new Ingredient("alcohol", 2),  
                            new Ingredient("rags", 2)
                            );


    //Pipe Bomb
    //1 Alcohol + 1 Gunpowder + 1 Canister
    public Recipe pipe_recipe = new Recipe("pipe_recipe", 
                            new Ingredient("alcohol", 1),  
                            new Ingredient("gunpowder", 1),
                            new Ingredient("canisters", 1)
                            );

    //Bile Bomb
    //1 Bile + 1 Gunpowder + 1 Canister
     public Recipe bile_recipe = new Recipe("bile_recipe", 
                            new Ingredient("bile", 1),  
                            new Ingredient("gunpowder", 1),
                            new Ingredient("canisters", 1)
                            );

    
    //checkers
    public bool hasRecipe(Recipe recipe){//
        List<Ingredient> playerIng = new List<Ingredient>();
        int passed = 0;
        foreach(Ingredient ingredient in recipe.ingredients){
            foreach(Supply supply in Inventory.instance.supplies){
                if(ingredient.name.Equals(supply.name) && ingredient.quantity <= supply.count && supply.count > 0){
                   passed++;
                }                    
            }
        }
        if(passed == recipe.ingredients.Count)        
            return true;        
        return false;
    }
}
