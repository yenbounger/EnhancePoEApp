﻿using ChaosRecipeEnhancer.UI.WPF.BusinessLogic.Items;
using ChaosRecipeEnhancer.UI.WPF.Properties;

namespace ChaosRecipeEnhancer.UI.WPF.BusinessLogic.FilterManipulation.FilterGeneration.Factory.Managers.Implementation;

internal class CBootsManager : ABaseItemClassManager
{
	#region Constructors

	public CBootsManager()
	{
		ClassName = "Boots";
		ClassFilterName = "\"Boots\"";
		ClassColor = Settings.Default.LootFilterBootsColor;
		AlwaysActive = Settings.Default.LootFilterBootsAlwaysActive;
	}

	#endregion

	#region Methods

	public override ActiveItemTypes SetActiveTypes(ActiveItemTypes activeItems, bool newValue)
	{
		activeItems.BootsActive = newValue;
		return activeItems;
	}

	#endregion
}