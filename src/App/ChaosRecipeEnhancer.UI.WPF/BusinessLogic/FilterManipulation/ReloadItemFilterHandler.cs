﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using ChaosRecipeEnhancer.UI.WPF.BusinessLogic.Constants;
using ChaosRecipeEnhancer.UI.WPF.DynamicControls;
using ChaosRecipeEnhancer.UI.WPF.Extensions.Native;
using ChaosRecipeEnhancer.UI.WPF.Properties;

namespace ChaosRecipeEnhancer.UI.WPF.BusinessLogic.FilterManipulation;

public static class ReloadItemFilterHandler
{
	public static void ReloadFilter()
	{
		var chatCommand = BuildFilterReloadCommand();
		if (chatCommand is null) return;

		// Map all current window names to their associated "handle to a window" pointers (HWND)
		var openWindows = NativeWindowExtensions.GetOpenWindows();

		foreach (var window in openWindows)
		{
			var handle = window.Key;
			var title = window.Value;

			Console.WriteLine("{0}: {1}", handle, title);
		}

		// Find the Process ID associated with the 'Path of Exile' game window
		var poeWindow = openWindows.FirstOrDefault(x => x.Value == "Path of Exile").Key;

		if (NativeWindowExtensions.CheckIfWindowExists(poeWindow))
		{
			ErrorWindow.Spawn("Could not find PoE window! Please make sure PoE is running." + StringConstruction.DoubleNewLineCharacter +
								 " If PoE is running in admin mode, try running our app in admin mode, as well.", "Error: PoE Window Not Found");
			return;
		}

		// Get 'Path of Exile' window in the foreground to actually send input to said window
		NativeWindowExtensions.SetForegroundWindow(poeWindow);

		//SendKeys.SendWait("{ENTER}");
		//SendKeys.SendWait(chatCommand);
		//SendKeys.SendWait("{ENTER}");

		Clipboard.Clear();  // Always clear the clipboard first
		Clipboard.SetText(chatCommand);

		SendKeys.SendWait("{ENTER}");
		SendKeys.SendWait("^(v)");
		SendKeys.SendWait("{ENTER}");
	}

	private static string BuildFilterReloadCommand()
	{
		var filterName = GetFilterName();

		if (!string.IsNullOrEmpty(filterName)) return "/itemfilter " + filterName;

		ErrorWindow.Spawn("Please configure your filter file location in the settings.", "Error: No Filter File Location Set");

		return null;
	}

	private static string GetFilterName()
	{
		return Path.GetFileName(Settings.Default.LootFilterFileLocation).Replace(".filter", "");
	}
}