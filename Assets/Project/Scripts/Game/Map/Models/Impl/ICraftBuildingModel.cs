using System;
using Game.Map.Data;

namespace Game.Map.Models
{
    public interface ICraftBuildingModel : IProductFactoryBuildingModel, IBuildingModel
    {
        event Action OnRecipeChanged;
        
        ToolsCraftData CurrentRecipe { get; }
        
        ToolsCraftData[] Recipes { get; }

        void SelectCraftRecipe(ToolsCraftData data);
    }
}