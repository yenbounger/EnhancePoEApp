﻿using ChaosRecipeEnhancer.UI.WPF.BusinessLogic.Items;
using ChaosRecipeEnhancer.UI.WPF.Properties;

namespace ChaosRecipeEnhancer.UI.WPF.BusinessLogic.FilterManipulation.FilterGeneration.Factory.Managers.Implementation;

internal class CBeltsManager : ABaseItemClassManager
{
	#region Constructors

	public CBeltsManager()
	{
		ClassName = "Belts";
		ClassFilterName = "\"Belts\"";
		ClassColor = Settings.Default.LootFilterBeltColor;
		AlwaysActive = Settings.Default.LootFilterBeltsAlwaysActive;
	}

	#endregion

	#region Methods

	public override ActiveItemTypes SetActiveTypes(ActiveItemTypes activeItems, bool newValue)
	{
		activeItems.BeltActive = newValue;
		return activeItems;
	}

	#endregion
}