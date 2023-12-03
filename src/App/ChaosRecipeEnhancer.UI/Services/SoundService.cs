using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Media;
using ChaosRecipeEnhancer.UI.Properties;

namespace ChaosRecipeEnhancer.UI.Services;

public interface ISoundService
{
    void PlaySound(SoundType soundType);
}

public class SoundService : ISoundService
{
    private static string RefreshFilterSoundPath => Settings.Default.RefreshFilterSoundPath;
    private static string SetCompleteSoundPath => Settings.Default.SetCompleteSoundPath;
    private static bool SoundEnabled => Settings.Default.SoundEnabled;
    private static double Volume => Settings.Default.SoundVolume;
    
    private string DefaultRefreshFilterSoundPath => "Assets\\Sounds\\FilterChanged.mp3";
    private string DefaultSetCompleteSoundPath => "Assets\\Sounds\\ItemPickedUp.mp3";


    public void PlaySound(SoundType soundType)
    {
        if (!SoundEnabled) return;
        var rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var soundPath = soundType switch
        {
            SoundType.RefreshFilter when RefreshFilterSoundPath == DefaultRefreshFilterSoundPath => 
                Path.Combine(rootDirectory!, RefreshFilterSoundPath),
            SoundType.RefreshFilter => RefreshFilterSoundPath,
            SoundType.SetComplete when SetCompleteSoundPath == DefaultSetCompleteSoundPath => 
                Path.Combine(rootDirectory!, SetCompleteSoundPath),
            SoundType.SetComplete => SetCompleteSoundPath,
            _ => throw new ApplicationException($"Unknown sound type: {soundType}")
        };
        var player = new MediaPlayer();
        player.Open(new Uri(soundPath));
        player.Volume = Volume;
        player.Play();
    }
}

public enum SoundType
{
    RefreshFilter,
    SetComplete
}