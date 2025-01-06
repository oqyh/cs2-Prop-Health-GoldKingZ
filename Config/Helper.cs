using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Utils;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Encodings.Web;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Cvars;
using System.Runtime.InteropServices;
using Prop_Health_GoldKingZ.Config;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace Prop_Health_GoldKingZ;

public class Helper
{
    public static void AdvancedPlayerPrintToChat(CCSPlayerController player, string message, params object[] args)
    {
        if (string.IsNullOrEmpty(message))return;

        for (int i = 0; i < args.Length; i++)
        {
            message = message.Replace($"{{{i}}}", args[i].ToString());
        }
        if (Regex.IsMatch(message, "{nextline}", RegexOptions.IgnoreCase))
        {
            string[] parts = Regex.Split(message, "{nextline}", RegexOptions.IgnoreCase);
            foreach (string part in parts)
            {
                string messages = part.Trim();
                player.PrintToChat(" " + messages);
            }
        }else
        {
            player.PrintToChat(message);
        }
    }
    public static void AdvancedServerPrintToChatAll(string message, params object[] args)
    {
        if (string.IsNullOrEmpty(message))return;

        for (int i = 0; i < args.Length; i++)
        {
            message = message.Replace($"{{{i}}}", args[i].ToString());
        }
        if (Regex.IsMatch(message, "{nextline}", RegexOptions.IgnoreCase))
        {
            string[] parts = Regex.Split(message, "{nextline}", RegexOptions.IgnoreCase);
            foreach (string part in parts)
            {
                string messages = part.Trim();
                Server.PrintToChatAll(" " + messages);
            }
        }else
        {
            Server.PrintToChatAll(message);
        }
    }
    public static void AdvancedPlayerPrintToConsole(CCSPlayerController player, string message, params object[] args)
    {
        if (string.IsNullOrEmpty(message))return;
        
        for (int i = 0; i < args.Length; i++)
        {
            message = message.Replace($"{{{i}}}", args[i].ToString());
        }
        if (Regex.IsMatch(message, "{nextline}", RegexOptions.IgnoreCase))
        {
            string[] parts = Regex.Split(message, "{nextline}", RegexOptions.IgnoreCase);
            foreach (string part in parts)
            {
                string messages = part.Trim();
                player.PrintToConsole(" " + messages);
            }
        }else
        {
            player.PrintToConsole(message);
        }
    }
    
    public static bool IsPlayerInGroupPermission(CCSPlayerController player, string groups)
    {
        var excludedGroups = groups.Split(',');
        foreach (var group in excludedGroups)
        {
            if(group.StartsWith("#"))
            {
                if (AdminManager.PlayerInGroup(player, group))
                {
                    return true;
                }

            }else if(group.StartsWith("@"))
            {
                if (AdminManager.PlayerHasPermissions(player, group))
                {
                    return true;
                }
            }else
            {
                if (AdminManager.PlayerInGroup(player, group))
                {
                    return true;
                }
            }
        }   
        return false;
    }

    public static List<CCSPlayerController> GetPlayersController(bool IncludeBots = false, bool IncludeSPEC = true, bool IncludeCT = true, bool IncludeT = true) 
    {
        var playerList = Utilities
            .FindAllEntitiesByDesignerName<CCSPlayerController>("cs_player_controller")
            .Where(p => p != null && p.IsValid && 
                        (IncludeBots || (!p.IsBot && !p.IsHLTV)) && 
                        p.Connected == PlayerConnectedState.PlayerConnected && 
                        ((IncludeCT && p.TeamNum == (byte)CsTeam.CounterTerrorist) || 
                        (IncludeT && p.TeamNum == (byte)CsTeam.Terrorist) || 
                        (IncludeSPEC && p.TeamNum == (byte)CsTeam.Spectator)))
            .ToList();

        return playerList;
    }
    public static int GetPlayersCount(bool IncludeBots = false, bool IncludeSPEC = true, bool IncludeCT = true, bool IncludeT = true)
    {
        return Utilities.GetPlayers().Count(p => 
            p != null && 
            p.IsValid && 
            p.Connected == PlayerConnectedState.PlayerConnected && 
            (IncludeBots || (!p.IsBot && !p.IsHLTV)) && 
            ((IncludeCT && p.TeamNum == (byte)CsTeam.CounterTerrorist) || 
            (IncludeT && p.TeamNum == (byte)CsTeam.Terrorist) || 
            (IncludeSPEC && p.TeamNum == (byte)CsTeam.Spectator))
        );
    }
    
    public static void ClearVariables()
    {
        PropHealthGoldKingZ.Instance.g_Main.Entitys.Clear();
        PropHealthGoldKingZ.Instance.g_Main.Attacker_Damage.Clear();
        PropHealthGoldKingZ.Instance.g_Main.JsonData?.Clear();

        SetValues();
    }
    
