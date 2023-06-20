using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace SimpleAudioOptions
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class SimpleAudioOptionsPlugin : BaseUnityPlugin
    {
        internal const string ModName = "SimpleAudioOptions";
        internal const string ModVersion = "1.0.1";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;

        internal static SimpleAudioOptionsPlugin context = null;
        private readonly Harmony _harmony = new(ModGUID);

        public static readonly ManualLogSource SimpleAudioOptionsLogger = BepInEx.Logging.Logger.CreateLogSource(ModName);

        // The enum values mimic the AudioSpeakerMode enum, but we can't use that because it includes RAW which is not supported.
        public enum CustomAudioSpeakerMode
        {
            Mono = 1,
            Stereo = 2,
            Quad = 3,
            Surround = 4,
            Mode5point1 = 5,
            Mode7point1 = 6,
            Prologic = 7,
        }

        private enum Toggle
        {
            Off,
            On
        }

        public void Awake()
        {
            context = this;
            // Create a configuration option for the speaker mode
            // Don't include RAW as a setting for AudioSpeakerMode
            // because it's not supported by the game
            EnableModControl = config("1 - General", "Enable Mod Control", Toggle.On, new ConfigDescription("Enable the mod's control over audio settings. If disabled, the game's defaults will be used.", null, new ConfigurationManagerAttributes { Order = 3 }));
            SpeakerMode = config("1 - General", "Speaker Mode", CustomAudioSpeakerMode.Stereo, new ConfigDescription("The speaker mode to use for the game. This will be applied on game start. Changing this value live can result in audio issues, but it is possible to change it live.", null, new ConfigurationManagerAttributes { Order = 2 }));
            DspBufferSize = config("1 - General", "DSP Buffer Size", 1024, new ConfigDescription("The size of the digital signal processing buffer.", null, new ConfigurationManagerAttributes { Order = 1 }));
            SampleRate = config("1 - General", "Sample Rate", 44100, new ConfigDescription("The audio system sample rate.", null, new ConfigurationManagerAttributes { Order = 0 }));

            if (EnableModControl.Value == Toggle.On)
            {
                var audioConfig = AudioSettings.GetConfiguration();
                audioConfig.speakerMode = (AudioSpeakerMode)SpeakerMode.Value;
                audioConfig.dspBufferSize = DspBufferSize.Value;
                audioConfig.sampleRate = SampleRate.Value;
                AudioSettings.Reset(audioConfig);
            }

            SpeakerMode.SettingChanged += UpdateAudioSettings;
            DspBufferSize.SettingChanged += UpdateAudioSettings;
            SampleRate.SettingChanged += UpdateAudioSettings;


            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
            SetupWatcher();
            Utilities.AutoDoc();
        }


        private void OnDestroy()
        {
            Config.Save();
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                SimpleAudioOptionsLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
            }
            catch
            {
                SimpleAudioOptionsLogger.LogError($"There was an issue loading your {ConfigFileName}");
                SimpleAudioOptionsLogger.LogError("Please check your config entries for spelling and format!");
            }
        }

        private void UpdateAudioSettings(object sender, EventArgs e)
        {
            // Only apply the mod settings if EnableModControl is true
            if (EnableModControl.Value != Toggle.On) return;
            var audioConfig = AudioSettings.GetConfiguration();
            audioConfig.speakerMode = (AudioSpeakerMode)SpeakerMode.Value;
            audioConfig.dspBufferSize = DspBufferSize.Value;
            audioConfig.sampleRate = SampleRate.Value;
            AudioSettings.Reset(audioConfig);
            Config.Save();
        }


        #region ConfigOptions

        private static ConfigEntry<Toggle> EnableModControl = null!;
        private static ConfigEntry<CustomAudioSpeakerMode> SpeakerMode = null!;
        private static ConfigEntry<int> DspBufferSize = null!;
        private static ConfigEntry<int> SampleRate = null!;

        private class ConfigurationManagerAttributes
        {
            [UsedImplicitly] public int? Order;
            [UsedImplicitly] public bool? Browsable;
            [UsedImplicitly] public string? Category;
            [UsedImplicitly] public Action<ConfigEntryBase>? CustomDrawer;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description)
        {
            return config(group, name, value, new ConfigDescription(description));
        }

        #endregion
    }
}