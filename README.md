## .:[ Join Our Discord For Support ]:.
<a href="https://discord.com/invite/U7AuQhu"><img src="https://discord.com/api/guilds/651838917687115806/widget.png?style=banner2"></a>

***
# [CS2] Prop-Health-GoldKingZ (1.0.0)

### Prop Health

![prop_health](https://github.com/user-attachments/assets/2f1246f4-efce-4fe9-bceb-8ba42f5de78e)


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

  //Who Can Access To Get Props Paths
  "Get_Prop_Settings_Flags": "@css/root,@css/admin",

  //Command In Game To Get Props Paths
  "Get_Prop_Settings_CommandsInGame": "!getprop,!getprops",

//----------------------------[ ↓ Prop Configs ↓ ]-------------------------------

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
	
    "PrintChatToPlayer.Not.Allowed.ToReload": "{green}Gold KingZ {grey}| {darkred}You Dont Have Access To This Command.",
    "PrintChatToPlayer.Plugin.Reloaded": "{green}Gold KingZ {grey}| {lime}Prop Plugin Reloaded!",
    "PrintChatToPlayer.GetProp": "{green}Gold KingZ {grey}| Getting Props Paths {nextline} {green}Gold KingZ {grey}| For Map: {purple}{0} {nextline} ------------------------------------- {nextline} {green}Gold KingZ {grey}| All Prop Paths Saved In: {nextline} {darkblue} 'Prop-Health-GoldKingZ/Props/Prop_List.txt' {nextline} -------------------------------------", //{0} = Map Name
    "PrintChatToPlayer.Prop.Health": "{green}Gold KingZ {grey}| Prop Health: {purple}{0}", //{0} = Prop Health
    "ShowCenter.Prop.Health": "<font color='grey'>Prop Health:  <font color='#ff66ff'>{0}", //{0} = Prop Health
    "ShowCenter_Bottom.Prop.Health": "Prop Health: {0}" //{0} = Prop Health
}
```

## .:[ Change Log ]:.
```
(1.0.0)
-Initial Release
```

## .:[ Donation ]:.

If this project help you reduce time to develop, you can give me a cup of coffee :)

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://paypal.me/oQYh)