    public static string ReplaceMessages(string Message, string date, string time, string PlayerName, string SteamId, string ipAddress, string reason)
    {
        var replacedMessage = Message
                                    .Replace("{TIME}", time)
                                    .Replace("{DATE}", date)
                                    .Replace("{PLAYERNAME}", PlayerName.ToString())
                                    .Replace("{STEAMID}", SteamId.ToString())
                                    .Replace("{IP}", ipAddress.ToString())
                                    .Replace("{REASON}", reason);
        return replacedMessage;
    }
    public static CCSGameRules? GetGameRules()
    {
        try
        {
            var gameRulesEntities = Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules");
            return gameRulesEntities.First().GameRules;
        }
        catch
        {
            return null;
        }
    }
    public static bool IsWarmup()
    {
        return GetGameRules()?.WarmupPeriod ?? false;
    }
	public static string GetServerPublicIP()
    {
        try
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("https://api.ipify.org").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody.Trim();
            }
        }
        catch 
        {
            return "";
        }
    }
	
	public static void DebugMessage(string message)
    {
        if (!Configs.GetConfigData().EnableDebug) return;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("[Debug Pro Health]: " + message);
        Console.ResetColor();
    }

    public static void SetValues(CCSPlayerController player = null!)
    {
        try
        {
            string jsonFilePath = $"{Configs.Shared.CookiesModule}/../../plugins/Prop-Health-GoldKingZ/config/Prop_Settings.json";
            
            if (!File.Exists(jsonFilePath))
            {
                if (player != null && player.IsValid)
                    player.PrintToChat(" \x06[Prop-Health] \x02 JSON file does not exist. Prop_Settings.json In config Folder");
                DebugMessage($"JSON file does not exist. Prop_Settings.json In config Folder");
                return;
            }

            string jsonContent = File.ReadAllText(jsonFilePath);

            if (string.IsNullOrEmpty(jsonContent))
            {
                if (player != null && player.IsValid)
                    player.PrintToChat(" \x06[Prop-Health] \x02 JSON content is empty.");
                DebugMessage("JSON content is empty.");
                return;
            }

            JObject jsonObject = JObject.Parse(jsonContent);
            if (jsonObject == null) return;

            PropHealthGoldKingZ.Instance.g_Main.JsonData = jsonObject.ToObject<Dictionary<string, object>>();

            if (player != null && player.IsValid)
                player.PrintToChat(" \x06[Prop-Health] \x03 Prop_Settings.json Loaded Successfully");
        }
        catch (JsonReaderException ex)
        {
            if (player != null && player.IsValid)
                player.PrintToChat(" \x06[Prop-Health] \x02 Error On Prop_Settings.json " + ex.Message);
            DebugMessage($"Error On Prop_Settings.json {ex.Message}");
        }
    }

    public static void StartHighlightEnt(CEntityInstance Entity)
    {
        
        if (Entity == null || !Entity.IsValid)return;

        var entity_model = Entity.As<CBaseModelEntity>();
        if (entity_model == null || !entity_model.IsValid)return;

        string colorstring = Configs.GetConfigData().Prop_Color_Argb;
        if (colorstring == "-1")return;

        string[] colorParts = colorstring.Split(' ');
        
        if (colorParts.Length != 4)
        {
            DebugMessage("Please Make Prop_Color_Argb In Config Has A r g b Try This Site https://rgbcolorpicker.com/");
            return;
        }
        entity_model.Render = Color.FromArgb(int.Parse(colorstring.Split(' ')[0]),
                                    int.Parse(colorstring.Split(' ')[1]),
                                    int.Parse(colorstring.Split(' ')[2]),
                                    int.Parse(colorstring.Split(' ')[3]));
        Utilities.SetStateChanged(entity_model, "CBaseModelEntity", "m_clrRender");
    }
    public static void RemoveHighlightEnt(CEntityInstance Entity)
    {
        if (Entity == null || !Entity.IsValid)return;
        var entity_model = Entity.As<CBaseModelEntity>();
        if (entity_model == null || !entity_model.IsValid)return;
        entity_model.Render = Color.FromArgb(255,255,255,255);
        Utilities.SetStateChanged(entity_model, "CBaseModelEntity", "m_clrRender");
    }

    public static async Task DownloadMissingFiles()
    {
        string baseFolderPath = Configs.Shared.CookiesModule!;

        string settingsFileName = "config/Prop_Settings.json";
        string settingsGithubUrl = "https://raw.githubusercontent.com/oqyh/cs2-Prop-Health-GoldKingZ/main/Resources/Prop_Settings.json";
        string settingsFilePath = Path.Combine(baseFolderPath, settingsFileName);
        string settingsDirectoryPath = Path.GetDirectoryName(settingsFilePath)!;
        await DownloadFileIfNotExists(settingsFilePath, settingsGithubUrl, settingsDirectoryPath);
    }
    public static async Task DownloadFileIfNotExists(string filePath, string githubUrl, string directoryPath)
    {
        if (!File.Exists(filePath))
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            await DownloadFileFromGithub(githubUrl, filePath);
        }
    }

    public static async Task DownloadFileFromGithub(string url, string destinationPath)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(destinationPath, fileBytes);
            }
            catch (Exception ex)
            {
                DebugMessage($"Error downloading file: {ex.Message}");
            }
        }
    }
	
}