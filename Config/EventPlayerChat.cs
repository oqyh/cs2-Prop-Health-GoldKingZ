using CounterStrikeSharp.API.Core;
using Prop_Health_GoldKingZ.Config;
using CounterStrikeSharp.API.Modules.Commands;
using Newtonsoft.Json.Linq;
using CounterStrikeSharp.API;

namespace Prop_Health_GoldKingZ;

public class PlayerChat
{
    public HookResult OnPlayerChat(CCSPlayerController? player, CommandInfo info, bool TeamChat)
	{
        if (player == null || !player.IsValid)return HookResult.Continue;
        var playerid = player.SteamID;
        var eventmessage = info.ArgString;
        eventmessage = eventmessage.TrimStart('"');
        eventmessage = eventmessage.TrimEnd('"');
        
        if (string.IsNullOrWhiteSpace(eventmessage)) return HookResult.Continue;
        string trimmedMessageStart = eventmessage.TrimStart();
        string message = trimmedMessageStart.TrimEnd();

        string[] Reload_Prop_Settings_CommandsInGames = Configs.GetConfigData().Reload_Prop_Settings_CommandsInGame.Split(',');
        if (Reload_Prop_Settings_CommandsInGames.Any(cmd => cmd.Equals(message, StringComparison.OrdinalIgnoreCase)))
        {
            if (!string.IsNullOrEmpty(Configs.GetConfigData().Reload_Prop_Settings_Flags) && !Helper.IsPlayerInGroupPermission(player, Configs.GetConfigData().Reload_Prop_Settings_Flags))
            {
                Helper.AdvancedPlayerPrintToChat(player, Configs.Shared.StringLocalizer!["PrintChatToPlayer.Not.Allowed.ToReload"]);
                return HookResult.Continue;
            }
            foreach (var ent in PropHealthGoldKingZ.Instance.g_Main.Entitys.Values)
            {
                if(ent == null)continue;
                var entity = ent.Target_Entity;
                if(entity == null || !entity.IsValid)continue;
                Helper.RemoveHighlightEnt(entity);
                ent.Entity_Health_Max = -1;
                ent.Entity_Health = -1;
                entity = null;
            }

            Helper.SetValues(player);

            foreach (var ent in PropHealthGoldKingZ.Instance.g_Main.Entitys.Values)
            {
                if(ent == null)continue;
                var entity = ent.Entity;
                if(entity == null || !entity.IsValid)continue;
                var getenity = entity.As<CBaseModelEntity>();
                var entitypath = getenity.CBodyComponent?.SceneNode?.GetSkeletonInstance().ModelState.ModelName;

                if(!PropHealthGoldKingZ.Instance.g_Main.Entitys.ContainsKey(entity))
                {
                    int health = Configs.GetConfigData().Default_Health;
                    PropHealthGoldKingZ.Instance.g_Main.Entitys.Add(entity, new Globals.GetEnt(entity,null!,health,health,string.Empty));
                }

                if(PropHealthGoldKingZ.Instance.g_Main.Entitys.ContainsKey(entity) && !string.IsNullOrEmpty(entitypath))
                {
                    var jsonData = PropHealthGoldKingZ.Instance.g_Main.JsonData;
                    if (jsonData == null) return HookResult.Continue;
                    foreach (var item in jsonData)
                    {
                        if (item.Key.Equals(entitypath, StringComparison.OrdinalIgnoreCase))
                        {
                            if (item.Value is JObject modelData)
                            {
                                int? Prop_Health = modelData["Prop_Health"]?.ToObject<int?>() ?? Configs.GetConfigData().Default_Health;
                                
                                int I_health;
                                if(Prop_Health == -1)
                                {
                                    I_health = -1;
                                }else
                                {
                                    if(Prop_Health.HasValue)
                                    {
                                        I_health = Prop_Health.Value;
                                    }else
                                    {
                                        I_health = Configs.GetConfigData().Default_Health;
                                    }
                                }
                                

                                ent.Target_Entity = entity;
                                ent.Entity_Health_Max = I_health;
                                ent.Entity_Health  = I_health;
                                Helper.StartHighlightEnt(ent.Target_Entity);
                            }
                        }
                    }
                }
            }
            Helper.AdvancedPlayerPrintToChat(player, Configs.Shared.StringLocalizer!["PrintChatToPlayer.Plugin.Reloaded"]); 
        }

        string[] Get_Prop_Settings_CommandsInGames = Configs.GetConfigData().Get_Prop_Settings_CommandsInGame.Split(',');
        if (Get_Prop_Settings_CommandsInGames.Any(cmd => cmd.Equals(message, StringComparison.OrdinalIgnoreCase)))
        {
            if (!string.IsNullOrEmpty(Configs.GetConfigData().Get_Prop_Settings_Flags) && !Helper.IsPlayerInGroupPermission(player, Configs.GetConfigData().Get_Prop_Settings_Flags))
            {
                Helper.AdvancedPlayerPrintToChat(player, Configs.Shared.StringLocalizer!["PrintChatToPlayer.Not.Allowed.ToReload"]);
                return HookResult.Continue;
            }
            string Fpath = Path.Combine(Configs.Shared.CookiesModule!, $"../../plugins/Prop-Health-GoldKingZ/Props/");
            string Fpathc = Path.Combine(Configs.Shared.CookiesModule!, $"../../plugins/Prop-Health-GoldKingZ/Props/Prop_List.txt");
            string mapname = NativeAPI.GetMapName();

            if (Directory.Exists(Fpath))
            {
                try
                {
                    Directory.Delete(Fpath, true);
                }
                catch (Exception ex)
                {
                    Helper.DebugMessage(ex.Message);
                }
            }

            if (!Directory.Exists(Fpath))
            {
                try
                {
                    Directory.CreateDirectory(Fpath);
                }
                catch (Exception ex)
                {
                    Helper.DebugMessage(ex.Message);
                }
            }

            if (!File.Exists(Fpathc))
            {
                try
                {
                    using (File.Create(Fpathc)) { }
                }
                catch (Exception ex)
                {
                    Helper.DebugMessage(ex.Message);
                }
            }

            try
            {
                var uniquePaths = new HashSet<string>();

                File.AppendAllLines(Fpathc, new[] { $"Prop List In Map: {mapname}" });
                File.AppendAllLines(Fpathc, new[] { "--------------------------------------------------------" });
                
                foreach (var ent in PropHealthGoldKingZ.Instance.g_Main.Entitys.Values)
                {
                    var entity = ent.Entity_Path;
                    
                    if (!string.IsNullOrEmpty(entity) && uniquePaths.Add(entity))
                    {
                        File.AppendAllLines(Fpathc, new[] { entity });
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.DebugMessage(ex.Message);
            }
            
            Helper.AdvancedPlayerPrintToChat(player, Configs.Shared.StringLocalizer!["PrintChatToPlayer.GetProp"], mapname); 
        }
        return HookResult.Continue;
    }
}