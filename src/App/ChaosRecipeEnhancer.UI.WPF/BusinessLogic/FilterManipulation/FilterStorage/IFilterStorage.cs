﻿using System.Threading.Tasks;

namespace ChaosRecipeEnhancer.UI.WPF.BusinessLogic.FilterManipulation.FilterStorage;

public interface IFilterStorage
{
	Task<string> ReadLootFilterAsync();
	Task WriteLootFilterAsync(string filter);
}