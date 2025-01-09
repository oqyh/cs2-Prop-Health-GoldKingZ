## .:[ Join Our Discord For Support ]:.
<a href="https://discord.com/invite/U7AuQhu"><img src="https://discord.com/api/guilds/651838917687115806/widget.png?style=banner2"></a>

***
# [CS2] Prop-Health-GoldKingZ (1.0.2)

### Prop Health

![prop_health](https://github.com/user-attachments/assets/2f1246f4-efce-4fe9-bceb-8ba42f5de78e)

![prop_stylenew](https://github.com/user-attachments/assets/c230a401-596c-458d-86a0-c9b0a129959c)


## .:[ Dependencies ]:.
[Metamod:Source (2.x)](https://www.sourcemm.net/downloads.php/?branch=master)

[CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp/releases)

[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)

## .:[ Configuration ]:.

> [!CAUTION]
> Config Located In ..\addons\counterstrikesharp\plugins\Prop-Health-GoldKingZ\config\config.json                                           
>

```json
{

//----------------------------[ ↓ Main Configs ↓ ]-------------------------------

  //Who Can Access To Reload Prop_Settings.json
  "Reload_Prop_Settings_Flags": "@css/root,@css/admin",

  //Command In Game To Reload Prop_Settings.json
  "Reload_Prop_Settings_CommandsInGame": "!reloadprop,!reloadprops",

  //Who Can Access To Get Props Paths List
  "Get_Props_List_Flags": "@css/root,@css/admin",

  //Command In Game To Get Props Paths List
  "Get_Props_List_CommandsInGame": "!getprop,!getprops",

  //Who Can Access To Get Props Paths By Shooting
  "Get_Prop_Path_ByShooting_Flags": "@css/root,@css/admin",

  //Command In Game To Get Props Paths By Shooting
  "Get_Prop_Path_ByShooting_CommandsInGame": "!getpropbyshooting,!getpropbyshoot,!gpbs",

//----------------------------[ ↓ Prop Configs ↓ ]-------------------------------

  //Start Damage OnStart Round After X Secs 
  //(0) = Disabled
  "StartDamageOnStartRoundAfterXSecs": 10,

  //If Not Found Prop_Health In Prop_Settings.json What Default Health Will Be
  //(-1) = Doesnt break
  "Default_Health": -1,

  //Color Of The Prop
  //Color Must Be A R G P Try This Site https://rgbcolorpicker.com/
  //(-1) = No Color
  "Prop_Color_Argb": "255 0 0 255",

  //What Team Are Allowed To Damage Props
  //(0) = Any Team
  //(1) = CT
  //(2) = T
  "Prop_Only_TeamXCanDamage": 2,

  //Print Damage Into
  //(1) = Chat
  //(2) = Center 
  //(3) = Bottom Center 
  //(4) = Center (Bar Icon)
  //(5) = Bottom Center (Bar Icon)
  //(After : Is Duration In Secs Of Center Or Bottom Center) 
  "Prop_Damge_Print": "1,2:10",

//----------------------------[ ↓ Utilities ↓ ]----------------------------------------------
	
  //Enable Debug Will Print Server Console If You Face Any Issue
  "EnableDebug": false,
}
```

![329846165-ba02c700-8e0b-4ebe-bc28-103b796c0b2e](https://github.com/oqyh/cs2-Game-Manager/assets/48490385/3df7caa9-34a7-47da-94aa-8d682f59e85d)


## .:[ Language ]:.
```json
{
	//==========================
	//        Colors
	//==========================
	//{Yellow} {Gold} {Silver} {Blue} {DarkBlue} {BlueGrey} {Magenta} {LightRed}
	//{LightBlue} {Olive} {Lime} {Red} {Purple} {Grey}
	//{Default} {White} {Darkred} {Green} {LightYellow}
	//==========================
	//        Other
	//==========================
	//{nextline} = Print On Next Line
	//==========================
	
    "PrintChatToPlayer.Not.Allowed": "{green}Gold KingZ {grey}| {darkred}You Dont Have Access To This Command.",
    "PrintChatToPlayer.Plugin.Reloaded": "{green}Gold KingZ {grey}| {lime}Prop Plugin Reloaded!",
    "PrintChatToPlayer.Prop.Debug.Enabled": "{green}Gold KingZ {grey}| Get Path Props By Shooting Is {lime}Enabled {nextline} {green}Gold KingZ {grey}| Please Shoot Any Blue Prop To Get Prop Path {nextline} {green}Gold KingZ {grey}| Type Same Command To {darkred}Disable It",
    "PrintChatToPlayer.Prop.Debug.Disabled": "{green}Gold KingZ {grey}| Get Path Props By Shooting Is {darkred}Disabled",
    "PrintChatToPlayer.GetProp": "{green}Gold KingZ {grey}| Getting Props Paths {nextline} {green}Gold KingZ {grey}| For Map: {purple}{0} {nextline} ------------------------------------- {nextline} {green}Gold KingZ {grey}| All Prop Paths Saved In: {nextline} {darkblue} 'Prop-Health-GoldKingZ/Props/Prop_List.txt' {nextline} -------------------------------------",
    "PrintToChatToAll.Prop.Damage.Disabled": "{green}Gold KingZ {grey}| Props Damage Now {darkred}Disabled {grey}For {lime}{0} Secs",
    "PrintToChatToAll.Prop.Damage.Enabled": "{green}Gold KingZ {grey}| Props Damage Now {lime}Enabled",

    "PrintChatToPlayer.Prop.Health": "{green}Gold KingZ {grey}| Prop Health: {purple}{0}",
    "ShowCenter.Prop.Health": "<font color='grey'>Prop Health: <font color='#ff66ff'>{0}",
    "ShowCenter_Bottom.Prop.Health": "Prop Health: {0}",

    "ShowCenter.Prop.Health.100": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛",
    "ShowCenter.Prop.Health.95": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜",
    "ShowCenter.Prop.Health.90": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜",
    "ShowCenter.Prop.Health.85": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜",
    "ShowCenter.Prop.Health.80": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.75": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.70": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.65": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.60": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.55": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.50": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.45": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.40": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.35": "<font color='white'>⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.30": "<font color='white'>⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.25": "<font color='white'>⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.20": "<font color='white'>⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.15": "<font color='white'>⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.10": "<font color='white'>⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.5": "<font color='white'>⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter.Prop.Health.0": "<font color='white'>⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",


    "ShowCenter_Bottom.Prop.Health.100": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛",
    "ShowCenter_Bottom.Prop.Health.95": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜",
    "ShowCenter_Bottom.Prop.Health.90": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜",
    "ShowCenter_Bottom.Prop.Health.85": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.80": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.75": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.70": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.65": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.60": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.55": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.50": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.45": "⬛⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.40": "⬛⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.35": "⬛⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.30": "⬛⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.25": "⬛⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.20": "⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.15": "⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.10": "⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.5": "⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜",
    "ShowCenter_Bottom.Prop.Health.0": "⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜"

}
```

## .:[ Change Log ]:.
```
(1.0.2)
-Fix Apear Bar To Style 2 and 3
-Change Get_Prop_Settings_Flags To  Get_Props_List_Flags
-Change Get_Prop_Settings_CommandsInGame To  Get_Props_List_CommandsInGame
-Change rename Get_Props_List_Flags
-Added Get_Prop_Path_ByShooting_Flags
-Added Get_Prop_Path_ByShooting_CommandsInGame
-Added StartDamageOnStartRoundAfterXSecs

(1.0.1)
-Added Prop_Damge_Print 4 Center (Bar Icon)
-Added Prop_Damge_Print 5 Bottom Center (Bar Icon)

(1.0.0)
-Initial Release
```

## .:[ Donation ]:.

If this project help you reduce time to develop, you can give me a cup of coffee :)

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://paypal.me/oQYh)
