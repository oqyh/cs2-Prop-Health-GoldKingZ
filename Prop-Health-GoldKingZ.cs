using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Timers;
using Prop_Health_GoldKingZ.Config;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;
using System.Text;

namespace Prop_Health_GoldKingZ;

public class PropHealthGoldKingZ : BasePlugin
{
    public override string ModuleName => "Prop Health";
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "Gold KingZ";
    public override string ModuleDescription => "https://github.com/oqyh";
    internal static IStringLocalizer? Stringlocalizer;
    public static PropHealthGoldKingZ Instance { get; set; } = new();
    public Globals g_Main = new();
    private readonly PlayerChat _PlayerChat = new();

    public override void Load(bool hotReload)
    {
        Instance = this;
        Configs.Load(ModuleDirectory);
        Stringlocalizer = Localizer;
        Configs.Shared.CookiesModule = ModuleDirectory;
        Configs.Shared.StringLocalizer = Localizer;
        _ = Task.Run(async () =>
        {
            await Helper.DownloadMissingFiles();
        });


        Helper.SetValues();

        RegisterListener<Listeners.OnTick>(OnTick);
        RegisterListener<Listeners.OnMapEnd>(OnMapEnd);
        RegisterListener<Listeners.OnEntityCreated>(OnEntityCreated);
        RegisterListener<Listeners.OnEntitySpawned>(OnEntitySpawned);
        RegisterEventHandler<EventPlayerConnectFull>(OnEventPlayerConnectFull);
        RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);

