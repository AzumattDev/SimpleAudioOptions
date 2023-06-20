using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using BepInEx.Configuration;

namespace SimpleAudioOptions;

public class Utilities
{
    internal static void AutoDoc()
    {
#if DEBUG

        // Store Regex to get all characters after a [
        Regex regex = new(@"\[(.*?)\]");

        // Strip using the regex above from Config[x].Description.Description
        string Strip(string x) => regex.Match(x).Groups[1].Value;
        StringBuilder sb = new();
        string lastSection = "";
        foreach (ConfigDefinition x in SimpleAudioOptionsPlugin.context.Config.Keys)
        {
            // skip first line
            if (x.Section != lastSection)
            {
                lastSection = x.Section;
                sb.Append($"{Environment.NewLine}`{x.Section}`{Environment.NewLine}");
            }

            sb.Append($"\n{x.Key} [{Strip(SimpleAudioOptionsPlugin.context.Config[x].Description.Description)}]" +
                      $"{Environment.NewLine}   * {SimpleAudioOptionsPlugin.context.Config[x].Description.Description.Replace("[Synced with Server]", "").Replace("[Not Synced with Server]", "")}" +
                      $"{Environment.NewLine}     * Default Value: {SimpleAudioOptionsPlugin.context.Config[x].GetSerializedValue()}{Environment.NewLine}");
        }

        File.WriteAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, $"{SimpleAudioOptionsPlugin.ModName}_AutoDoc.md"), sb.ToString());
#endif
    }
}