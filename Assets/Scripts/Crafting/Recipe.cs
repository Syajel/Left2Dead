using System.Collections.Generic;

public class Recipe 
{
    public static List<Recipe> recipes = new List<Recipe>();
    public string name;
    public List<Ingredient> ingredients = new List<Ingredient>();

    public Recipe(string name, params Ingredient[] list){
        this.name = name; //rec. name
        foreach (var item in list)
        {
            ingredients.Add(item);
        }
        recipes.Add(this);
    }
    
}