        AddCommandListener("say", OnPlayerChat, HookMode.Post);
        AddCommandListener("say_team", OnPlayerChatTeam, HookMode.Post);
        //HookEntityOutput("weapon_knife","OnPlayerPickup",OnPlayerPickup,HookMode.Post);
        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(OnTakeDamage, HookMode.Post);
    }
    
    public override void Unload(bool hotReload)
    {
        Helper.ClearVariables();
        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Unhook(OnTakeDamage, HookMode.Post);
        //UnhookEntityOutput("weapon_knife","OnPlayerPickup",OnPlayerPickup,HookMode.Post);
    }

    /* public HookResult OnPlayerPickup(CEntityIOOutput output, string name, CEntityInstance activator, CEntityInstance caller, CVariant value, float delay)
    {
        Server.PrintToConsole($"weapon_knife called OnPlayerPickup ({name}, {activator}, {caller}, {delay})");
        return HookResult.Continue;
    } */

    public HookResult OnEventPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        if (@event == null)return HookResult.Continue;
        
        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;

        if(!g_Main.Attacker_Damage.ContainsKey(player))
        {
            g_Main.Attacker_Damage.Add(player, new Globals.GetAttacker(player,0,0,0,0,0,0,DateTime.Now));
        }

        return HookResult.Continue;
    }
    public HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        if (@event == null) return HookResult.Continue;

        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;
    
        if (g_Main.Attacker_Damage.ContainsKey(player))
        {
            g_Main.Attacker_Damage.Remove(player);
        }

        return HookResult.Continue;
    }

    public void OnTick()
    {
        foreach (var playerData in g_Main.Attacker_Damage.Values)
        {
            if (playerData == null) continue;
            var players = playerData.Attacker;
            if (players == null || !players.IsValid) continue;

            if(playerData.Show_Center_Now == 1 || playerData.Show_Center_Now == 2)
            {
                TimeSpan elapsedTime = DateTime.Now - playerData.LastTickTime;
                if (elapsedTime.TotalSeconds >= 1)
                {
                    if (playerData.Show_Center_Now_Server > 1)
                    {
                        playerData.Show_Center_Now_Server -= 1;
                    }else if (playerData.Show_Center_Now_Server <= 1)
                    {
                        playerData.Show_Center_Now = 0;
                    }
                    
                    playerData.LastTickTime = DateTime.Now;
                }
                StringBuilder builder = new StringBuilder();

                string localizeduse;
                if(playerData.Show_Center_Now == 1)
                {
                    localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health", playerData.Entity_Health<1?0:playerData.Entity_Health];
                }else
                {
                    int health_max = playerData.Entity_Health_Max;
                    int health = playerData.Entity_Health<1?0:playerData.Entity_Health;
                    int roundedHealthPercentage = (int)Math.Round((float)health / health_max * 100);

                    if (roundedHealthPercentage >= 100)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.100"];
                    }
                    else if (roundedHealthPercentage >= 95)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.95"];
                    }
                    else if (roundedHealthPercentage >= 90)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.90"];
                    }
                    else if (roundedHealthPercentage >= 85)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.85"];
                    }
                    else if (roundedHealthPercentage >= 80)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.80"];
                    }
                    else if (roundedHealthPercentage >= 75)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.75"];
                    }
                    else if (roundedHealthPercentage >= 70)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.70"];
                    }
                    else if (roundedHealthPercentage >= 65)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.65"];
                    }
                    else if (roundedHealthPercentage >= 60)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.60"];
                    }
                    else if (roundedHealthPercentage >= 55)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.55"];
                    }
                    else if (roundedHealthPercentage >= 50)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.50"];
                    }
                    else if (roundedHealthPercentage >= 45)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.45"];
                    }
                    else if (roundedHealthPercentage >= 40)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.40"];
                    }
                    else if (roundedHealthPercentage >= 35)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.35"];
                    }
                    else if (roundedHealthPercentage >= 30)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.30"];
                    }
                    else if (roundedHealthPercentage >= 25)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.25"];
                    }
                    else if (roundedHealthPercentage >= 20)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.20"];
                    }
                    else if (roundedHealthPercentage >= 15)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.15"];
                    }
                    else if (roundedHealthPercentage >= 10)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.10"];
                    }
                    else if (roundedHealthPercentage >= 5)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.5"];
                    }
                    else
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter.Prop.Health.0"];
                    }
                }
                builder.AppendFormat(localizeduse);
                var centerhtml = builder.ToString();
                players.PrintToCenterHtml(centerhtml);
            }

            if(playerData.ShowBottom_Now == 1 || playerData.ShowBottom_Now == 2)
            {
                TimeSpan elapsedTime = DateTime.Now - playerData.LastTickTime;
                if (elapsedTime.TotalSeconds >= 1)
                {
                    if (playerData.ShowBottom_Now_Server > 1)
                    {
                        playerData.ShowBottom_Now_Server -= 1;
                    }else if (playerData.ShowBottom_Now_Server <= 1)
                    {
                        playerData.ShowBottom_Now = 0;
                    }
                    
                    playerData.LastTickTime = DateTime.Now;
                }
                StringBuilder builder = new StringBuilder();

                string localizeduse;
                if(playerData.ShowBottom_Now == 1)
                {
                    localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health", playerData.Entity_Health<1?0:playerData.Entity_Health];
                }else
                {
                    int health_max = playerData.Entity_Health_Max;
                    int health = playerData.Entity_Health<1?0:playerData.Entity_Health;
                    int roundedHealthPercentage = (int)Math.Round((float)health / health_max * 100);

                    if (roundedHealthPercentage >= 100)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.100"];
                    }
                    else if (roundedHealthPercentage >= 95)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.95"];
                    }
                    else if (roundedHealthPercentage >= 90)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.90"];
                    }
                    else if (roundedHealthPercentage >= 85)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.85"];
                    }
                    else if (roundedHealthPercentage >= 80)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.80"];
                    }
                    else if (roundedHealthPercentage >= 75)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.75"];
                    }
                    else if (roundedHealthPercentage >= 70)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.70"];
                    }
                    else if (roundedHealthPercentage >= 65)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.65"];
                    }
                    else if (roundedHealthPercentage >= 60)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.60"];
                    }
                    else if (roundedHealthPercentage >= 55)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.55"];
                    }
                    else if (roundedHealthPercentage >= 50)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.50"];
                    }
                    else if (roundedHealthPercentage >= 45)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.45"];
                    }
                    else if (roundedHealthPercentage >= 40)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.40"];
                    }
                    else if (roundedHealthPercentage >= 35)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.35"];
                    }
                    else if (roundedHealthPercentage >= 30)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.30"];
                    }
                    else if (roundedHealthPercentage >= 25)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.25"];
                    }
                    else if (roundedHealthPercentage >= 20)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.20"];
                    }
                    else if (roundedHealthPercentage >= 15)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.15"];
                    }
                    else if (roundedHealthPercentage >= 10)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.10"];
                    }
                    else if (roundedHealthPercentage >= 5)
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.5"];
                    }
                    else
                    {
                        localizeduse = Configs.Shared.StringLocalizer!["ShowCenter_Bottom.Prop.Health.0"];
                    }
                }
                
                builder.AppendFormat(localizeduse);
                var centerhtml = builder.ToString();
                players.PrintToCenter(centerhtml);
            }

        }

    }


    private HookResult OnPlayerChat(CCSPlayerController? player, CommandInfo info)
    {
        if (player == null || !player.IsValid)return HookResult.Continue;
        _PlayerChat.OnPlayerChat(player, info, false);
        return HookResult.Continue;
    }
    private HookResult OnPlayerChatTeam(CCSPlayerController? player, CommandInfo info)
    {
        if (player == null || !player.IsValid)return HookResult.Continue;
        _PlayerChat.OnPlayerChat(player, info, true);
        return HookResult.Continue;
    }
    public void OnEntityCreated(CEntityInstance entity)
    {
        if (entity == null || !entity.IsValid)return;
        if(entity.DesignerName.Contains("prop_physics_override") || entity.DesignerName.Contains("prop_physics") || entity.DesignerName.Contains("prop_physics_multiplayer"))
        {
            Entity(entity, true);
        }
        
    }
    public void OnEntitySpawned(CEntityInstance entity)
    {
        if (entity == null || !entity.IsValid)return;
        if(entity.DesignerName.Contains("prop_physics_override") || entity.DesignerName.Contains("prop_physics") || entity.DesignerName.Contains("prop_physics_multiplayer"))
        {
            Entity(entity, false);
        }
    }
    public void Entity(CEntityInstance entity, bool OnEntityCreated)
    {
        if (entity == null || !entity.IsValid)return;

        var getenity = entity.As<CBaseModelEntity>();
        var entitypath = getenity.CBodyComponent?.SceneNode?.GetSkeletonInstance().ModelState.ModelName;
        
        if(!g_Main.Entitys.ContainsKey(entity))
        {
            int health = Configs.GetConfigData().Default_Health;

            g_Main.Entitys.Add(entity, new Globals.GetEnt(entity,null!,health,health,string.Empty));
        }

        if(g_Main.Entitys.ContainsKey(entity) && !string.IsNullOrEmpty(entitypath))
        {
            g_Main.Entitys[entity].Entity = entity;
            g_Main.Entitys[entity].Entity_Path = entitypath;
            string AddedBy = OnEntityCreated?"OnEntityCreated":"OnEntitySpawned";
            Helper.DebugMessage($"Added Prop By {AddedBy} || Path: {entitypath}");

            var jsonData = g_Main.JsonData;
            if (jsonData == null) return;

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

                        g_Main.Entitys[entity].Target_Entity = entity;
                        g_Main.Entitys[entity].Entity_Health_Max = I_health;
                        g_Main.Entitys[entity].Entity_Health = I_health;
                        g_Main.Entitys[entity].Entity_Path = entitypath;
                        Helper.StartHighlightEnt(g_Main.Entitys[entity].Target_Entity);
                        break;
                    }
                }
            }
        }
        
    }

    private HookResult OnTakeDamage(DynamicHook hook)
    {
        try
        {
            var ent = hook.GetParam<CEntityInstance>(0);
            if (ent == null)
            {
                return HookResult.Continue;
            }
            bool entityExists = g_Main.Entitys.Any(entity =>
                entity.Value.Target_Entity != null &&
                entity.Value.Target_Entity.IsValid &&
                entity.Value.Target_Entity == ent &&
                entity.Value.Entity_Health != -1
            );
            if (!entityExists)
            {
                return HookResult.Continue;
            }

            var damageinfo = hook.GetParam<CTakeDamageInfo>(1);
            if (damageinfo == null)
            {
                return HookResult.Continue;
            }
            var attackerHandle = damageinfo.Attacker?.Value;
            if (attackerHandle == null || !attackerHandle.IsValid)
            {
                return HookResult.Continue;
            }

            var playerPawn = attackerHandle.As<CCSPlayerPawn>();
            if (playerPawn == null || !playerPawn.IsValid)
            {
                return HookResult.Continue;
            }
            var player = Utilities.GetPlayerFromIndex((int)playerPawn.Controller.Index);
            if (player == null || !player.IsValid)
            {
                return HookResult.Continue;
            }

            if(Configs.GetConfigData().Prop_Only_TeamXCanDamage == 1 && player.TeamNum != (byte)CsTeam.CounterTerrorist)
            {
                return HookResult.Continue;
            }else if(Configs.GetConfigData().Prop_Only_TeamXCanDamage == 2 && player.TeamNum != (byte)CsTeam.Terrorist)
            {
                return HookResult.Continue;
            }            

            if(g_Main.Attacker_Damage.ContainsKey(player))
            {
                var damagedone = damageinfo.TotalledDamage;
                g_Main.Entitys[ent].Entity_Health -= (int)Math.Round(damagedone);
                g_Main.Attacker_Damage[player].Entity_Health = g_Main.Entitys[ent].Entity_Health;
                g_Main.Attacker_Damage[player].Entity_Health_Max = g_Main.Entitys[ent].Entity_Health_Max;

                string input = Configs.GetConfigData().Prop_Damge_Print;
                string[] inputParts = input.Split(':');

                int Defaultduration = 5;
                int duration = Defaultduration;

                if (inputParts.Length > 1 && int.TryParse(inputParts[1], out int parsedDuration))
                {
                    duration = parsedDuration;
                }
                if (inputParts[0].Split(',').Contains("1"))
                {
                    Helper.AdvancedPlayerPrintToChat(player, Configs.Shared.StringLocalizer!["PrintChatToPlayer.Prop.Health"], g_Main.Entitys[ent].Entity_Health < 1 ? 0 : g_Main.Entitys[ent].Entity_Health);
                }

                if (inputParts[0].Split(',').Contains("2"))
                {
                    g_Main.Attacker_Damage[player].Show_Center_Now_Server = duration;
                    g_Main.Attacker_Damage[player].Show_Center_Now = 1;
                    g_Main.Attacker_Damage[player].LastTickTime = DateTime.Now;
                }

                if (inputParts[0].Split(',').Contains("3"))
                {
                    g_Main.Attacker_Damage[player].ShowBottom_Now_Server = duration;
                    g_Main.Attacker_Damage[player].ShowBottom_Now = 1;
                    g_Main.Attacker_Damage[player].LastTickTime = DateTime.Now;
                }

                if (inputParts[0].Split(',').Contains("4"))
                {
                    g_Main.Attacker_Damage[player].Show_Center_Now_Server = duration;
                    g_Main.Attacker_Damage[player].Show_Center_Now = 2;
                    g_Main.Attacker_Damage[player].LastTickTime = DateTime.Now;
                }

                if (inputParts[0].Split(',').Contains("5"))
                {
                    g_Main.Attacker_Damage[player].ShowBottom_Now_Server = duration;
                    g_Main.Attacker_Damage[player].ShowBottom_Now = 2;
                    g_Main.Attacker_Damage[player].LastTickTime = DateTime.Now;
                }

                if (g_Main.Entitys[ent].Target_Entity != null && g_Main.Entitys[ent].Target_Entity.IsValid)
                {
                    if (g_Main.Entitys[ent].Entity_Health < 1)
                    {
                        g_Main.Entitys[ent].Target_Entity.AcceptInput("Kill");
                    }
                }
                
            }

        }
        catch (Exception ex)
        {
            Helper.DebugMessage(ex.Message);
        }

        return HookResult.Continue;
    }

    private void OnMapEnd()
    {
        Helper.ClearVariables();
    }

    /* [ConsoleCommand("css_test", "test")]
    public void tesstttt(CCSPlayerController? player, CommandInfo commandInfo)
    {
        if(player == null || !player.IsValid)return;

    } */
}