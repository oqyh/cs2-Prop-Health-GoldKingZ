using CounterStrikeSharp.API.Core;
using System.Diagnostics;

namespace Prop_Health_GoldKingZ;

public class Globals
{
    public class GetEnt
    {
        public CEntityInstance Entity { get; set; }
        public CEntityInstance Target_Entity { get; set; }
        public int Entity_Health { get; set; }
        public string Entity_Path { get; set; }
        
        public GetEnt(CEntityInstance entity, CEntityInstance target_Entity, int entity_Health, string entity_Path)
        {
            Entity = entity;
            Target_Entity = target_Entity;
            Entity_Health = entity_Health;
            Entity_Path = entity_Path;

        }

    }
    public Dictionary<CEntityInstance, GetEnt> Entitys = new Dictionary<CEntityInstance, GetEnt>();


    public class GetAttacker
    {
        public CCSPlayerController Attacker { get; set; }
        public int Entity_Health { get; set; }

        public bool Show_Center_Now { get; set; }
        public int Show_Center_Now_Server { get; set; }
        public bool ShowBottom_Now { get; set; }
        public int ShowBottom_Now_Server { get; set; }
        public DateTime LastTickTime { get; set; }
        public GetAttacker(CCSPlayerController attacker, int entity_Health,
        bool show_Center_Now, int show_Center_Now_Server, bool showBottom_Now, int showBottom_Now_Server, DateTime lastTickTime)
        {
            Attacker = attacker;
            Entity_Health = entity_Health;
            Show_Center_Now = show_Center_Now;
            Show_Center_Now_Server = show_Center_Now_Server;
            ShowBottom_Now = showBottom_Now;
            ShowBottom_Now_Server = showBottom_Now_Server;
            LastTickTime = lastTickTime;
        }

    }
    public Dictionary<CCSPlayerController, GetAttacker> Attacker_Damage = new Dictionary<CCSPlayerController, GetAttacker>();
    public Dictionary<string, object>? JsonData { get; set; }

}