using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using Discord.Rest;
using System.Reflection;
using System.Xml;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Immutable;
using System.Net;
using System.Collections;
using System.Xml.Serialization;
using System.Threading;

namespace Zombie_Apocalypse_Discord_Bot.Modules
{
    [XmlInclude(typeof(Charsheet))]
    [Serializable]
    // for commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class Commands : ModuleBase
    {
        [Command("serversetup", RunMode = RunMode.Async)]
        public async Task ServerSetupCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            List<string> ws = new List<string>();
            Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
            Weapon bow = new Weapon("bow", "ranged", "piercing", 10, "sturdy", "accurate", "quiet", "slow", "");
            Weapon revolver = new Weapon("revolver", "ranged", "piercing", 6, "rusty", "loud", "slow", "", "");
            Weapon handgun = new Weapon("handgun", "ranged", "piercing", 8, "well-oiled", "loud", "accurate", "", "");
            Weapon longRifle = new Weapon("long rifle", "ranged", "piercing", 10, "accurate", "sturdy", "loud", "slow", "");
            Weapon militaryRifle = new Weapon("military rifle", "ranged", "piercing", 12, "well-oiled", "loud", "fast", "sturdy", "");
            Weapon subMachineGun = new Weapon("submachine gun", "ranged", "piercing", 6, "well-oiled", "very fast", "loud", "inaccurate", "");
            Weapon shotgun = new Weapon("shotgun", "ranged", "piercing", 14, "very inaccurate", "very loud", "fast", "", "");
            Weapon crossbow = new Weapon("crossbow", "ranged", "piercing", 12, "very accurate", "very slow", "quiet", "", "");
            Weapon sniperRifle = new Weapon("sniper rifle", "ranged", "piercing", 18, "slow", "very accurate", "high-velocity", "well-oiled", "");
            Weapon lmg = new Weapon("light machine gun", "ranged", "piercing", 6, "very fast", "inaccurate", "well-oiled", "sturdy", "");
            Weapon branch = new Weapon("sturdy branch", "melee", "bludgeoning", 2, "fast", "sturdy", "unwieldy", "", "");
            Weapon bat = new Weapon("baseball bat", "melee", "bludgeoning", 5, "sturdy", "fast", "", "", "");
            Weapon baton = new Weapon("police baton", "melee", "bludgeoning", 4, "sturdy", "fast", "small", "", "");
            Weapon machete = new Weapon("machete", "melee", "slashing", 6, "fast", "well-oiled", "", "", "");
            Weapon katana = new Weapon("katana", "melee", "slashing", 10, "fast", "well-oiled", "sturdy", "nimble", "");
            Weapon crowbar = new Weapon("crowbar", "melee", "bludgeoning", 8, "slow", "sturdy", "heavy", "", "");
            Weapon pipeWrench = new Weapon("large pipe wrench", "melee", "bludgeoning", 10, "very slow", "sturdy", "heavy", "rusty", "");
            Weapon pitchfork = new Weapon("pitchfork", "melee", "piercing", 8, "slow", "sturdy", "rusty", "", "");
            Weapon hatchet = new Weapon("hatchet", "melee", "slashing", 8, "fast", "small", "well-oiled", "", "");
            Weapon sledgehammer = new Weapon("sledgehammer", "melee", "bludgeoning", 12, "very slow", "very heavy", "very sturdy", "crushing", "");
            Weapon plank = new Weapon("2 x 4 wooden plank", "melee", "bludgeoning", 3, "slow", "unwieldy", "", "", "");
            Weapon scythe = new Weapon("scythe", "melee", "slashing", 8, "fast", "slashing", "rusty", "long", "");
            Weapon nunchucks = new Weapon("nunchucks", "melee", "bludgeoning", 6, "very fast", "small", "", "", "");
            Weapon makeshiftSpear = new Weapon("makeshift spear", "melee", "piercing", 8, "fast", "fragile", "rusty", "", "");
            Weapon cattleProd = new Weapon("cattle prod", "melee", "elemental", 6, "fast", "small", "shocking", "", "");
            Weapon brassKnuckles = new Weapon("brass knuckles", "melee", "bludgeoning", 4, "fast", "very small", "light-weight", "", "");
            Weapon shovel = new Weapon("shovel", "melee", "bludgeoning", 10, "slow", "rusty", "unwieldy", "", "");
            Weapon dirtyHoe = new Weapon("dirty hoe", "melee", "piercing", 6, "fast", "unwieldy", "rusty", "", "");
            Weapon tableLeg = new Weapon("table leg", "melee", "bludgeoning", 4, "slow", "unwieldy", "very fragile", "", "");
            Weapon claymore = new Weapon("claymore", "melee", "slashing", 12, "slow", "heavy", "well-oiled", "sturdy", "");
            Weapon fists = new Weapon("fists", "melee", "bludgeoning", 2, "", "", "", "", "");

            weapons.Add("bow", bow);
            weapons.Add("revolver", revolver);
            weapons.Add("handgun", handgun);
            weapons.Add("long rifle", longRifle);
            weapons.Add("military rifle", militaryRifle);
            weapons.Add("submachine gun", subMachineGun);
            weapons.Add("shotgun", shotgun);
            weapons.Add("crossbow", crossbow);
            weapons.Add("sniper rifle", sniperRifle);
            weapons.Add("light machine gun", lmg);
            weapons.Add("sturdy branch", branch);
            weapons.Add("baseball bat", bat);
            weapons.Add("police baton", baton);
            weapons.Add("machete", machete);
            weapons.Add("katana", katana);
            weapons.Add("crowbar", crowbar);
            weapons.Add("large pipe wrench", pipeWrench);
            weapons.Add("pitchfork", pitchfork);
            weapons.Add("hatchet", hatchet);
            weapons.Add("sledgehammer", sledgehammer);
            weapons.Add("2 x 4 wooden plank", plank);
            weapons.Add("scythe", scythe);
            weapons.Add("nunchucks", nunchucks);
            weapons.Add("makeshift spear", makeshiftSpear);
            weapons.Add("cattle prod", cattleProd);
            weapons.Add("brass knuckles", brassKnuckles);
            weapons.Add("shovel", shovel);
            weapons.Add("dirty hoe", dirtyHoe);
            weapons.Add("table leg", tableLeg);
            weapons.Add("claymore", claymore);
            weapons.Add("fists", fists);

            Serialize(weapons, "weaponDictionary");
            ws.Add("bow");
            ws.Add("revolver");
            ws.Add("handgun");
            ws.Add("long rifle");
            ws.Add("military rifle");
            ws.Add("submachine gun");
            ws.Add("shotgun");
            ws.Add("crossbow");
            ws.Add("sniper rifle");
            ws.Add("light machine gun");
            ws.Add("sturdy branch");
            ws.Add("baseball bat");
            ws.Add("police baton");
            ws.Add("machete");
            ws.Add("katana");
            ws.Add("crowbar");
            ws.Add("large pipe wrench");
            ws.Add("pitchfork");
            ws.Add("hatchet");
            ws.Add("sledgehammer");
            ws.Add("2 x 4 wooden plank");
            ws.Add("scythe");
            ws.Add("nunchucks");
            ws.Add("makeshift spear");
            ws.Add("cattle prod");
            ws.Add("brass knuckles");
            ws.Add("shovel");
            ws.Add("dirty hoe");
            ws.Add("table leg");
            ws.Add("claymore");
            ws.Add("fists");

            Serialize(ws, "weaponsList");

            List<Settlement> blankSettlements = new List<Settlement>();
            Settlement settlement = new Settlement();
            string settlementName = "";
            settlementName = "7-Eleven";
            settlement = new Settlement(settlementName, 1, 1, 1);
            blankSettlements.Add(settlement);
            settlementName = "Duplex";
            settlement = new Settlement(settlementName, 2, 2, 2);
            blankSettlements.Add(settlement);
            settlementName = "Church";
            settlement = new Settlement(settlementName, 3, 3, 3);
            blankSettlements.Add(settlement);
            settlementName = "Farmhouse";
            settlement = new Settlement(settlementName, 4, 4, 4);
            blankSettlements.Add(settlement);
            settlementName = "Apartment Complex";
            settlement = new Settlement(settlementName, 5, 5, 5);
            blankSettlements.Add(settlement);
            settlementName = "Department Store";
            settlement = new Settlement(settlementName, 6, 6, 6);
            blankSettlements.Add(settlement);
            settlementName = "Police Station";
            settlement = new Settlement(settlementName, 7, 7, 7);
            blankSettlements.Add(settlement);
            settlementName = "Mini-Mall";
            settlement = new Settlement(settlementName, 8, 8, 8);
            blankSettlements.Add(settlement);
            settlementName = "Small Airport";
            settlement = new Settlement(settlementName, 9, 9, 9);
            blankSettlements.Add(settlement);
            settlementName = "Prison";
            settlement = new Settlement(settlementName, 10, 10, 10);
            blankSettlements.Add(settlement);
            Serialize(blankSettlements, "\\Settlements\\settlementDatabase");


            // build out the reply
            sb.AppendLine($"Databases successfully built. You may begin playing now. You only need to run this command once.");

            // set embed fields
            embed.Title = "Building Databases...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("help", RunMode = RunMode.Async)]
        [Alias("h")]
        public async Task HelpCommand(string cmd = "")
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();
            var user = Context.User;
            await BeginMethod(user);
            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            cmd = cmd.ToLower();
            if (cmd != "")
            {
                switch (cmd)
                {
                    case "help":
                        sb.AppendLine($"Shows a list of all commands in the bot!");
                        sb.AppendLine($" Usage: =help");
                        break;
                    case "stats":
                        sb.AppendLine($"Shows you your current stats screen!");
                        sb.AppendLine($" Usage: =stats");
                        break;
                    case "forage":
                        sb.AppendLine($"This is the main command for the bot. You can gather materials, or possibly run into zombies while foraging, so be careful!");
                        sb.AppendLine($"Usage: =forage, =for, or =f");
                        break;
                    case "for":
                        sb.AppendLine($"This is the main command for the bot. You can gather materials, or possibly run into zombies while foraging, so be careful!");
                        sb.AppendLine($"Usage: =forage, =for, or =f");
                        break;
                    case "f":
                        sb.AppendLine($"This is the main command for the bot. You can gather materials, or possibly run into zombies while foraging, so be careful!");
                        sb.AppendLine($"Usage: =forage, =for, or =f");
                        break;
                    case "inventory":
                        sb.AppendLine($"This is the command to check your inventory. Any items discovered that aren't in your stats screen will show up here!");
                        sb.AppendLine($"Usage: =inventory, =inv, or =i");
                        break;
                    case "inv":
                        sb.AppendLine($"This is the command to check your inventory. Any items discovered that aren't in your stats screen will show up here!");
                        sb.AppendLine($"Usage: =inventory, =inv, or =i");
                        break;
                    case "i":
                        sb.AppendLine($"This is the command to check your inventory. Any items discovered that aren't in your stats screen will show up here!");
                        sb.AppendLine($"Usage: =inventory, =inv, or =i");
                        break;
                    case "heal":
                        sb.AppendLine($"This command will heal any injuries you've sustained while foraging. Be warned, healing costs Meds!");
                        sb.AppendLine($"Usage: =heal [quantity to use], =bandage [quantity to use], or =he [quantity to use]");
                        break;
                    case "he":
                        sb.AppendLine($"This command will heal any injuries you've sustained while foraging. Be warned, healing costs Meds!");
                        sb.AppendLine($"Usage: =heal [quantity to use], =bandage [quantity to use], or =he [quantity to use]");
                        break;
                    case "bandage":
                        sb.AppendLine($"This command will heal any injuries you've sustained while foraging. Be warned, healing costs Meds!");
                        sb.AppendLine($"Usage: =heal [quantity to use], =bandage [quantity to use], or =he [quantity to use]");
                        break;
                    case "eat":
                        sb.AppendLine($"This command will fill your hunger, which drops after every forage. Be warned, eating costs Food!");
                        sb.AppendLine($"Usage: =eat [quantity to use], =e [quantity to use], or =feed [quantity to use]");
                        break;
                    case "e":
                        sb.AppendLine($"This command will fill your hunger, which drops after every forage. Be warned, eating costs Food!");
                        sb.AppendLine($"Usage: =eat [quantity to use], =e [quantity to use], or =feed [quantity to use]");
                        break;
                    case "feed":
                        sb.AppendLine($"This command will fill your hunger, which drops after every forage. Be warned, eating costs Food!");
                        sb.AppendLine($"Usage: =eat [quantity to use], =e [quantity to use], or =feed [quantity to use]");
                        break;
                    case "drink":
                        sb.AppendLine($"This command will fill your thirst, which drops after every forage. Be warned, drinking costs Water!");
                        sb.AppendLine($"Usage: =drink [quantity to use], =d [quantity to use], or =rehydrate [quantity to use]");
                        break;
                    case "d":
                        sb.AppendLine($"This command will fill your thirst, which drops after every forage. Be warned, drinking costs Water!");
                        sb.AppendLine($"Usage: =drink [quantity to use], =d [quantity to use], or =rehydrate [quantity to use]");
                        break;
                    case "rehydrate":
                        sb.AppendLine($"This command will fill your thirst, which drops after every forage. Be warned, drinking costs Water!");
                        sb.AppendLine($"Usage: =drink [quantity to use], =d [quantity to use], or =rehydrate [quantity to use]");
                        break;
                    case "disinfect":
                        sb.AppendLine($"This command will decrease your infection levels. Note that unless you disinfect enough to completely get rid of your infection, it will continue to get worse. Be warned, disinfecting costs Meds!");
                        sb.AppendLine($"Usage: =disinfect [quantity to use], =dis [quantity to use], or =disinf [quantity to use]");
                        break;
                    case "dis":
                        sb.AppendLine($"This command will decrease your infection levels. Note that unless you disinfect enough to completely get rid of your infection, it will continue to get worse. Be warned, disinfecting costs Meds!");
                        sb.AppendLine($"Usage: =disinfect [quantity to use], =dis [quantity to use], or =disinf [quantity to use]");
                        break;
                    case "disinf":
                        sb.AppendLine($"This command will decrease your infection levels. Note that unless you disinfect enough to completely get rid of your infection, it will continue to get worse. Be warned, disinfecting costs Meds!");
                        sb.AppendLine($"Usage: =disinfect [quantity to use], =dis [quantity to use], or =disinf [quantity to use]");
                        break;
                    case "checkinfection":
                        sb.AppendLine($"This command will give you a rough idea of how severe your infection is.");
                        sb.AppendLine($"Usage: =checkinfection, =checkinf, =cinf");
                        break;
                    case "checkinf":
                        sb.AppendLine($"This command will give you a rough idea of how severe your infection is.");
                        sb.AppendLine($"Usage: =checkinfection, =checkinf, =cinf");
                        break;
                    case "cinf":
                        sb.AppendLine($"This command will give you a rough idea of how severe your infection is.");
                        sb.AppendLine($"Usage: =checkinfection, =checkinf, =cinf");
                        break;
                    case "alliance":
                        sb.AppendLine($"The alliance commands relate to player alliances that you can create and join. Alliances have no player limit, so you can have as big of one as you'd like.");
                        sb.AppendLine($"If you join an alliance, other players in that alliance may forage with you, and you with them! You'll each get the same amount of loot as you would have foraging alone, with the added benefit of having backup if you run into zombies!");
                        sb.AppendLine($"Available alliance commands: =alliance leave, =alliance create, =alliance join");
                        break;
                    case "alliance create":
                        sb.AppendLine($"This command creates an alliance with only you in it, if one by that name doesn't already exist, and if you aren't in one already. You must provide a name to create an alliance.");
                        sb.AppendLine($"Usage: =alliance create, =acreate, =alliance c");
                        break;
                    case "alliance c":
                        sb.AppendLine($"This command creates an alliance with only you in it, if one by that name doesn't already exist, and if you aren't in one already. You must provide a name to create an alliance.");
                        sb.AppendLine($"Usage: =alliance create, =acreate, =alliance c");
                        break;
                    case "acreate":
                        sb.AppendLine($"This command creates an alliance with only you in it, if one by that name doesn't already exist, and if you aren't in one already. You must provide a name to create an alliance.");
                        sb.AppendLine($"Usage: =alliance create, =acreate, =alliance c");
                        break;
                    case "alliance invite":
                        sb.AppendLine($"This command allows any user in an alliance to invite other people to the alliance. As of yet, there is no way to prevent any user in your alliance from inviting others.");
                        sb.AppendLine($"Usage: =alliance invite, =ainvite, =alliance i");
                        break;
                    case "ainvite":
                        sb.AppendLine($"This command allows any user in an alliance to invite other people to the alliance. As of yet, there is no way to prevent any user in your alliance from inviting others.");
                        sb.AppendLine($"Usage: =alliance invite, =ainvite, =alliance i");
                        break;
                    case "alliance i":
                        sb.AppendLine($"This command allows any user in an alliance to invite other people to the alliance. As of yet, there is no way to prevent any user in your alliance from inviting others.");
                        sb.AppendLine($"Usage: =alliance invite, =ainvite, =alliance i");
                        break;
                    case "alliance leave":
                        sb.AppendLine($"This command allows any user in an alliance to leave that alliance. Be warned that, if any other players are in that alliance, the alliance itself will still be active. You will just no longer be in it.");
                        sb.AppendLine($"Usage: =alliance leave, =aleave, =alliance l");
                        break;
                    case "aleave":
                        sb.AppendLine($"This command allows any user in an alliance to leave that alliance. Be warned that, if any other players are in that alliance, the alliance itself will still be active. You will just no longer be in it.");
                        sb.AppendLine($"Usage: =alliance leave, =aleave, =alliance l");
                        break;
                    case "alliance l":
                        sb.AppendLine($"This command allows any user in an alliance to leave that alliance. Be warned that, if any other players are in that alliance, the alliance itself will still be active. You will just no longer be in it.");
                        sb.AppendLine($"Usage: =alliance leave, =aleave, =alliance l");
                        break;
                    case "storagestore":
                        sb.AppendLine($"This command allows any user to store an item in their alliance storage, provided that someone in their alliance has built a Storage Area.");
                        sb.AppendLine($"Usage: =storagestore [item quantity] [name of item], =store [item quantity] [name of item]");
                        break;
                    case "store":
                        sb.AppendLine($"This command allows any user to store an item in their alliance storage, provided that someone in their alliance has built a Storage Area.");
                        sb.AppendLine($"Usage: =storagestore [item quantity] [name of item], =store [item quantity] [name of item]");
                        break;
                    case "storagefetch":
                        sb.AppendLine($"This command allows any user to fetch an item from their alliance storage, provided that someone in their alliance has built a Storage Area, and that item has been stored there.");
                        sb.AppendLine($"Usage: =storagefetch [item quantity] [name of item], =fetch [item quantity] [name of item]");
                        break;
                    case "fetch":
                        sb.AppendLine($"This command allows any user to fetch an item from their alliance storage, provided that someone in their alliance has built a Storage Area, and that item has been stored there.");
                        sb.AppendLine($"Usage: =storagefetch [item quantity] [name of item], =fetch [item quantity] [name of item]");
                        break;
                    case "storagelist":
                        sb.AppendLine($"This command allows any user in an alliance to see all items stored in their alliance storage.");
                        sb.AppendLine($"Usage: =storagelist");
                        break;
                    case "skill":
                        sb.AppendLine($"This command allows you to either view how many skill points you have, or spend them to increase a stat. You get a skill point when you level up!");
                        sb.AppendLine($"Usage: =skill to check your skill points, or =skill [stat] [points] to spend them.");
                        break;
                    case "melee":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Melee attacks can be done with melee weapons, and are made more powerful by Strength points.");
                        sb.AppendLine($"Usage: =melee [zombie to target] [weapon to attack with]");
                        break;
                    case "m":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Melee attacks can be done with melee weapons, and are made more powerful by Strength points.");
                        sb.AppendLine($"Usage: =melee [zombie to target] [weapon to attack with]");
                        break;
                    case "shoot":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Shoot attacks can be done with ranged weapons, and are made more powerful by Intelligence points.");
                        sb.AppendLine($"Usage: =shoot [zombie to target] [weapon to attack with]");
                        break;
                    case "sh":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Shoot attacks can be done with ranged weapons, and are made more powerful by Intelligence points.");
                        sb.AppendLine($"Usage: =shoot [zombie to target] [weapon to attack with]");
                        break;
                    case "distract":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Distractions lower zombies' attack damage for one round, and are made more powerful by Charisma points.");
                        sb.AppendLine($"Usage: =distract [zombie to target]");
                        break;
                    case "di":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Distractions lower zombies' attack damage for one round, and are made more powerful by Charisma points.");
                        sb.AppendLine($"Usage: =distract [zombie to target]");
                        break;
                    case "run":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Running removes you from combat, keeping you from taking damage, but also prevents you from helping at all in the remaning fight.");
                        sb.AppendLine($"This action requires no roll, and can be done at any time during combat.");
                        sb.AppendLine($"Usage: =run, or =r");
                        break;
                    case "r":
                        sb.AppendLine($"This is an action you can take against zombies during combat. Running removes you from combat, keeping you from taking damage, but also prevents you from helping at all in the remaning fight.");
                        sb.AppendLine($"This action requires no roll, and can be done at any time during combat.");
                        sb.AppendLine($"Usage: =run, or =r");
                        break;
                    case "trade":
                        sb.AppendLine($"This command is used to begin a trade with the other player, known as the 'target'. Simply use this command, then follow the on-screen prompts.");
                        sb.AppendLine($"Usage: =trade [@user]");
                        break;
                    case "settlementlist":
                        sb.AppendLine($"This command will print a list of all of your owned settlements.");
                        sb.AppendLine($"Usage: =settlementlist, =slist");
                        break;
                    case "slist":
                        sb.AppendLine($"This command will print a list of all of your owned settlements.");
                        sb.AppendLine($"Usage: =settlementlist, =slist");
                        break;
                    case "settlementinfo":
                        sb.AppendLine($"This command will show you information about a settlement that you specify.");
                        sb.AppendLine($"Usage: =settlementinfo [settlement name], =sinfo [settlement name]");
                        break;
                    case "sinfo":
                        sb.AppendLine($"This command will show you information about a settlement that you specify.");
                        sb.AppendLine($"Usage: =settlementinfo [settlement name], =sinfo [settlement name]");
                        break;
                    case "settlementrebuild":
                        sb.AppendLine($"This command will start the process to rebuild a settlement that you specify.");
                        sb.AppendLine($"Usage: =settlementrebuild [settlement name], =srebuild [settlement name]");
                        break;
                    case "srebuild":
                        sb.AppendLine($"This command will start the process to rebuild a settlement that you specify.");
                        sb.AppendLine($"Usage: =settlementrebuild [settlement name], =srebuild [settlement name]");
                        break;
                    case "settlementdevelop":
                        sb.AppendLine($"This command will start the process to develop a module in a settlement that you specify.");
                        sb.AppendLine($"Usage: =settlementdevelop [settlement name], =sdevelop [settlement name]");
                        break;
                    case "sdevelop":
                        sb.AppendLine($"This command will start the process to rebuild a settlement that you specify.");
                        sb.AppendLine($"Usage: =settlementrebuild [settlement name], =srebuild [settlement name]");
                        break;
                    case "settlementremovemodule":
                        sb.AppendLine($"This command will remove a module from a settlement that you specify. Your scrap will be refunded.");
                        sb.AppendLine($"Usage: =settlementremovemodule [slot number] [settlement name], =srm [slot number] [settlement name]");
                        break;
                    case "srm":
                        sb.AppendLine($"This command will remove a module from a settlement that you specify. Your scrap will be refunded.");
                        sb.AppendLine($"Usage: =settlementremovemodule [slot number] [settlement name], =srm [slot number] [settlement name]");
                        break;
                    case "settlementcheckrebuild":
                        sb.AppendLine($"This command will check how much time is remaining on a rebuild you began.");
                        sb.AppendLine($"Usage: =settlementcheckrebuild, =scheckr, =scheckrebuild");
                        break;
                    case "scheckr":
                        sb.AppendLine($"This command will check how much time is remaining on a rebuild you began.");
                        sb.AppendLine($"Usage: =settlementcheckrebuild, =scheckr, =scheckrebuild");
                        break;
                    case "scheckrebuild":
                        sb.AppendLine($"This command will check how much time is remaining on a rebuild you began.");
                        sb.AppendLine($"Usage: =settlementcheckrebuild, =scheckr, =scheckrebuild");
                        break;
                    case "settlementcollect":
                        sb.AppendLine($"This command will collect any resources that your settlements have produced or gathered recently.");
                        sb.AppendLine($"Usage: =settlementcollect, =scollect");
                        break;
                    case "scollect":
                        sb.AppendLine($"This command will collect any resources that your settlements have produced or gathered recently.");
                        sb.AppendLine($"Usage: =settlementcollect, =scollect");
                        break;
                    case "moduleinfo":
                        sb.AppendLine($"This command will provide a list of the different available modules for each settlement, as well as their purpose.");
                        sb.AppendLine($"Usage: =moduleinfo, =modinfo");
                        break;
                    case "modinfo":
                        sb.AppendLine($"This command will provide a list of the different available modules for each settlement, as well as their purpose.");
                        sb.AppendLine($"Usage: =moduleinfo, =modinfo");
                        break;
                    case "shop":
                        sb.AppendLine($"This command will start the process for you to buy or sell items to the NPC shop, run by Dusty.");
                        sb.AppendLine($"Usage: =shop");
                        break;
                    case "give":
                        sb.AppendLine($"This command allows you to gift items to players for nothing in return.");
                        sb.AppendLine($"Usage: =give [user to target] [quantity] [name of item]");
                        break;
                    default:
                        sb.AppendLine($"Command " + cmd + " not found. Please try again, or just type =help for a list of commands!");
                        break;
                }
            }
            else
            {
                sb.AppendLine("**Available Commands**");
                sb.AppendLine("help");
                sb.AppendLine("stats");
                sb.AppendLine("forage");
                sb.AppendLine("inventory");
                sb.AppendLine("heal");
                sb.AppendLine("drink");
                sb.AppendLine("eat");
                sb.AppendLine("checkinfection");
                sb.AppendLine("disinfect");
                sb.AppendLine("alliance create");
                sb.AppendLine("alliance invite");
                sb.AppendLine("alliance leave");
                sb.AppendLine("storagestore");
                sb.AppendLine("storagefetch");
                sb.AppendLine("storagelist");
                sb.AppendLine("skill");
                sb.AppendLine("melee");
                sb.AppendLine("shoot");
                sb.AppendLine("distract");
                sb.AppendLine("run");
                sb.AppendLine("trade");
                sb.AppendLine("settlementlist");
                sb.AppendLine("settlementinfo");
                sb.AppendLine("settlementrebuild");
                sb.AppendLine("settlementdevelop");
                sb.AppendLine("settlementremovemodule");
                sb.AppendLine("settlementcheckrebuild");
                sb.AppendLine("settlementcollect");
                sb.AppendLine("moduleinfo");
                sb.AppendLine("shop");
                sb.AppendLine("give");
                sb.AppendLine($"You can type =h [command name] to learn more about that command!");
            }
            // build out the reply
            sb.AppendLine($"");

            // set embed fields
            embed.Title = "Help";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("adminhelp", RunMode = RunMode.Async)]
        [Alias("ahelp")]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task AdminHelpCommand(string cmd = "")
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;

            if (cmd != "")
            {
                switch (cmd)
                {
                    case "levelup":
                        sb.AppendLine($"This command allows you to force yourself, or any user if they're specified, to level up.");
                        sb.AppendLine($"Usage: =levelup [@user]");
                        break;
                    case "adminhelp":
                        sb.AppendLine($"This command shows this screen, all the admin commands you can use!");
                        sb.AppendLine($"Usage: =adminhelp");
                        break;
                    case "additem":
                        sb.AppendLine($"This command allows you to add any item to your inventory, or to a user's inventory if they're specified. Also optional is the quantity of the item.");
                        sb.AppendLine($"Usage: =additem [@user] [add or subtract] [quantity] [item]");
                        break;
                    case "reset":
                        sb.AppendLine($"This command lets you reset your own progress, or another user's if you specify them.");
                        sb.AppendLine($"Usage: =reset [@user]");
                        break;
                    case "playeredit":
                        sb.AppendLine($"This command lets you add or subtract points from player stats.");
                        sb.AppendLine($"Usage: =playeredit [username] [stat to be edited] [add, subtract, or set] [quantity to modify by]");
                        break;
                    case "addskillpoints":
                        sb.AppendLine($"This command lets you add skill points to a player.");
                        sb.AppendLine($"Usage: =addskillpoints [quantity] [@user]");
                        break;
                    case "addcustomweapon":
                        sb.AppendLine($"This commmand allows you to add a custom weapon to the game. Note that custom weapons cannot yet be found while foraging; they must be given to a player with additem.");
                        sb.AppendLine($"Usage: =addcustomweapon [melee or ranged] [piercing, slashing, bludgeoning, or elemental damage] [base damage] [weapon tag 1] [weapon tag 2] [weapon tag 3] [weapon tag 4] [weapon tag 5] [name of weapon]");
                        sb.AppendLine($"");
                        sb.AppendLine("WARNING: This command requires very specific syntax. If you choose not to provide all 5 tags, please mark whatever tags you don't provide as 0. Doing otherwise will break your weapon. Also note that names must be a maximum of three words.");
                            break;
                    case "resetmodules":
                        sb.AppendLine($"This command lets you reset all modules of a specified player to their original blank state.");
                        sb.AppendLine($"Usage: =resetmodules [user to target] [name of their settlement]");
                        break;
                    case "setserverinfo":
                        sb.AppendLine($"This command should be run before all other commands so that the storage for your server is initialized.");
                        sb.AppendLine($"Usage: =setserverinfo [@owner] [server prefix] [mention channel for horde battles to take place in] [name of server], or =ssi [@owner] [server prefix] [mention channel for horde battles to take place in] [name of server]");
                        break;
                    case "ssi":
                        sb.AppendLine($"This command should be run before all other commands so that the storage for your server is initialized.");
                        sb.AppendLine($"Usage: =setserverinfo [@owner] [server prefix] [mention channel for horde battles to take place in] [name of server], or =ssi [@owner] [server prefix] [mention channel for horde battles to take place in] [name of server]");
                        break;
                    case "resetattackstates":
                        sb.AppendLine($"This command resets the \"is under attack\" variable for all settlements, in case of a broken horde attack.");
                        sb.AppendLine($"Usage: =resetattackstates, =ras");
                        break;
                    case "ras":
                        sb.AppendLine($"This command resets the \"is under attack\" variable for all settlements, in case of a broken horde attack.");
                        sb.AppendLine($"Usage: =resetattackstates, =ras");
                        break;
                    case "addsettlement":
                        sb.AppendLine($"This command adds a settlement to any player's settlement list. Note that you can only add real settlements, and development still must go through =sdevelop.");
                        sb.AppendLine($"Usage: =addsettlement [@user] [settlement name]");
                        break;
                    case "clearsettlements":
                        sb.AppendLine($"This command clears all of a user's settlements. If no user is specified, it will clear the settlements of the user running the command.");
                        sb.AppendLine($"Usage: =clearsettlements [@user]");
                        break;
                    case "dustyrestock":
                        sb.AppendLine($"This command instantly refreshes Dusty's stock.");
                        sb.AppendLine($"Usage: =dustyrestock");
                        break;
                    case "restockinterval":
                        sb.AppendLine($"This command allows you to specify how many hours it takes before Dusty refreshes her inventory.");
                        sb.AppendLine($"Usage: =restockinterval [hours]");
                        break;
                    case "backup":
                        sb.AppendLine($"This command will back up the server's files immediately.");
                        sb.AppendLine($"Usage: =backup");
                        break;
                    case "userstats":
                        sb.AppendLine($"This command lets you view another user's character sheet.");
                        sb.AppendLine($"Usage: =userstats [@user]");
                        break;
                    case "resetspv":
                        sb.AppendLine($"Don't touch this command unless you know what you're doing! Resets player's settlement variables in case they're in an invalid state.");
                        sb.AppendLine($"Usage: =resetspv [@user]");
                        break;
                    default:
                        sb.AppendLine($"Admin command " + cmd + " not found. Either the command is not an admin command, or it doesn't exist! Please try again.");
                        break;
                }
            }
            else
            {
                sb.AppendLine($"**Admin Commands**");
                sb.AppendLine($"levelup");
                sb.AppendLine($"additem");
                sb.AppendLine($"playeredit");
                sb.AppendLine($"addskillpoints");
                sb.AppendLine($"addcustomweapon");
                sb.AppendLine($"setserverinfo");
                sb.AppendLine($"reset");
                sb.AppendLine($"resetmodules");
                sb.AppendLine($"resetattackstates");
                sb.AppendLine($"addsettlement");
                sb.AppendLine($"clearsettlements");
                sb.AppendLine($"dustyrestock");
                sb.AppendLine($"restockinterval");
                sb.AppendLine($"backup");
                sb.AppendLine($"userstats");
                sb.AppendLine($"resetspv");
                sb.AppendLine($"Do =adminhelp [command] to see more info on a certain admin command!");
            }
            // build out the reply
            sb.AppendLine($"");

            // set embed fields
            embed.Title = "Admin Help";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("dustyrestock", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task DustyRestockCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            List<string> consumables = new List<string>();
            List<string> weapons = DeserializeWeaponsList();
            List<string> validItems = new List<string>();
            consumables.Add("Water");
            consumables.Add("Food");
            consumables.Add("Meds");
            consumables.Add("Ammo");
            consumables.Add("Scrap");
            weapons.Remove("fists");
            List<string> choicesSoFar = new List<string>();
            List<Item> items = new List<Item>();
            int cnt = 1;
            string choice = null;
            while (cnt <= 5)
            {
                int qty = 5;
                int cost = 0;
                if (cnt <= 3)
                {
                    Random rand = new Random();
                    bool picked = false;
                    bool endLoop = false;
                    if (choicesSoFar.Count >= 1)
                    {
                        while (!endLoop)
                        {
                            picked = false;
                            int choiceIndex = rand.Next(0, consumables.Count());
                            choice = consumables.ElementAt(choiceIndex);
                            foreach (string c in choicesSoFar)
                            {
                                if (c == choice)
                                {
                                    picked = true;
                                }
                            }
                            if (!picked)
                            {
                                choicesSoFar.Add(choice);
                                endLoop = true;
                            }
                        }
                    }
                    else
                    {
                        int choiceIndex = rand.Next(0, consumables.Count());
                        choice = consumables.ElementAt(choiceIndex);
                        choicesSoFar.Add(choice);
                    }
                    qty = rand.Next(25, 50);
                    cost = 1;
                }
                else
                {
                    Random rand = new Random();
                    int choiceIndex = rand.Next(0, weapons.Count());
                    choice = weapons.ElementAt(choiceIndex);
                    Dictionary<string, Weapon> wDict = DeserializeWeaponDictionary();
                    Weapon w = new Weapon();
                    if (wDict.ContainsKey(choice))
                    {
                        w = wDict[choice];
                    }
                    else
                    {
                        break;
                    }
                    int dmg = w.damage;
                    if (dmg > 8)
                    {
                        cost = (dmg - 8) * 25;
                    }
                    else
                    {
                        cost = 50;
                    }
                }
                Item item = new Item(choice, qty, cost);
                items.Add(item);
                cnt++;
            }
            Dusty dusty = new Dusty(items, DateTimeOffset.Now);
            Serialize(dusty, "shop");
            // build out the reply
            sb.AppendLine($"New stock:");
            foreach (Item i in dusty.stock)
            {
                sb.AppendLine($"{i.GetItemQty()} of {i.GetItemName()}");
            }
            // set embed fields
            embed.Title = user.Username + " has forced a restock of Dusty's store.";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("restockinterval", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task RestockIntervalCommand(string hoursInput)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            var user = Context.User;
            // get user info from the Context
            ServerData sd = DeserializeServerData();
            TimeSpan oldInterval = new TimeSpan();
            if (sd.dustyRestockInterval == null)
            {
                oldInterval = new TimeSpan(3, 0, 0);
            }
            else
            {
                oldInterval = sd.dustyRestockInterval;
            }
            bool validInput = Int32.TryParse(hoursInput, out int hours);
            // build out the reply
            if (validInput)
            {
                sb.AppendLine($"Interval changed from " + oldInterval.Hours + " hours to " + hours + " hours.");
                sd.dustyRestockInterval = new TimeSpan(hours, 0, 0);
                Serialize(sd, "Server Data\\" + sd.serverId);
                // set embed fields
                embed.Title = user.Username + " has changed Dusty's restock interval.";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                sb.AppendLine($"Input was in the wrong format. Please only use numerical digits 0-9!");
                // set embed fields
                embed.Title = user.Username + " attempted to change Dusty's restock interval.";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("clearsettlements", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task ClearSettlemenstCommand(string userName)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            ulong id = 0;
            if (userName != null)
            {
                IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
                List<ulong> ids = i.ToList<ulong>();
                foreach (ulong x in ids)
                {
                    IGuildUser u = await Context.Guild.GetUserAsync(x);
                    id = x;
                }
            }
            else
            {
                id = user.Id;
            }
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<Settlement> sList = new List<Settlement>();
            if (sDict.ContainsKey(id))
            {
                sDict.Remove(id);
                sDict.Add(id, sList);
            }
            Serialize(sDict, "//Settlements//PlayerSettlementDictionary");
            // build out the reply
            sb.AppendLine($"Settlements cleared!");
            IUser usr = (IUser)Context.Client.GetUserAsync(id);
            // set embed fields
            embed.Title = "Clearing Settlements For " + usr.Username;
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("addsettlement", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task AddSettlementCommand(string target, string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            ulong id = 0;
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            List<ulong> ids = i.ToList<ulong>();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                id = x;
            }
            if (name2 != null)
            {
                settlementName += " ";
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                settlementName += name5;
            }
            // build an embed, because they're shinier
            char firstLet = char.ToUpper(settlementName[0]);
            settlementName = firstLet + settlementName.Substring(1);
            int index = 0;
            bool moreWords = true;
            while (moreWords)
            {
                int spaceIndex = settlementName.IndexOf(" ", index);
                if (spaceIndex != -1)
                {
                    char nextWord = char.ToUpper(settlementName[spaceIndex + 1]);
                    string previousString = settlementName.Substring(0, spaceIndex);
                    settlementName = previousString + " " + nextWord + settlementName.Substring(spaceIndex + 2);
                    index = spaceIndex + 2;
                }
                else
                {
                    moreWords = false;
                }
                if (settlementName.Contains("-"))
                {
                    int dashIndex = settlementName.IndexOf("-");
                    char e = char.ToUpper(settlementName[dashIndex + 1]);
                    string previousString = settlementName.Substring(0, dashIndex);
                    settlementName = previousString + "-" + e + settlementName.Substring(dashIndex + 2);
                }
            }
            List<Settlement> settlementDB = DeserializeSettlementDatabase();
            bool settlementIsValid = false;
            Settlement settlement = new Settlement();
            foreach (Settlement s in settlementDB)
            {
                if (s.name == settlementName)
                {
                    settlementIsValid = true;
                    settlement = s;
                }
            }
            if (settlementIsValid)
            {
                Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
                List<Settlement> sList = new List<Settlement>();
                if (sDict.ContainsKey(id))
                {
                    sList = sDict[id];
                }
                Settlement s = settlement;
                List<SettlementModule> modules = new List<SettlementModule>();
                SettlementModule modOne = new SettlementModule("null", 0, 0, 1);
                SettlementModule modTwo = new SettlementModule("null", 0, 0, 2);
                SettlementModule modThree = new SettlementModule("null", 0, 0, 3);
                SettlementModule modFour = new SettlementModule("null", 0, 0, 4);
                SettlementModule modFive = new SettlementModule("null", 0, 0, 5);
                SettlementModule modSix = new SettlementModule("null", 0, 0, 6);
                SettlementModule modSeven = new SettlementModule("null", 0, 0, 7);
                SettlementModule modEight = new SettlementModule("null", 0, 0, 8);
                SettlementModule modNine = new SettlementModule("null", 0, 0, 9);
                SettlementModule modTen = new SettlementModule("null", 0, 0, 10);
                switch (s.totalModules)
                {
                    case 1:
                        modules.Insert(0, modOne);
                        break;
                    case 2:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        break;
                    case 3:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        break;
                    case 4:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        break;
                    case 5:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        modules.Insert(4, modFive);
                        break;
                    case 6:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        modules.Insert(4, modFive);
                        modules.Insert(5, modSix);
                        break;
                    case 7:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        modules.Insert(4, modFive);
                        modules.Insert(5, modSix);
                        modules.Insert(6, modSeven);
                        break;
                    case 8:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        modules.Insert(4, modFive);
                        modules.Insert(5, modSix);
                        modules.Insert(6, modSeven);
                        modules.Insert(7, modEight);
                        break;
                    case 9:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        modules.Insert(4, modFive);
                        modules.Insert(5, modSix);
                        modules.Insert(6, modSeven);
                        modules.Insert(7, modEight);
                        modules.Insert(8, modNine);
                        break;
                    case 10:
                        modules.Insert(0, modOne);
                        modules.Insert(1, modTwo);
                        modules.Insert(2, modThree);
                        modules.Insert(3, modFour);
                        modules.Insert(4, modFive);
                        modules.Insert(5, modSix);
                        modules.Insert(6, modSeven);
                        modules.Insert(7, modEight);
                        modules.Insert(8, modNine);
                        modules.Insert(9, modTen);
                        break;
                }
                s.isIntact = true;
                sList.Add(s);
                IUser usr = (IUser)Context.Client.GetUserAsync(id);
                sb.AppendLine(usr.Username + ", your " + s.name + " settlement has been added.");
                sDict.Remove(id);
                sDict.Add(id, sList);
                Serialize(sDict, "\\Settlements\\PlayerSettlementDictionary");
                // build out the reply
                sb.AppendLine($"");

                // set embed fields
                embed.Title = "";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                sb.AppendLine(user.Username + ", your " + settlementName + " settlement was invalid. Please try again.");
                // set embed fields
                embed.Title = "Invalid Settlement Name";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("resetattackstates", RunMode = RunMode.Async)]
        [Alias("ras")]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task ClearSettlementAttacksCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            foreach (List<Settlement> ls in sDict.Values)
            {
                foreach (Settlement s in ls)
                {
                    if (s.isUnderAttack)
                    {
                        s.isUnderAttack = false;
                    }
                }
            }
            Serialize(sDict, "\\Settlements\\PlayerSettlementDictionary");
            // build out the reply
            sb.AppendLine($"Attack states reset.");

            // set embed fields
            embed.Title = "Resetting all settlements' attack states";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("addcustomweapon", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task AddCustomWeaponCommand(string category, string damageType, int damage, string tag1 = null, string tag2 = null, string tag3 = null, string tag4 = null, string tag5 = null, string nameWord1 = null, string nameWord2 = null, string nameWord3 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            string name = "";
            if (nameWord1 != null)
            {
                name += nameWord1;
            }
            if (nameWord2 != null)
            {
                name += " ";
                name += nameWord2;
            }
            if (nameWord3 != null)
            {
                name += " ";
                name += nameWord3;
            }
            if (damage <= 20)
            {
                // get user info from the Context
                Weapon weapon = new Weapon(name, category, damageType, damage, tag1, tag2, tag3, tag4, tag5);
                List<string> weaponsList = DeserializeWeaponsList();
                Dictionary<string, Weapon> weaponsDict = DeserializeWeaponDictionary();

                weaponsList.Add(name);
                weaponsDict.Add(name, weapon);

                Serialize(weaponsList, "weaponsList");
                Serialize(weaponsDict, "weaponDictionary");
                // build out the reply
                sb.AppendLine($"Custom weapon \"" + name + "\" added.");

                // set embed fields
                embed.Title = "Adding custom weapon...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                sb.AppendLine($"Custom weapon \"" + name + "\" could not be added. Please keep the damage at 20 or lower for balance purposes!");

                // set embed fields
                embed.Title = "Adding custom weapon...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("LevelUp", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task LevelupCommand(string username = "null")
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();
            var user = Context.User;
            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            ulong id = 0;

            // get user info from the Context
            if (username == "null")
            {
                id = user.Id;
            }
            else
            {
                IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
                List<ulong> ids = i.ToList<ulong>();
                foreach (ulong idx in ids)
                {
                    id = idx;
                }
            }

            Charsheet player = PlayerLoad(id);
            player.level++;
            SavePlayerData(id, player);

            // build out the reply
            sb.AppendLine($"Level added.");
            sb.AppendLine($"New player level is " + player.level);

            // set embed fields
            embed.Title = "Debug_LevelUp";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("AddItem", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task AddItemCommand(string player, string action, int qty, string itemWord1, string itemWord2 = null, string itemWord3 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong id = 0;
            List<ulong> ids = i.ToList<ulong>();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                string disc = u.Discriminator;
                string name = u.Username.ToString();
                string username = name + "#" + disc;
                id = x;
            }

            // get user info from the Context
            var user = Context.User;
            string item = itemWord1;
            if (itemWord2 != null)
            {
                item += " ";
                item += itemWord2;
            }
            if (itemWord3 != null)
            {
                item += " ";
                item += itemWord3;
            }
            item = item.ToLower();

            if (item == "food" || item == "water" || item == "meds" || item == "ammo")
            {
                char firstLet = char.ToUpper(item[0]);
                item = firstLet + item.Substring(1);
            }
            if (item.Contains("map"))
            {
                char firstLet = char.ToUpper(item[0]);
                item = firstLet + item.Substring(1);
                int index = 0;
                bool moreWords = true;
                while (moreWords)
                {
                    int spaceIndex = item.IndexOf(" ", index);
                    if (spaceIndex != -1)
                    {
                        char nextWord = char.ToUpper(item[spaceIndex + 1]);
                        string previousString = item.Substring(0, spaceIndex);
                        item = previousString + " " + nextWord + item.Substring(spaceIndex + 2);
                        index = spaceIndex + 2;
                    }
                    else
                    {
                        moreWords = false;
                    }
                }
                if (item.Contains("-"))
                {
                    int dashIndex = item.IndexOf("-");
                    char e = char.ToUpper(item[dashIndex + 1]);
                    string previousString = item.Substring(0, dashIndex);
                    item = previousString + "-" + e + item.Substring(dashIndex + 2);
                }
            }
            if (player == "null")
            {
                if (action == "add")
                {
                    InventoryAppend(user.Id, item, qty);
                }
                if (action == "subtract")
                {
                    qty = 0 - qty;
                    InventoryAppend(user.Id, item, qty);
                }
            }
            else
            {
                if (action == "add")
                {
                    InventoryAppend(id, item, qty);
                }
                if (action == "subtract")
                {
                    qty = 0 - qty;
                    InventoryAppend(id, item, qty);
                }
            }

            // build out the reply
            sb.AppendLine(qty + " of item " + item + " added.");

            // set embed fields
            embed.Title = "Adding item...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("PlayerEdit", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task PlayerEditCommand(string playerName, string stat, string action, int quantity = 0)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();
            var user = Context.User;
            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong id = 0;
            List<ulong> ids = i.ToList<ulong>();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                string disc = u.Discriminator;
                string name = u.Username.ToString();
                string username = name + "#" + disc;
                id = x;
            }
            Charsheet player = PlayerLoad(id);
            if (action == "subtract")
            {
                quantity = 0 - quantity;
            }
            if (action == "add" || action == "subtract")
            {
                switch (stat)
                {
                    case "strength":
                        player.strength += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "str":
                        player.strength += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "intelligence":
                        player.intelligence += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "int":
                        player.intelligence += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "charisma":
                        player.charisma += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "cha":
                        player.charisma += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "constitution":
                        player.constitution += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "con":
                        player.constitution += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "health":
                        player.health += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "hunger":
                        player.hunger += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "thirst":
                        player.thirst += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "infection":
                        player.infection += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "level":
                        player.level += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "exp":
                        player.exp += quantity;
                        int newLevel = LevelCalc(id);
                        if (newLevel != player.GetLevel())
                        {
                            player.level = newLevel;
                            player.skillPoints++;
                        }
                        SavePlayerData(id, player);
                        break;
                    case "biteseverity":
                        player.biteSeverity += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "scrap":
                        player.scrap += quantity;
                        SavePlayerData(id, player);
                        break;
                    case "cash":
                        player.cash += quantity;
                        SavePlayerData(id, player);
                        break;
                    default:
                        break;
                }
            }
            if (action == "set")
            {
                switch (stat)
                {
                    case "strength":
                        player.strength = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "str":
                        player.strength = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "intelligence":
                        player.intelligence = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "int":
                        player.intelligence = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "charisma":
                        player.charisma = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "cha":
                        player.charisma = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "constitution":
                        player.constitution = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "con":
                        player.constitution = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "health":
                        player.health = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "hunger":
                        player.hunger = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "thirst":
                        player.thirst = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "infection":
                        player.infection = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "level":
                        player.level = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "exp":
                        player.exp = quantity;
                        int newLevel = LevelCalc(id);
                        if (newLevel != player.GetLevel())
                        {
                            player.level = newLevel;
                            player.skillPoints++;
                        }
                        SavePlayerData(id, player);
                        break;
                    case "biteseverity":
                        player.biteSeverity = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "scrap":
                        player.scrap = quantity;
                        SavePlayerData(id, player);
                        break;
                    case "cash":
                        player.cash = quantity;
                        SavePlayerData(id, player);
                        break;
                    default:
                        break;
                }
            }
            // get user info from the Context

            // build out the reply
            sb.AppendLine(playerName + "'s " + stat + " stat has been adjusted.");

            // set embed fields
            embed.Title = "Editing Player...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("AddSkillPoints", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task AddSkillPointsCommand(int quantity = 1, string playerName = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            ulong id = 0;
            if (playerName != null)
            {
                IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
                List<ulong> ids = i.ToList<ulong>();
                foreach (ulong x in ids)
                {
                    IGuildUser u = await Context.Guild.GetUserAsync(x);
                    string disc = u.Discriminator;
                    string name = u.Username.ToString();
                    string username = name + "#" + disc;
                    id = x;
                }
            }
            else
            {
                id = user.Id;
            }
            Charsheet player = PlayerLoad(id);
            player.skillPoints += quantity;
            SavePlayerData(id, player);
            // build out the reply
            sb.AppendLine($"Successfully added " + quantity + " points to player " + playerName + ".");

            // set embed fields
            embed.Title = "Adding points...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("reset", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task Debug_ResetCommand(string username = "null")
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            var user = Context.User;
            ulong id = 0;

            // get user info from the Context
            if (username == "null")
            {
                username = user.ToString();
                id = user.Id;
            }
            else
            {
                IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
                List<ulong> ids = i.ToList<ulong>();
                foreach (ulong x in ids)
                {
                    IGuildUser u = await Context.Guild.GetUserAsync(x);
                    string disc = u.Discriminator;
                    string name = u.Username.ToString();
                    username = name + "#" + disc;
                    Console.WriteLine("Id to username: " + username);
                    id = x;
                }
            }
            Charsheet playerReset = PlayerLoad(id);
            Dictionary<ulong, Charsheet> playerDB = DeserializeCharDictionary();
            playerDB.Remove(id);
            playerReset = new Charsheet(username);
            playerDB.Add(id, playerReset);
            Serialize(playerDB, "playerDictionary");

            PlayerInventory inv = GetPlayerInventory(id);
            inv.items.Clear();
            Item food = new Item("Food", 5);
            Item water = new Item("Water", 5);
            Item meds = new Item("Meds", 5);
            Item ammo = new Item("Ammo", 5);
            Item fists = new Item("Fists", 1);
            inv.items.Add(food);
            inv.items.Add(water);
            inv.items.Add(meds);
            inv.items.Add(ammo);
            inv.items.Add(fists);
            Dictionary<ulong, PlayerInventory> players = DeserializePlayerInventoryDictionary();
            players.Remove(id);
            players.Add(id, inv);
            Serialize(players, "playerInventoryDictionary");

            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<Settlement> sList = new List<Settlement>();
            if (sDict.ContainsKey(user.Id))
            {
                sDict.Remove(user.Id);
            }
            sDict.Add(user.Id, sList);
            Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
            // build out the reply
            sb.AppendLine($"Player " + username + " reset.");

            // set embed fields
            embed.Title = "PLAYER RESET";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("resetmodules", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task ResetModulesCommand(string target, string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            if (name2 != null)
            {
                settlementName += " ";
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                settlementName += name5;
            }
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong targetId = 0;
            List<ulong> ids = i.ToList<ulong>();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                targetId = x;
            }
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<Settlement> sList = new List<Settlement>();
            Settlement settlement = new Settlement();
            bool exists = false;
            if (sDict.ContainsKey(targetId))
            {
                sList = sDict[targetId];
                sDict.Remove(targetId);
            }
            List<Settlement> toRemove = new List<Settlement>();
            foreach (Settlement s in sList)
            {
                if (s.name == settlementName)
                {
                    settlement = s;
                    if (settlement.isIntact)
                    {
                        exists = true;
                        toRemove.Add(s);
                    }
                }
            }
            foreach (Settlement x in toRemove)
            {
                sList.Remove(x);
            }
            SettlementModule modOne = new SettlementModule("null", 0, 0, 1);
            SettlementModule modTwo = new SettlementModule("null", 0, 0, 2);
            SettlementModule modThree = new SettlementModule("null", 0, 0, 3);
            SettlementModule modFour = new SettlementModule("null", 0, 0, 4);
            SettlementModule modFive = new SettlementModule("null", 0, 0, 5);
            SettlementModule modSix = new SettlementModule("null", 0, 0, 6);
            SettlementModule modSeven = new SettlementModule("null", 0, 0, 7);
            SettlementModule modEight = new SettlementModule("null", 0, 0, 8);
            SettlementModule modNine = new SettlementModule("null", 0, 0, 9);
            SettlementModule modTen = new SettlementModule("null", 0, 0, 10);
            List<SettlementModule> modules = new List<SettlementModule>();
            int size = settlement.totalModules;
            switch (size)
            {
                case 1:
                    modules.Add(modOne);
                    break;
                case 2:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    break;
                case 3:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    break;
                case 4:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    break;
                case 5:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    break;
                case 6:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    break;
                case 7:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    break;
                case 8:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    modules.Add(modEight);
                    break;
                case 9:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    modules.Add(modEight);
                    modules.Add(modNine);
                    break;
                case 10:
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    modules.Add(modEight);
                    modules.Add(modNine);
                    modules.Add(modTen);
                    break;
            }
            if (exists)
            {
                settlement.modules = modules;
            }
            sList.Add(settlement);
            sDict.Add(targetId, sList);
            Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
            // build out the reply
            sb.AppendLine($"Modules in settlement " + settlementName + " cleared.");
            IUser targetUser = await Context.Guild.GetUserAsync(targetId);
            // set embed fields
            embed.Title = "Clearing " + targetUser.Username + "'s modules.";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("setserverinfo", RunMode = RunMode.Async)]
        [Alias("ssi")]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task SetServerInfoCommand(string owner, string prefix, string hordeChannelInput = null, string name = null, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            ulong hordeChannel = 0;
            if (hordeChannelInput == null)
            {
                hordeChannel = Context.Channel.Id;
            }
            else
            {
                List<ulong> channels = Context.Message.MentionedChannelIds.ToList();
                foreach (ulong id in channels)
                {
                    hordeChannel = id;
                }

            }
            if (name2 != null)
            {
                name += " ";
                name += name2;
            }
            if (name3 != null)
            {
                name += " ";
                name += name3;
            }
            if (name4 != null)
            {
                name += " ";
                name += name4;
            }
            if (name5 != null)
            {
                name += " ";
                name += name5;
            }
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            IReadOnlyCollection<ulong> inv = Context.Message.MentionedUserIds;
            List<ulong> ids = inv.ToList();
            ulong ownerId = 0;
            foreach (ulong id in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(id);
                ownerId = u.Id;
            }
            ServerData sd = new ServerData(prefix, name, ownerId, hordeChannel);
            sd.serverId = Context.Guild.Id;
            if (!Directory.Exists("D:\\Zombot Files\\Servers"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Server Data\\"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Server Data\\");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Settlements\\"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Settlements\\");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Active Fights\\"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Active Fights\\");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Participants\\"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + sd.serverId + "\\Participants\\");
            }
            Serialize(sd, "Server Data\\" + sd.serverId);
            // build out the reply
            sb.AppendLine($"Server data set!");
            sb.AppendLine($"Server name: " + name);
            sb.AppendLine($"Server owner: " + owner);
            sb.AppendLine($"Server prefix: " + prefix);

            // set embed fields
            embed.Title = "Server Info Setter";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("fixsettlements", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task FixSettlementsCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<SettlementModule> newModules = new List<SettlementModule>();
            foreach (ulong id in sDict.Keys)
            {
                List<Settlement> sl = sDict[id];
                foreach (Settlement s in sl)
                {
                    s.owner = id;
                    foreach (SettlementModule sm in s.modules)
                    {
                        int index = s.modules.FindIndex(x => x.slot == sm.slot);
                        int trueSlot = sm.slot - 1;
                        if (index != trueSlot && trueSlot >= 0)
                        {
                            newModules.Insert(trueSlot, sm);
                        }
                    }
                    s.modules = newModules;
                }
            }
            Serialize(sDict, "\\Settlements\\PlayerSettlementDictionary");
            // build out the reply
            sb.AppendLine($"All misaligned module slots have been realigned, and owners properly assigned.");

            // set embed fields
            embed.Title = "Settlements repaired.";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("backup", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task BackupCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            // build out the reply

            sb.AppendLine($"Backup completed.");
            Backup(Context.Guild.Id);

            // set embed fields
            embed.Title = user.Username + " has started a backup of the server files.";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("userstats", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task UserStatsCommand(string target)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong targetId = 0;
            List<ulong> ids = i.ToList();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                targetId = x;
            }
            Charsheet player = PlayerLoad(targetId);
            if (player.GetName() != null)
            {
                // build out the reply
                sb.AppendLine($"Alliance: " + player.alliance);
                sb.AppendLine($"");
                sb.AppendLine($"Health: " + player.health);
                sb.AppendLine($"Hunger: " + player.hunger);
                sb.AppendLine($"Thirst: " + player.thirst);
                sb.AppendLine($"");
                sb.AppendLine($"Scrap: " + player.scrap);
                sb.AppendLine($"Cash: $" + player.cash);
                sb.AppendLine($"Influence: " + player.influence);
                sb.AppendLine($"");
                sb.AppendLine($"Strength: " + player.strength);
                sb.AppendLine($"Intelligence: " + player.intelligence);
                sb.AppendLine($"Charisma: " + player.charisma);
                sb.AppendLine($"Constitution: " + player.constitution);
                sb.AppendLine($"");
                sb.AppendLine($"Level: " + player.level);
                sb.AppendLine($"Experience: " + player.exp);

                // set embed fields
                embed.Title = player.playerName + "'s Character Sheet";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                sb.AppendLine($"The player you've specified either doesn't exist, or hasn't started playing the game yet!");
                embed.Title = "Error";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("resetspv", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task ResetSettlementPlayerVariablesCommand(string target)
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong targetId = 0;
            List<ulong> ids = i.ToList();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                targetId = x;
            }
            SettlementPlayerVariables spv = new SettlementPlayerVariables();
            Dictionary<ulong, SettlementPlayerVariables> spvDict = DeserializeSettlementVariableDictionary();
            if (spvDict.ContainsKey(targetId))
            {
                spvDict[targetId] = spv;
            }
            Serialize(spvDict, "Settlements\\SettlementPlayerVariables");
            var targetUser = await Context.Client.GetUserAsync(targetId);
            sb.AppendLine($"SPV for " + targetUser.Username + " reset.");
            embed.Title = "Clearing SPV";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("resetplayervariables", RunMode = RunMode.Async)]
        [RequireRole("Botman", Group = "Permission")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task ResetPlayerVariablesCommand(string target)
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong targetId = 0;
            List<ulong> ids = i.ToList();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                targetId = x;
            }
            Dictionary<ulong, PlayerVariable> pvDict = DeserializePlayerVariableDictionary();
            if (pvDict.ContainsKey(targetId))
            {
                pvDict[targetId].isDefending = false;
                pvDict[targetId].isForaging = false;
            }
            Serialize(pvDict, "PlayerVariables");
            var targetUser = await Context.Client.GetUserAsync(targetId);
            sb.AppendLine($"PV for " + targetUser.Username + " reset.");
            embed.Title = "Clearing PV";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("stats", RunMode = RunMode.Async)]
        public async Task StatsCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            Charsheet player = PlayerLoad(user.Id);

            // build out the reply
            sb.AppendLine($"Alliance: " + player.alliance);
            sb.AppendLine($"");
            sb.AppendLine($"Health: " + player.health);
            sb.AppendLine($"Hunger: " + player.hunger);
            sb.AppendLine($"Thirst: " + player.thirst);
            sb.AppendLine($"");
            sb.AppendLine($"Scrap: " + player.scrap);
            sb.AppendLine($"Cash: $" + player.cash);
            sb.AppendLine($"Influence: " + player.influence);
            sb.AppendLine($"");
            sb.AppendLine($"Strength: " + player.strength);
            sb.AppendLine($"Intelligence: " + player.intelligence);
            sb.AppendLine($"Charisma: " + player.charisma);
            sb.AppendLine($"Constitution: " + player.constitution);
            sb.AppendLine($"");
            sb.AppendLine($"Level: " + player.level);
            sb.AppendLine($"Experience: " + player.exp);

            // set embed fields
            embed.Title = player.playerName + "'s Character Sheet";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("forage", RunMode = RunMode.Async)]
        [RequireCooldown()]
        [Alias("for", "f")]
        public async Task ForageCommand()
        {
            StringBuilder sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;

            await BeginMethod(user);

            Participant party = DeserializeAllParticipants(user.Id);
            bool isParticipant = true;
            if (party.GetForager() == 0)
            {
                isParticipant = false;
            }

            Charsheet y = PlayerLoad(user.Id);
            Dictionary<ulong, PlayerVariable> playVarDict = DeserializePlayerVariableDictionary();
            PlayerVariable pv = new PlayerVariable();
            if (playVarDict.ContainsKey(user.Id))
            {
                pv = playVarDict[user.Id];
            }
            bool isForaging = pv.GetIsForaging();
            if (isParticipant || isForaging)
            {
                sb.AppendLine(user.ToString() + ", you're currently participating in another forage; end that forage before you start this one!");
                // set embed fields
                embed.Title = "Foraging...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                pv.StartForaging();
                if (playVarDict.ContainsKey(user.Id))
                {
                    playVarDict.Remove(user.Id);
                }
                playVarDict.Add(user.Id, pv);
                Serialize(playVarDict, "PlayerVariables");
                string fatigue = "";
                StringBuilder playersGoing = new StringBuilder();
                ISocketMessageChannel channel = (ISocketMessageChannel)Context.Channel;
                string m = "";
                if (y.GetAlliance() != "None")
                {
                    m = user.Username + " wants to go forage. Who will join them? Note: Must be in the " + y.GetAlliance() + " alliance to join. [Leaving in: 2 minutes 0 seconds]";
                }
                else
                {
                    m = user.Username + " wants to go forage. [Leaving in: 2 minutes 0 seconds]";
                }
                var message = await channel.SendMessageAsync(m);
                var check = new Emoji("\u2705");
                await message.AddReactionAsync(check);
                List<IUser> reactors = await ForageCountdown(message, user);
                List<Charsheet> allParticipants = new List<Charsheet>();
                List<Participant> participants = new List<Participant>();
                foreach (IUser react in reactors)
                {
                    Charsheet re = PlayerLoad(react.Id);
                    Participant participant = new Participant(re.GetName(), react.Id, user.Id);
                    participants.Add(participant);
                    allParticipants.Add(re);
                }
                Participant f = new Participant(user.ToString(), user.Id, user.Id);
                participants.Add(f);
                Serialize(participants, "\\Participants\\" + user.Id + "_forage");
                Charsheet player = PlayerLoad(user.Id);
                StringBuilder zb = new StringBuilder();
                string loot = "";
                string lootChosen = ForageLootGen();
                //string lootChosen = "zombies";
                switch (lootChosen)
                {
                    case "zombies":
                        List<Participant> ps = new List<Participant>();
                        foreach (IUser x in reactors)
                        {
                            Participant pz = new Participant(x.Username, x.Id, user.Id);
                            ps.Add(pz);
                        }
                        loot = "You were trying to gather supplies in a run-down old gas station, but you dropped a glass bottle!\n\n" +
                            $"The sound of it breaking is sure to get some attention...";
                        Random rand = new Random();
                        List<string> helpers = new List<string>();
                        List<Participant> px = DeserializeParticipants(user.Id);
                        foreach (Participant py in px)
                        {
                            helpers.Add(py.GetName());
                        }
                        int maxZombies = 0;
                        foreach (Participant py in px)
                        {
                            Charsheet hSheet = PlayerLoad(py.id);
                            if (hSheet.GetLevel() <= 10)
                            {
                                maxZombies += 4;
                            }
                            else
                            {
                                int math = hSheet.GetLevel() % 10;
                                if (math != 0)
                                {
                                    double mathDouble = hSheet.GetLevel() / 10;
                                    math = Convert.ToInt32(Math.Floor(mathDouble));                                   
                                }
                                else
                                {
                                    math = hSheet.GetLevel() / 10;
                                }
                                int zombiesToAdd = 4 + math;
                                if (zombiesToAdd > 14)
                                {
                                    zombiesToAdd = 15;
                                }
                                maxZombies += zombiesToAdd;
                            }
                        }
                        int combatantsRoll = rand.Next(1, 100);
                        int combatants = 0;
                        double percent = combatantsRoll / 100.0;
                        if (combatantsRoll <= 75)
                        {
                            int max = Convert.ToInt32(Math.Round(percent * maxZombies));
                            if (max == 1)
                            {
                                combatants = 1;
                            }
                            else
                            {
                                combatants = rand.Next(1, max);
                            }
                        }
                        else if (combatantsRoll > 75)
                        {
                            if (maxZombies == 1)
                            {
                                combatants = 1;
                            }
                            else
                            {
                                combatants = rand.Next(1, maxZombies);
                            }
                        }
                        await channel.SendMessageAsync($"{user.Mention} has begun a fight!", false, null, null);
                        zb.Append(FightBuilder(user.Id, combatants));
                        break;
                    case "scraps":
                        loot = ScrapLoot(player, allParticipants);
                        break;
                    case "cash":
                        loot = CashLoot(player, allParticipants);
                        break;
                    case "food":
                        loot = FoodLoot(player, allParticipants);
                        break;
                    case "water":
                        loot = WaterLoot(player, allParticipants);
                        break;
                    case "medicine":
                        loot = MedsLoot(player, allParticipants);
                        break;
                    case "weapons":
                        loot = WeaponsLoot(player);
                        break;
                    case "ammo":
                        loot = AmmoLoot(player, allParticipants);
                        break;
                    case "supply cache":
                        loot = SupplyCacheLoot(player, allParticipants);
                        break;
                    case "settlement":
                        loot = SettlementLoot(player);
                        break;
                }
                if (reactors.Count() >= 1)
                {
                    if (reactors.Count >= 2)
                    {
                        int rCount = reactors.Count();
                        foreach (IUser r in reactors)
                        {
                            string reactorName = r.Username.ToString();
                            ulong reactorid = r.Id;
                            fatigue += Fatigue(reactorName, reactorid);
                            Charsheet reactorSheet = PlayerLoad(reactorid);
                            int biteSeverity = reactorSheet.GetBiteSeverity();
                            if (biteSeverity > 0)
                            {
                                switch (biteSeverity)
                                {
                                    case 1:
                                        sb.AppendLine(reactorSheet.GetName() + ", your bite is red and inflamed, but not too infected. You've got time to fix the injury, for now.");
                                        reactorSheet.infection += 5;
                                        break;
                                    case 2:
                                        sb.AppendLine(reactorSheet.GetName() + ", your bite is pretty deep, and looking greenish in some places. You'd better get it fixed up quickly.");
                                        reactorSheet.infection += 15;
                                        break;
                                    case 3:
                                        sb.AppendLine(reactorSheet.GetName() + ", the veins around your bite are turning green, and the skin is black all around the wound. Get it fixed up as soon as possible.");
                                        reactorSheet.infection += 25;
                                        break;
                                }
                            }
                            SavePlayerData(reactorid, reactorSheet);
                            int index = playersGoing.Length - 1;
                            if (index < 0)
                            {
                                index = 0;
                            }
                            if (rCount >= 2)
                            {
                                playersGoing.Append($"" + reactorName + ", ");
                            }
                            else if (rCount == 1)
                            {
                                playersGoing.Append($"" + reactorName + " ");
                            }
                            playersGoing.Append("and " + user.Username + " have gone foraging.");
                        }                           
                    }
                    foreach (IUser r in reactors)
                    {
                        string rName = r.Username.ToString();
                        ulong rID = r.Id;
                        fatigue += Fatigue(rName, rID);
                        Charsheet rSheet = PlayerLoad(rID);
                        int bSev = rSheet.GetBiteSeverity();
                        if (bSev > 0)
                        {
                            switch (bSev)
                            {
                                case 1:
                                    sb.AppendLine(rSheet.GetName() + ", your bite is red and inflamed, but not too infected. You've got time to fix the injury, for now.");
                                    rSheet.infection += 5;
                                    break;
                                case 2:
                                    sb.AppendLine(rSheet.GetName() + ", your bite is pretty deep, and looking greenish in some places. You'd better get it fixed up quickly.");
                                    rSheet.infection += 15;
                                    break;
                                case 3:
                                    sb.AppendLine(rSheet.GetName() + ", the veins around your bite are turning green, and the skin is black all around the wound. Get it fixed up as soon as possible.");
                                    rSheet.infection += 25;
                                    break;
                            }
                        }
                        SavePlayerData(rID, rSheet);
                        int ind = playersGoing.Length - 1;
                        if (ind < 0)
                        {
                            ind = 0;
                        }
                        playersGoing.Append($"" + rName + " and " + user.Username + " have gone foraging.");
                    }
                }
                else
                {
                    playersGoing.Append($"" + user.Username + " has gone foraging.");
                }
                ulong id = user.Id;
                fatigue += Fatigue(user.ToString(), id);
                Charsheet inf = PlayerLoad(id);
                int biteSev = inf.GetBiteSeverity();
                if (biteSev > 0)
                {
                    switch (biteSev)
                    {
                        case 1:
                            sb.AppendLine(inf.GetName() + ", your bite is red and inflamed, but not too infected. You've got time to fix the injury, for now.");
                            inf.infection += 5;
                            break;
                        case 2:
                            sb.AppendLine(inf.GetName() + ", your bite is pretty deep, and looking greenish in some places. You'd better get it fixed up quickly.");
                            inf.infection += 15;
                            break;
                        case 3:
                            sb.AppendLine(inf.GetName() + ", the veins around your bite are turning green, and the skin is black all around the wound. Get it fixed up as soon as possible.");
                            inf.infection += 25;
                            break;
                    }
                }
                SavePlayerData(id, inf);
                // build out the reply
                sb.AppendLine($"");
                sb.AppendLine($"" + playersGoing.ToString());
                sb.AppendLine($"");
                sb.AppendLine($"" + loot);
                sb.AppendLine($"");
                sb.AppendLine($"" + zb);
                sb.AppendLine($"");
                sb.AppendLine($"" + fatigue);
                if (lootChosen != "zombies")
                {
                    player = PlayerLoad(user.Id);
                    Random rand = new Random();
                    int upperThreshhold = 15;
                    if (player.level <= 10)
                    {
                        upperThreshhold = 15;
                    }
                    else if (player.level <= 30 && player.level > 10)
                    {
                        upperThreshhold = 12;
                    }
                    else if (player.level <= 50 && player.level > 30)
                    {
                        upperThreshhold = 10;
                    }
                    else if (player.level <= 70 && player.level > 50)
                    {
                        upperThreshhold = 8;
                    }
                    else if (player.level <= 90 && player.level > 70)
                    {
                        upperThreshhold = 6;
                    }
                    else if (player.level <= 100 && player.level > 100)
                    {
                        upperThreshhold = 4;
                    }
                    int randNum = rand.Next(3, upperThreshhold);
                    int xpToNextLevel = XpToLevel(player);
                    int xpToCurrentLevel = Convert.ToInt32(Math.Round(100 * Math.Pow(player.level, 1.5)));
                    int xpToLevel = xpToNextLevel - xpToCurrentLevel;
                    decimal xpPercent = randNum / 100m;
                    decimal xpToDecimal = xpPercent * xpToLevel;
                    int xpToAdd = Convert.ToInt32(Math.Round(xpToDecimal, 0));
                    Console.WriteLine("Old XP: " + player.exp);
                    player.exp += xpToAdd;
                    Console.WriteLine("New XP: " + player.exp);
                    SavePlayerData(id, player);
                    bool hasLeveledUp = false;
                    int newLevel = LevelCalc(user.Id);
                    Console.WriteLine("New level: " + newLevel);
                    if (player.level != newLevel)
                    // below this, set hasLeveledUp to true. lower, if it's true, then print a username has leveled up message.
                    {
                        hasLeveledUp = true;
                    }
                    if (hasLeveledUp == true)
                    {
                        sb.AppendLine($"" + user.Username + " has leveled up!");
                        player.level = newLevel;
                        player.skillPoints++;
                        SavePlayerData(id, player);
                    }

                    foreach (IUser r in reactors)
                    {
                        string rUser = r.ToString();
                        Charsheet reactorPlayer = PlayerLoad(r.Id);
                        xpToLevel = XpToLevel(reactorPlayer);
                        xpPercent = randNum / 100m;
                        xpToDecimal = xpPercent * xpToLevel;
                        xpToAdd = Convert.ToInt32(Math.Round(xpToDecimal, 0));

                        Console.WriteLine("Old XP: " + player.exp);
                        reactorPlayer.exp += xpToAdd;
                        Console.WriteLine("New XP: " + player.exp);
                        ulong rID = r.Id;
                        SavePlayerData(rID, reactorPlayer);
                        hasLeveledUp = false;
                        newLevel = LevelCalc(r.Id);
                        Console.WriteLine(newLevel);
                        if (reactorPlayer.level != newLevel)
                        {
                            hasLeveledUp = true;
                        }
                        if (hasLeveledUp == true)
                        {
                            reactorPlayer.level = newLevel;
                            reactorPlayer.skillPoints++;
                            SavePlayerData(rID, reactorPlayer);
                            sb.AppendLine($"" + rUser + " has leveled up!");
                        }
                        Dictionary<ulong, PlayerVariable> pz = DeserializePlayerVariableDictionary();
                        PlayerVariable rpv = new PlayerVariable();
                        if (pz.ContainsKey(r.Id))
                        {
                            rpv = pz[r.Id];
                            pz.Remove(r.Id);
                            rpv.isForaging = false;
                        }
                        else
                        {
                            rpv = new PlayerVariable(r.Username, false);
                        }
                        pz.Add(r.Id, rpv);
                        Serialize(pz, "PlayerVariables");
                    }
                }
                // set embed fields
                embed.Title = "Foraging...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                IUserMessage forageMessage = await ReplyAsync(null, false, embed.Build());
                if (lootChosen == "zombies")
                {
                    await FightProgress(user.Id, forageMessage, zb, false);
                }
                pv.isForaging = false;
                Dictionary<ulong, PlayerVariable> pvy = DeserializePlayerVariableDictionary();
                if (pvy.ContainsKey(user.Id))
                {
                    pvy.Remove(user.Id);
                }
                pvy.Add(user.Id, pv);
                Serialize(pvy, "PlayerVariables");
            }
            pv.isForaging = false;
            Dictionary<ulong, PlayerVariable> playerVariables = DeserializePlayerVariableDictionary();
            if (playerVariables.ContainsKey(user.Id))
            {
                playerVariables.Remove(user.Id);
            }
            playerVariables.Add(user.Id, pv);
            Serialize(playerVariables, "PlayerVariables");
            ulong serverId = Context.Guild.Id;
            File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\" + user.Id + "_forage.xml");
        }

        [Command("inventory", RunMode = RunMode.Async)]
        [Alias("inv", "i")]
        public async Task InventoryCommand()
        {
            // get user info from the Context
            var user = Context.User;
            string username = user.ToString();
            await BeginMethod(user);
            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            StringBuilder sb = new StringBuilder();
            string header = $"" + username + "'s Inventory";
            PlayerInventory inv = GetPlayerInventory(user.Id);
            List<Item> playerItems = inv.items.ToList();
            List<string> nonConsumables = new List<string>();
            sb.AppendLine($"Item Name: Item Qty");
            int charCount = 25;
            string dashes = "";
            string d = "-";
            int cnt = 0;
            while (charCount > 0)
            {
                dashes = dashes.Insert(cnt, d);
                cnt++;
                charCount--;
            }
            sb.AppendLine($"" + dashes);
            foreach (Item i in playerItems)
            {
                if (i.GetItemQty() <= 0)
                {
                    inv.items.Remove(i);
                }
            }
            Dictionary<ulong, PlayerInventory> piDict = DeserializePlayerInventoryDictionary();
            if (piDict.ContainsKey(user.Id))
            {
                piDict.Remove(user.Id);
            }
            piDict.Add(user.Id, inv);
            Serialize(piDict, "playerInventoryDictionary");
            playerItems = inv.items.ToList();
            foreach (Item item in playerItems)
            {
                if (item.GetItemName() == "Food")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else if (item.GetItemName() == "Water")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else if (item.GetItemName() == "Meds")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else if (item.GetItemName() == "Ammo")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else
                {
                    nonConsumables.Add(item.GetItemName());
                }
            }
            nonConsumables.Sort();
            List<Item> sortedNonConsumables = new List<Item>();
            foreach (string name in nonConsumables)
            {
                foreach (Item item in playerItems)
                {
                    if (item.GetItemName() == name)
                    {
                        sortedNonConsumables.Add(item);
                    }
                }
            }
            foreach (Item nC in sortedNonConsumables)
            {
                string str = nC.GetItemName();
                char firstLet = char.ToUpper(str[0]);
                str = firstLet + str.Substring(1);
                sb.AppendLine($"" + str + ": " + nC.GetItemQty());
            }
            sb.AppendLine($"" + dashes);
            // set embed fields
            embed.Title = header;
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("heal", RunMode = RunMode.Async)]
        [Alias("he", "bandage")]
        public async Task HealCommand(int quantity = 1)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string txt = "";
            bool hasConsumable = false;
            PlayerInventory inv = GetPlayerInventory(user.Id);
            int points = 5 * quantity;
            int qty = GetItemQtyFromInv(inv, "Meds");
            if (qty >= quantity)
            {
                hasConsumable = true;
            }
            if (hasConsumable == true)
            {
                // build out the reply
                Random rand = new Random();
                int randNum = rand.Next(1, 5);

                switch (randNum)
                {
                    case 1:
                        txt = $"You grab a bottle of painkillers out of your bag and pop out a couple of the pills.\n\n" +
                            $"You swallow the pills with a mouthfull of water.\n\n" +
                            $"[+" + points + " Health]";
                        break;
                    case 2:
                        txt = $"You reach into your bag and pull out a large bandage. You peel the backing off of the adhesive.\n\n" +
                            $"You wince at the pain when you place the bandage against the wound, but the bleeding is under control now.\n\n" +
                            $"[+" + points + " Health]";
                        break;
                    case 3:
                        txt = $"You take a few deep breaths to prepare yourself as you take the suture kit out of your bag.\n\n" +
                            $"You force yourself to concentrate as you stitch up the wound on your arm. It doesn't look infected, so you lucked out this time.\n\n" +
                            $"[+" + points + " Health]";
                        break;
                    case 4:
                        txt = $"You've been feeling a deep ache in your leg all day, so you decide to sit and check it out.\n\n" +
                            $"When you roll up your pantleg, you see that the gash on your leg is at least partially infected.\n\n" +
                            $"You open your pack, take out a bottle of antibiotics, and take your first dose. Hopefully, that will clear it up.\n\n" +
                            $"[+" + points + " Health]";
                        break;
                    case 5:
                        txt = $"You wince as you open your pack, your obviously-broken finger sending sharp pains up your arm when you use it.\n\n" +
                            $"You pull a small splint kit out of your bag, and clumsily tie the splint inside to your left ring finger.\n\n" +
                            $"It'll ache for a while, but it should set correctly.\n\n" +
                            $"[+" + points + " Health]";
                        break;
                }
            }
            else
            { 
                txt = $"You rummage through your pack, but are dismayed to find that you don't enough any meds for that.";
                quantity = 0;
            }

            sb.AppendLine($"" + txt);
            int subtract = 0 - quantity;
            InventoryAppend(user.Id, "Meds", subtract);
            Charsheet player = PlayerLoad(user.Id);
            player.health += points;
            if (player.health >= 100)
            {
                player.health = 100;
            }
            SavePlayerData(user.Id, player);
            // set embed fields
            embed.Title = "Healing " + user.Username + "...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("eat", RunMode = RunMode.Async)]
        [Alias("e", "feed")]
        public async Task EatCommand(int quantity = 1)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string txt = "";
            bool hasConsumable = false;
            PlayerInventory inv = GetPlayerInventory(user.Id);
            int points = quantity * 5;
            int qty = GetItemQtyFromInv(inv, "Food");
            if (qty >= quantity)
            {
                hasConsumable = true;
            }
            // build out the reply
            if (hasConsumable == true)
            {
                Random rand = new Random();
                int randNum = rand.Next(1, 5);

                switch (randNum)
                {
                    case 1:
                        txt = $"You take a rest and pull a granola bar out of your pocket.\n\n" +
                            $"You unwrap it and take a big bite, immediately feeling a bit better.\n\n" +
                            $"[+" + points + " Hunger]";
                        break;
                    case 2:
                        txt = $"You rummage through your pack and find an unopened bag of chips near the bottom.\n\n" +
                            $"You open the bag and crunch into the first chip, savoring the salty, potatoe-y flavor.\n\n" +
                            $"[+" + points + " Hunger]";
                        break;
                    case 3:
                        txt = $"You look around to make sure no one is watching you as you pull your secret treasure out of your pack.\n\n" +
                            $"You carefully unwrap one of the very few chocolate bars you've ever found while foraging, and take a bite.\n\n" +
                            $"The taste is phenomenal, and you savor every bit of it.\n\n" +
                            $"[+" + points + " Hunger]";
                        break;
                    case 4:
                        txt = $"Your stomach rumbles to remind you that eating is a necessity, and you take a seat to appease it.\n\n" +
                            $"You pull out your lunch, a standard military-grade MRE, and reluctantly dig in. God, these things are nasty.\n\n" +
                            $"[+" + points + " Hunger]";
                        break;
                    case 5:
                        txt = $"You're relaxing in your bunk, when your most recent foraging haul catches your attention.\n\n" +
                            $"You grab the bag of candy you found on the floor at Walmart and tuck in.\n\n" +
                            $"[+" + points + " Hunger]";
                        break;
                }
            }
            else
            {
                txt = $"You rummage through your pack, but your stomach grumbles in complaint when you find that you don't have enough food for that.";
                quantity = 0;
            }
            int subtract = 0 - quantity;
            InventoryAppend(user.Id, "Food", subtract);
            Charsheet player = PlayerLoad(user.Id);
            player.hunger += points;
            if (player.hunger >= 100)
            {
                player.hunger = 100;
            }
            SavePlayerData(user.Id, player);
            sb.AppendLine($"" + txt);

            // set embed fields
            embed.Title = "Feeding " + user.Username + "...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("drink", RunMode = RunMode.Async)]
        [Alias("d", "rehydrate")]
        public async Task DrinkCommand(int quantity = 1)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string txt = "";
            bool hasConsumable = false;
            PlayerInventory inv = GetPlayerInventory(user.Id);
            int points = quantity * 5;
            int qty = GetItemQtyFromInv(inv, "Water");
            if (qty >= quantity)
            {
                hasConsumable = true;
            }
            if (hasConsumable == true)
            {
                Random rand = new Random();
                int randNum = rand.Next(1, 5);
                switch (randNum)
                {
                    case 1:
                        txt = $"You take a rest and pull a bottle of water from your bag.\n\n" +
                            $"You unscrew the cap and take a big sip.\n\n" +
                            $"[+" + points + " Thirst]";
                        break;
                    case 2:
                        txt = $"You pull an apple juice carton from your bag, and poke the straw through the top.\n\n" +
                            $"You sip the juice down, reminiscing about when these things were easy to find.\n\n" +
                            $"[+" + points + " Thirst]";
                        break;
                    case 3:
                        txt = $"You pull a water filter out of your bag, and affix it to the water collector in front of you.\n\n" +
                            $"You turn the valve, and let the water flow into your bottle. You shut the valve and take a big gulp, before putting the filter back in your bag.\n\n" +
                            $"[+" + points + " Thirst]";
                        break;
                    case 4:
                        txt = $"You take a small break from night watch and pull a soda bottle out of your bag..\n\n" +
                            $"You open the bottle with a loud 'tss' sound, and take a sip.\n\n" +
                            $"[+" + points + " Thirst]";
                        break;
                    case 5:
                        txt = $"You find a house with working plumbing, thanks the previous owner being a doomsday prepper and having his own water pump.\n\n" +
                            $"You attach a filter to the sink and fill up your water bottle, drinking almost the whole thing down before refilling it.\n\n" +
                            $"You take your filter back off of the sink, and put it and your bottle into your bag.\n\n" +
                            $"[+" + points + " Thirst]";
                        break;
                }
            }
            else
            {
                txt = $"You dig through your pack, but are terrified to find that you don't have enough water for that.";
                quantity = 0;
            }
            // build out the reply
            sb.AppendLine($"" + txt);
            int subtract = 0 - quantity;
            InventoryAppend(user.Id, "Water", subtract);
            Charsheet player = PlayerLoad(user.Id);
            player.thirst += points;
            if (player.thirst >= 100)
            {
                player.thirst = 100;
            }
            SavePlayerData(user.Id, player);
            // set embed fields
            embed.Title = "Rehydrating  " + user.Username + "...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("disinfect", RunMode = RunMode.Async)]
        [Alias("dis", "disinf")]
        public async Task DisinfectCommand(int quantity = 1)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string txt = "";
            bool hasConsumable = false;
            PlayerInventory inv = GetPlayerInventory(user.Id);
            int points = quantity * 5;
            int qty = GetItemQtyFromInv(inv, "Meds");
            if (qty >= quantity)
            {
                hasConsumable = true;
            }
            if (hasConsumable == true)
            {
                // build out the reply
                Random rand = new Random();
                int randNum = rand.Next(1, 5);
                switch (randNum)
                {
                    case 1:
                        txt = $"You grab a bottle of antibiotics out of your bag and pop out a couple of the pills.\n\n" +
                            $"You swallow the pills with a mouthfull of water.\n\n" +
                            $"[Infection Lowered]";
                        break;
                    case 2:
                        txt = $"You reach into your bag and pull out a large bandage. You peel the backing off of the adhesive.\n\n" +
                            $"You wince at the pain when you place the bandage against your infected bite, but it should stay clean now.\n\n" +
                            $"[Infection Lowered]";
                        break;
                    case 3:
                        txt = $"You take a few deep breaths to prepare yourself as you take the suture kit out of your bag.\n\n" +
                            $"You force yourself to concentrate as you stitch up the wound on your arm. Hopefully, this will keep it from getting more infected.\n\n" +
                            $"[Infection Lowered]";
                        break;
                    case 4:
                        txt = $"You've been feeling a deep ache in your leg, where the bite is, all day, so you decide to sit and check it out.\n\n" +
                            $"When you roll up your pantleg, you see that the bite is very infected.\n\n" +
                            $"You open your pack, take out a bottle of prescription-strength antibiotics, and take your first dose.\n\n" +
                            $"[Infection Lowered]";
                        break;
                    case 5:
                        txt = $"You move your arm a certain way and feel a stabbing pain in your shoulder.\n\n" + 
                            $"When you roll your sleeve up to investigate, you find that the bite that you received earlier has a small object inside of it.\n\n" +
                            $"You sterilize the wound with some rubbing alcohol from your bag, and then use a pair of pliars to dig out a tooth from the zombie that bit you. Gross.\n\n" + 
                            $"[Infection Lowered]";
                        break;
                }
            }
            else
            { 
                txt = $"You rummage through your pack, but are dismayed to find that you don't have any meds.";
                quantity = 0;
            }

            sb.AppendLine($"" + txt);
            int subtract = 0 - quantity;
            InventoryAppend(user.Id, "Meds", subtract);
            Charsheet player = PlayerLoad(user.Id);
            player.infection -= points;
            if (player.infection < 0)
            {
                player.infection = 0;
            }
            if (player.GetInfection() > 0)
            {
                sb.AppendLine($"You feel your symptoms recede, but they don't go away. You're still infected, and you know it.");
            }
            else if (player.GetInfection() == 0)
            {
                sb.AppendLine($"As you sit back after treating your bite, you realize that your symptoms seem to be receding completely. You shout out with joy as you realize that you've survived infection... For now.");
                player.biteSeverity = 0;
            }
            SavePlayerData(user.Id, player);
            // set embed fields
            embed.Title = "Disinfecting " + user.Username + "...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("checkinfection", RunMode = RunMode.Async)]
        [Alias("checkinf", "cinf")]
        public async Task CheckInfectionCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            Charsheet player = PlayerLoad(user.Id);
            int infection = player.GetInfection();
            string txt = "";

            if (infection == 0)
            {
                txt = "You check yourself for symptoms, but are relieved to find that, as far as you can tell, you're infection-free.";
            }

            else if (infection <= 10)
            {
                txt = "You seem to be doing okay, at least for now. Your infection doesn't look to have progressed very far.";
            }

            else if (infection <= 20)
            {
                txt = "The site around the wound has grown pinkish, and feels a bit flushed, but that's your only new symptom. It's getting worse, but it's not too bad yet.";
            }

            else if (infection <= 30)
            {
                txt = "You're feeling pretty flushed. You check your temperature, and see that it's 99.5 degress Fahrenheit, 37.5 degrees Celsius. That's not a *great* sign...";
            }

            else if (infection <= 40)
            {
                txt = "You check the wound, and find that some of the ragged bits of torn skin are starting to turn black at the tips.";
            }

            else if (infection <= 50)
            {
                txt = "The site of your bite is itchy and, occasionally, burning. You try your best to ignore it, but it's hard to not think about the fact that it's getting worse.";
            }

            else if (infection <= 60)
            {
                txt = "You gingerly press on the flesh around the wound, and nearly cry out in pain. It wasn't this sensitive before, or this inflamed.";
            }

            else if (infection <= 70)
            {
                txt = "You're constantly wiping your mouth. You just can't seem to stop salivating.";
            }

            else if (infection <= 80)
            {
                txt = "You look in a mirror and find that your eyes are bloodshot, and your pupils are practically pinpoints. This is very bad. You need to get this fixed, now.";
            }

            else if (infection <= 90)
            {
                txt = "You're walking along a road when you collapse to the ground. You try to get up, but you can't. Your vision fades to black... and then, you can see again. You don't have much time left.";
            }

            string biteTxt = null;
            if (player.GetBiteSeverity() == 0)
            {
                biteTxt = "You check yourself for bites, but don't find any.";
            }
            else if (player.GetBiteSeverity() == 1)
            {
                biteTxt = "You have a bite, but it's not too deep. The infection will spread slowly.";
            }
            else if (player.GetBiteSeverity() == 2)
            {
                biteTxt = "You have a bite, and it's worrying. It's not the deepest wound in the world, but it'll spread at a decent pace.";
            }
            else if (player.GetBiteSeverity() == 3)
            {
                biteTxt = "You have a bite, but it's bad. You think you see a bit of muscle in the wound. The infection will spread very quickly.";
            }

            // build out the reply
            sb.AppendLine(txt);
            sb.AppendLine();
            sb.AppendLine(biteTxt);

            // set embed fields
            embed.Title = "Checking " + user.Username + "'s infection...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("alliance create", RunMode = RunMode.Async)]
        [Alias("a create", "acreate", "alliance c")]
        public async Task AllianceCreateCommand(string alliance, string a2 = "", string a3 = "", string a4 = "", string a5 = "")
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();
            if (a2 != "")
            {
                alliance += " ";
                alliance += a2;
            }
            if (a3 != "")
            {
                alliance += " ";
                alliance += a3;
            }
            if (a4 != "")
            {
                alliance += " ";
                alliance += a4;
            }
            if (a5 != "")
            {
                alliance += " ";
                alliance += a5;
            }
            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            string allianceName = alliance;
            var user = Context.User;
            await BeginMethod(user);

            if (alliance != "None")
            {

                Charsheet player = PlayerLoad(user.Id);
                if (player.alliance != "None")
                {
                    sb.AppendLine($"You're already part of an alliance. Leave your current one to start a new one!");
                }
                else
                {
                    player.alliance = allianceName;
                    ulong id = user.Id;
                    SavePlayerData(id, player);
                    sb.AppendLine($"Congratulations! Alliance " + allianceName + " created.");
                }
            }
            else
            {
                sb.AppendLine($"Error: \"None\" is not an acceptable name for an alliance. Please try again.");
            }

            // set embed fields
            embed.Title = "Creating alliance...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("alliance invite", RunMode = RunMode.Async)]
        [Alias("ainvite", "alliance i", "invite")]
        public async Task AllianceInviteCommand(string invitee)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);


            string username = user.ToString();
            Charsheet inviter = PlayerLoad(user.Id);
            if (inviter.alliance != "None")
            {
                ISocketMessageChannel channel = (ISocketMessageChannel)Context.Channel;
                IReadOnlyCollection<ulong> inv = Context.Message.MentionedUserIds;
                List<ulong> ids = inv.ToList();
                List<Invitee> invitees = new List<Invitee>();
                foreach (ulong id in ids)
                {
                    IGuildUser u = await Context.Guild.GetUserAsync(id);
                    Invitee i = new Invitee(user.Id, id, u.ToString());
                    invitees.Add(i);
                }
                Invitee invited = invitees.FirstOrDefault();
                var message = await channel.SendMessageAsync(username + " wants " + invited.GetName() + " to join their alliance named the " + inviter.alliance + " alliance.  Do you accept, " + invited.GetName() + "?");
                var check = new Emoji("\u2705");
                await message.AddReactionAsync(check);
                Charsheet invite = PlayerLoad(invited.GetId());
                string prevAlliance = invite.alliance;
                DateTimeOffset prepThen = message.Timestamp;
                DateTimeOffset prepNow = DateTimeOffset.Now;
                TimeSpan cooldown = new TimeSpan(0, 2, 0);
                TimeSpan elapsed = prepNow - prepThen;
                IEnumerable<IUser> reactors = await message.GetReactionUsersAsync(check, 100).FlattenAsync();
                List<IUser> react = reactors.Where(reactors => reactors.IsBot == false).ToList();
                bool isMatch = false;
                while (!isMatch && elapsed < cooldown)
                {
                    prepNow = DateTimeOffset.Now;
                    elapsed = prepNow - prepThen;
                    reactors = await message.GetReactionUsersAsync(check, 100).FlattenAsync();
                    react = reactors.Where(reactors => reactors.IsBot == false).ToList();
                    if (react.Count > 0)
                    {
                        foreach (IUser r in react)
                        {
                            isMatch = await AllianceJoin(message, invited.GetId(), r.Id);
                        }
                    }
                }
                if (elapsed > cooldown)
                {
                    sb.AppendLine($"Too much time has passed. Try again!");
                }
                else
                {

                    if (prevAlliance == "None" && isMatch)
                    {
                        sb.AppendLine($"You have joined the " + inviter.alliance + " alliance!");
                        invite.alliance = inviter.alliance;
                        SavePlayerData(invited.GetId(), invite);
                    }
                    else if (prevAlliance != "None" && isMatch)
                    {
                        sb.AppendLine($"" + invitee + ", you're already in an alliance! Leave your current one before trying to join another.");
                    }
                }
            }
            else
            { sb.AppendLine($"" + username + ", you are not currently in an alliance. Please create or join an alliance with =alliance create before inviting others to it!"); }

            // build out the reply

            // set embed fields
            embed.Title = "Attempting to join alliance...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("alliance leave", RunMode = RunMode.Async)]
        [Alias("aleave", "alliance l", "leave")]
        public async Task AllianceLeaveCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            ulong id = user.Id;
            await BeginMethod(user);


            string username = user.ToString();
            Charsheet player = PlayerLoad(id);
            // build out the reply
            sb.AppendLine($"" + username + " has left the " + player.alliance + " alliance.");
            player.alliance = "None";
            SavePlayerData(id, player);

            // set embed fields
            embed.Title = "Leaving alliance...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("skill", RunMode = RunMode.Async)]
        public async Task SkillCommand(string stat = "", int points = 0)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            string username = user.ToString();
            await BeginMethod(user);


            Charsheet player = PlayerLoad(user.Id);
            int existingPoints = player.GetSkillPoints();
            bool statFound = true;
            if (existingPoints < 1)
            {
                sb.AppendLine($"" + username + ", you do not have any points to spend! Gain experience by foraging to level up and get skill points!");
            }
            else
            {
                if (stat != "" && points != 0)
                {
                    switch (stat)
                    {
                        case "str":
                            player.strength += points;
                            break;
                        case "int":
                            player.intelligence += points;
                            break;
                        case "cha":
                            player.charisma += points;
                            break;
                        case "con":
                            player.constitution += points;
                            break;
                        case "strength":
                            player.strength += points;
                            break;
                        case "intelligence":
                            player.intelligence += points;
                            break;
                        case "charisma":
                            player.charisma += points;
                            break;
                        case "constitution":
                            player.constitution += points;
                            break;
                        default:
                            sb.AppendLine($"Stat not found. Please try again!");
                            statFound = false;
                            break;
                    }
                    if (statFound)
                    {
                        sb.AppendLine($"Added " + points + " point(s) to " + stat + ".");
                        player.skillPoints -= points;
                        ulong id = GetIDFromUsername(player.GetName());
                        SavePlayerData(id, player);
                    }
                }
                else if (points == 0 && stat != "")
                {
                    sb.AppendLine($"You must specify a point value!");
                }
                else if (points == 0 && stat == "")
                {
                    sb.AppendLine($"" + username + ", you have " + existingPoints + " points available!");
                }

            }

            // build out the reply

            // set embed fields
            embed.Title = "Skill Screen";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("melee", RunMode = RunMode.Async)]
        [Alias("m")]
        public async Task MeleeCommand(int zombie, string weaponWord1, string weaponWord2 = null, string weaponWord3 = null)
        {
            string weapon = weaponWord1;
            if (weaponWord2 != null)
            {
                weapon += " ";
                weapon += weaponWord2;
            }
            if (weaponWord3 != null)
            {
                weapon += " ";
                weapon += weaponWord3;
            }
            weapon = weapon.ToLower();
            if (weapon == "branch")
            {
                weapon = "sturdy branch";
            }
            if (weapon == "bat")
            {
                weapon = "baseball bat";
            }
            if (weapon == "baton")
            {
                weapon = "police baton";
            }
            if (weapon == "pipe wrench")
            {
                weapon = "large pipe wrench";
            }
            if (weapon == "wood plank" || weapon == "plank")
            {
                weapon = "2x4 wood plank";
            }
            if (weapon == "spear")
            {
                weapon = "makeshift spear";
            }
            if (weapon == "prod")
            {
                weapon = "cattle prod";
            }
            if (weapon == "knuckles")
            {
                weapon = "brass knuckles";
            }
            if (weapon == "hoe")
            {
                weapon = "dirty hoe";
            }
            if (weapon == "leg")
            {
                weapon = "table leg";
            }
            if (weapon == "sword")
            {
                weapon = "claymore";
            }
            // get user info from the Context
            weapon = weapon.ToLower();
            var user = Context.User;
            await BeginMethod(user);
            string username = user.ToString();
            Participant participant = DeserializeAllParticipants(user.Id);
            bool isParticipant = false;
            if (participant.GetName() != null)
            {
                isParticipant = true;
            }
            bool isDefender = IsAllianceDefender(user.Id);
            if (isDefender)
            {
                isParticipant = true;
                participant = FetchAllianceDefender(user.Id);
            }
            string chosenAction = participant.GetAction();
            PlayerInventory inv = GetPlayerInventory(user.Id);
            bool hasWeapon = false;
            List<Item> playerItems = inv.items.ToList();
            Dictionary<string, Weapon> wDB = DeserializeWeaponDictionary();
            Weapon playerWeapon = new Weapon();
            foreach (Item i in playerItems)
            {
                string itemName = i.GetItemName();
                if (itemName == weapon)
                {
                    hasWeapon = true;
                    playerWeapon = wDB[itemName];
                }
            }
            if (!hasWeapon)
            {
                await Context.Channel.SendMessageAsync(weapon + " not found in your inventory. Be sure to type the name exactly!");
            }
            else
            {
                if (playerWeapon.category == "melee")
                {
                    if (isParticipant && chosenAction == null)
                    {
                        await Context.Channel.SendMessageAsync(username + " has opted to melee attack Zombie " + zombie + " with " + weapon + ".");
                        participant.action = "melee";
                        participant.weapon = weapon;
                        participant.target = zombie;
                        participant.id = user.Id;
                        bool hasWritten = false;
                        while (!hasWritten)
                        {
                            if (isDefender)
                            {
                                try
                                {
                                    SaveAllianceDefender(participant);
                                    hasWritten = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                            else
                            {
                                try
                                {
                                    SaveParticipant(participant);
                                    hasWritten = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                        }
                    }
                    else if (!isParticipant && chosenAction == null)
                    {
                        await Context.Channel.SendMessageAsync(username + ", you're not part of this fight!");
                    }
                    else if (isParticipant && chosenAction != null)
                    {
                        await Context.Channel.SendMessageAsync(username + ", it's too late to change your mind! You've already committed to the " + chosenAction + " action!");
                    }
                }
                else
                { 
                    await Context.Channel.SendMessageAsync(username + ", that's not a melee weapon! Pick again!"); 
                }
            }
        }

        [Command("shoot", RunMode = RunMode.Async)]
        [Alias("sh")]
        public async Task ShootCommand(int zombie, string weaponWord1, string weaponWord2 = null, string weaponWord3 = null)
        {
            string weapon = weaponWord1;
            if (weaponWord2 != null)
            {
                weapon += " ";
                weapon += weaponWord2;
            }
            if (weaponWord3 != null)
            {
                weapon += " ";
                weapon += weaponWord3;
            }
            weapon = weapon.ToLower();
            if (weapon == "smg")
            {
                weapon = "submachine gun";
            }
            if (weapon == "lmg")
            {
                weapon = "light machine gun";
            }

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string username = user.ToString();
            bool isParticipant = false;
            Participant participant = DeserializeAllParticipants(user.Id);
            if (participant.GetName() != null)
            {
                isParticipant = true;
            }
            bool isDefender = IsAllianceDefender(user.Id);
            if (isDefender)
            {
                isParticipant = true;
                participant = FetchAllianceDefender(user.Id);
            }
            string chosenAction = participant.GetAction();
            PlayerInventory inv = GetPlayerInventory(user.Id);
            bool hasWeapon = false;
            List<Item> playerItems = inv.items.ToList();
            foreach (Item i in playerItems)
            {
                string itemName = i.GetItemName();
                if (itemName == weapon)
                {
                    hasWeapon = true;
                }
            }
            Dictionary<string, Weapon> wDB = DeserializeWeaponDictionary();
            Weapon playerWeapon = new Weapon();
            foreach (Item i in playerItems)
            {
                string itemName = i.GetItemName();
                if (itemName == weapon)
                {
                    hasWeapon = true;
                    playerWeapon = wDB[itemName];
                }
            }
            if (!hasWeapon)
            {
                await Context.Channel.SendMessageAsync(weapon + " not found in your inventory. Be sure to type the name exactly!");
            }
            else
            {
                if (playerWeapon.category == "ranged")
                {
                    if (isParticipant && chosenAction == null)
                    {
                        await Context.Channel.SendMessageAsync(username + " has opted to shoot Zombie " + zombie + " with " + weapon + ".");
                        participant.action = "shoot";
                        participant.weapon = weapon;
                        participant.target = zombie;
                        bool hasWritten = false;
                        while (!hasWritten)
                        {
                            if (isDefender)
                            {
                                try
                                {
                                    SaveAllianceDefender(participant);
                                    hasWritten = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                            else
                            {
                                try
                                {
                                    SaveParticipant(participant);
                                    hasWritten = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                        }
                    }
                    else if (!isParticipant && chosenAction == null)
                    {
                        await Context.Channel.SendMessageAsync(username + ", you're not part of this fight!");
                    }
                    else if (isParticipant && chosenAction != null)
                    {
                        await Context.Channel.SendMessageAsync(username + ", it's too late to change your mind! You've already committed to the " + chosenAction + " action!");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync(username + ", that's not a ranged weapon! Please try again.");
                }
            }
        }

        [Command("distract", RunMode = RunMode.Async)]
        [Alias("di")]
        public async Task DistractCommand(int zombie)
        {
            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string username = user.ToString();
            bool isParticipant = false;
            Participant participant = DeserializeAllParticipants(user.Id);
            if (participant.GetName() != null)
            {
                isParticipant = true;
            }
            bool isDefender = IsAllianceDefender(user.Id);
            if (isDefender)
            {
                isParticipant = true;
                participant = FetchAllianceDefender(user.Id);
            }
            string chosenAction = participant.GetAction();
            if (isParticipant && chosenAction == null)
            {
                await Context.Channel.SendMessageAsync(username + " has opted to distract Zombie " + zombie + ".");
                participant.action = "distract";
                participant.weapon = "";
                participant.target = zombie;
                bool hasWritten = false;
                while (!hasWritten)
                {
                    if (isDefender)
                    {
                        try
                        {
                            SaveAllianceDefender(participant);
                            hasWritten = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                            Console.WriteLine(ex);
                            throw;
                        }
                    }
                    else
                    {
                        try
                        {
                            SaveParticipant(participant);
                            hasWritten = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                            Console.WriteLine(ex);
                            throw;
                        }
                    }
                }
            }
            else if (!isParticipant && chosenAction == null)
            {
                await Context.Channel.SendMessageAsync(username + ", you're not part of this fight!");
            }
            else if (isParticipant && chosenAction != null)
            {
                await Context.Channel.SendMessageAsync(username + ", it's too late to change your mind! You've already committed to the " + chosenAction + " action!");
            }
        }

        [Command("run", RunMode = RunMode.Async)]
        [Alias("r")]
        public async Task RunCommand()
        {
            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);

            string username = user.ToString();
            bool isParticipant = false;
            Participant p = DeserializeAllParticipants(user.Id);
            if (p.GetName() != null)
            {
                isParticipant = true;
            }
            string chosenAction = p.GetAction();
            if (isParticipant && chosenAction == null)
            {
                await Context.Channel.SendMessageAsync(username + " has opted to run away.");
                Participant participant = DeserializeAllParticipants(user.Id);
                participant.action = "run";
                participant.weapon = "";
                participant.target = 0;
                bool hasWritten = false;
                while (!hasWritten)
                {
                    try
                    {
                        SaveParticipant(participant);
                        hasWritten = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: Foraging file \"" + participant.forager + "_forage.xml\" already being accessed.");
                        Console.WriteLine(ex);
                        throw;
                    }
                }
            }
            else if (!isParticipant && chosenAction == null)
            {
                await Context.Channel.SendMessageAsync(username + ", you're not part of this fight!");
            }
            else if (isParticipant && chosenAction != null)
            {
                await Context.Channel.SendMessageAsync(username + ", it's too late to change your mind! You've already committed to the " + chosenAction + " action!");
            }
        }

        [Command("trade", RunMode = RunMode.Async)]
        public async Task TradeCommand(string target)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            var channel = Context.Channel;
            await BeginMethod(user);
            Item userItem = new Item();
            Item targetItem = new Item();
            Dictionary<ulong, PlayerInventory> pInvDict = DeserializePlayerInventoryDictionary();
            PlayerInventory playerInv = new PlayerInventory();
            if (pInvDict.ContainsKey(user.Id))
            {
                playerInv = pInvDict[user.Id];
            }

            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong targetId = 0;
            List<ulong> ids = i.ToList<ulong>();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                targetId = x;
            }
            PlayerInventory targetInv = new PlayerInventory();
            if (pInvDict.ContainsKey(targetId))
            {
                targetInv = pInvDict[targetId];
            }
            IUser targetUser = await Context.Guild.GetUserAsync(targetId);
            sb.AppendLine($"" + user.Username + " wants to trade with " + targetUser.Username + ". " + targetUser.Username + ", do you accept?");
            embed.Title = user.Username + " trading...";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));
            var msg = await ReplyAsync(null, false, embed.Build());
            var check = new Emoji("\u2705");
            await msg.AddReactionAsync(check);
            DateTimeOffset then = msg.Timestamp;
            DateTimeOffset now = DateTimeOffset.Now;
            TimeSpan wait = now - then;
            int waitSecs = wait.Seconds;
            List<IUser> reactors = new List<IUser>();
            bool targetHasReacted = false;
            while (waitSecs < 60 && !targetHasReacted)
            {
                now = DateTimeOffset.Now;
                IEnumerable<IUser> r = await msg.GetReactionUsersAsync(check, 100).FlattenAsync();
                reactors = r.Where(x => x.Id == targetId).ToList();
                if (reactors.Count >= 1)
                {
                    targetHasReacted = true;   
                }
            }
            if (waitSecs >= 60 && !targetHasReacted)
            {
                await channel.SendMessageAsync(targetUser.Username + " has not responded in time; please try again later.");
            }
            else
            {
                if (targetHasReacted)
                {
                    string content = null;
                    var userMessage = await channel.SendMessageAsync(user.Username + ", what would you like to trade? Note: Format of response must be [quantity], [item name]", false, null, null);
                    then = userMessage.Timestamp;
                    now = DateTimeOffset.Now;
                    wait = now - then;
                    waitSecs = wait.Seconds;
                    bool userHasResponded = false;
                    while (waitSecs < 60 && !userHasResponded)
                    {
                        now = DateTimeOffset.Now;
                        IEnumerable<IMessage> msgs = await channel.GetMessagesAsync(1).FlattenAsync();
                        foreach (IMessage m in msgs)
                        {
                            ulong senderID = m.Author.Id;
                            if (senderID == user.Id)
                            {
                                content = m.Content;
                                userHasResponded = true;
                                await userMessage.DeleteAsync();
                            }
                        }
                    }
                    int indexOfComma = content.IndexOf(",");
                    string itemName = content.Substring(indexOfComma).ToLower();
                    string quantity = content.Substring(0, (content.Length - itemName.Length));
                    itemName = itemName.Substring(2);
                    if (itemName == "food" || itemName == "water" || itemName == "meds" || itemName == "ammo" || itemName == "cash" | itemName == "scrap")
                    {
                        char firstLet = char.ToUpper(itemName[0]);
                        itemName = firstLet + itemName.Substring(1);
                    }
                    bool quantityIsValid = Int32.TryParse(quantity, out int qty);
                    List<string> validItems = new List<string>();
                    List<string> weapons = DeserializeWeaponsList();
                    validItems.Add("Water");
                    validItems.Add("Food");
                    validItems.Add("Meds");
                    validItems.Add("Ammo");
                    validItems.Add("Scrap");
                    validItems.Add("Cash");
                    foreach (string weapon in weapons)
                    {
                        validItems.Add(weapon);
                    }
                    bool choiceIsValid = false;
                    foreach (string item in validItems)
                    {
                        if (itemName == item)
                        {
                            choiceIsValid = true;
                        }
                    }
                    if (waitSecs >= 60 && !userHasResponded)
                    {
                        await channel.SendMessageAsync(user.Username + " has not responded in time; please try again later.");
                    }
                    else if (!choiceIsValid)
                    {
                        await channel.SendMessageAsync(user.Username + ", your item name was invalid! Please start the trade over.");
                    }
                    else if (!quantityIsValid)
                    {
                        await channel.SendMessageAsync(user.Username + ", your item quantity was invalid! Please start the trade over. Note that quantities must be entered numerically (e.g. 1, 2, 3, etc.)");
                    }
                    else if (!quantityIsValid && !choiceIsValid)
                    {
                        await channel.SendMessageAsync(user.Username + ", your item quantity and item name were invalid! Please start the trade over. Note that quantities must be entered numerically (e.g. 1, 2, 3, etc.)");
                    }
                    else
                    {
                        bool hasItem = false;
                        List<Item> playerItems = playerInv.GetInventory();
                        foreach (Item playerItem in playerItems)
                        {
                            if (playerItem.GetItemName() == itemName && playerItem.GetItemQty() >= qty)
                            {
                                hasItem = true;
                                userItem = new Item(itemName, qty);
                            }
                        }
                        if (!hasItem)
                        {
                            await channel.SendMessageAsync(user.Username + ", you don't have enough of that item!");
                        }
                        else
                        {
                            content = null;
                            var targetMessage = await channel.SendMessageAsync(user.Username + " has selected " + userItem.GetItemQty() + " " + userItem.GetItemName() + ". " + targetUser.Username + ", what would you like to trade? Note: Format of response must be [quantity], [item name]", false, null, null);
                            then = targetMessage.Timestamp;
                            now = DateTimeOffset.Now;
                            wait = now - then;
                            waitSecs = wait.Seconds;
                            bool targetHasResponded = false;
                            while (waitSecs < 60 && !targetHasResponded)
                            {
                                now = DateTimeOffset.Now;
                                IEnumerable<IMessage> msgs = await channel.GetMessagesAsync(1).FlattenAsync();
                                foreach (IMessage m in msgs)
                                {
                                    ulong senderID = m.Author.Id;
                                    if (senderID == targetUser.Id)
                                    {
                                        content = m.Content;
                                        targetHasResponded = true;
                                        await targetMessage.DeleteAsync();
                                    }
                                }
                            }
                            indexOfComma = content.IndexOf(",");
                            string targetItemName = content.Substring(indexOfComma).ToLower();
                            string targetQuantity = content.Substring(0, (content.Length - targetItemName.Length));
                            targetItemName = targetItemName.Substring(2);
                            if (targetItemName == "food" || targetItemName == "water" || targetItemName == "meds" || targetItemName == "ammo" || targetItemName == "cash" | targetItemName == "scrap")
                            {
                                char firstLet = char.ToUpper(targetItemName[0]);
                                targetItemName = firstLet + targetItemName.Substring(1);
                            }
                            bool targetChoiceIsValid = false;
                            bool targetQuantityIsValid = Int32.TryParse(targetQuantity, out int targetQty);
                            foreach (string item in validItems)
                            {
                                if (targetItemName == item)
                                {
                                    targetChoiceIsValid = true;
                                }
                            }
                            if (waitSecs >= 60 && !targetHasResponded)
                            {
                                await channel.SendMessageAsync(targetUser.Username + " has not responded in time; please start the trade over.");
                            }
                            else if (!targetChoiceIsValid)
                            {
                                await channel.SendMessageAsync(targetUser.Username + ", your item name was invalid! Please start the trade over.");
                            }
                            else if (!targetQuantityIsValid)
                            {
                                await channel.SendMessageAsync(targetUser.Username + ", your item targetQuantity was invalid! Please start the trade over. Note that quantities must be entered numerically (e.g. 1, 2, 3, etc.)");
                            }
                            else if (!targetQuantityIsValid && !targetChoiceIsValid)
                            {
                                await channel.SendMessageAsync(targetUser.Username + ", your item targetQuantity and item name were invalid! Please start the trade over. Note that quantities must be entered numerically (e.g. 1, 2, 3, etc.)");
                            }
                            else
                            {
                                bool targetHasItem = false;
                                List<Item> targetItems = targetInv.GetInventory();
                                foreach (Item targetInvItem in targetItems)
                                {
                                    if (targetInvItem.GetItemName() == targetItemName && targetInvItem.GetItemQty() >= targetQty)
                                    {
                                        targetHasItem = true;
                                        targetItem = new Item(targetItemName, targetQty);
                                    }
                                }
                                if (!targetHasItem)
                                {
                                    await channel.SendMessageAsync(targetUser.Username + ", you don't have enough of that item!");
                                }
                                else
                                {
                                    foreach (Item x in playerItems)
                                    {
                                        if (x.GetItemName() == userItem.GetItemName())
                                        {
                                            x.itemQty -= userItem.GetItemQty();
                                        }
                                    }
                                    playerInv.items = playerItems;
                                    foreach (Item y in targetItems)
                                    {
                                        if (y.GetItemName() == targetItem.GetItemName())
                                        {
                                            y.itemQty -= targetItem.GetItemQty();
                                        }
                                    }
                                    bool playerHadItem = false;
                                    foreach (Item x in playerItems)
                                    {
                                        if (x.GetItemName() == targetItem.GetItemName())
                                        {
                                            x.itemQty += targetItem.GetItemQty();
                                            playerHadItem = true;
                                        }
                                    }
                                    bool targetHadItem = false;
                                    foreach (Item y in targetItems)
                                    {
                                        if (y.GetItemName() == userItem.GetItemName())
                                        {
                                            y.itemQty += userItem.GetItemQty();
                                            targetHadItem = true;
                                        }
                                    }
                                    if (!playerHadItem)
                                    {
                                        playerItems.Add(targetItem);
                                    }
                                    if (!targetHadItem)
                                    {
                                        targetItems.Add(userItem);
                                    }
                                    playerInv.items = playerItems;
                                    targetInv.items = targetItems;
                                    pInvDict.Remove(user.Id);
                                    pInvDict.Remove(targetUser.Id);
                                    pInvDict.Add(user.Id, playerInv);
                                    pInvDict.Add(targetUser.Id, targetInv);
                                    Serialize(pInvDict, "playerInventoryDictionary");
                                    await channel.SendMessageAsync("Trade complete! " + user.Username + " has given " + targetUser.Username + " " + userItem.GetItemQty() + " " + userItem.GetItemName() + ", and " + targetUser.Username + "has given " + user.Username + " " + targetItem.GetItemQty() + " " + targetItem.GetItemName() + ".");
                                }
                            }
                        }
                    }
                }
            }
        }

        [Command("settlementlist", RunMode = RunMode.Async)]
        [Alias("slist")]
        public async Task SettlementListCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            Dictionary<ulong, List<Settlement>> settlementDict = DeserializeSettlementDictionary();
            List<Settlement> userSettlements = new List<Settlement>();
            if (settlementDict.ContainsKey(user.Id))
            {
                userSettlements = settlementDict[user.Id];
            }
            // build out the reply
            foreach (Settlement s in userSettlements)
            {
                sb.AppendLine(s.name);
            }

            // set embed fields
            embed.Title = user.Username + "'s Settlements";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("settlementinfo", RunMode = RunMode.Async)]
        [Alias("sinfo")]
        public async Task SettlementInfoCommand(string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            char firstLetter = char.ToUpper(settlementName[0]);
            settlementName = firstLetter + settlementName.Substring(1);
            if (name2 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name2[0]);
                name2 = firstLet + name2.Substring(1);
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name3[0]);
                name3 = firstLet + name3.Substring(1);
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name4[0]);
                name4 = firstLet + name4.Substring(1);
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name5[0]);
                name5 = firstLet + name5.Substring(1);
                settlementName += name5;
            }
            if (settlementName.Contains("-"))
            {
                int dashIndex = settlementName.IndexOf("-");
                char e = char.ToUpper(settlementName[dashIndex + 1]);
                string previousString = settlementName.Substring(0, dashIndex);
                settlementName = previousString + "-" + e + settlementName.Substring(dashIndex + 2);
            }
            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            Dictionary<ulong, List<Settlement>> settlementDict = DeserializeSettlementDictionary();
            List<Settlement> userSettlements = new List<Settlement>();
            if (settlementDict.ContainsKey(user.Id))
            {
                userSettlements = settlementDict[user.Id];
            }
            bool hasSettlement = false;
            Settlement selection = new Settlement();
            foreach (Settlement s in userSettlements)
            {
                if (s.name == settlementName)
                {
                    hasSettlement = true;
                    selection = s;
                }
            }
            // build out the reply
            if (hasSettlement)
            {
                bool hasModules = true;
                if (selection.modules == null)
                {
                    hasModules = false;
                }
                sb.AppendLine("Settlement Info");
                sb.AppendLine("________________________________");
                sb.AppendLine("Name: " + selection.name);
                sb.AppendLine("Rarity: " + selection.rarity);
                sb.AppendLine("Total Modules: " + selection.totalModules);
                sb.AppendLine("________________________________");
                if (hasModules)
                {
                    int cnt = 1;
                    foreach (SettlementModule sm in selection.modules)
                    {
                        TimeSpan sinceLastCollect = DateTimeOffset.Now - sm.lastCollection;
                        string type = sm.type;
                        char firstLet = char.ToUpper(type[0]);
                        type = firstLet + type.Substring(1);
                        sb.AppendLine("Module " + cnt);
                        if (sm.type != "null")
                        {
                            sb.AppendLine("Type: " + type);
                        }
                        else
                        {
                            sb.AppendLine("Type: Empty");
                        }
                        sb.AppendLine("Level: " + sm.level);
                        sb.AppendLine("Cost: " + sm.cost);
                        if (sm.type != "null" && sm.type != "barracks" && sm.type != "watchtower" && sm.type != "chain-link fence")
                        {
                            sb.AppendLine("Since Last Collection: " + sinceLastCollect.Minutes + "m, " + sinceLastCollect.Seconds + "s.");
                        }
                        sb.AppendLine("------------------");
                        cnt++;
                    }
                }
            }
            else
            {
                sb.AppendLine(user.Username + " has no settlements with that name!");
            }

            // set embed fields
            embed.Title = user.Username + "'s Settlement " + settlementName + " Info";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("settlementrebuild", RunMode = RunMode.Async)]
        [Alias("srebuild")]
        public async Task SettlementRebuildCommand(string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            var user = Context.User;
            await BeginMethod(user);
            // initialize empty string builder for reply
            var sb = new StringBuilder();
            if (name2 != null)
            {
                settlementName += " ";
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                settlementName += name5;
            }
            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            char firstLet = char.ToUpper(settlementName[0]);
            settlementName = firstLet + settlementName.Substring(1);
            int index = 0;
            bool moreWords = true;
            while (moreWords)
            {
                int spaceIndex = settlementName.IndexOf(" ", index);
                if (spaceIndex != -1)
                {
                    char nextWord = char.ToUpper(settlementName[spaceIndex + 1]);
                    string previousString = settlementName.Substring(0, spaceIndex);
                    settlementName = previousString + " " + nextWord + settlementName.Substring(spaceIndex + 2);
                    index = spaceIndex + 2;
                }
                else
                {
                    moreWords = false;
                }
                if (settlementName.Contains("-"))
                {
                    int dashIndex = settlementName.IndexOf("-");
                    char e = char.ToUpper(settlementName[dashIndex + 1]);
                    string previousString = settlementName.Substring(0, dashIndex);
                    settlementName = previousString + "-" + e + settlementName.Substring(dashIndex + 2);
                }
            }
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List <Settlement> sList = new List<Settlement>();
            if (sDict.ContainsKey(user.Id))
            {
                sList = sDict[user.Id];
            }
            List<Settlement> settlementDB = DeserializeSettlementDatabase();
            bool settlementIsValid = false;
            bool alreadyOwnsSettlement = false;
            Settlement settlement = new Settlement();
            foreach (Settlement s in settlementDB)
            {
                if (s.name == settlementName)
                {
                    settlementIsValid = true;
                    settlement = s;
                }
            }
            foreach (Settlement s in sList)
            {
                if (s.name == settlementName)
                {
                    alreadyOwnsSettlement = true;
                }
            }
            if (settlementIsValid && !alreadyOwnsSettlement)
            {
                sb.AppendLine(user.Username + " wants to rebuild the " + settlement.name + " settlement.");
                int rebuildPrice = settlement.rarity * 100;
                int totalMinutes = settlement.rarity * 10;
                TimeSpan rebuildTimer = new TimeSpan(0, totalMinutes, 0);
                sb.AppendLine($"This will cost " + rebuildPrice + " Scrap, and take " + totalMinutes + " minutes to rebuild.");
                sb.AppendLine($"Do you wish to continue with the rebuild?");
                embed.Title = user.Username + "'s Rebuild Estimate";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                var msg = await ReplyAsync(null, false, embed.Build());
                var check = new Emoji("\u2705");
                await msg.AddReactionAsync(check);
                DateTimeOffset then = msg.Timestamp;
                DateTimeOffset now = DateTimeOffset.Now;
                TimeSpan wait = now - then;
                int waitSecs = wait.Seconds;
                List<IUser> reactors = new List<IUser>();
                bool userHasReacted = false;
                while (waitSecs < 60 && !userHasReacted)
                {
                    now = DateTimeOffset.Now;
                    IEnumerable<IUser> r = await msg.GetReactionUsersAsync(check, 100).FlattenAsync();
                    reactors = r.Where(x => x.Id == user.Id).ToList();
                    if (reactors.Count >= 1)
                    {
                        userHasReacted = true;
                    }
                }
                if (userHasReacted)
                {
                    string nameMap = settlementName + " Map";
                    bool hasMap = false;
                    Item map = new Item();
                    PlayerInventory playerInventory = new PlayerInventory();
                    Dictionary<ulong, PlayerInventory> pvd = DeserializePlayerInventoryDictionary();
                    if (pvd.ContainsKey(user.Id))
                    {
                        playerInventory = pvd[user.Id];
                    }
                    foreach (Item i in playerInventory.items)
                    {
                        if (i.GetItemName() == nameMap)
                        {
                            hasMap = true;
                            map = i;
                        }
                    }
                    Charsheet player = PlayerLoad(user.Id);
                    int playerScrap = player.GetScrap();
                    if (hasMap && playerScrap >= rebuildPrice)
                    {
                        SettlementPlayerVariables spv = new SettlementPlayerVariables();
                        spv.isRebuilding = false;
                        Dictionary<ulong, SettlementPlayerVariables> spvDict = DeserializeSettlementVariableDictionary();
                        if (spvDict.ContainsKey(user.Id))
                        {
                            spv = spvDict[user.Id];
                        }
                        if (!spv.isRebuilding)
                        {
                            spvDict.Remove(user.Id);
                            spv.isRebuilding = true;
                            spv.rebuildBegan = DateTimeOffset.Now;
                            spv.rebuildTimer = rebuildTimer;
                            spv.settlementRebuilding = settlement;
                            spvDict.Add(user.Id, spv);
                            Serialize(spvDict, "Settlements\\SettlementPlayerVariables");
                            player.scrap -= rebuildPrice;
                            SavePlayerData(user.Id, player);
                            playerInventory.items.Remove(map);
                            pvd.Remove(user.Id);
                            pvd.Add(user.Id, playerInventory);
                            Serialize(pvd, "playerInventoryDictionary");
                            sb = new StringBuilder();
                            sb.AppendLine(user.Username + ", congratulations! Your rebuild has begun.");
                            sb.AppendLine($"Be sure to do =settlementcheckrebuild when the timer is up, so that your settlement finishes rebuilding!");
                            // set embed fields
                            embed.Title = user.Username + "'s Rebuild";
                            embed.Description = sb.ToString();
                            embed.WithColor(new Color(0, 255, 0));

                            // send simple string reply
                            await ReplyAsync(null, false, embed.Build());
                        }
                        else
                        {
                            sb = new StringBuilder();
                            sb.AppendLine(user.Username + " is already rebuilding a settlement! Please wait until your previous settlement is finished rebuilding.");

                            // set embed fields
                            embed.Title = "Failed to rebuild!";
                            embed.Description = sb.ToString();
                            embed.WithColor(new Color(0, 255, 0));

                            // send simple string reply
                            await ReplyAsync(null, false, embed.Build());
                        }
                    }
                    else
                    {
                        sb = new StringBuilder();
                        sb.AppendLine(user.Username + ", you don't have enough Scrap, or you don't have the map for that settlement!");
                        // set embed fields
                        embed.Title = "Failed to rebuild!";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));

                        // send simple string reply
                        await ReplyAsync(null, false, embed.Build());
                    }                        
                }
                else
                {
                    sb.AppendLine(user.Username + " failed to react in time. Command canceled.");

                    // set embed fields
                    embed.Title = "Failed to rebuild!";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));

                    // send simple string reply
                    await ReplyAsync(null, false, embed.Build());
                }
            }
            else if (!settlementIsValid)
            {
                sb.AppendLine(user.Username + ", this settlement does not exist! Please check your spelling and spacing.");

                // set embed fields
                embed.Title = "Failed to rebuild!";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else if (alreadyOwnsSettlement)
            {
                sb.AppendLine(user.Username + ", you already own a settlement of this name!");

                // set embed fields
                embed.Title = "Failed to rebuild!";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("settlementdevelop", RunMode = RunMode.Async)]
        [Alias("sdevelop")]
        public async Task SettlementDevelopCommand(string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            char firstLetter = char.ToUpper(settlementName[0]);
            settlementName = firstLetter + settlementName.Substring(1);
            if (name2 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name2[0]);
                name2 = firstLet + name2.Substring(1);
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name3[0]);
                name3 = firstLet + name3.Substring(1);
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name4[0]);
                name4 = firstLet + name4.Substring(1);
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name5[0]);
                name5 = firstLet + name5.Substring(1);
                settlementName += name5;
            }
            if (settlementName.Contains("-"))
            {
                int dashIndex = settlementName.IndexOf("-");
                char e = char.ToUpper(settlementName[dashIndex + 1]);
                string previousString = settlementName.Substring(0, dashIndex);
                settlementName = previousString + "-" + e + settlementName.Substring(dashIndex + 2);
            }
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<Settlement> sList = new List<Settlement>();
            Settlement settlement = new Settlement();
            bool exists = false;
            if (sDict.ContainsKey(user.Id))
            {
                sList = sDict[user.Id];
            }
            foreach (Settlement s in sList)
            {
                if (s.name == settlementName)
                {
                    settlement = s;
                    if (settlement.isIntact)
                    {
                        exists = true;
                    }
                }
            }
            if (exists)
            {
                sList.Remove(settlement);
                sb.AppendLine(user.Username + ", which module would you like to develop in your " + settlement.name + " settlement?");
            }
            else
            {
                sb.AppendLine(user.Username + ", you don't currently own that settlement, or it isn't yet rebuilt!");
            }
            embed.Title = user.Username + "'s Settlement Develop";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));
            var msg = await ReplyAsync(null, false, embed.Build());
            var one = new Emoji("\u0031\u20e3");
            var two = new Emoji("\u0032\u20e3");
            var three = new Emoji("\u0033\u20e3");
            var four = new Emoji("\u0034\u20e3");
            var five = new Emoji("\u0035\u20e3");
            var six = new Emoji("\u0036\u20e3");
            var seven = new Emoji("\u0037\u20e3");
            var eight = new Emoji("\u0038\u20e3");
            var nine = new Emoji("\u0039\u20e3");
            var ten = new Emoji("\uD83D\uDD1F");
            int size = settlement.totalModules;
            List<Emoji> emojiList = new List<Emoji>();
            switch (size)
            {
                case 1:
                    emojiList.Add(one);
                    break;
                case 2:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    break;
                case 3:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    break;
                case 4:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    break;
                case 5:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    break;
                case 6:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    emojiList.Add(six);
                    break;
                case 7:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    emojiList.Add(six);
                    emojiList.Add(seven);
                    break;
                case 8:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    emojiList.Add(six);
                    emojiList.Add(seven);
                    emojiList.Add(eight);
                    break;
                case 9:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    emojiList.Add(six);
                    emojiList.Add(seven);
                    emojiList.Add(eight);
                    emojiList.Add(nine);
                    break;
                case 10:
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    emojiList.Add(six);
                    emojiList.Add(seven);
                    emojiList.Add(eight);
                    emojiList.Add(nine);
                    emojiList.Add(ten);
                    break;
            }
            Emoji[] emojis = emojiList.ToArray();
            await msg.AddReactionsAsync(emojis);
            DateTimeOffset then = msg.Timestamp;
            DateTimeOffset now = DateTimeOffset.Now;
            TimeSpan wait = now - then;
            int waitSecs = wait.Seconds;
            List<IUser> reactors = new List<IUser>();
            bool targetHasReacted = false;
            int playerChoice = 0;
            while (waitSecs < 60 && !targetHasReacted)
            {
                now = DateTimeOffset.Now;
                IEnumerable<IUser> ones = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                IEnumerable<IUser> twos = await msg.GetReactionUsersAsync(two, 100).FlattenAsync();
                IEnumerable<IUser> threes = await msg.GetReactionUsersAsync(three, 100).FlattenAsync();
                IEnumerable<IUser> fours = await msg.GetReactionUsersAsync(four, 100).FlattenAsync();
                IEnumerable<IUser> fives = await msg.GetReactionUsersAsync(five, 100).FlattenAsync();
                IEnumerable<IUser> sixes = await msg.GetReactionUsersAsync(six, 100).FlattenAsync();
                IEnumerable<IUser> sevens = await msg.GetReactionUsersAsync(seven, 100).FlattenAsync();
                IEnumerable<IUser> eights = await msg.GetReactionUsersAsync(eight, 100).FlattenAsync();
                IEnumerable<IUser> nines = await msg.GetReactionUsersAsync(nine, 100).FlattenAsync();
                IEnumerable<IUser> tens = await msg.GetReactionUsersAsync(ten, 100).FlattenAsync();
                List<IEnumerable<IUser>> reactionChecks = new List<IEnumerable<IUser>>();
                reactionChecks.Add(ones);
                reactionChecks.Add(twos);
                reactionChecks.Add(threes);
                reactionChecks.Add(fours);
                reactionChecks.Add(fives);
                reactionChecks.Add(sixes);
                reactionChecks.Add(sevens);
                reactionChecks.Add(eights);
                reactionChecks.Add(nines);
                reactionChecks.Add(tens);
                int cnt = 1;
                foreach (IEnumerable<IUser> r in reactionChecks)
                {
                    if (r.Count() != 0)
                    {
                        reactors = r.Where(x => x.Id == user.Id).ToList();
                        if (reactors.Count >= 1)
                        {
                            targetHasReacted = true;
                            playerChoice = cnt;
                            await msg.DeleteAsync();
                        }
                    }
                    cnt++;
                }
            }
            sb = new StringBuilder();
            SettlementModule chosen = new SettlementModule();
            if (settlement.modules == null)
            {
                List<SettlementModule> modules = new List<SettlementModule>();
                int cnt = 0;
                while (cnt < settlement.totalModules)
                {
                    SettlementModule blank = new SettlementModule("null", 0, 0, cnt);
                    modules.Add(blank);
                    cnt++;
                }
                settlement.modules = modules;
            }
            playerChoice--;
            foreach (SettlementModule mod in settlement.modules)
            {
                if (mod.slot == playerChoice && mod.type == "null")
                {
                    chosen = mod;

                    sb.AppendLine("Modules you can build will be specified by a number. React with that number to build that module.");
                    sb.AppendLine($"If you don't know what a module does, do =moduleinfo to get a description of each module.");
                    sb.AppendLine($"-----------------------------");
                    sb.AppendLine($"1. Garden. Cost: 50 Scrap"); //+5 Food every 20 minutes. Maximum of 50 food before it's full. Costs: 50 scrap.
                    SettlementModule garden = new SettlementModule("garden", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"2. Water Collector. Cost: 50 Scrap"); //+5 Water every 20 minutes. Maximum of 50 water before it's full. Costs: 50 scrap.
                    SettlementModule waterCollector = new SettlementModule("water collector", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"3. Medical Tent. Cost: 50 Scrap"); //+5 Health every 20 minutes. Maxes out at 100. Costs: 50 scrap.
                    SettlementModule medicalTent = new SettlementModule("medical tent", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"4. Ammunition Press. Cost: 100 Scrap"); //+5 Ammo every 20 minutes. Maximum of 50 ammo before it's full. Cost: 100 scrap.
                    SettlementModule ammunitionPress = new SettlementModule("ammunition press", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"5. Watchtower. Cost: 150 Scrap"); //+10 Defense. Cost: 150 scrap.
                    SettlementModule watchtower = new SettlementModule("watchtower", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"6. Chain-link Fence. Cost: 50 Scrap"); //+5 Defense. Cost: 50 scrap.
                    SettlementModule chainFence = new SettlementModule("chain-link fence", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"7. Barracks. Cost: 150 Scrap"); //Allows up to 2 random people in your alliance to gain the benefits of your modules every turn. Cost: 150 scrap.
                    SettlementModule barracks = new SettlementModule("barracks", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"8. Recycling Station. Cost: 100 Scrap"); //+5 Scrap every 20 minutes. Maximum of 50 scrap before it's full. Costs: 100 scrap.
                    SettlementModule recyclingStation = new SettlementModule("recycling station", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"9. Storage Area. Cost: 150 Scrap"); //Allows people to store up to 5 types of item in the settlement storage area, where anyone in the alliance can access it. Cost: 150 scrap.
                    SettlementModule storageArea = new SettlementModule("storage area", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"10. Workshop. Cost: 50 Scrap"); //Allows people to make an Intelligence roll to add or remove tags from weapons. Cost: 50 scrap.
                    SettlementModule workshop = new SettlementModule("workshop", 1, 50, chosen.slot, DateTimeOffset.Now);
                    sb.AppendLine($"");
                    sb.AppendLine($"[WARNING: Workshop commands not yet implemented. If you build module 10, you will not be able to use it yet.]");
                    embed.Title = user.Username + "'s Module Menu";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    emojiList = new List<Emoji>();
                    emojiList.Add(one);
                    emojiList.Add(two);
                    emojiList.Add(three);
                    emojiList.Add(four);
                    emojiList.Add(five);
                    emojiList.Add(six);
                    emojiList.Add(seven);
                    emojiList.Add(eight);
                    emojiList.Add(nine);
                    emojiList.Add(ten);
                    emojis = emojiList.ToArray();
                    msg = await ReplyAsync(null, false, embed.Build());
                    await msg.AddReactionsAsync(emojis);
                    then = msg.Timestamp;
                    now = DateTimeOffset.Now;
                    wait = now - then;
                    waitSecs = wait.Seconds;
                    reactors = new List<IUser>();
                    targetHasReacted = false;
                    playerChoice = 0;
                    while (waitSecs < 60 && !targetHasReacted)
                    {
                        IEnumerable<IUser> oneReacts = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                        IEnumerable<IUser> twoReacts = await msg.GetReactionUsersAsync(two, 100).FlattenAsync();
                        IEnumerable<IUser> threeReacts = await msg.GetReactionUsersAsync(three, 100).FlattenAsync();
                        IEnumerable<IUser> fourReacts = await msg.GetReactionUsersAsync(four, 100).FlattenAsync();
                        IEnumerable<IUser> fiveReacts = await msg.GetReactionUsersAsync(five, 100).FlattenAsync();
                        IEnumerable<IUser> sixReacts = await msg.GetReactionUsersAsync(six, 100).FlattenAsync();
                        IEnumerable<IUser> sevenReacts = await msg.GetReactionUsersAsync(seven, 100).FlattenAsync();
                        IEnumerable<IUser> eightReacts = await msg.GetReactionUsersAsync(eight, 100).FlattenAsync();
                        IEnumerable<IUser> nineReacts = await msg.GetReactionUsersAsync(nine, 100).FlattenAsync();
                        IEnumerable<IUser> tenReacts = await msg.GetReactionUsersAsync(ten, 100).FlattenAsync();
                        List<IEnumerable<IUser>> rChecks = new List<IEnumerable<IUser>>();
                        rChecks.Add(oneReacts);
                        rChecks.Add(twoReacts);
                        rChecks.Add(threeReacts);
                        rChecks.Add(fourReacts);
                        rChecks.Add(fiveReacts);
                        rChecks.Add(sixReacts);
                        rChecks.Add(sevenReacts);
                        rChecks.Add(eightReacts);
                        rChecks.Add(nineReacts);
                        rChecks.Add(tenReacts);
                        int count = 1;
                        foreach (IEnumerable<IUser> r in rChecks)
                        {
                            if (r.Count() != 0)
                            {
                                reactors = r.Where(x => x.Id == user.Id).ToList();
                                if (reactors.Count >= 1)
                                {
                                    playerChoice = count;
                                    targetHasReacted = true;
                                    await msg.DeleteAsync();
                                }
                            }
                            count++;
                        }
                    }
                    switch (playerChoice)
                    {
                        case 1:
                            chosen = garden;
                            break;
                        case 2:
                            chosen = waterCollector;
                            break;
                        case 3:
                            chosen = medicalTent;
                            break;
                        case 4:
                            chosen = ammunitionPress;
                            break;
                        case 5:
                            chosen = watchtower;
                            break;
                        case 6:
                            chosen = chainFence;
                            break;
                        case 7:
                            chosen = barracks;
                            break;
                        case 8:
                            chosen = recyclingStation;
                            break;
                        case 9:
                            chosen = storageArea;
                            break;
                        case 10:
                            chosen = workshop;
                            break;
                    }
                    Charsheet player = PlayerLoad(user.Id);
                    if (player.scrap >= chosen.cost)
                    {
                        player.scrap -= chosen.cost;
                        SavePlayerData(user.Id, player);
                        List<SettlementModule> mods = new List<SettlementModule>();
                        if (settlement.modules.Count > 0)
                        {
                            mods = settlement.modules;
                        }
                        List<SettlementModule> toRemove = new List<SettlementModule>();
                        foreach (SettlementModule m in mods)
                        {
                            if (m.slot == chosen.slot)
                            {
                                toRemove.Add(m);
                            }
                        }
                        foreach (SettlementModule rem in toRemove)
                        {
                            mods.Remove(rem);
                        }
                        mods.Insert(chosen.slot, chosen);
                        sList.Add(settlement);
                        sDict = DeserializeSettlementDictionary();
                        if (sDict.ContainsKey(user.Id))
                        {
                            sDict.Remove(user.Id);
                        }
                        sDict.Add(user.Id, sList);
                        Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
                        if (playerChoice == 9)
                        {
                            List<Item> storage = new List<Item>();
                            AllianceStorage aStore = new AllianceStorage(player.alliance, storage);
                            SaveAllianceStorage(aStore);
                        }
                        sb = new StringBuilder();
                        sb.AppendLine($"Congratulations, " + user.Username + "! You have built a " + chosen.type + " in slot number " + (chosen.slot + 1) + " of your " + settlement.name + " settlement!");
                        embed.Title = user.Username + "'s Settlement Developed";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                        break;
                    }
                    else
                    {
                        sb = new StringBuilder();
                        sb.AppendLine(user.Username + ", you don't have enough scrap to build that! You need " + chosen.cost + " scrap to build this module.");
                        embed.Title = user.Username + "'s Settlement Develop Failed";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                }
                else if (mod.slot == playerChoice && mod.type != "null")
                {
                    sb = new StringBuilder();
                    sb.AppendLine(user.Username + ", this slot is already occupied by a " + mod.type + " module. Please try again, and select a different module this time.");
                    embed.Title = user.Username + "'s Settlement Develop Failed";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
            }
        }

        [Command("settlementremovemodule", RunMode = RunMode.Async)]
        [Alias("srm")]
        public async Task SettlementRemoveModuleCommand(string moduleNum, string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();
            var user = Context.User;
            await BeginMethod(user);
            char firstLetter = char.ToUpper(settlementName[0]);
            settlementName = firstLetter + settlementName.Substring(1);
            if (name2 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name2[0]);
                name2 = firstLet + name2.Substring(1);
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name3[0]);
                name3 = firstLet + name3.Substring(1);
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name4[0]);
                name4 = firstLet + name4.Substring(1);
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name5[0]);
                name5 = firstLet + name5.Substring(1);
                settlementName += name5;
            }
            if (settlementName.Contains("-"))
            {
                int dashIndex = settlementName.IndexOf("-");
                char e = char.ToUpper(settlementName[dashIndex + 1]);
                string previousString = settlementName.Substring(0, dashIndex);
                settlementName = previousString + "-" + e + settlementName.Substring(dashIndex + 2);
            }
            var embed = new EmbedBuilder();
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<Settlement> sList = new List<Settlement>();
            if (sDict.ContainsKey(user.Id))
            {
                sList = sDict[user.Id];
            }
            Settlement s = new Settlement();
            s.name = "none";
            foreach (Settlement x in sList)
            {
                if (x.name == settlementName)
                {
                    s = x;
                }
            }
            if (s.name == "none")
            {
                sb.AppendLine($"Either you mistyped the name, or you don't currently own a copy of that settlement! Check to make sure you have already rebuilt it.");
                embed.Title = user.Username + " does not own that settlement.";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                bool validNumber = Int32.TryParse(moduleNum, out int slot);
                slot--;
                if (validNumber)
                {
                    if (slot <= s.totalModules)
                    {
                        int refund = 0;
                        foreach (SettlementModule sm in s.modules)
                        {
                            if (sm.slot == slot)
                            {
                                refund = sm.cost;
                                sm.type = "null";
                                sm.lastCollection = DateTimeOffset.MinValue;
                                sm.level = 0;
                            }
                        }
                        List<Settlement> toRemove = new List<Settlement>();
                        foreach (Settlement x in sList)
                        {
                            if (x.name == s.name)
                            {
                                toRemove.Add(x);
                            }
                        }
                        foreach (Settlement x in toRemove)
                        {
                            sList.Remove(x);
                        }
                        sList.Add(s);
                        sDict.Remove(user.Id);
                        sDict.Add(user.Id, sList);
                        Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
                        Charsheet player = PlayerLoad(user.Id);
                        player.scrap += refund;
                        SavePlayerData(user.Id, player);
                        sb.AppendLine($"Your module in slot {slot} has been cleared! You have been refunded {refund} scrap.");
                        embed.Title = user.Username + " has cleared a module.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                    else
                    {
                        sb.AppendLine($"Your settlement only has {s.totalModules} modules, and you tried to edit the module in slot number {slot}!");
                        embed.Title = user.Username + " entered an invalid slot number.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                }
                else
                {
                    sb.AppendLine($"Please only enter digits 0-9 for slot numbers!");
                    embed.Title = user.Username + " entered an invalid slot number.";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
            }
        }

        [Command("settlementcheckrebuild", RunMode = RunMode.Async)]
        [Alias("scheckr", "checkrebuild")]
        public async Task SettlementCheckRebuildCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            ulong id = user.Id;
            Dictionary<ulong, SettlementPlayerVariables> spvd = DeserializeSettlementVariableDictionary();
            SettlementPlayerVariables spv = new SettlementPlayerVariables();
            if (spvd.ContainsKey(id))
            {
                spv = spvd[id];
                spvd.Remove(id);
            }
            if (spv.isRebuilding)
            {
                DateTimeOffset beganRebuilding = spv.rebuildBegan;
                TimeSpan rebuildTimer = spv.rebuildTimer;
                DateTimeOffset now = DateTimeOffset.Now;
                TimeSpan timeElapsed = now - beganRebuilding;
                if (timeElapsed > rebuildTimer)
                {
                    Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
                    List<Settlement> sList = new List<Settlement>();
                    if (sDict.ContainsKey(id))
                    {
                        sList = sDict[id];
                    }
                    Settlement s = spv.settlementRebuilding;
                    List<SettlementModule> modules = new List<SettlementModule>();
                    SettlementModule modOne = new SettlementModule("null", 0, 0, 1);
                    SettlementModule modTwo = new SettlementModule("null", 0, 0, 2);
                    SettlementModule modThree = new SettlementModule("null", 0, 0, 3);
                    SettlementModule modFour = new SettlementModule("null", 0, 0, 4);
                    SettlementModule modFive = new SettlementModule("null", 0, 0, 5);
                    SettlementModule modSix = new SettlementModule("null", 0, 0, 6);
                    SettlementModule modSeven = new SettlementModule("null", 0, 0, 7);
                    SettlementModule modEight = new SettlementModule("null", 0, 0, 8);
                    SettlementModule modNine = new SettlementModule("null", 0, 0, 9);
                    SettlementModule modTen = new SettlementModule("null", 0, 0, 10);
                    switch (s.totalModules)
                    {
                        case 1:
                            modules.Insert(0, modOne);
                            break;
                        case 2:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            break;
                        case 3:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            break;
                        case 4:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            break;
                        case 5:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            modules.Insert(4, modFive);
                            break;
                        case 6:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            modules.Insert(4, modFive);
                            modules.Insert(5, modSix);
                            break;
                        case 7:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            modules.Insert(4, modFive);
                            modules.Insert(5, modSix);
                            modules.Insert(6, modSeven);
                            break;
                        case 8:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            modules.Insert(4, modFive);
                            modules.Insert(5, modSix);
                            modules.Insert(6, modSeven);
                            modules.Insert(7, modEight);
                            break;
                        case 9:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            modules.Insert(4, modFive);
                            modules.Insert(5, modSix);
                            modules.Insert(6, modSeven);
                            modules.Insert(7, modEight);
                            modules.Insert(8, modNine);
                            break;
                        case 10:
                            modules.Insert(0, modOne);
                            modules.Insert(1, modTwo);
                            modules.Insert(2, modThree);
                            modules.Insert(3, modFour);
                            modules.Insert(4, modFive);
                            modules.Insert(5, modSix);
                            modules.Insert(6, modSeven);
                            modules.Insert(7, modEight);
                            modules.Insert(8, modNine);
                            modules.Insert(9, modTen);
                            break;
                    }
                    s.isIntact = true;
                    sList.Add(s);
                    sb.AppendLine(user.Username + ", your " + s.name + " settlement has finished rebuilding! Don't forget to check out =sdevelop to make your settlement better!");
                    sDict.Remove(id);
                    sDict.Add(id, sList);
                    Serialize(sDict, "\\Settlements\\PlayerSettlementDictionary");
                    spv.isRebuilding = false;
                    spvd.Add(id, spv);
                    Serialize(spvd, "\\Settlements\\SettlementPlayerVariables");
                }
                else
                {
                    TimeSpan timeRemaining = rebuildTimer - timeElapsed;
                    sb.AppendLine(user.Username + ", your rebuild still has " + timeRemaining.Minutes + " minutes, " + timeRemaining.Seconds + " seconds remaining before it's complete! Check back later.");
                }
            }
            else
            {
                sb.AppendLine(user.Username + ", you're not currently rebuliding any derelict settlements! Please find one in foraging and rebuild it with =srebuild to start this process.");
            }
            embed.Title = user.Username + "'s Rebuild Check";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("moduleinfo", RunMode = RunMode.Async)]
        [Alias("modinfo")]
        public async Task ModuleInfoCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            // build out the reply
            var user = Context.User;
            await BeginMethod(user);

            sb.AppendLine($"1. Garden. Cost: 50 Scrap. +5 Food every 20 minutes. Can hold a maximum of 50 food before it's full. Costs: 50 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"2. Water Collector. Cost: 50 Scrap. +5 Water every 20 minutes. Can hold a maximum of 50 water before it's full. Costs: 50 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"3. Medical Tent. Cost: 50 Scrap. +5 Health every 20 minutes. Costs: 50 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"4. Ammunition Press. Cost: 100 Scrap. +5 Ammo every 20 minutes. Can hold a maximum of 50 ammo before it's full. Cost: 100 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"5. Watchtower. Cost: 150 Scrap. +10 Defense for your settlement. Cost: 150 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"6. Chain-link Fence. Cost: 50 Scrap. +5 Defense for your settlement. Cost: 50 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"7. Barracks. Cost: 150 Scrap. Allows up to 2 random people in your alliance to gain the benefits of your modules every turn. Cost: 150 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"8. Recycling Station. Cost: 100 Scrap. +5 Scrap every 20 minutes. Can hold a maximum of 50 scrap before it's full. Costs: 100 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"9. Storage Area. Cost: 150 Scrap. Allows you to store up to 5 types of item in the settlement storage area, where anyone in the alliance can access it. Cost: 150 scrap.");
            sb.AppendLine($"");
            sb.AppendLine($"10. Workshop. Cost: 50 Scrap. Allows you to make an Intelligence roll to add or remove tags from weapons.");
            sb.AppendLine($"");

            // set embed fields
            embed.Title = "Module Info";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("settlementcollect", RunMode = RunMode.Async)]
        [Alias("scollect")]
        public async Task SettlementCollectCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            Charsheet player = PlayerLoad(user.Id);
            Dictionary<ulong, PlayerInventory> pid = DeserializePlayerInventoryDictionary();
            PlayerInventory userInv = new PlayerInventory();
            if (pid.ContainsKey(user.Id))
            {
                userInv = pid[user.Id];
            }
            List<Settlement> sList = new List<Settlement>();
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            if (sDict.ContainsKey(user.Id))
            {
                sList = sDict[user.Id];
            }
            int grossProduct = 0;
            Dictionary<ulong, List<Item>> benefactorsInvLists = new Dictionary<ulong, List<Item>>();
            Dictionary<ulong, Charsheet> benefactorsCharsheets = new Dictionary<ulong, Charsheet>();
            foreach (Settlement s in sList)
            {
                sb.AppendLine($"You check the collection boxes at your " + s.name + " settlement...");
                Dictionary<ulong, PlayerInventory> beneInvs = new Dictionary<ulong, PlayerInventory>();
                Dictionary<ulong, Charsheet> beneCharsheets = new Dictionary<ulong, Charsheet>();
                Dictionary<ulong, List<Item>> beneInvLists = new Dictionary<ulong, List<Item>>();
                beneInvs.Add(user.Id, userInv);
                beneInvLists.Add(user.Id, userInv.items);
                beneCharsheets.Add(user.Id, player);
                if (s.isUnderAttack)
                {
                    sb.AppendLine($"Your " + s.name + " settlement is under attack by a horde! You'll have to fight them off before you can collect anything!");
                }
                else
                {
                    if (s.modules != null)
                    {
                        List<SettlementModule> smList = s.modules;
                        TimeSpan coolDown = new TimeSpan(0, 20, 0);
                        foreach (SettlementModule sm in smList)
                        {
                            if (sm.lastCollection == DateTimeOffset.MinValue)
                            {
                                sm.lastCollection = DateTimeOffset.Now - coolDown;
                            }
                            if (sm.type == "barracks")
                            {
                                Dictionary<ulong, Charsheet> playerDict = DeserializeCharDictionary();
                                List<KeyValuePair<ulong, Charsheet>> playerDictList = playerDict.ToList();
                                List<KeyValuePair<ulong, Charsheet>> allianceMembers = new List<KeyValuePair<ulong, Charsheet>>();
                                foreach (KeyValuePair<ulong, Charsheet> kvp in playerDictList)
                                {
                                    if (kvp.Value.alliance == player.alliance)
                                    {
                                        KeyValuePair<ulong, Charsheet> member = new KeyValuePair<ulong, Charsheet>(kvp.Key, kvp.Value);
                                        if (!allianceMembers.Contains(member))
                                        {
                                            allianceMembers.Add(member);
                                        }
                                    }
                                }
                                Random rand = new Random();
                                int totalBenefits = sm.level * 2;
                                int cnt = 0;
                                if (allianceMembers.Count > 2)
                                {
                                    while (cnt < totalBenefits)
                                    {
                                        bool success = false;
                                        while (!success)
                                        {
                                            int selectedIndex = rand.Next(0, allianceMembers.Count);
                                            KeyValuePair<ulong, Charsheet> p = allianceMembers.ElementAt(selectedIndex);
                                            if (!beneCharsheets.ContainsKey(p.Key) && !beneInvs.ContainsKey(p.Key) && !beneInvLists.ContainsKey(p.Key))
                                            {
                                                beneCharsheets.Add(p.Key, p.Value);
                                                PlayerInventory kvpInv = new PlayerInventory();
                                                if (pid.ContainsKey(p.Key))
                                                {
                                                    kvpInv = pid[p.Key];
                                                }
                                                beneInvs.Add(p.Key, kvpInv);
                                                beneInvLists.Add(p.Key, kvpInv.items);
                                                cnt++;
                                                success = true;
                                            }
                                        }
                                    }
                                }
                                else if (allianceMembers.Count == 2)
                                {
                                    int selectedIndex = 0;
                                    while (selectedIndex < 2)
                                    {
                                        KeyValuePair<ulong, Charsheet> p = allianceMembers.ElementAt(selectedIndex);
                                        if (!beneCharsheets.ContainsKey(p.Key) && !beneInvs.ContainsKey(p.Key) && !beneInvLists.ContainsKey(p.Key))
                                        {
                                            beneCharsheets.Add(p.Key, p.Value);
                                            PlayerInventory kvpInv = new PlayerInventory();
                                            if (pid.ContainsKey(p.Key))
                                            {
                                                kvpInv = pid[p.Key];
                                            }
                                            beneInvs.Add(p.Key, kvpInv);
                                            beneInvLists.Add(p.Key, kvpInv.items);
                                        }
                                        selectedIndex++;
                                    }
                                }
                                else if (allianceMembers.Count == 1)
                                {
                                    KeyValuePair<ulong, Charsheet> p = allianceMembers.ElementAt(0);
                                    if (!beneCharsheets.ContainsKey(p.Key) && !beneInvs.ContainsKey(p.Key) && !beneInvLists.ContainsKey(p.Key))
                                    {
                                        beneCharsheets.Add(p.Key, p.Value);
                                        PlayerInventory kvpInv = new PlayerInventory();
                                        if (pid.ContainsKey(p.Key))
                                        {
                                            kvpInv = pid[p.Key];
                                        }
                                        beneInvs.Add(p.Key, kvpInv);
                                        beneInvLists.Add(p.Key, kvpInv.items);
                                    }
                                }
                            }
                        }
                        foreach (SettlementModule sm in smList)
                        { 
                            if (sm.type == "garden")
                            {
                                int totalProduced = 0;
                                foreach (List<Item> inv in beneInvLists.Values)
                                {
                                    foreach (Item i in inv)
                                    {
                                        if (i.GetItemName() == "Food")
                                        {
                                            DateTimeOffset now = DateTimeOffset.Now;
                                            TimeSpan diff = now - sm.lastCollection;
                                            if (diff > coolDown)
                                            {
                                                sm.lastCollection = DateTimeOffset.Now;
                                                double diffM = diff.TotalMinutes;
                                                int diffMin = Convert.ToInt32(Math.Floor(diffM));
                                                double timesFilled = diffMin / coolDown.Minutes;
                                                int wholeTimesFilled = Convert.ToInt32(Math.Floor(timesFilled));
                                                if (wholeTimesFilled > 10)
                                                {
                                                    wholeTimesFilled = 10;
                                                }
                                                if (wholeTimesFilled > 1)
                                                {
                                                    i.itemQty += (5 * wholeTimesFilled);
                                                }
                                                else if (wholeTimesFilled == 1)
                                                {
                                                    i.itemQty += 5;
                                                }
                                                totalProduced = 5 * wholeTimesFilled;
                                                grossProduct += totalProduced;
                                            }
                                        }
                                    }
                                }
                                if (totalProduced > 0)
                                {
                                    sb.AppendLine("Your garden produced " + totalProduced + " food!");
                                }
                            }
                            if (sm.type == "water collector")
                            {
                                int totalProduced = 0;
                                foreach (List<Item> inv in beneInvLists.Values)
                                {
                                    foreach (Item i in inv)
                                    {
                                        if (i.GetItemName() == "Water")
                                        {
                                            DateTimeOffset now = DateTimeOffset.Now;
                                            TimeSpan diff = now - sm.lastCollection;
                                            if (diff > coolDown)
                                            {
                                                sm.lastCollection = DateTimeOffset.Now;
                                                double diffM = diff.TotalMinutes;
                                                int diffMin = Convert.ToInt32(Math.Floor(diffM));
                                                double timesFilled = diffMin / coolDown.Minutes;
                                                int wholeTimesFilled = Convert.ToInt32(Math.Floor(timesFilled));
                                                if (wholeTimesFilled > 10)
                                                {
                                                    wholeTimesFilled = 10;
                                                }
                                                if (wholeTimesFilled > 1)
                                                {
                                                    i.itemQty += (5 * wholeTimesFilled);
                                                }
                                                else if (wholeTimesFilled == 1)
                                                {
                                                    i.itemQty += 5;
                                                }
                                                totalProduced = 5 * wholeTimesFilled;
                                                grossProduct += totalProduced;
                                            }
                                        }
                                    }
                                }
                                if (totalProduced > 0)
                                {
                                    sb.AppendLine("Your water collector collected " + totalProduced + " water!");
                                }
                            }
                            if (sm.type == "medical tent")
                            {
                                int playerPrevHealth = player.health;
                                string playerName = player.playerName;
                                int playerNowHealth = 0;
                                int totalHealthCollected = 0;
                                foreach (Charsheet benePlayer in beneCharsheets.Values)
                                {
                                    DateTimeOffset now = DateTimeOffset.Now;
                                    TimeSpan diff = now - sm.lastCollection;
                                    if (diff > coolDown)
                                    {
                                        sm.lastCollection = DateTimeOffset.Now;
                                        int prevHealth = benePlayer.health;
                                        double diffM = diff.TotalMinutes;
                                        int diffMin = Convert.ToInt32(Math.Floor(diffM));
                                        double timesFilled = diffMin / coolDown.Minutes;
                                        int wholeTimesFilled = Convert.ToInt32(Math.Floor(timesFilled));
                                        if (wholeTimesFilled > 10)
                                        {
                                            wholeTimesFilled = 10;
                                        }
                                        totalHealthCollected += wholeTimesFilled;
                                        if (wholeTimesFilled > 1)
                                        {
                                            benePlayer.health += (5 * wholeTimesFilled);
                                            if (benePlayer.health > 100)
                                            {
                                                benePlayer.health = 100;
                                            }
                                        }
                                        else if (wholeTimesFilled == 1)
                                        {
                                            benePlayer.health += 5;
                                            if (benePlayer.health > 100)
                                            {
                                                benePlayer.health = 100;
                                            }
                                        }
                                        if (benePlayer.playerName == playerName)
                                        {
                                            playerNowHealth = benePlayer.health;
                                        }
                                    }
                                }
                                if (totalHealthCollected > 0)
                                {
                                    if (playerPrevHealth == 100)
                                    {
                                        sb.AppendLine("Your medical tent attempted to heal you, but you were already uninjured!");
                                    }
                                    else
                                    {
                                        sb.AppendLine("Your medical tent healed you for " + (playerNowHealth - playerPrevHealth) + " health!");
                                    }
                                    grossProduct += totalHealthCollected;
                                    if (beneCharsheets.Count > 1)
                                    {
                                        sb.AppendLine($"It has also healed " + beneCharsheets.Count + " of your alliance members!");
                                    }
                                }
                            }
                            if (sm.type == "ammunition press")
                            {
                                int totalProduced = 0;
                                foreach (List<Item> inv in beneInvLists.Values)
                                {
                                    foreach (Item i in inv)
                                    {
                                        if (i.GetItemName() == "Ammo")
                                        {
                                            DateTimeOffset now = DateTimeOffset.Now;
                                            TimeSpan diff = now - sm.lastCollection;
                                            if (diff > coolDown)
                                            {
                                                sm.lastCollection = DateTimeOffset.Now;
                                                double diffM = diff.TotalMinutes;
                                                int diffMin = Convert.ToInt32(Math.Floor(diffM));
                                                double timesFilled = diffMin / coolDown.Minutes;
                                                int wholeTimesFilled = Convert.ToInt32(Math.Floor(timesFilled));
                                                if (wholeTimesFilled > 10)
                                                {
                                                    wholeTimesFilled = 10;
                                                }
                                                if (wholeTimesFilled > 1)
                                                {
                                                    i.itemQty += (5 * wholeTimesFilled);
                                                }
                                                else if (wholeTimesFilled == 1)
                                                {
                                                    i.itemQty += 5;
                                                }
                                                totalProduced = 5 * wholeTimesFilled;
                                                grossProduct += totalProduced;
                                            }
                                        }
                                    }
                                }
                                if (totalProduced > 0)
                                {
                                    sb.AppendLine("Your ammunition press produced " + totalProduced + " ammo!");
                                }
                            }
                            if (sm.type == "recycling station")
                            {
                                int totalProduced = 0;
                                foreach (Charsheet benePlayer in beneCharsheets.Values)
                                {
                                    DateTimeOffset now = DateTimeOffset.Now;
                                    TimeSpan diff = now - sm.lastCollection;
                                    if (diff > coolDown)
                                    {
                                        sm.lastCollection = DateTimeOffset.Now;
                                        double diffM = diff.TotalMinutes;
                                        int diffMin = Convert.ToInt32(Math.Floor(diffM));
                                        double timesFilled = diffMin / coolDown.Minutes;
                                        int wholeTimesFilled = Convert.ToInt32(Math.Floor(timesFilled));
                                        if (wholeTimesFilled > 10)
                                        {
                                            wholeTimesFilled = 10;
                                        }
                                        if (wholeTimesFilled > 1)
                                        {
                                            benePlayer.scrap += (5 * wholeTimesFilled);
                                        }
                                        else if (wholeTimesFilled == 1)
                                        {
                                            benePlayer.scrap += 5;
                                        }
                                        totalProduced = 5 * wholeTimesFilled;
                                        grossProduct += totalProduced;
                                    }
                                }
                                if (totalProduced > 0)
                                {
                                    sb.AppendLine("Your recycling station recycled " + totalProduced + " scrap!");
                                }
                            }
                        }
                    }
                }
                foreach (KeyValuePair<ulong, Charsheet> x in beneCharsheets)
                {
                    if (!benefactorsCharsheets.ContainsKey(x.Key))
                    {
                        benefactorsCharsheets.Add(x.Key, x.Value);
                    }
                    else
                    {
                        benefactorsCharsheets.Remove(x.Key);
                        benefactorsCharsheets.Add(x.Key, x.Value);
                    }
                }
                foreach (KeyValuePair<ulong, List<Item>> x in beneInvLists)
                {
                    if (!benefactorsInvLists.ContainsKey(x.Key))
                    {
                        benefactorsInvLists.Add(x.Key, x.Value);
                    }
                    else
                    {
                        benefactorsInvLists.Remove(x.Key);
                        benefactorsInvLists.Add(x.Key, x.Value);
                    }
                }
                sb.AppendLine($"");
            }
            foreach (KeyValuePair<ulong, List<Item>> x in benefactorsInvLists)
            {
                if (pid.ContainsKey(x.Key))
                {
                    pid.Remove(x.Key);
                }
                IUser y = await Context.Guild.GetUserAsync(x.Key);
                PlayerInventory bInv = new PlayerInventory(y.Username, x.Value, x.Key);
                pid.Add(x.Key, bInv);
            }
            foreach (KeyValuePair<ulong, Charsheet> x in benefactorsCharsheets)
            {
                SavePlayerData(x.Key, x.Value);
            }
            if (sDict.ContainsKey(user.Id))
            {
                sDict.Remove(user.Id);
            }
            sDict.Add(user.Id, sList);
            Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
            Serialize(pid, "playerInventoryDictionary");
            // set embed fields
            embed.Title = user.Username + " Settlement Collection";
            if (grossProduct == 0)
            {
                sb.AppendLine($"None of your settlements are ready for harvesting yet. Come back later!");
            }
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("storagelist", RunMode = RunMode.Async)]
        public async Task StorageListCommand()
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;
            Charsheet player = PlayerLoad(user.Id);
            AllianceStorage aStore = FetchAllianceStorage(player.alliance);
            List<Item> storageItems = aStore.storage;
            string header = $"" + aStore.allianceName + "'s Alliance Inventory";
            List<string> nonConsumables = new List<string>();
            string dashes = "";
            int charCount = 25;
            string d = "-";
            int cnt = 0;
            while (charCount > 0)
            {
                dashes = dashes.Insert(cnt, d);
                cnt++;
                charCount--;
            }
            sb.AppendLine($"" + dashes);
            foreach (Item item in storageItems)
            {
                if (item.GetItemName() == "Food")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else if (item.GetItemName() == "Water")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else if (item.GetItemName() == "Meds")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else if (item.GetItemName() == "Ammo")
                {
                    sb.AppendLine($"" + item.GetItemName() + ": " + item.GetItemQty());
                }
                else
                {
                    nonConsumables.Add(item.GetItemName());
                }
            }
            nonConsumables.Sort();
            List<Item> sortedNonConsumables = new List<Item>();
            foreach (string name in nonConsumables)
            {
                foreach (Item item in storageItems)
                {
                    if (item.GetItemName() == name)
                    {
                        sortedNonConsumables.Add(item);
                    }
                }
            }
            foreach (Item nC in sortedNonConsumables)
            {
                string str = nC.GetItemName();
                char firstLet = char.ToUpper(str[0]);
                str = firstLet + str.Substring(1);
                sb.AppendLine($"" + str + ": " + nC.GetItemQty());
            }
            sb.AppendLine($"" + dashes);
            // set embed fields
            embed.Title = header;
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));

            // send simple string reply
            await ReplyAsync(null, false, embed.Build());
        }

        [Command("storagefetch", RunMode = RunMode.Async)]
        [Alias("fetch")]
        public async Task StorageFetchCommand(string quantity, string itemName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;
            if (name2 != null)
            {
                itemName += " ";
                itemName += name2;
            }
            if (name3 != null)
            {
                itemName += " ";
                itemName += name3;
            }
            if (name4 != null)
            {
                itemName += " ";
                itemName += name4;
            }
            if (name5 != null)
            {
                itemName += " ";
                itemName += name5;
            }
            bool validQty = Int32.TryParse(quantity, out int qty);
            if (validQty)
            {
                Charsheet player = PlayerLoad(user.Id);
                AllianceStorage aStore = FetchAllianceStorage(player.alliance);
                if (aStore.allianceName != null)
                {
                    Item i = new Item();
                    i.itemName = "none";
                    foreach (Item x in aStore.storage)
                    {
                        if (x.GetItemName() == itemName)
                        {
                            i = x;
                        }
                    }
                    if (i.itemName != "none")
                    {
                        if (i.itemQty >= qty)
                        {
                            foreach (Item x in aStore.storage)
                            {
                                if (x.itemName == itemName)
                                {
                                    x.itemQty -= qty;
                                }
                            }
                            SaveAllianceStorage(aStore);
                            InventoryAppend(user.Id, itemName, qty);
                            sb.AppendLine($"{qty} of {itemName} have been retrieved from {player.alliance}'s alliance storage.");
                            embed.Title = $"{user.Username} has retrieved an item from alliance storage.";
                            embed.Description = sb.ToString();
                            embed.WithColor(new Color(0, 255, 0));
                            await ReplyAsync(null, false, embed.Build());
                        }
                        else
                        {
                            sb.AppendLine($"Your alliance storage doesn't have {quantity} of {itemName} to fetch!");
                            embed.Title = $"{ user.Username}, invalid selection.";
                            embed.Description = sb.ToString();
                            embed.WithColor(new Color(0, 255, 0));
                            await ReplyAsync(null, false, embed.Build());
                        }
                    }
                    else
                    {
                        sb.AppendLine($"Your alliance storage doesn't have {quantity} of {itemName} to fetch!");
                        embed.Title = $"{ user.Username}, invalid selection.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                }
                else
                {
                    sb.AppendLine($"Your alliance doesn't have a storage unit!");
                    embed.Title = $"{ user.Username}, no alliance storage was found.";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
            }
            else
            {
                sb.AppendLine($"You entered an invalid number for the quantity of the item to be stored. Please enter only digits 0-9.");
                embed.Title = $"{ user.Username}, invalid quantity.";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("storagestore", RunMode = RunMode.Async)]
        [Alias("store")]
        public async Task StorageStoreCommand(string qty, string itemName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;
            if (name2 != null)
            {
                itemName += " ";
                itemName += name2;
            }
            if (name3 != null)
            {
                itemName += " ";
                itemName += name3;
            }
            if (name4 != null)
            {
                itemName += " ";
                itemName += name4;
            }
            if (name5 != null)
            {
                itemName += " ";
                itemName += name5;
            }
            bool validQty = Int32.TryParse(qty, out int quantity);
            if (validQty)
            {
                Charsheet player = PlayerLoad(user.Id);
                PlayerInventory pInv = GetPlayerInventory(user.Id);
                Item x = new Item();
                foreach (Item y in pInv.items)
                {
                    if (y.GetItemName() == itemName)
                    {
                        x = y;
                    }
                }
                if (x.GetItemQty() >= quantity)
                {
                    AllianceStorage aStore = FetchAllianceStorage(player.alliance);
                    if (aStore.allianceName != null)
                    {
                        Item i = new Item(itemName, quantity);
                        aStore.storage.Add(i);
                        foreach (Item y in pInv.items)
                        {
                            if (y.GetItemName() == itemName)
                            {
                                y.itemQty -= quantity;
                            }
                        }
                        SaveAllianceStorage(aStore);
                        sb.AppendLine($"Other people in your alliance can retrieve some of that item using =storagefetch!");
                        sb.AppendLine($"Anyone in your alliance can also see what's in the alliance storage by using =storagelist.");
                        embed.Title = $"{ user.Username}, {quantity} of {itemName} was stored.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                    else
                    {
                        sb.AppendLine($"Your alliance doesn't have a storage unit!");
                        embed.Title = $"{ user.Username}, no alliance storage was found.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                }
                else
                {
                    sb.AppendLine($"You don't have {quantity} of {itemName} to store!");
                    embed.Title = $"{ user.Username}, invalid selection.";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
            }
            else
            {
                sb.AppendLine($"You entered an invalid number for the quantity of the item to be stored. Please enter only digits 0-9.");
                embed.Title = $"{ user.Username}, invalid quantity.";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("shop", RunMode = RunMode.Async)]
        public async Task ShopCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            Dusty dusty = new Dusty();
            dusty = DeserializeDusty();
            if (dusty.stock == null)
            {
                dusty.lastRestock = DateTimeOffset.MinValue;
            }
            Dictionary<ulong, PlayerVariable> pvd = DeserializePlayerVariableDictionary();
            PlayerVariable pv = new PlayerVariable();
            if (pvd.ContainsKey(user.Id))
            {
                pv = pvd[user.Id];
            }
            bool hasVisited = pv.GetHasVisitedShop();
            Emoji buy = new Emoji("\uD83D\uDCE6");
            Emoji sell = new Emoji("\uD83D\uDCB0");
            Emoji cancel = new Emoji("\uD83D\uDEAB");
            List<Emoji> emojis = new List<Emoji>();
            emojis.Add(buy);
            emojis.Add(sell);
            emojis.Add(cancel);
            int waitTimer = 0;
            if (!hasVisited)
            {
                // build out the reply
                sb.AppendLine($"You sneak along the highway to a more tucked-away part of Miami, and come across a huge wall made of large sheets of cut tin.\n");
                sb.AppendLine($"You follow the wall to the east, coming to a place in the wall where, about two meters up, there's a small section of wall cut out. In its place is a sheet of plywood. You knock on the wood three times in quick succession, wait two seconds, and knock once more.\n");
                sb.AppendLine($"After about ten seconds, the wood swings away from the wall, revealing two piercing gray eyes staring back at you from a face full of wrinkles.\n");
                sb.AppendLine($"Recognizing you as a human, the face closes the wood panel, and opens the door to you.\n");
                sb.AppendLine($"The owner of the face you saw through the wall now stands in front of you, hands in her pockets. She's a shorter woman, maybe 5'2\" (or 157 cm), with a closely-shaved head. She's wearing camo overalls and a white t-shirt, with a pair of black combat boots.\n");
                sb.AppendLine($"She stares intensely at you for a moment, as if sizing you up... And then, she sticks her bony, wrinkled hand out to shake yours.\n");
                sb.AppendLine($"\"I'm Dusty,\" she says. \"Welcome to my little slice of heaven!\" She shuts the door behind you, and leads you to a small shack.\n");
                sb.AppendLine($"Inside are a few very useful items, which she explains she'd be more than happy to give to you... for a price.\n");
                sb.AppendLine($"\"I barter in old-world cash only. None of that scrap nonsense. Bring me cash, I'm more than happy to trade with you. If you sell me somethin', I'll give ya a fair price for it, too. Deal?\"\n");
                sb.AppendLine($"You accept, and she smiles and shows you all that the shop has to offer.\n");
                pv.hasVisitedShop = true;
                pvd.Remove(user.Id);
                pvd.Add(user.Id, pv);
                Serialize(pvd, "PlayerVariables");
                waitTimer = 120;
            }
            else
            {
                sb.AppendLine($"You walk up to Dusty's door, and give the signature knock. She opens the plywood window and looks at you, and her face brightens into a smile.");
                sb.AppendLine($"She greets you with her usual happy small talk as you both walk to the store. After pleasantries are exchanged, you get down to business.");
                waitTimer = 60;
            }
            sb.AppendLine($"");
            sb.AppendLine($"[REACT WITH " + buy + " TO SEE THE BUY MENU, OR WITH " + sell + " TO SEE THE SELL MENU]");
            embed.Title = user.Username + " has visited Dusty's Odds and Ends.";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));
            var msg = await ReplyAsync(null, false, embed.Build());
            foreach (Emoji e in emojis)
            {
                await msg.AddReactionAsync(e);
            }
            DateTimeOffset then = msg.Timestamp;
            DateTimeOffset now = DateTimeOffset.Now;
            TimeSpan wait = now - then;
            int waitSecs = wait.Seconds;
            List<IUser> reactors = new List<IUser>();
            bool targetHasReacted = false;
            string playerChoice = null;
            while (waitSecs < waitTimer && !targetHasReacted)
            {
                now = DateTimeOffset.Now;
                IEnumerable<IUser> buys = await msg.GetReactionUsersAsync(buy, 100).FlattenAsync();
                IEnumerable<IUser> sells = await msg.GetReactionUsersAsync(sell, 100).FlattenAsync();
                IEnumerable<IUser> cancels = await msg.GetReactionUsersAsync(cancel, 100).FlattenAsync();
                List<IEnumerable<IUser>> reactionChecks = new List<IEnumerable<IUser>>();
                reactionChecks.Add(buys);
                reactionChecks.Add(sells);
                reactionChecks.Add(cancels);
                int cnt = 1;
                foreach (IEnumerable<IUser> r in reactionChecks)
                {
                    if (r.Count() != 0)
                    {
                        reactors = r.Where(x => x.Id == user.Id).ToList();
                        if (reactors.Count >= 1)
                        {
                            targetHasReacted = true;
                            if (cnt == 1)
                            {
                                playerChoice = "buy";
                            }
                            else if (cnt == 2)
                            {
                                playerChoice = "sell";
                            }
                            else if (cnt == 3)
                            {
                                playerChoice = "cancel";
                            }
                            await msg.DeleteAsync();
                        }
                    }
                    cnt++;
                }
            }
            if (!targetHasReacted)
            {
                sb = new StringBuilder();
                sb.AppendLine(user.Username + ", you failed to react in time. Please try again.");
                embed.Title = user.Username + " has failed to react.";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                msg = await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                sb = new StringBuilder();
                List<string> consumables = new List<string>();
                List<string> weapons = DeserializeWeaponsList();
                List<string> validItems = new List<string>();
                consumables.Add("Water");
                consumables.Add("Food");
                consumables.Add("Meds");
                consumables.Add("Ammo");
                consumables.Add("Scrap");
                weapons.Remove("fists");
                foreach (string c in consumables)
                {
                    validItems.Add(c);
                }
                foreach (string w in weapons)
                {
                    validItems.Add(w);
                }
                if (playerChoice == "buy")
                {
                    now = DateTimeOffset.Now;
                    TimeSpan dustyDiff = now - dusty.lastRestock;
                    ServerData sd = DeserializeServerData();
                    TimeSpan restock = new TimeSpan();
                    TimeSpan n = new TimeSpan(0, 0, 0);
                    if (sd.dustyRestockInterval == n)
                    {
                        restock = new TimeSpan(3, 0, 0);
                    }
                    else
                    {
                        restock = sd.dustyRestockInterval;
                    }
                    if (dustyDiff >= restock)
                    {
                        List<string> choicesSoFar = new List<string>();
                        List<Item> items = new List<Item>();
                        int cnt = 1;
                        string choice = null;
                        while (cnt <= 5)
                        {
                            int qty = 5;
                            int cost = 0;
                            if (cnt <= 3)
                            {
                                Random rand = new Random();
                                bool picked = false;
                                bool endLoop = false;
                                if (choicesSoFar.Count >= 1)
                                {
                                    while (!endLoop)
                                    {
                                        picked = false;
                                        int choiceIndex = rand.Next(0, consumables.Count());
                                        choice = consumables.ElementAt(choiceIndex);
                                        foreach (string c in choicesSoFar)
                                        {
                                            if (c == choice)
                                            {
                                                picked = true;
                                            }
                                        }
                                        if (!picked)
                                        {
                                            choicesSoFar.Add(choice);
                                            endLoop = true;
                                        }
                                    }
                                }
                                else
                                {
                                    int choiceIndex = rand.Next(0, consumables.Count());
                                    choice = consumables.ElementAt(choiceIndex);
                                    choicesSoFar.Add(choice);
                                }
                                qty = rand.Next(25, 50);
                                cost = 1;
                            }
                            else
                            {
                                Random rand = new Random();
                                int choiceIndex = rand.Next(0, weapons.Count());
                                choice = weapons.ElementAt(choiceIndex);
                                Dictionary<string, Weapon> wDict = DeserializeWeaponDictionary();
                                Weapon w = new Weapon();
                                if (wDict.ContainsKey(choice))
                                {
                                    w = wDict[choice];
                                }
                                else
                                {
                                    break;
                                }
                                int dmg = w.damage;
                                if (dmg > 8)
                                {
                                    cost = (dmg - 8) * 25;
                                }
                                else
                                {
                                    cost = 50;
                                }
                            }
                            Item item = new Item(choice, qty, cost);
                            items.Add(item);
                            cnt++;
                        }
                        dusty = new Dusty(items, DateTimeOffset.Now);
                        Serialize(dusty, "shop");
                    }
                    dusty = DeserializeDusty();
                    sb.AppendLine($"On the table and available for purchase are:");
                    sb.AppendLine($"");
                    int count = 1;
                    foreach (Item i in dusty.stock)
                    {
                        sb.AppendLine(count + ". " + i.GetItemName() + ", Price: " + i.cost + ". Quantity Available: " + i.GetItemQty());
                        count++;
                    }
                    sb.AppendLine($"");
                    sb.AppendLine($"[TO PURCHASE AN ITEM, REACT WITH THE NUMBER NEXT TO THE ITEM YOU WISH TO BUY]");
                    embed.Title = user.Username + " has chosen \"Buy\".";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    msg = await ReplyAsync(null, false, embed.Build());
                    var one = new Emoji("\u0031\u20e3");
                    var two = new Emoji("\u0032\u20e3");
                    var three = new Emoji("\u0033\u20e3");
                    var four = new Emoji("\u0034\u20e3");
                    var five = new Emoji("\u0035\u20e3");
                    cancel = new Emoji("\uD83D\uDEAB");
                    emojis = new List<Emoji>();
                    switch (dusty.stock.Count)
                    {
                        case 1:
                            emojis.Add(one);
                            break;
                        case 2:
                            emojis.Add(one);
                            emojis.Add(two);
                            break;
                        case 3:
                            emojis.Add(one);
                            emojis.Add(two);
                            emojis.Add(three);
                            break;
                        case 4:
                            emojis.Add(one);
                            emojis.Add(two);
                            emojis.Add(three);
                            emojis.Add(four);
                            break;
                        case 5:
                            emojis.Add(one);
                            emojis.Add(two);
                            emojis.Add(three);
                            emojis.Add(four);
                            emojis.Add(five);
                            break;
                    }
                    emojis.Add(cancel);
                    foreach (Emoji e in emojis)
                    {
                        await msg.AddReactionAsync(e);
                    }
                    Thread.Sleep(5000);
                    then = msg.Timestamp;
                    now = DateTimeOffset.Now;
                    wait = now - then;
                    waitSecs = wait.Seconds;
                    reactors = new List<IUser>();
                    targetHasReacted = false;
                    int intChoice = 0;
                    while (waitSecs < 60 && !targetHasReacted)
                    {
                        now = DateTimeOffset.Now;
                        List<IEnumerable<IUser>> reactionChecks = new List<IEnumerable<IUser>>();
                        switch (dusty.stock.Count)
                        {
                            case 1:
                                IEnumerable<IUser> ones = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                                reactionChecks.Add(ones);
                                break;
                            case 2:
                                ones = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                                IEnumerable<IUser> twos = await msg.GetReactionUsersAsync(two, 100).FlattenAsync();
                                reactionChecks.Add(ones);
                                reactionChecks.Add(twos);
                                break;
                            case 3:
                                ones = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                                twos = await msg.GetReactionUsersAsync(two, 100).FlattenAsync();
                                IEnumerable<IUser> threes = await msg.GetReactionUsersAsync(three, 100).FlattenAsync();
                                reactionChecks.Add(ones);
                                reactionChecks.Add(twos);
                                reactionChecks.Add(threes);
                                break;
                            case 4:
                                ones = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                                twos = await msg.GetReactionUsersAsync(two, 100).FlattenAsync();
                                threes = await msg.GetReactionUsersAsync(three, 100).FlattenAsync();
                                IEnumerable<IUser> fours = await msg.GetReactionUsersAsync(four, 100).FlattenAsync();
                                reactionChecks.Add(ones);
                                reactionChecks.Add(twos);
                                reactionChecks.Add(threes);
                                reactionChecks.Add(fours);
                                break;
                            case 5:
                                ones = await msg.GetReactionUsersAsync(one, 100).FlattenAsync();
                                twos = await msg.GetReactionUsersAsync(two, 100).FlattenAsync();
                                threes = await msg.GetReactionUsersAsync(three, 100).FlattenAsync();
                                fours = await msg.GetReactionUsersAsync(four, 100).FlattenAsync();
                                IEnumerable<IUser> fives = await msg.GetReactionUsersAsync(five, 100).FlattenAsync();
                                reactionChecks.Add(ones);
                                reactionChecks.Add(twos);
                                reactionChecks.Add(threes);
                                reactionChecks.Add(fours);
                                reactionChecks.Add(fives);
                                break;
                        }
                        IEnumerable<IUser> cancels = await msg.GetReactionUsersAsync(cancel, 100).FlattenAsync();
                        reactionChecks.Add(cancels);
                        int cnt = 1;
                        foreach (IEnumerable<IUser> r in reactionChecks)
                        {
                            if (r.Count() != 0)
                            {
                                reactors = r.Where(z => z.Id == user.Id).ToList();
                                if (reactors.Count >= 1)
                                {
                                    targetHasReacted = true;
                                    intChoice = cnt;
                                    await msg.DeleteAsync();
                                }
                            }
                            cnt++;
                        }
                    }
                    if (!targetHasReacted)
                    {
                        sb = new StringBuilder();
                        sb.AppendLine(user.Username + ", you failed to react in time. Please try again.");
                        embed.Title = user.Username + " has failed to react.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        msg = await ReplyAsync(null, false, embed.Build());
                    }
                    else if (intChoice == emojis.Count)
                    {
                        sb = new StringBuilder();
                        sb.AppendLine("Shop command has been terminated.");
                        embed.Title = user.Username + " Shop canceled.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                    else
                    {
                        Item chosen = dusty.stock.ElementAt(intChoice - 1);
                        sb = new StringBuilder();
                        var channel = Context.Channel;
                        sb.AppendLine(user.Username + ", please type below the quantity you wish to purchase!");
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        msg = await ReplyAsync(null, false, embed.Build());
                        then = msg.Timestamp;
                        now = DateTimeOffset.Now;
                        wait = now - then;
                        waitSecs = wait.Seconds;
                        bool userHasResponded = false;
                        bool hasCanceled = false;
                        string content = null;
                        while (waitSecs < 60 && !userHasResponded)
                        {
                            now = DateTimeOffset.Now;
                            IEnumerable<IMessage> msgs = await channel.GetMessagesAsync(1).FlattenAsync();
                            foreach (IMessage m in msgs)
                            {
                                ulong senderID = m.Author.Id;
                                if (senderID == user.Id)
                                {
                                    string cancelTest = m.Content.ToLower();
                                    if (cancelTest != "cancel")
                                    {
                                        content = m.Content;
                                        userHasResponded = true;
                                        await msg.DeleteAsync();
                                    }
                                    else
                                    {
                                        hasCanceled = true;
                                        await msg.DeleteAsync();
                                    }
                                }
                            }
                        }
                        if (!hasCanceled)
                        {
                            bool validEntry = Int32.TryParse(content, out int chosenQty);
                            if (validEntry)
                            {
                                if (chosenQty <= chosen.GetItemQty())
                                {
                                    Charsheet player = PlayerLoad(user.Id);
                                    int itemCost = chosen.cost * chosenQty;
                                    if (player.cash >= itemCost)
                                    {
                                        player.cash -= itemCost;
                                        if (chosen.GetItemName() == "Scrap")
                                        {
                                            player.scrap += chosen.itemQty;
                                        }
                                        else
                                        {
                                            Dictionary<ulong, PlayerInventory> pIDict = DeserializePlayerInventoryDictionary();
                                            PlayerInventory pInv = new PlayerInventory();
                                            if (pIDict.ContainsKey(user.Id))
                                            {
                                                pInv = pIDict[user.Id];
                                                pIDict.Remove(user.Id);
                                            }
                                            bool hasItem = false;
                                            foreach (Item i in pInv.items)
                                            {
                                                if (i.GetItemName() == chosen.GetItemName())
                                                {
                                                    i.itemQty += chosen.itemQty;
                                                    hasItem = true;
                                                }
                                            }
                                            if (!hasItem)
                                            {
                                                pInv.items.Add(chosen);
                                            }
                                            pIDict.Add(user.Id, pInv);
                                            Serialize(pIDict, "playerInventoryDictionary");
                                        }
                                        SavePlayerData(user.Id, player);
                                        dusty.stock.ElementAt(intChoice - 1).itemQty -= chosenQty;
                                        List<Item> toRemove = new List<Item>();
                                        foreach (Item i in dusty.stock)
                                        {
                                            if (i.GetItemQty() <= 0)
                                            {
                                                toRemove.Add(i);
                                            }
                                        }
                                        foreach (Item i in toRemove)
                                        {
                                            dusty.stock.Remove(i);
                                        }
                                        Serialize(dusty, "shop");
                                        sb = new StringBuilder();
                                        sb.AppendLine($"Dusty shakes your hand with a friendly smile and takes your cash. She hands you your item.");
                                        sb.AppendLine($"\"Pleasure doing business with ya.\"");
                                        embed.Title = user.Username + " has purchased " + chosenQty + " of " + chosen.GetItemName();
                                        embed.Description = sb.ToString();
                                        embed.WithColor(new Color(0, 255, 0));
                                        await ReplyAsync(null, false, embed.Build());
                                    }
                                    else
                                    {
                                        sb = new StringBuilder();
                                        sb.AppendLine($"Dusty furrows her brow in confusion.");
                                        sb.AppendLine($"\"Looks like ya didn't bring enough cash with ya. Come back later with enough to pay for the merch, sound good?\"");
                                        embed.Title = user.Username + " doesn't have enough cash to purchase this item.";
                                        embed.Description = sb.ToString();
                                        embed.WithColor(new Color(0, 255, 0));
                                        await ReplyAsync(null, false, embed.Build());
                                    }
                                }
                                else
                                {
                                    sb = new StringBuilder();
                                    sb.AppendLine($"Dusty furrows her brow in confusion.");
                                    sb.AppendLine($"\"Hold yer horses, there, friend. I don't have that much on me!\"");
                                    embed.Title = user.Username + ", Dusty only has " + chosen.GetItemQty() + " of " + chosen.GetItemName() + " to sell you, and you tried to buy + " + chosenQty + ".";
                                    embed.Description = sb.ToString();
                                    embed.WithColor(new Color(0, 255, 0));
                                    await ReplyAsync(null, false, embed.Build());
                                }
                            }
                            else
                            {
                                sb = new StringBuilder();
                                sb.AppendLine($"You didn't enter a valid quantity to buy! Please try again, and enter a numerical amount (e.g. 1, 2, 3, etc.)");
                                embed.Title = user.Username + ", invalid quantity.";
                                embed.Description = sb.ToString();
                                embed.WithColor(new Color(0, 255, 0));
                                await ReplyAsync(null, false, embed.Build());
                            }
                        }
                        else
                        {
                            sb = new StringBuilder();
                            sb.AppendLine("Shop command has been terminated.");
                            embed.Title = user.Username + " Shop canceled.";
                            embed.Description = sb.ToString();
                            embed.WithColor(new Color(0, 255, 0));
                            await ReplyAsync(null, false, embed.Build());
                        }
                    }
                }
                else if (playerChoice == "sell")
                {
                    int price = 50;
                    Dictionary<ulong, PlayerInventory> pIDict = DeserializePlayerInventoryDictionary();
                    PlayerInventory pInv = new PlayerInventory();
                    if (pIDict.ContainsKey(user.Id))
                    {
                        pInv = pIDict[user.Id];
                    }
                    var channel = Context.Channel;
                    string content = null;
                    var userMessage = await channel.SendMessageAsync(user.Username + ", what would you like to sell? If you wish to cancel, type \"cancel\". Note: Format of response must be [quantity], [item name]", false, null, null);
                    then = userMessage.Timestamp;
                    now = DateTimeOffset.Now;
                    wait = now - then;
                    waitSecs = wait.Seconds;
                    bool userHasResponded = false;
                    bool hasCanceled = false;
                    while (waitSecs < 60 && !userHasResponded && !hasCanceled)
                    {
                        now = DateTimeOffset.Now;
                        IEnumerable<IMessage> msgs = await channel.GetMessagesAsync(1).FlattenAsync();
                        foreach (IMessage m in msgs)
                        {
                            string cancelTest = "";
                            ulong senderID = m.Author.Id;
                            if (senderID == user.Id)
                            {
                                content = m.Content;
                                if (content != null)
                                {
                                    cancelTest = content.ToLower();
                                    if (cancelTest == "cancel")
                                    {
                                        hasCanceled = true;
                                    }
                                    userHasResponded = true;
                                    await userMessage.DeleteAsync();
                                }
                            }
                        }
                    }
                    if (!hasCanceled)
                    {
                        int indexOfComma = content.IndexOf(",");
                        string itemName = content.Substring(indexOfComma).ToLower();
                        string quantity = content.Substring(0, (content.Length - itemName.Length));
                        itemName = itemName.Substring(2);
                        if (itemName == "food" || itemName == "water" || itemName == "meds" || itemName == "ammo" || itemName == "cash" | itemName == "scrap")
                        {
                            char firstLet = char.ToUpper(itemName[0]);
                            itemName = firstLet + itemName.Substring(1);
                            price = 1;
                        }
                        bool validQty = Int32.TryParse(quantity, out int qty);
                        validItems = new List<string>();
                        weapons = DeserializeWeaponsList();
                        validItems.Add("Water");
                        validItems.Add("Food");
                        validItems.Add("Meds");
                        validItems.Add("Ammo");
                        validItems.Add("Scrap");
                        weapons.Remove("fists");
                        foreach (string weapon in weapons)
                        {
                            validItems.Add(weapon);
                        }
                        bool choiceIsValid = false;
                        foreach (string item in validItems)
                        {
                            if (itemName == item)
                            {
                                choiceIsValid = true;
                            }
                        }
                        if (choiceIsValid && validQty)
                        {
                            bool hasItem = false;
                            foreach (Item i in pInv.items)
                            {
                                if (i.GetItemName() == itemName)
                                {
                                    if (i.GetItemQty() >= qty)
                                    {
                                        hasItem = true;
                                    }
                                }
                            }
                            if (hasItem)
                            {
                                sb = new StringBuilder();
                                Charsheet player = PlayerLoad(user.Id);
                                player.cash += (price * qty);
                                SavePlayerData(user.Id, player);
                                List<Item> toRemove = new List<Item>();
                                foreach (Item i in pInv.items)
                                {
                                    if (i.GetItemName() == itemName)
                                    {
                                        i.itemQty -= qty;
                                        if (i.GetItemQty() <= 0)
                                        {
                                            toRemove.Add(i);
                                        }
                                    }
                                }
                                foreach (Item i in toRemove)
                                {
                                    pInv.items.Remove(i);
                                }
                                pIDict.Remove(user.Id);
                                pIDict.Add(user.Id, pInv);
                                Serialize(pIDict, "playerInventoryDictionary");
                                sb.AppendLine($"Dusty scratches her chin in deep thought for a minute, before holding out her hand to shake yours. \"Deal.\"");
                                sb.AppendLine($"She hands you $" + (price * qty) + ", and you hand her " + qty + " " + itemName + ".");
                                embed.Title = user.Username + " has sold something to Dusty.";
                                embed.Description = sb.ToString();
                                embed.WithColor(new Color(0, 255, 0));
                                await ReplyAsync(null, false, embed.Build());
                            }
                            else
                            {
                                sb = new StringBuilder();
                                sb.AppendLine(user.Username + ", you don't have that item, or you don't have enough of that item! The command has been canceled.");
                                embed.Title = user.Username + ", item name or quantity not found.";
                                embed.Description = sb.ToString();
                                embed.WithColor(new Color(0, 255, 0));
                                await ReplyAsync(null, false, embed.Build());
                            }
                        }
                        else
                        {
                            sb = new StringBuilder();
                            sb.AppendLine(user.Username + ", your sale message was in an invalid format! Please check your spaces and comma, and only use alphanumeric characters. The command has been canceled.");
                            embed.Title = user.Username + ", invalid input.";
                            embed.Description = sb.ToString();
                            embed.WithColor(new Color(0, 255, 0));
                            await ReplyAsync(null, false, embed.Build());
                        }
                    }
                    else
                    {
                        sb = new StringBuilder();
                        sb.AppendLine("Shop command has been terminated.");
                        embed.Title = user.Username + " Shop canceled.";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                }
                else if (playerChoice == "cancel")
                {
                    sb = new StringBuilder();
                    sb.AppendLine("Shop command has been terminated.");
                    embed.Title = user.Username + " Shop canceled.";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
            }
        }

        [Command("give", RunMode = RunMode.Async)]
        public async Task GiveCommand(string target, string quantity, string itemName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();

            // get user info from the Context
            var user = Context.User;
            await BeginMethod(user);
            if (name2 != null)
            {
                itemName += " ";
                itemName += name2;
            }
            if (name3 != null)
            {
                itemName += " ";
                itemName += name3;
            }
            if (name4 != null)
            {
                itemName += " ";
                itemName += name4;
            }
            if (name5 != null)
            {
                itemName += " ";
                itemName += name5;
            }
            IReadOnlyCollection<ulong> i = Context.Message.MentionedUserIds;
            ulong targetId = 0;
            List<ulong> ids = i.ToList<ulong>();
            foreach (ulong x in ids)
            {
                IGuildUser u = await Context.Guild.GetUserAsync(x);
                targetId = x;
            }
            IUser tUser = await Context.Guild.GetUserAsync(targetId);
            if (itemName == "food" || itemName == "water" || itemName == "meds" || itemName == "ammo" || itemName == "cash" | itemName == "scrap")
            {
                char firstLet = char.ToUpper(itemName[0]);
                itemName = firstLet + itemName.Substring(1);
            }
            bool quantityIsValid = int.TryParse(quantity, out int qty);
            List<string> validItems = new List<string>();
            List<string> weapons = DeserializeWeaponsList();
            validItems.Add("Water");
            validItems.Add("Food");
            validItems.Add("Meds");
            validItems.Add("Ammo");
            validItems.Add("Scrap");
            validItems.Add("Cash");
            foreach (string weapon in weapons)
            {
                validItems.Add(weapon);
            }
            bool choiceIsValid = false;
            foreach (string item in validItems)
            {
                if (itemName == item)
                {
                    choiceIsValid = true;
                }
            }
            bool userHasItem = false;
            if (choiceIsValid && quantityIsValid)
            {
                if (itemName == "Scrap" || itemName == "Cash")
                {
                    Charsheet uChar = PlayerLoad(user.Id);
                    Charsheet tChar = PlayerLoad(targetId);
                    switch (itemName)
                    {
                        case "Scrap":
                            if (uChar.scrap >= qty)
                            {
                                tChar.scrap += qty;
                                uChar.scrap -= qty;
                                userHasItem = true;
                            }
                            break;
                        case "Cash":
                            if (uChar.cash >= qty)
                            {
                                tChar.cash += qty;
                                uChar.cash -= qty;
                                userHasItem = true;
                            }
                            break;
                    }
                    SavePlayerData(targetId, tChar);
                    SavePlayerData(user.Id, uChar);
                }
                else
                {
                    Dictionary<ulong, PlayerInventory> userInv = DeserializePlayerInventoryDictionary();
                    PlayerInventory uInv = new PlayerInventory();
                    if (userInv.ContainsKey(user.Id))
                    {
                        uInv = userInv[user.Id];
                    }
                    foreach (Item it in uInv.items)
                    {
                        if (it.GetItemName() == itemName)
                        {
                            if (it.GetItemQty() >= qty)
                            {
                                userHasItem = true;
                            }
                        }
                    }
                    if (userHasItem)
                    {
                        InventoryAppend(targetId, itemName, qty);
                        int neg = 0 - qty;
                        InventoryAppend(user.Id, itemName, neg);
                    }
                }
                if (userHasItem)
                {
                    sb.AppendLine($"Success! " + user.Username + " has gifted " + qty + " " + itemName + " to " + tUser.Username + "!");
                    embed.Title = user.Username + " wants to give something to " + tUser.Username + ".";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
                else
                {
                    sb.AppendLine($"You don't have enough of that item!");
                    embed.Title = user.Username + " wants to give something to " + tUser.Username + ".";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    await ReplyAsync(null, false, embed.Build());
                }
            }
            else if (!choiceIsValid)
            {
                sb.AppendLine($"Item name is invalid. Please check spacing, and try again.");
                embed.Title = user.Username + " wants to give something to " + tUser.Username + ".";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await ReplyAsync(null, false, embed.Build());
            }
            else if (!quantityIsValid)
            {
                sb.AppendLine($"Item quantity is invalid. Please only use numeric characters (e.g. 1, 2, 3, etc.) and try again.");
                embed.Title = user.Username + " wants to give something to " + tUser.Username + ".";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await ReplyAsync(null, false, embed.Build());
            }
        }

        [Command("defend", RunMode = RunMode.Async)]
        public async Task DefendCommand(string settlementName, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            char firstLetter = char.ToUpper(settlementName[0]);
            settlementName = firstLetter + settlementName.Substring(1);
            if (name2 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name2[0]);
                name2 = firstLet + name2.Substring(1);
                settlementName += name2;
            }
            if (name3 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name3[0]);
                name3 = firstLet + name3.Substring(1);
                settlementName += name3;
            }
            if (name4 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name4[0]);
                name4 = firstLet + name4.Substring(1);
                settlementName += name4;
            }
            if (name5 != null)
            {
                settlementName += " ";
                char firstLet = char.ToUpper(name5[0]);
                name5 = firstLet + name5.Substring(1);
                settlementName += name5;
            }
            if (settlementName.Contains("-"))
            {
                int dashIndex = settlementName.IndexOf("-");
                char e = char.ToUpper(settlementName[dashIndex + 1]);
                string previousString = settlementName.Substring(0, dashIndex);
                settlementName = previousString + "-" + e + settlementName.Substring(dashIndex + 2);
            }
            // get user info from the Context
            var user = Context.User;
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary();
            List<Settlement> sList = new List<Settlement>();
            if (sDict.ContainsKey(user.Id))
            {
                sList = sDict[user.Id];
            }
            bool hasSettlement = false;
            bool isUnderAttack = false;
            Settlement attacked = new Settlement();
            foreach (Settlement s in sList)
            {
                if (s.name == settlementName)
                {
                    hasSettlement = true;
                    if (s.isUnderAttack == true)
                    {
                        isUnderAttack = true;
                        attacked = s;
                    }
                }
            }
            if (hasSettlement && isUnderAttack)
            {
                bool isSuccess;
                Random rand = new Random();
                int defenseFights = rand.Next(1, 3);
                Dictionary<ulong, PlayerVariable> pvDict = DeserializePlayerVariableDictionary();
                PlayerVariable pv = new PlayerVariable(user.Username);
                List<ulong> toRemove = new List<ulong>();
                if (pvDict.ContainsKey(user.Id))
                {
                    pv = pvDict[user.Id];
                    toRemove.Add(user.Id);
                }
                foreach (ulong i in toRemove)
                {
                    pvDict.Remove(i);
                }
                if (pv.defenseFights <= 0)
                {
                    pv.defenseFights = defenseFights;
                }
                else
                {
                    defenseFights = pv.defenseFights;
                }
                pvDict.Add(user.Id, pv);
                Serialize(pvDict, "PlayerVariables");
                if (!pv.isDefending)
                {
                    pv.isDefending = true;
                    pvDict.Remove(user.Id);
                    pvDict.Add(user.Id, pv);
                    Serialize(pvDict, "PlayerVariables");
                    sb.AppendLine($"You climb to a good vantage point in your settlement and get ready for a fight.");
                    sb.AppendLine($"You see a section of the horde stampeding toward your walls. You bring your weapon to bear and grimly await their arrival.");
                    sb.AppendLine($"[You will have {defenseFights} fights to fight before your settlement is complete. You don't have to do them all at once, though!]");
                    embed.Title = user.Username + " Settlement Defense!";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));
                    var msg = await ReplyAsync(null, false, embed.Build());
                    Dictionary<ulong, Participant> allianceDefenders = new Dictionary<ulong, Participant>();
                    Charsheet player = PlayerLoad(user.Id);
                    var allUsers = await Context.Guild.GetUsersAsync();
                    foreach (IGuildUser u in allUsers)
                    {
                        Charsheet uPlayer = PlayerLoad(u.Id);
                        if (uPlayer.alliance == player.alliance)
                        {
                            Participant x = new Participant(u.Username, u.Id, user.Id);
                            allianceDefenders.Add(u.Id, x);
                        }
                    }
                    Serialize(allianceDefenders, "Settlements\\" + player.alliance + "_allianceDefenders");
                    int maxZombies = 0;
                    if (player.GetLevel() <= 10)
                    {
                        maxZombies += 4;
                    }
                    else
                    {
                        int math = player.GetLevel() % 10;
                        if (math != 0)
                        {
                            double mathDouble = player.GetLevel() / 10;
                            math = Convert.ToInt32(Math.Floor(mathDouble));
                        }
                        else
                        {
                            math = player.GetLevel() / 10;
                        }
                        int zombiesToAdd = 4 + math;
                        if (zombiesToAdd > 14)
                        {
                            zombiesToAdd = 15;
                        }
                        maxZombies += zombiesToAdd;
                    }
                    int combatantsRoll = rand.Next(1, 100);
                    int combatants = 0;
                    double percent = combatantsRoll / 100.0;
                    if (combatantsRoll <= 75)
                    {
                        int max = Convert.ToInt32(Math.Round(percent * maxZombies));
                        if (max == 1)
                        {
                            combatants = 1;
                        }
                        else
                        {
                            combatants = rand.Next(1, max);
                        }
                    }
                    else if (combatantsRoll > 75)
                    {
                        if (maxZombies == 1)
                        {
                            combatants = 1;
                        }
                        else
                        {
                            combatants = rand.Next(1, maxZombies);
                        }
                    }
                    sb = new StringBuilder();
                    var channel = Context.Channel;
                    await channel.SendMessageAsync($"{user.Mention} has begun a defense!", false, null, null);
                    sb.Append(FightBuilder(user.Id, combatants));
                    isSuccess = await FightProgress(user.Id, msg, sb, true);
                    pv.defenseFights--;
                    defenseFights = pv.defenseFights;
                    pv.isDefending = false;
                    pvDict.Remove(user.Id);
                    pvDict.Add(user.Id, pv);
                    Serialize(pvDict, "PlayerVariables");
                    if (isSuccess && defenseFights >= 1)
                    {
                        sb = new StringBuilder();
                        sb.AppendLine($"Congratulations on your victory! You've still got " + defenseFights + " fights to go, though, so keep going!");
                        embed.Title = user.Username + " Settlement Defense!";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                    }
                    else if (isSuccess && defenseFights <= 0)
                    {
                        sb = new StringBuilder();
                        sb.AppendLine($"Congratulations on your victory! The last of the zombies have fled the area! Your settlement is safe.");
                        embed.Title = user.Username + " Settlement Defense!";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                        pv.isDefending = false;
                        pv.defenseFights = 0;
                        foreach (Settlement s in sList)
                        {
                            if (attacked.name == s.name)
                            {
                                s.isUnderAttack = false;
                                s.defense = attacked.maxDefense;
                            }
                        }
                        sDict.Remove(user.Id);
                        sDict.Add(user.Id, sList);
                        pvDict.Remove(user.Id);
                        pvDict.Add(user.Id, pv);
                        Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
                        Serialize(pvDict, "PlayerVariables");
                    }
                    else
                    {
                        double maxHPLoss = attacked.maxDefense / 2;
                        int maxHP = Convert.ToInt32(Math.Round(maxHPLoss));
                        int lostHP = rand.Next(1, maxHP);
                        attacked.defense -= lostHP;
                        sb = new StringBuilder();
                        sb.AppendLine($"The zombies have beaten you back for now...");
                        if (attacked.defense > 0)
                        {
                            sb.AppendLine($"Your settlement has lost " + lostHP + " defense! " + attacked.defense + " defense remaining!");
                            sb.AppendLine($"You're still in the fight! Try to defend again!");
                        }
                        else
                        {
                            sb.AppendLine($"Oh no! The zombies managed to break into your settlement!");
                            sb.AppendLine($"You and the other denizens fight as hard as you can, but there's nothing you can do. With a heavy heart, you call a retreat.");
                            sb.AppendLine($"You return weeks later to survey the damage, and you realize that you'd have to rebuild the entire settlement, modules and all, from the ground up.");
                            sb.AppendLine($"You note the location on a map, and return home.\n");
                            sb.AppendLine($"[" + attacked.name + " Map added]");
                            InventoryAppend(user.Id, attacked.name + " Map", 1);
                        }
                        embed.Title = user.Username + " Settlement Defense!";
                        embed.Description = sb.ToString();
                        embed.WithColor(new Color(0, 255, 0));
                        await ReplyAsync(null, false, embed.Build());
                        if (attacked.defense > 0)
                        {
                            foreach (Settlement s in sList)
                            {
                                if (s.name == settlementName)
                                {
                                    s.defense = attacked.defense;
                                }
                            }
                        }
                        else
                        {
                            pv.isDefending = false;
                            pv.defenseFights = 0;
                            pvDict.Remove(user.Id);
                            pvDict.Add(user.Id, pv);
                            List<Settlement> sToRemove = new List<Settlement>();
                            Serialize(pvDict, "PlayerVariables");
                            foreach (Settlement s in sList)
                            {
                                if (s.name == settlementName)
                                {
                                    sToRemove.Add(s);
                                }
                            }
                            foreach (Settlement s in sToRemove)
                            {
                                sList.Remove(s);
                            }
                        }
                        sDict.Remove(user.Id);
                        sDict.Add(user.Id, sList);
                        Serialize(sDict, "Settlements\\PlayerSettlementDictionary");
                    }
                }
                else
                {
                    sb.AppendLine($"Finish your other fight before you try to defend again!");
                    embed.Title = user.Username + ", you're already defending!";
                    embed.Description = sb.ToString();
                    embed.WithColor(new Color(0, 255, 0));

                    // send simple string reply
                    await ReplyAsync(null, false, embed.Build());
                }
            }
            else if (!hasSettlement)
            {
                sb.AppendLine($"Either the settlement you specified doesn't exist, or you don't own it!");

                // set embed fields
                embed.Title = user.Username + ", you don't have that settlement!";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            else if (!isUnderAttack)
            {
                sb.AppendLine($"The settlement you specified is not under attack! Make sure that your settlement was mentioned in the horde spawn message!");

                // set embed fields
                embed.Title = user.Username + ", this settlement isn't under attack!";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));

                // send simple string reply
                await ReplyAsync(null, false, embed.Build());
            }
            Charsheet defender = PlayerLoad(user.Id);
            File.Delete("D:\\Zombot Files\\Servers\\" + Context.Guild.Id + "\\Settlements\\" + defender.alliance + "_allianceDefenders.xml");
        }

        /*

        This template is to easily create new commands. Keep it commented out, and copy and paste it where needed.

        [Command("", RunMode = RunMode.Async)]
        public async Task COMMANDNAMECommand()
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var user = Context.User;
            sb.AppendLine($"");
            embed.Title = "";
            embed.Description = sb.ToString();
            embed.WithColor(new Color(0, 255, 0));
            await ReplyAsync(null, false, embed.Build());
        }
        */

        public async Task BeginMethod(IUser user)
        {
            ulong serverId = Context.Guild.Id;
            string username = user.ToString();
            StringBuilder sb = new StringBuilder();
            EmbedBuilder embed = new EmbedBuilder();
            bool exists = false;
            if (File.Exists("D:\\Zombot Files\\Servers\\" + serverId + "\\playerDictionary.xml"))
            {
                Charsheet playerExists = PlayerLoad(user.Id);
                if (playerExists.GetName() != "")
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                Charsheet player = new Charsheet(username);
                SavePlayerData(user.Id, player);
                string[] items = { "Food", "Water", "Meds", "Ammo" };
                List<Item> playerInventory = new List<Item>();
                foreach (string it in items)
                {
                    Item item = new Item(it, 5);
                    playerInventory.Add(item);
                }
                Item fists = new Item("fists", 1);
                playerInventory.Add(fists);
                ulong id = user.Id;
                PlayerInventory inv = new PlayerInventory(username, playerInventory, id);
                Dictionary<ulong, PlayerInventory> playerInventories = DeserializePlayerInventoryDictionary();
                playerInventories.Add(id, inv);
                Serialize(playerInventories, "playerInventoryDictionary");

                // build out the reply
                sb.AppendLine($"Your beginning resources are... ");
                sb.AppendLine($"Hunger: " + player.hunger);
                sb.AppendLine($"Thirst: " + player.thirst);
                sb.AppendLine($"Health: " + player.health);
                sb.AppendLine($"Infection: " + player.infection);
                sb.AppendLine($"Influence: " + player.influence);
                sb.AppendLine($"Cash: " + player.cash);
                sb.AppendLine($"Scrap: " + player.scrap);
                sb.AppendLine($"Your beginning stats are...");
                sb.AppendLine($"Strength: " + player.strength);
                sb.AppendLine($"Intelligence: " + player.intelligence);
                sb.AppendLine($"Charisma: " + player.charisma);
                sb.AppendLine($"Constitution: " + player.constitution);

                // set embed fields
                embed.Title = $"Welcome to the apocalypse, " + player.playerName;
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await ReplyAsync(null, false, embed.Build());
            }
        }

        public static void Backup(ulong serverID)
        {
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_1"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_1");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_1\\Server Data");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_1\\Settlements");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_2"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_2");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_2\\Server Data");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_2\\Settlements");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_3"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_3");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_3\\Server Data");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_3\\Settlements");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_4"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_4");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_4\\Server Data");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_4\\Settlements");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_5"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_5");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_5\\Server Data");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_5\\Settlements");
            }
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\Server Data");
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\Settlements");
            }
            int totalCount = 0;
            if (!File.Exists("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\count.txt"))
            {
                using (StreamWriter file = new StreamWriter("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\count.txt"))
                {
                    file.Write(1);
                    totalCount = 1;
                }
            }
            else
            {
                string text = File.ReadAllText("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\count.txt");
                using (StreamWriter file = new StreamWriter("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\count.txt"))
                {
                    bool isNumber = Int32.TryParse(text, out totalCount);
                    if (isNumber)
                    {
                        totalCount++;
                        if (totalCount < 100)
                        {
                            file.Write(totalCount);
                        }
                        else
                        {
                            File.Delete("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\count.txt");
                        }
                    }
                }
            }
            bool isHundredth = false;
            if (totalCount >= 100)
            {
                isHundredth = true;
            }
            bool isEmpty = false;
            int fileNumber = 0;
            while (!isEmpty)
            {
                int cnt = 1;
                while (cnt <= 5)
                {
                    string[] files = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_" + cnt);
                    if (files.Count() == 0)
                    {
                        isEmpty = true;
                        fileNumber = cnt;
                    }
                    cnt++;
                }
                if (cnt > 5 && !isEmpty)
                {
                    string[] files = Directory.GetDirectories("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\");
                    Dictionary<DateTime, string> fileAccessTimes = new Dictionary<DateTime, string>();
                    cnt = 1;
                    foreach (string f in files)
                    {
                        if (f != "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm")
                        {
                            fileAccessTimes.Add(Directory.GetLastWriteTime("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_" + cnt), f);
                            cnt++;
                        }
                    }
                    List<DateTime> y = new List<DateTime>();
                    foreach (KeyValuePair<DateTime, string> x in fileAccessTimes)
                    {
                        y.Add(x.Key);
                    }
                    DateTime minDate = y.Min();
                    string file = fileAccessTimes[minDate];
                    string fileNum = file.ElementAt(file.Length - 1).ToString();
                    fileNumber = Int32.Parse(fileNum);
                    isEmpty = true;
                }
            }
            string[] filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Server Data");
            foreach (var filename in filePaths)
            {
                string file = filename.ToString();
                string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID + "\\Server Data", "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_" + fileNumber + "\\Server Data");
                if (!File.Exists(str))
                {
                    File.Copy(file, str);
                }
                else
                {
                    File.Delete(str);
                    File.Copy(file, str);
                }
            }
            filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Settlements");
            foreach (var filename in filePaths)
            {
                string file = filename.ToString();
                string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID + "\\Settlements", "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_" + fileNumber + "\\Settlements");
                if (!File.Exists(str))
                {
                    File.Copy(file, str);
                }
                else
                {
                    File.Delete(str);
                    File.Copy(file, str);
                }
            }
            filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID);
            foreach (var filename in filePaths)
            {
                string file = filename.ToString();
                string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID, "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\backup_" + fileNumber);
                if (!File.Exists(str))
                {
                    File.Copy(file, str);
                }
                else
                {
                    File.Delete(str);
                    File.Copy(file, str);
                }
            }
            if (Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\").Length == 0)
            {
                filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Server Data");
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();
                    string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID + "\\Server Data", "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\Server Data");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                    else
                    {
                        File.Delete(str);
                        File.Copy(file, str);
                    }
                }
                filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Settlements");
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();
                    string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID + "\\Settlements", "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\Settlements");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                    else
                    {
                        File.Delete(str);
                        File.Copy(file, str);
                    }
                }
                filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID);
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();
                    string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID, "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                    else
                    {
                        File.Delete(str);
                        File.Copy(file, str);
                    }
                }
            }
            if (isHundredth)
            {
                filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Server Data");
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();
                    string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID + "\\Server Data", "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\Server Data");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                    else
                    {
                        File.Delete(str);
                        File.Copy(file, str);
                    }
                }
                filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID + "\\Settlements");
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();
                    string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID + "\\Settlements", "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm\\Settlements");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                    else
                    {
                        File.Delete(str);
                        File.Copy(file, str);
                    }
                }
                filePaths = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverID);
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();
                    string str = file.Replace("D:\\Zombot Files\\Servers\\" + serverID, "D:\\Zombot Files\\Servers\\" + serverID + "\\Backups\\LongTerm");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                    else
                    {
                        File.Delete(str);
                        File.Copy(file, str);
                    }
                }
            }
        }

        public void Serialize(object o, string fileName)
        {
            ulong serverId = Context.Guild.Id;
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\" + fileName + ".xml", FileMode.OpenOrCreate);
            formatter.Serialize(s, o);
            s.Close();
        }

        public Dictionary<string, AllianceStorage> DeserializeAllianceStorageDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<string, AllianceStorage> o = new Dictionary<string, AllianceStorage>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\AllianceStorages.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<string,AllianceStorage>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public AllianceStorage FetchAllianceStorage(string alliance)
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<string, AllianceStorage> o = DeserializeAllianceStorageDictionary();
            AllianceStorage x = new AllianceStorage();
            if (o.ContainsKey(alliance))
            {
                x = o[alliance];
            }
            return x;
        }

        public void SaveAllianceStorage(AllianceStorage aStore)
        {
            Dictionary<string, AllianceStorage> o = DeserializeAllianceStorageDictionary();
            AllianceStorage x = new AllianceStorage();
            List<string> toRemove = new List<string>();
            if (o.ContainsKey(aStore.allianceName))
            {
                toRemove.Add(aStore.allianceName);
            }
            foreach (string y in toRemove)
            {
                o.Remove(y);
            }
            o.Add(aStore.allianceName, aStore);
            Serialize(aStore, "AllianceStorages");
        }
        
        public void SaveAllianceDefender(Participant p)
        {
            Charsheet player = PlayerLoad(p.id);
            Dictionary<ulong, Participant> aDDict = DeserializeAllianceDefenderDictionary(player.alliance);
            if (aDDict.ContainsKey(p.id))
            {
                aDDict[p.id] = p;
            }
            Serialize(aDDict, "Settlements\\" + player.alliance + "_allianceDefenders");
        }

        public Dusty DeserializeDusty()
        {
            ulong serverId = Context.Guild.Id;
            Dusty o = new Dusty();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\shop.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dusty)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<ulong, Participant> DeserializeAllianceDefenderDictionary(string alliance)
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, Participant> o = new Dictionary<ulong, Participant>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Settlements\\" + alliance + "_allianceDefenders.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, Participant>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<ulong, Charsheet> DeserializeCharDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, Charsheet> o = new Dictionary<ulong, Charsheet>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\playerDictionary.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, Charsheet>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<ulong, Distractor> DeserializeDistractorDictionary(ulong forager)
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, Distractor> o = new Dictionary<ulong, Distractor>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\" + forager + "_distractors.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, Distractor>)formatter.Deserialize(s);
            }

            s.Close();

            return o;
        }

        public Dictionary<ulong, Charsheet> DeserializeInfectedDeathDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, Charsheet> o = new Dictionary<ulong, Charsheet>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\infectionDeathDictionary.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, Charsheet>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public List<Participant> DeserializeParticipants(ulong forager)
        {
            ulong serverId = Context.Guild.Id;
            List<Participant> o = new List<Participant>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\" + forager + "_forage.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (List<Participant>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public List<string> DeserializeWeaponsList()
        {
            ulong serverId = Context.Guild.Id;
            List<string> o = new List<string>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\weaponsList.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (List<string>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public List<Settlement> DeserializeSettlementDatabase()
        {
            ulong serverId = Context.Guild.Id;
            List<Settlement> o = new List<Settlement>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Settlements\\settlementDatabase.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (List<Settlement>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Participant DeserializeAllParticipants(ulong ID)
        {
            ulong serverId = Context.Guild.Id;
            Participant o = new Participant();
            int fileCount = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\").Length;
            if (fileCount != 0)
            {
                string[] participantFiles = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\");
                foreach (string participantFile in participantFiles)
                {
                    int count = File.ReadLines(participantFile).Count();
                    BinaryFormatter formatter = new BinaryFormatter();
                    Stream s = File.Open(participantFile, FileMode.OpenOrCreate);
                    List<Participant> x = new List<Participant>();
                    if (count > 1)
                    {
                        x = (List<Participant>)formatter.Deserialize(s);
                        IEnumerable<Participant> y = x.Where(z => z.GetID() == ID);
                        if (y.Count() != 0)
                        {
                            o = y.FirstOrDefault();
                        }
                    }
                    s.Close();
                }
            }
            return o;
        }

        public Dictionary<ulong, PlayerInventory> DeserializePlayerInventoryDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, PlayerInventory> o = new Dictionary<ulong, PlayerInventory>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\playerInventoryDictionary.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, PlayerInventory>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<ulong, List<Settlement>> DeserializeSettlementDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, List<Settlement>> o = new Dictionary<ulong, List<Settlement>>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Settlements\\PlayerSettlementDictionary.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, List<Settlement>>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<ulong, PlayerVariable> DeserializePlayerVariableDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, PlayerVariable> o = new Dictionary<ulong, PlayerVariable>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\PlayerVariables.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, PlayerVariable>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<ulong, SettlementPlayerVariables> DeserializeSettlementVariableDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<ulong, SettlementPlayerVariables> o = new Dictionary<ulong, SettlementPlayerVariables>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Settlements\\SettlementPlayerVariables.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<ulong, SettlementPlayerVariables>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public ServerData DeserializeServerData()
        {
            ulong serverId = Context.Guild.Id;
            ServerData o = new ServerData();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Server Data\\" + serverId + ".xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (ServerData)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<string, StandardZombie> DeserializeCombatants(ulong forager)
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<string, StandardZombie> o = new Dictionary<string, StandardZombie>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Active Fights\\" + forager + "_combatants.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<string, StandardZombie>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public Dictionary<string, Weapon> DeserializeWeaponDictionary()
        {
            ulong serverId = Context.Guild.Id;
            Dictionary<string, Weapon> o = new Dictionary<string, Weapon>();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\weaponDictionary.xml", FileMode.OpenOrCreate);
            if (s.Length != 0)
            {
                o = (Dictionary<string, Weapon>)formatter.Deserialize(s);
            }
            s.Close();

            return o;
        }

        public void SaveParticipant(Participant p)
        {
            ulong serverId = Context.Guild.Id;
            ulong forager = p.GetForager();
            List<Participant> px = DeserializeParticipants(forager);
            IEnumerable<Participant> py = px.Where(z => z.GetID() == p.GetID());
            if (py.Count() > 0)
            {
                int index = px.FindIndex(x => x.GetID() == p.GetID());
                px.RemoveAt(index);
            }
            px.Add(p);
            Serialize(px, "\\Participants\\" + forager + "_forage");
        }

        public ulong GetIDFromUsername(string username)
        {
            ulong id = 0;
            IGuild guild = Context.Guild;
            var x = guild.GetUsersAsync().Result;
            List<IGuildUser> u = x.ToList<IGuildUser>();

            foreach (IGuildUser user in u)
            {
                IEnumerable<IGuildUser> players = u.Where(u => u.ToString() == username);
                if (players.Count() == 1)
                {
                    IGuildUser player = players.First();
                    id = player.Id;
                }
                else
                {
                    id = 1;
                }
            }
            return id;
        }

        public int GetItemQtyFromInv(PlayerInventory inv, string nameOfItem)
        {
            List<Item> inventory = inv.GetInventory();
            Item x = inventory.Find(i => i.itemName.Contains(nameOfItem));
            int qty = x.GetItemQty();
            return qty;
        }

        public DateTimeOffset ForageCooldown(ulong id)
        {
            PlayerVariable player = new PlayerVariable();
            Dictionary<ulong, PlayerVariable> x = DeserializePlayerVariableDictionary();
            if (x.ContainsKey(id))
            {
                player = x[id];
            }
            DateTimeOffset time = player.GetLastForage();
            return time;
            
        }

        Dictionary<ulong, Charsheet> players = new Dictionary<ulong, Charsheet>();
        public void SavePlayerData(ulong playerID, Charsheet player)
        {
            ulong serverId = Context.Guild.Id;
            if (player.health > 100)
            {
                player.health = 100;
            }
            if (player.hunger > 100)
            {
                player.hunger = 100;
            }
            if (player.thirst > 100)
            {
                player.thirst = 100;
            }
            if (File.Exists("D:\\Zombot Files\\Servers\\" + serverId + "\\playerDictionary.xml"))
            {
                players = DeserializeCharDictionary();
            }
            if (players.ContainsKey(playerID))
            {
                players.Remove(playerID);
            }
            players.Add(playerID, player);
            Serialize(players, "playerDictionary");
        }

        public Charsheet PlayerLoad(ulong id)
        {
            ulong serverId = Context.Guild.Id;
            Charsheet player = new Charsheet();
            Dictionary<ulong, Charsheet> players = DeserializeCharDictionary();
            if (players.ContainsKey(id))
            {
                player = players[id];
            }
            else
            {
                player.playerName = "";
            }
            return player;
        }

        public PlayerInventory GetPlayerInventory(ulong id)
        {
            PlayerInventory inv = new PlayerInventory();
            Dictionary<ulong, PlayerInventory> players = DeserializePlayerInventoryDictionary();
            if (players.ContainsKey(id))
            {
                inv = players[id];
            }
            return inv;
        }

        public void InventoryAppend(ulong id, string item, int qty)
        {
            Item i = new Item(item, qty);
            PlayerInventory inv = GetPlayerInventory(id);
            List<Item> playerInv = inv.GetInventory();
            List<string> itemNames = new List<string>();
            if (playerInv.Count != 0)
            {
                foreach (Item it in playerInv)
                {
                    itemNames.Add(it.GetItemName());
                }
            }
            bool hasItem = itemNames.Contains(item);
            if (hasItem)
            {
                int index = itemNames.IndexOf(item);
                Item loadedItem = playerInv.ElementAt(index);
                inv.items.Remove(loadedItem);
                loadedItem.itemQty += qty;
                if (loadedItem.itemQty < 0)
                {
                    loadedItem.itemQty = 0;
                }
                else
                {
                    inv.items.Add(loadedItem);
                }
            }
            else
            {
                inv.items.Add(i);
            }
            Dictionary<ulong, PlayerInventory> players = DeserializePlayerInventoryDictionary();
            bool inventoryExists = players.ContainsKey(id);
            if (inventoryExists)
            {
                players.Remove(id);
            }
            players.Add(id, inv);
            Serialize(players, "playerInventoryDictionary");
        }

        public bool IsAllianceDefender(ulong id)
        {
            bool isDefender = false;
            Charsheet user = PlayerLoad(id);
            Dictionary<ulong, Participant> allianceDefenders = DeserializeAllianceDefenderDictionary(user.alliance);
            if (allianceDefenders.ContainsKey(id))
            {
                isDefender = true;
            }
            return isDefender;
        }

        public Participant FetchAllianceDefender(ulong id)
        {
            Participant p = new Participant();
            Charsheet user = PlayerLoad(id);
            Dictionary<ulong, Participant> allianceDefenders = DeserializeAllianceDefenderDictionary(user.alliance);
            if (allianceDefenders.ContainsKey(id))
            {
                p = allianceDefenders[id];
            }
            return p;
        }

        public string Fatigue(string username, ulong id)
        {
            string fatigue = "";
            Charsheet player = PlayerLoad(id);
            Random rand = new Random();
            int hungerBase = rand.Next(1, 15);
            int thirstBase = rand.Next(1, 15);

            int con = player.constitution;
            double statDouble = Convert.ToDouble(Math.Max(con / 2, 1));
            int statMod = Convert.ToInt32(Math.Floor(statDouble));
            if (statMod >= hungerBase)
            {
                statMod = hungerBase - 1;
            }
            int hungerLoss = hungerBase - statMod;
            if (statMod >= thirstBase)
            {
                statMod = thirstBase - 1;
            }
            int thirstLoss = thirstBase - statMod;

            player.hunger -= hungerLoss;
            player.thirst -= thirstLoss;

            if (player.hunger < 0)
            { player.hunger = 0; };
            if (player.thirst < 0)
            { player.thirst = 0; };

            SavePlayerData(id, player);
            if (player.hunger < 25)
            {
                fatigue = username + ", you're getting pretty hungry...";
            }
            if (player.thirst < 25)
            {
                fatigue = username + ", you're getting pretty thirsty...";
            }
            if (player.hunger == 0)
            {
                fatigue = username + $" has died of starvation!";
                DeathHandler(id);
            }
            if (player.thirst == 0)
            {
                fatigue = username + $" has died of dehydration!";
                DeathHandler(id);
            }

            return fatigue;
        }

        public bool DeathHandler(ulong id)
        {
            Charsheet player = PlayerLoad(id);
            if (player.infection >= 100)
            {
                Dictionary<ulong, Charsheet> infectionDeaths = DeserializeInfectedDeathDictionary();
                bool playerAdded = infectionDeaths.ContainsKey(id);
                if (playerAdded)
                {
                    infectionDeaths.Remove(id);
                }
                infectionDeaths.Add(id, player);
                Serialize(infectionDeaths, "infectionDeathDictionary");
            }
            Random rand = new Random();

            int foodLoss = 0 - rand.Next(1, 20);
            int waterLoss = 0 - rand.Next(1, 20);
            int medsLoss = 0 - rand.Next(1, 20);
            int ammoLoss = 0 - rand.Next(1, 20);
            int cashLoss = rand.Next(1, 20);
            int scrapLoss = rand.Next(1, 20);

            player.cash -= cashLoss;
            if (player.cash < 0)
            { player.cash = 0; };

            player.scrap -= scrapLoss;
            if (player.scrap < 0)
            { player.scrap = 0; };

            player.health = 100;
            player.hunger = 100;
            player.thirst = 100;
            player.infection = 0;

            SavePlayerData(id, player);

            InventoryAppend(id, "Food", foodLoss);
            InventoryAppend(id, "Water", waterLoss);
            InventoryAppend(id, "Meds", medsLoss);
            InventoryAppend(id, "Ammo", ammoLoss);
            bool isDead = true;
            return isDead;
        }

        public async Task<List<IUser>> ForageCountdown(RestUserMessage message, IUser author)
        {
            var check = new Emoji("\u2705");
            DateTimeOffset prepThen = message.Timestamp;
            DateTimeOffset prepNow = DateTime.Now;
            TimeSpan cooldown = new TimeSpan(0, 2, 0); //0, 2, 0
            TimeSpan elapsed = prepNow - prepThen;
            TimeSpan diff = cooldown - elapsed;
            int diffMin = Convert.ToInt32(diff.Minutes);
            int diffSec = Convert.ToInt32(diff.Seconds);
            int diffInt = Convert.ToInt32(diff.TotalSeconds);
            List<IUser> a = new List<IUser>();
            Charsheet player = PlayerLoad(author.Id);
            while (elapsed < cooldown)
            {
                prepNow = DateTimeOffset.Now;
                elapsed = prepNow - prepThen;
                diff = cooldown - elapsed;
                diffMin = diff.Minutes;
                diffSec = diff.Seconds;
                diffInt = Convert.ToInt32(Math.Floor(diff.TotalSeconds));
                if ((diffInt % 5) == 0)
                {
                    string msg = "";
                    if (player.GetAlliance() != "None")
                    {
                        msg = author.Username + " wants to go forage. Who will join them? Note: Must be in the " + player.GetAlliance() + " alliance to join. [Leaving in: " + diffMin + " minutes " + diffSec + " seconds]";
                    }
                    else
                    {
                        msg = author.Username + " wants to go forage. [Leaving in: " + diffMin + " minutes " + diffSec + " seconds]";
                    }
                    await message.ModifyAsync(m => m.Content = msg);

                }
                IEnumerable<IUser> y = await message.GetReactionUsersAsync(check, 100).FlattenAsync();
                a = y.Where(y => y.IsBot == false).ToList();
                foreach (IUser az in a)
                {
                    Dictionary<ulong, PlayerVariable> ax = DeserializePlayerVariableDictionary();
                    PlayerVariable ay = new PlayerVariable();
                    if (ax.ContainsKey(az.Id))
                    {
                        ay = ax[az.Id];
                        ax.Remove(az.Id);
                    }
                    ay.isForaging = true;
                    ax.Add(az.Id, ay);
                    Serialize(ax, "PlayerVariables");
                }
            }
            string foragerAlliance = player.GetAlliance();
            List<IUser> all = a.Where(a => a.Id != author.Id).ToList();
            List<IUser> yes = new List<IUser>();
            foreach (IUser user in all)
            {               
                Charsheet userSheet = PlayerLoad(user.Id);
                string playerAlliance = userSheet.alliance;
                if (foragerAlliance == playerAlliance)
                {
                    yes.Add(user);
                }
            }
            await message.DeleteAsync();
            return yes;
        }

        public async Task<bool> AllianceJoin(RestUserMessage message, ulong invitee, ulong reactor)
        {
            bool isMatch = false;
            if (reactor == invitee)
            {
                isMatch = true;
                await message.DeleteAsync();
            }
            return isMatch;
        }

        public int XpToLevel(Charsheet player)
        {
            int baseXP = 100;
            int level = player.level;
            int xpToLevel = Convert.ToInt32(Math.Round(baseXP * Math.Pow(level + 1, 1.5)));
            return xpToLevel;
        }

        public int LevelCalc(ulong id)
        {
            int baseXP = 100;
            Charsheet player = PlayerLoad(id);
            int newLevel = player.level + 1;
            int oldLevel = player.level;
            int xp = player.exp;
            int xpToOldLevel;
            if (oldLevel != 1)
            {
                xpToOldLevel = Convert.ToInt32(Math.Round(baseXP * Math.Pow(oldLevel, 1.5)));
            }
            else
            {
                xpToOldLevel = 100;
            }
            int xpToNewLevel = Convert.ToInt32(Math.Round(baseXP * Math.Pow(newLevel, 1.5)));
            int xpEarned = xp - xpToOldLevel;

            int level;
            if (xpEarned >= xpToNewLevel)
            {
                level = newLevel;
            }
            else
            {
                level = oldLevel;
            }
            return level;
        }

        public StringBuilder FightBuilder(ulong foragerID, int qty)
        {
            List<StandardZombie> zeds = new List<StandardZombie>();
            Dictionary<string, StandardZombie> combatants = new Dictionary<string, StandardZombie>();
            int zombieCount = 1;
            while (zombieCount <= qty)
            {
                string zID = foragerID + "_" + zombieCount;
                StandardZombie z = new StandardZombie(zID);
                zeds.Add(z);
                combatants.Add(zID, z);
                zombieCount++;
            }
            Serialize(combatants, "\\Active Fights\\" + foragerID + "_combatants");
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            if (qty > 1)
            {
                sb.AppendLine("Sure enough, " + qty + " zombies come running out of a nearby doorway, snarling for their upcoming meal.");
            }
            else
            {
                sb.AppendLine($"Luckily enough, only " + qty + " zombie heard you. It shambles toward you, hoping for an easy snack.");
            }
            sb.AppendLine($"");
            int cnt = 1;
            foreach (StandardZombie z in zeds)
            {
                sb.AppendLine($"Zombie " + cnt + ":");
                string healthBar = "";
                int healthCount = z.health;
                while (healthCount > 0)
                {
                    int oddTest = healthCount % 2;
                    if (oddTest == 0)
                    {
                        string sliver = "[";
                        healthBar += sliver;
                    }
                    else
                    {
                        string sliver = "]";
                        healthBar += sliver;
                    }
                    healthCount--;
                }
                cnt++;
                sb.AppendLine($"HP: " + healthBar);
            }

            sb.AppendLine($"");
            sb.AppendLine($"Participants have 30 seconds to act. What would you like to do?");
            sb.AppendLine($"");
            sb.AppendLine($"Type =[action] [zombie #] [weapon] to choose an action. Example: =shoot 2 long rifle");
            sb.AppendLine($"Possible actions: melee, shoot, distract, run");

            return sb;
        }

        public StringBuilder FightHandler(ulong forager, ulong hitterID, string hitter, string action, string weapon, int target)
        {
            StringBuilder sb = new StringBuilder();
            string ID = forager + "_" + target;
            Dictionary<string, StandardZombie> combatants = DeserializeCombatants(forager);
            StandardZombie combatant = new StandardZombie();
            if (combatants.ContainsKey(ID))
            {
                combatant = combatants[ID];
            }
            bool targetExists = false;
            if (combatant.ID != "")
            {
                targetExists = true;
            }
            if (targetExists)
            {
                if (action == "melee")
                {
                    StandardZombie z = combatant;
                    Charsheet smasher = PlayerLoad(hitterID);
                    Dictionary<string, Weapon> weapons = DeserializeWeaponDictionary();
                    Weapon w = new Weapon();
                    if (weapons.ContainsKey(weapon))
                    {
                        w = weapons[weapon];
                    }
                    Random rand = new Random();
                    int weaponRoll = 0;
                    if (w.damage == 1)
                    {
                        weaponRoll = 1;
                    }
                    else
                    {
                        double minDamDouble = w.damage / 2;
                        int minDamage = Convert.ToInt32(Math.Floor(minDamDouble));
                        weaponRoll = rand.Next(minDamage, w.damage);
                    }
                    int totalDamage = weaponRoll - (z.armor / 5) + smasher.strength;
                    if (totalDamage < 0)
                    {
                        totalDamage = 0;
                    }
                    z.health -= totalDamage;
                    if (z.health < 0)
                    {
                        z.health = 0;
                    }
                    Dictionary<string, StandardZombie> combatantsDict = DeserializeCombatants(forager);
                    if (combatantsDict.ContainsKey(ID))
                    {
                        combatantsDict.Remove(ID);
                    }
                    combatantsDict.Add(ID, z);
                    Serialize(combatantsDict, "\\Active Fights\\" + forager + "_combatants");
                    sb.AppendLine(hitter + " has hit Zombie " + target + " for " + totalDamage + " damage.");
                }
                if (action == "shoot")
                {
                    Charsheet shooter = PlayerLoad(hitterID);
                    PlayerInventory shooterInv = GetPlayerInventory(hitterID);
                    int ammo = GetItemQtyFromInv(shooterInv, "Ammo");
                    if (ammo != 0)
                    {
                        StandardZombie z = new StandardZombie();
                        if (combatants.ContainsKey(ID))
                        {
                            z = combatants[ID];
                        }
                        Dictionary<string, Weapon> weapons = DeserializeWeaponDictionary();
                        Weapon w = new Weapon();
                        if (weapons.ContainsKey(weapon))
                        {
                            w = weapons[weapon];
                        }
                        Random rand = new Random();
                        int weaponRoll = 0;
                        if (w.damage == 1)
                        {
                            weaponRoll = 1;
                        }
                        else
                        {
                            double minDamDouble = w.damage / 2;
                            int minDamage = Convert.ToInt32(Math.Floor(minDamDouble));
                            weaponRoll = rand.Next(minDamage, w.damage);
                        }
                        int totalDamage = weaponRoll - (z.armor / 5) + shooter.intelligence;
                        if (totalDamage < 0)
                        {
                            totalDamage = 0;
                        }
                        z.health -= totalDamage;
                        if (z.health < 0)
                        {
                            z.health = 0;
                        }
                        Dictionary<string, StandardZombie> combatantsDict = DeserializeCombatants(forager);
                        if (combatantsDict.ContainsKey(ID))
                        {
                            combatantsDict.Remove(ID);
                        }
                        combatantsDict.Add(ID, z);
                        Serialize(combatantsDict, "\\Active Fights\\" + forager + "_combatants");
                        ammo--;
                        InventoryAppend(hitterID, "Ammo", ammo);
                        sb.AppendLine(hitter + " has shot Zombie " + target + " for " + totalDamage + " damage.");
                    }
                    else
                    {
                        sb.AppendLine(hitter + " tried to shoot Zombie " + target + ", but found that they had no ammo!");
                    }
                }
            }
            bool isDefender = IsAllianceDefender(hitterID);
            if (!isDefender)
            {
                Participant p = DeserializeAllParticipants(hitterID);
                p.action = "";
                SaveParticipant(p);
            }
            else
            {
                Participant p = FetchAllianceDefender(hitterID);
                p.action = "";
                SaveAllianceDefender(p);
            }
            return sb;
        }

        public StringBuilder ZombieActions(ulong forager, List<Distractor> distractors)
        {
            StringBuilder sb = new StringBuilder();
            List<Participant> participants = DeserializeParticipants(forager);
            int participantsLength = participants.Count;
            Dictionary<string, StandardZombie> combatants = DeserializeCombatants(forager);
            List<StandardZombie> zombies = combatants.Values.ToList();
            Random rand = new Random();
            int playerChoice = 0;
            int zQty = 1;
            foreach (StandardZombie z in zombies)
            {
                if (z.health != 0)
                {
                    int totalCha = 0;
                    foreach (Distractor d in distractors)
                    {
                        string cID = forager + "_" + d.target;
                        if (z.ID == cID)
                        {
                            totalCha += d.charisma;
                        }
                    }
                    if (participantsLength > 1)
                    {
                        playerChoice = rand.Next(0, participantsLength);
                    }
                    else
                    {
                        playerChoice = 0;
                    }
                    ulong p = 0;
                    if (participantsLength > 1)
                    {
                        Participant part = participants.ElementAt(playerChoice);
                        p = part.id;
                    }
                    else
                    {
                        p = forager;
                    }
                    Charsheet player = PlayerLoad(p);
                    double lT = z.damage / 2;
                    if (lT < 1)
                    {
                        lT = 1;
                    }
                    int lowerThreshold = Convert.ToInt32(Math.Floor(lT));
                    int zDamage = (rand.Next(lowerThreshold, z.damage) - totalCha);
                    string number = z.ID.Substring(z.ID.Length - 1);
                    sb.AppendLine(player.playerName + " has been hit by Zombie " + number + " for " + zDamage + " damage.");
                    Random biteRand = new Random();
                    int biteRoll = biteRand.Next(1, 100);
                    int biteSeverity = 0;
                    if (player.GetConstitution() > 4)
                    {
                        biteRoll += 4;
                    }
                    else
                    {
                        biteRoll += player.GetConstitution();
                    }
                    if (biteRoll <= 5)
                    {
                        sb.AppendLine($"");
                        sb.AppendLine(player.playerName + " has been bitten by Zombie " + number + "!");
                        int biteSevRoll = biteRand.Next(1, 12);
                        if (biteSevRoll <= 6)
                        {
                            biteSeverity = 1;
                            sb.AppendLine($"It's a fairly shallow bite... You should have plenty of time to heal up!");
                        }
                        else if (biteSevRoll > 6 && biteSevRoll <= 10)
                        {
                            biteSeverity = 2;
                            sb.AppendLine($"It doesn't look great. You should get your bite looked at soon!");
                        }
                        else
                        {
                            biteSeverity = 3;
                            sb.AppendLine($"The bite looks very bad. This could be dangerous if it's not treated as soon as possible!");
                        }
                        player.biteSeverity += biteSeverity;
                    }
                    player.health -= zDamage;
                    SavePlayerData(p, player);
                    zQty++;
                }
            }
            return sb;
        }

        public async Task<bool> FightProgress(ulong forager, IUserMessage message, StringBuilder sb, bool isDefense)
        {
            bool isSuccess = false;
            Dictionary<string, StandardZombie> x = DeserializeCombatants(forager);
            List<StandardZombie> combatants = x.Values.ToList();
            int qty = combatants.Count;

            // initialize empty string builder for reply
            var pb = new StringBuilder();

            // build an embed, because they're shinier
            if (!isDefense)
            {
                if (qty > 1)
                {
                    pb.AppendLine("Sure enough, " + qty + " zombies come running out of a nearby doorway, snarling for their upcoming meal.");
                }
                else
                {
                    pb.AppendLine($"Luckily enough, only " + qty + " zombie heard you. It shambles toward you, hoping for an easy snack.");
                }
            }
            bool hasEdited = false;
            int totalHP = 0;
            DateTimeOffset then = message.Timestamp;
            foreach (StandardZombie c in combatants)
            {
                int health = c.health;
                totalHP += health;
            }
            bool allDead = false;
            if (totalHP <= 0)
            {
                allDead = true;
            }
            bool isParticipants = true;
            while (!allDead && isParticipants)
            {
                List<Participant> particip = new List<Participant>();
                if (!isDefense)
                {
                    particip = DeserializeParticipants(forager);
                }
                else
                {
                    var u = await Context.Guild.GetUserAsync(forager);
                    Participant p = new Participant(u.Username, u.Id, u.Id);
                    particip.Add(p);
                    Serialize(particip, "\\Participants\\" + forager + "_forage");
                }
                if (particip.Count == 0)
                {
                    isParticipants = false;
                }
                EmbedBuilder embed = new EmbedBuilder();
                if (!isDefense)
                {
                    embed.Title = "Foraging...";
                }
                else
                {
                    embed.Title = "Defending...";
                }
                embed.WithColor(new Color(0, 255, 0));
                DateTimeOffset now = DateTimeOffset.Now;
                TimeSpan moveOn = new TimeSpan(0, 0, 20);
                TimeSpan firstEdit = new TimeSpan(0, 0, 10);
                TimeSpan fightLength = now - then;
                int oldDiff = 0;
                while (fightLength < moveOn)
                {
                    now = DateTimeOffset.Now;
                    moveOn = new TimeSpan(0, 0, 30);
                    fightLength = now - then;
                    if (fightLength >= firstEdit)
                    {
                        double secondsDouble = fightLength.Seconds;
                        int seconds = Convert.ToInt32(Math.Round(secondsDouble));
                        int remainder = seconds % 5;
                        if (remainder == 0)
                        {
                            TimeSpan diff = moveOn - fightLength;
                            double diffDouble = diff.TotalSeconds;
                            int diffS = Convert.ToInt32(Math.Round(diffDouble));
                            if (!hasEdited)
                            {
                                if (oldDiff == 0)
                                {
                                    sb.AppendLine($"Time remaining: " + diffS + " seconds.");
                                }
                                else
                                {
                                    sb.Replace($"Time remaining: " + oldDiff + " seconds.", $"Time remaining: " + diffS + " seconds.");
                                }
                                oldDiff = diffS;
                                embed.Description = sb.ToString();
                            }
                            else
                            {
                                if (oldDiff == 0)
                                {
                                    pb.AppendLine($"Time remaining: " + diffS + " seconds.");
                                }
                                else
                                {
                                    pb.Replace($"Time remaining: " + oldDiff + " seconds.", $"Time remaining: " + diffS + " seconds.");
                                }
                                oldDiff = diffS;
                                embed.Description = pb.ToString();
                            }
                            await message.ModifyAsync(m => m.Embed = embed.Build());
                        }
                    }
                }
                pb.Clear();
                int isDistract = 0;
                List<Participant> parts = DeserializeParticipants(forager);
                foreach (Participant p in parts)
                {
                    ulong id = p.GetID();
                    Charsheet participant = PlayerLoad(id);
                    if (participant.GetHealth() == 0)
                    {
                        parts.Remove(p);
                    }
                    if (parts.Count() == 0)
                    {
                        break;
                    }
                }
                Serialize(parts, "\\Participants\\" + forager + "_forage");
                Charsheet fx = PlayerLoad(forager);
                Dictionary<ulong, Participant> aDefenders = DeserializeAllianceDefenderDictionary(fx.alliance);
                List<Participant> adParts = aDefenders.Values.ToList();
                List<Participant> allParts = new List<Participant>();
                foreach (Participant p in parts)
                {
                    allParts.Add(p);
                }
                foreach (Participant p in adParts)
                {
                    allParts.Add(p);
                }
                List<Distractor> distractors = new List<Distractor>();
                Dictionary<ulong, Distractor> dx = new Dictionary<ulong, Distractor>();
                foreach (Participant p in allParts)
                {
                    StringBuilder temp = new StringBuilder();
                    if (p.action == "distract")
                    {
                        isDistract++;
                        ulong id = p.id;
                        Charsheet dist = PlayerLoad(id);
                        Distractor d = new Distractor(p.name, p.target, dist.charisma, p.id);
                        dx.Add(id, d);
                        distractors.Add(d);
                        Serialize(dx, forager + "_distractors");
                    }
                    if (p.action == "run")
                    {
                        if (!isDefense)
                        {
                            Charsheet runner = PlayerLoad(p.id);
                            Random rand = new Random();
                            int runRoll = rand.Next(1, 20) + runner.constitution;
                            if (runRoll > 10)
                            {
                                parts.Remove(p);
                                Serialize(parts, "\\Participants\\" + forager + "_forage");
                                pb.AppendLine(p.GetName() + " has fled the fight!");
                                Dictionary<ulong, PlayerVariable> pz = DeserializePlayerVariableDictionary();
                                PlayerVariable rpv = new PlayerVariable();
                                if (pz.ContainsKey(p.id))
                                {
                                    rpv = pz[p.id];
                                    pz.Remove(p.id);
                                    rpv.isForaging = false;
                                }
                                else
                                {
                                    rpv = new PlayerVariable(p.GetName(), false);
                                }
                                pz.Add(p.id, rpv);
                                Serialize(pz, "PlayerVariables");
                            }
                            else if (runRoll > 1)
                            {
                                pb.AppendLine(p.GetName() + " tried to flee, but they couldn't find a way out!");
                            }
                            else
                            {
                                StandardZombie z = new StandardZombie("Z999");
                                double lT = z.damage / 2;
                                if (lT < 1)
                                {
                                    lT = 1;
                                }
                                int lowerThreshold = Convert.ToInt32(Math.Floor(lT));
                                int zDamage = rand.Next(lowerThreshold, z.damage);
                                runner.health -= zDamage;
                                SavePlayerData(p.id, runner);
                                pb.AppendLine(p.GetName() + " tried to flee, but got grabbed by a zombie! They took " + zDamage + " damage!");
                            }
                        }
                        else
                        {
                            pb.AppendLine(p.GetName() + ", you're the only one who can defend your settlement! You can't run!");
                        }
                    }
                    else
                    {
                        var h = await Context.Guild.GetUserAsync(p.id);
                        temp = FightHandler(forager, p.id, p.name, p.action, p.weapon, p.target);
                    }
                    pb.Append(temp);
                    if (parts.Count() == 0)
                    {
                        isParticipants = false;
                        break;
                    }
                }
                pb.Append(ZombieActions(forager, distractors));
                List<Participant> fighters = DeserializeParticipants(forager);
                foreach (Participant participant in fighters)
                {
                    participant.action = null;
                }
                Serialize(fighters, "\\Participants\\" + forager + "_forage");
                Charsheet f = PlayerLoad(forager);
                Dictionary<ulong, Participant> allianceDefenders = DeserializeAllianceDefenderDictionary(f.alliance);
                foreach (KeyValuePair<ulong, Participant> kvp in allianceDefenders)
                {
                    kvp.Value.action = null;
                }
                Serialize(allianceDefenders, "Settlements\\" + f.alliance + "_allianceDefenders");
                Dictionary<string, StandardZombie> zx = DeserializeCombatants(forager);
                List<StandardZombie> combat = zx.Values.ToList();
                int zQty = 1;
                foreach (StandardZombie c in combat)
                {
                    pb.AppendLine($"Zombie " + zQty + ":");
                    string healthBar = "";
                    int healthCount = c.health;
                    while (healthCount > 0)
                    {
                        int oddTest = healthCount % 2;
                        if (oddTest == 0)
                        {
                            string sliver = "[";
                            healthBar += sliver;
                        }
                        else
                        {
                            string sliver = "]";
                            healthBar += sliver;
                        }
                        healthCount--;
                    }
                    if (c.health <= 0)
                    {
                        healthBar = "[DEAD]";
                    }
                    pb.AppendLine($"HP: " + healthBar);
                    zQty++;
                }
                pb.AppendLine($"");
                pb.AppendLine($"Participants have 30 seconds to act. What would you like to do?");
                pb.AppendLine($"");
                pb.AppendLine($"Type =[action] [zombie #] [weapon] to choose an action. Example: =shoot 2 long rifle");
                pb.AppendLine($"Possible actions: melee, shoot, distract, run");
                embed.Description = pb.ToString();
                await message.DeleteAsync();
                message = await ReplyAsync(null, false, embed.Build());
                hasEdited = true;
                // await message.ModifyAsync(m => m.Embed = embed.Build());
                then = message.Timestamp;
                pb.Replace($"Time remaining: " + oldDiff + " seconds.","");
                List<StandardZombie> cx = DeserializeCombatants(forager).Values.ToList();
                foreach (StandardZombie c in cx)
                {
                    int health = c.health;
                    totalHP += health;
                }
                if (totalHP == 0)
                { 
                    allDead = true;
                    pb.AppendLine($"");
                    pb.AppendLine($"All zombies defeated! Please wait...");
                    embed.Description = pb.ToString();
                    await message.DeleteAsync();
                    message = await ReplyAsync(null, false, embed.Build());
                }
                totalHP = 0;
            }
            if (isParticipants == true && allDead == true)
            {
                Thread.Sleep(7500);
                StringBuilder eb = new StringBuilder();
                eb.AppendLine($"Congratulations! You have defeated all zombies in the fight.");
                List<Participant> winners = DeserializeParticipants(forager);
                foreach (Participant win in winners)
                {
                    Charsheet winningPlayer = PlayerLoad(win.GetID());
                    ulong id = win.id;
                    Random rand = new Random();
                    int upperThreshhold = 15;
                    if (winningPlayer.level <= 10)
                    {
                        upperThreshhold = 15;
                    }
                    else if (winningPlayer.level <= 30)
                    {
                        upperThreshhold = 12;
                    }
                    else if (winningPlayer.level <= 50)
                    {
                        upperThreshhold = 10;
                    }
                    else if (winningPlayer.level <= 70)
                    {
                        upperThreshhold = 8;
                    }
                    else if (winningPlayer.level <= 90)
                    {
                        upperThreshhold = 6;
                    }
                    else if (winningPlayer.level <= 100 && winningPlayer.level > 100)
                    {
                        upperThreshhold = 4;
                    }
                    upperThreshhold *= 2;
                    int randNum = rand.Next(6, upperThreshhold);
                    int xpToNextLevel = XpToLevel(winningPlayer);
                    int xpToCurrentLevel = Convert.ToInt32(Math.Round(100 * Math.Pow(winningPlayer.level, 1.5)));
                    int xpToLevel = xpToNextLevel - xpToCurrentLevel;
                    decimal xpPercent = randNum / 100m;
                    decimal xpToDecimal = xpPercent * xpToLevel;
                    int xpToAdd = Convert.ToInt32(Math.Round(xpToDecimal, 0));

                    Console.WriteLine("Old XP: " + winningPlayer.GetExp());
                    winningPlayer.exp += xpToAdd;
                    eb.AppendLine($"Player " + winningPlayer.GetName() + " has earned " + xpToAdd + " experience.");
                    Console.WriteLine("New XP: " + winningPlayer.GetExp());
                    SavePlayerData(id, winningPlayer);
                    int newLevel = LevelCalc(id);
                    Console.WriteLine(newLevel);
                    if (winningPlayer.GetLevel() != newLevel)
                    {
                        eb.AppendLine($"" + win.GetName() + " has leveled up!");
                        winningPlayer.level = newLevel;
                        winningPlayer.skillPoints++;
                        SavePlayerData(id, winningPlayer);
                    }
                    Dictionary<ulong, PlayerVariable> pz = DeserializePlayerVariableDictionary();
                    PlayerVariable rpv = new PlayerVariable();
                    if (pz.ContainsKey(id))
                    {
                        rpv = pz[id];
                        pz.Remove(id);
                        rpv.isForaging = false;
                    }
                    else
                    {
                        rpv = new PlayerVariable(win.GetName(), false);
                    }
                    pz.Add(id, rpv);
                    Serialize(pz, "PlayerVariables");
                }
                EmbedBuilder emb = new EmbedBuilder
                {
                    Description = eb.ToString(),
                    Title = "Victory!"
                };
                emb.WithColor(new Color(0, 255, 0));
                await message.DeleteAsync();
                await ReplyAsync(null, false, emb.Build());
                hasEdited = true;
                ulong serverId = Context.Guild.Id;
                File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\" + forager + "_forage.xml");
                File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Active Fights\\" + forager + "_combatants.xml");
                isSuccess = true;
            }
            else
            {
                EmbedBuilder emb = new EmbedBuilder
                {
                    Description = $"All participants ran from the fight, zombies snapping at their heels!",
                    Title = "Retreat!"
                };
                emb.WithColor(new Color(0, 255, 0));
                await message.DeleteAsync();
                await ReplyAsync(null, false, emb.Build());
                hasEdited = true;
                ulong serverId = Context.Guild.Id;
                File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\" + forager + "_forage.xml");
                File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Active Fights\\" + forager + "_combatants.xml");
            }
            return isSuccess;
        }

        public  string ForageLootGen()
        {
            string lootName = "";

            var rand = new Random();

            int randomNum = rand.Next(1, 10);
            switch (randomNum)
            {
                case 1:
                    lootName = "zombies";
                    break;
                case 2:
                    lootName = "scraps";
                    break;
                case 3:
                    lootName = "cash";
                    break;
                case 4:
                    lootName = "food";
                    break;
                case 5:
                    lootName = "water";
                    break;
                case 6:
                    lootName = "medicine";
                    break;
                case 7:
                    lootName = "weapons";
                    break;
                case 8:
                    lootName = "ammo";
                    break;
                case 9:
                    lootName = "supply cache";
                    break;
                case 10:
                    lootName = "settlement";
                    break;
            }

            return lootName;
        }

        public  string ScrapLoot(Charsheet player, List<Charsheet> helpers)
        {
            string scrapLoot = "";
            Random rand = new Random();
            int randNum = rand.Next(1, 10);

            switch (randNum)
            {
                case 1:
                    scrapLoot = "You tried to find some snacks in a vending machine, but all you could seem to find was a couple of empty soda cans.\n\n" +
                        "They may be worth a little as scrap, but not much else.\n\n" +
                        "[+10 Scrap]";
                    break;
                case 2:
                    scrapLoot = "You tried to find some canned fruits and vegetables in a restaurant's kitchen, but someone got to them first. All you managed to get were some empty tin cans.\n\n" +
                        "They could at least make some decent scrap.\n\n" +
                        "[+20 Scrap]";

                    break;
                case 3:
                    scrapLoot = "You're walking along the street, looking for supplies, when you trip over a knocked-over stop sign. Looking at it, you have an idea on how to make the sign useful.\n\n" +
                        "You decide it would be good as scrap.\n\n" +
                        "[+30 Scrap]";
                    break;
                case 4:
                    scrapLoot = "You're rummaging around in some dumpsters behind an old hardware store, when you pull out a small toolbox.\n\n" +
                        "You think you could tear it apart and use it for scrap.\n\n" +
                        "[+40 Scrap]";
                    break;
                case 5:
                    scrapLoot = "You're walking back home from foraging, empty-handed, when you see a big metal briefcase lying in the road.\n\n" +
                        "You think this would make good scrap material, and take it with you.\n\n" +
                        "[+50 Scrap]";
                    break;
                case 6:
                    scrapLoot = "You're walking along a back alley when you come across a rarity: an aluminum trash can, in great condition.\n\n" +
                        "You heft it over your shoulder and take it home for scrap.\n\n" +
                        "[+60 Scrap]";
                    break;
                case 7:
                    scrapLoot = "You pass an abandoned taxi cab on the street, and see that one of the doors has been torn off.\n\n" +
                        "You wipe some of the blood off the window and take it with you for scrap.\n\n" +
                        "[+70 Scrap]";
                    break;
                case 8:
                    scrapLoot = "You're walking along a road that used to be rather busy, when you stub your toe on something hard. When you look down, you see that it's most of a Miami-Dade SWAT riot shield.\n\n" +
                        "You can tell that it's just about useless for its original purpose. However, the durable metal would make fantastic scrap.\n\n" +
                        "[+80 Scrap]";
                    break;
                case 9:
                    scrapLoot = "The sun is almost down as you walk along the street. A glint catches your eye. You look for the source, and find a small box full of stainless steel plates.\n\n" +
                        "You close the box and tuck it under your arm. This will make great scrap.\n\n" +
                        "[+90 Scrap]";
                    break;
                case 10:
                    scrapLoot = "You're walking behind an old factory at dusk, and you see a small cart full of metal scrap. The only problem is, there are three zombies shambling nearby.\n\n" +
                        "Thinking quickly, you pick up a rock and chuck it as hard as you can in another direction. The zombies chase after the sound, and leave the cart unattended.\n\n" +
                        "You quietly wheel the cart back home for scrap.\n\n" +
                        "[+100 Scrap]";
                    break;
            }
            player.scrap += randNum * 10;
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            SavePlayerData(id, player);
            foreach (Charsheet helper in helpers)
            {
                helper.scrap += randNum * 10;
                string hname = helper.GetName();
                ulong hID = GetIDFromUsername(hname);
                SavePlayerData(hID, helper);
            }
            Console.WriteLine(randNum);
            return scrapLoot;
        }

        public  string CashLoot(Charsheet player, List<Charsheet> helpers)
        {
            string cashLoot = "";
            Random rand = new Random();
            int randNum = rand.Next(1, 10);

            switch (randNum)
            {
                case 1:
                    cashLoot = "You're cleaning off your weapon after killing a zombie when you see a small piece of paper sticking out of its pocket.\n\n" +
                        "You take the paper out, and see that it's a dollar bill.\n\n" +
                        "[+10 Cash]";
                    break;
                case 2:
                    cashLoot = "You find a pair of pants laying in the road. You're not sure why they're there, but when you check the pockets, you find a wallet.\n\n" +
                        "Inside is a few dollar bills.\n\n" +
                        "[+2 Cash]";
                    break;
                case 3:
                    cashLoot = "You're looting a gas station, when you notice that the register is open.\n\n" +
                        "You peek inside and take out a small handful of money.\n\n" +
                        "[+30 Cash]";
                    break;
                case 4:
                    cashLoot = "You spot a corpse laying in an alley. You decide to investigate. \n\n" +
                        "You jump back as the corpse lashes out at you, proving itself to not be that dead. You quickly dispatch it, and find a few dollars in its pockets.\n\n" +
                        "[+40 Cash]";
                    break;
                case 5:
                    cashLoot = "You find a small safe in the back of a clothing outlet you're looting. \n\n" +
                        "You pick it up and dash it against the ground, and the door falls open. Inside is a decent chunk of money.\n\n" +
                        "[+50 Cash]";
                    break;
                case 6:
                    cashLoot = "You stumble across the butchered remains of a bachelor party. They're all carrying singles, presumably because they were going to visit a gentleman's club.\n\n" +
                        "You relieve the degenerates of their wordly posessions.\n\n" +
                        "[+60 Cash]";
                    break;
                case 7:
                    cashLoot = "You walk by an ATM, and find that it's been busted open. The perpetrator is long gone, and probably dead, judging from the bloodstains...\n\n" +
                        "...and the fact that they left the money in the ATM.\n\n" +
                        "[+70 Cash]";
                    break;
                case 8:
                    cashLoot = "You see a very old-looking corpse sitting in a very expensive-looking car. As you get closer, you see that the corpse is covered in fine jewelry.\n\n" +
                        "You strip the jewelry from the body, thinking you could use them to barter or sell later.\n\n" +
                        "[+80 Cash]";
                    break;
                case 9:
                    cashLoot = "You see the wreckage of what used to be an armored transport truck, sticking halfway out of a post office.\n\n" +
                        "When you investigate, you find that the truck is open. Inside are a couple of duffel bags full of cash.\n\n" +
                        "[+90 Cash]";
                    break;
                case 10:
                    cashLoot = "You're walking by a very official-looking bank, when you see something peculiar: a black-masked corpse laying in the entry.\n\n" +
                        "The body looks very old, but you can tell that they died from gunshot wounds. Several of them.\n\n" +
                        "When you enter the bank to investigate, you find the vault hanging wide open, with no one around. Most of the money in the vault is in duffel bags.\n\n" +
                        "You grab as many bags as you can carry.\n\n" +
                        "[+100 Cash]";
                    break;
            }
            player.cash += randNum * 10;
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            SavePlayerData(id, player);
            foreach (Charsheet helper in helpers)
            {
                helper.cash += randNum * 10;
                string hname = helper.GetName();
                ulong hID = GetIDFromUsername(hname);
                SavePlayerData(hID, helper);
            }
            return cashLoot;
        }

        public  string FoodLoot(Charsheet player, List<Charsheet> helpers)
        {
            string foodLoot = "";
            int foodQty = 0;
            Random rand = new Random();
            int randNum = rand.Next(1, 10);

            switch (randNum)
            {
                case 1:
                    foodLoot = "You rattle a vending machine, and a single twinkie falls out of it.\n\n" +
                        "These *do* have an expiration date, you know. You pick it up anyway.\n\n" +
                        "[+1 Food]";
                    foodQty = 1;
                    break;
                case 2:
                    foodLoot = "You pass a small minivan and notice that one of the doors is open. When you peek inside, you see a bag of chips on the floorboard.\n\n" +
                        "You put the chips in your bag.\n\n" +
                        "[+2 Food]";
                    foodQty = 2;
                    break;
                case 3:
                    foodLoot = "You found a still-legible advertisement for a local bee farm.\n\n" +
                        "You travel to the farm's location, and find it obviously abandoned. You look inside and find a fair decent amount of honey inside.\n\n" +
                        "Containers of honey are added to your inventory.\n\n" +
                        "[+3 Food]";
                    foodQty = 3;
                    break;
                case 4:
                    foodLoot = "You pass the corpse of a survivor who was obviously less fortunate than you. When you roll him over, you find that they have a small pack on their back.\n\n" +
                        "You open the pack and see a few MRE boxes. You grab them, and put them in your bag. They wouldn't do him any good anyway.\n\n" +
                        "[+4 Food]";
                    foodQty = 4;
                    break;
                case 5:
                    foodLoot = "You're passing what used to be a farmer's market, and find a stall where they sold dehydrated food.\n\n" +
                        "You grab as many packs as you can find, which isn't too many. But the food will last, and that's what's important. \n\n" +
                        "[+5 Food]";
                    foodQty = 5;
                    break;
                case 6:
                    foodLoot = "You find a small local grocer that doesn't look too badly looted. You cautiously step inside and look around.\n\n" +
                        "You step over what little remains of the grocer who owned this place, and find a small trove of chips to reward you for your perseverence.\n\n" +
                        "[+6 Food]";
                    foodQty = 6;
                    break;
                case 7:
                    foodLoot = "You see a truck trailer knocked over in the distance.\n\n" +
                        "When you inspect the insides of the trailer, you find a half dozen cans of fruit left from scavengers.\n\n" +
                        "[+7 Food]";
                    foodQty = 7;
                    break;
                case 8:
                    foodLoot = "You come across a run-down camp, with no survivors around.\n\n" +
                        "Rummaging through their no-longer-needed supplies, you discover several cans of vegetables.\n\n" +
                        "[+8 Food]";
                    foodQty = 8;
                    break;
                case 9:
                    foodLoot = "You pass a health food store, and find that there is still a whole box of granola bars that is miraculously untouched. \n\n" +
                        "You grab the box and put it in your bag.\n\n" +
                        "[+9 Food]";
                    foodQty = 9;
                    break;
                case 10:
                    foodLoot = "While exploring the city, you check out a ransacked store just to be thorough.\n\n" +
                        "In the stockroom, on a shelf in the back you find a large bag of uncooked white rice.\n\n" +
                        "You slide it out and take it with you.\n\n" +
                        "[+10 Food]";
                    foodQty = 10;
                    break;
            }
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            InventoryAppend(id, "Food", foodQty);
            foreach (Charsheet helper in helpers)
            {
                string hname = helper.GetName();
                ulong h = GetIDFromUsername(hname);
                InventoryAppend(h, "Food", foodQty);
            }
            return foodLoot;
        }

        public  string WaterLoot(Charsheet player, List<Charsheet> helpers)
        {
            string waterLoot = "";
            int waterQty = 0;
            Random rand = new Random();
            int randNum = rand.Next(1, 10);

            switch (randNum)
            {
                case 1:
                    waterLoot = "You go to a local restroom to see if there's any running water.\n\n" +
                        "There's not, but there is an abandoned and unopened water bottle lying on the ground.\n\n" +
                        "[+1 Water]";
                    waterQty = 1;
                    break;
                case 2:
                    waterLoot = "You walk past an abandoned convertible, and see two water bottles in the cupholders.\n\n" +
                        "You stash the two bottles in your bag. You know you could boil them to disinfect them, if needed.\n\n" +
                        "[+2 Water]";
                    waterQty = 2;
                    break;
                case 3:
                    waterLoot = "You're in a gas station, raiding it for supplies, when you notice that there's a bit of plastic poking out from under a shelf.\n\n" +
                        "You grab the plastic and pull, and are rewarded with a mostly-empty case of water bottles. You stash the bottles away in your bag.\n\n" +
                        "[+3 Water]";
                    waterQty = 3;
                    break;
                case 4:
                    waterLoot = "You decide to look through the local Walmart again. This time, you're astounded to find a case of apple juice boxes in the back.\n\n" +
                        "They're still good, somehow, so you stash the case away in your pack.\n\n" +
                        "[+4 Water]";
                    waterQty = 4;
                    break;
                case 5:
                    waterLoot = "You rummage through a pile of garbage outside of an apartment building, looking for scraps, when some movement catches your eye.\n\n" +
                        "When you investigate, you find a half-full water jug rolling across the road. You don't know what started it rolling, and you don't want to. You grab the jug and run.\n\n" +
                        "[+5 Water]";
                    waterQty = 5;
                    break;
                case 6:
                    waterLoot = "You're searching through an abandoned home. When you reach the kitchen, you find a water filter attached to the sink.\n\n" +
                        "Without replacement filters, it'll only last a little while. But, you'll make it work.\n\n" +
                        "[+6 Water]";
                    waterQty = 6;
                    break;
                case 7:
                    waterLoot = "You're on top of an apartment building, taking a look around at the area from above, when the light glints off something and catches your eye.\n\n" +
                        "When you look at whatever the light was glinting off of, you find a UPS truck. Upon closer examination, you find a package in the back full of water filters.\n\n" +
                        "[+7 Water]";
                    waterQty = 7;
                    break;
                case 8:
                    waterLoot = "Looting through a bar, you find that, somehow, the soda nozzles still function.\n\n" +
                        "You grab as many plastic bottles as you can find and fill them to the top with soda.\n\n" +
                        "[+8 Water]";
                    waterQty = 8;
                    break;
                case 9:
                    waterLoot = "You're walking between cars, all bumper-to-bumper on the highway, when you catch a glimpse of plastic in one of the windows.\n\n" +
                        "When you open the door, you find a full case of water bottles.\n\n" +
                        "[+9 Water]";
                    waterQty = 9;
                    break;
                case 10:
                    waterLoot = "You spot a water delivery truck flipped over on the road ahead of you.\n\n" +
                        "Upon approach, you find the back doors wide open. Inside are two large water jugs, both full and capped.\n\n" +
                        "You giddily tuck one under each arm and head home.\n\n" +
                        "[+10 Water]";
                    waterQty = 10;
                    break;
            }
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            InventoryAppend(id, "Water", waterQty);
            foreach (Charsheet helper in helpers)
            {
                string hname = helper.GetName();
                ulong h = GetIDFromUsername(hname);
                InventoryAppend(h, "Water", waterQty);
            }
            return waterLoot;
        }

        public  string MedsLoot(Charsheet player, List<Charsheet> helpers)
        {
            string medsLoot = "";
            int medsQty = 0;
            Random rand = new Random();
            int randNum = rand.Next(1, 10);

            switch (randNum)
            {
                case 1:
                    medsLoot = "You find a half-empty box of bandaids on the floor of the supermarket.\n\n" +
                        "[+1 Meds]";
                    medsQty = 1;
                    break;
                case 2:
                    medsLoot = "While rooting through an auto repair shop, you find a small first-aid kit. It's mostly gone, but there's a roll of gauze left.\n\n " +
                        "[+2 Meds]";
                    medsQty = 2;
                    break;
                case 3:
                    medsLoot = "You're strolling along the sidewalk next to a quiet street, when you spot a small red box near a garbage can.\n\n" +
                        "Inside the box, you find some stitching needles, unopened. These will definitely come in handy.\n\n" +
                        "[+3 Meds]";
                    medsQty = 3;
                    break;
                case 4:
                    medsLoot = "You're walking through the medicine section at Walmart, which is mostly ransacked. Underneath a cart in the corner of the aisle, you find a box of large bandages.\n\n" +
                        "You open the box, and are pleased to find that a few still remain.\n\n" +
                        "[+4 Meds]";
                    medsQty = 4;
                    break;
                case 5:
                    medsLoot = "You're looking for a restroom, walking through an alley, when you look to your left and find a first-aid kit.\n\n" +
                        "You pick it up and open it, and are pleased to find some painkillers and some antibiotics.\n\n" +
                        "[+5 Meds]";
                    medsQty = 5;
                    break;
                case 6:
                    medsLoot = "You stumble across a small family pharmacy in a back road. You push open the door and find that they're mostly untouched.\n\n" +
                        "Your excitement fades as you realize that they only have over-the-counter drugs, no prescription-strength. Still, though, the painkillers and decongestants would be helpful.\n\n" +
                        "[+6 Meds]";
                    medsQty = 6;
                    break;
                case 7:
                    medsLoot = "You're rooting through some luggage at the airport when you hit a small jackpot. Someone on the plane had a kidney infection.\n\n" +
                        "Their luggage has three weeks' worth of prescription-strength antibiotics in it.\n\n" +
                        "[+7 Meds]";
                    medsQty = 7;
                    break;
                case 8:
                    medsLoot = "You find an ambulance overturned on the freeway. It looks mostly picked clean, but you remember hearing that some EMTs keep a spare kit under their seats.\n\n" +
                        "You look under the driver's seat, and nearly jump for joy when you find a kit. It's got two full suture kits and an IV bag in it.\n\n" +
                        "[+8 Meds]";
                    medsQty = 8;
                    break;
                case 9:
                    medsLoot = "You're taking a break near the beach, eating your lunch, when you see what looks like a stretcher wash up on the shore.\n\n" +
                        "You walk over to investigate. A zombie is strapped to the stretcher, snarling aand biting at you, but she's unable to get free to come after you. You check under the stretcher for supplies.\n\n" +
                        "You find a bag full of bandages, gauze, stitches, and tourniquets.\n\n" +
                        "[+9 Meds]";
                    medsQty = 9;
                    break;
                case 10:
                    medsLoot = "You're quietly sneaking through the hospital, trying to find any medicines that were passed over. The hospital is far from empty, so you have to be careful.\n\n" +
                        "You sneak by the cancer ward, which is full of shambling zombies, and nearly trip over a nurse's cart.\n\n" +
                        "This cart is a treasure trove. All kinds of antibiotics, muscle relaxers, blood thinners, and other high-strength prescription meds. You take as many as you can carry, and sneak out.\n\n" +
                        "[+10 Meds]";
                    medsQty = 10;
                    break;
            }
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            InventoryAppend(id, "Meds", medsQty);
            foreach (Charsheet helper in helpers)
            {
                string hname = helper.GetName();
                ulong h = GetIDFromUsername(hname);
                InventoryAppend(h, "Meds", medsQty);
            }
            return medsLoot;
        }

        public  string WeaponsLoot(Charsheet player)
        {
            string weaponsLoot = "";
            string weaponsName = "";
            Random rand = new Random();
            int randNum = rand.Next(1, 5);
            if (randNum == 5)
            {
                int randomNum = rand.Next(1, 10);
                switch (randomNum)
                {
                    case 1:
                        weaponsLoot = $"While walking through the forest, you find a bow laying, forgotten, near a campsite. You grab it.\n\n" +
                            $"[Bow Added]";
                        weaponsName = "bow";
                        break;
                    case 2:
                        weaponsLoot = $"You're passing a small family-owned gun shop, when a glint of metal catches your eye.\n\n" +
                            $"When you investigate, you're surprised to find that there's still one gun remaining: a small black revolver.\n\n" +
                            $"[Revolver Added]";
                        weaponsName = "revolver";
                        break;
                    case 3:
                        weaponsLoot = $"You just finished taking out a normal zombie when you see a bulky shape under its shirt.\n\n" +
                            $"You try not to gag as you pull the rotted cloth away from its even more rotten stomach, and find a handgun tucked into what's left of its pants.\n\n" +
                            $"You wipe the gunk off of it and put it in your bag.\n\n" +
                            $"[Handgun Added]";
                        weaponsName = "handgun";
                        break;
                    case 4:
                        weaponsLoot = "You're rooting through a watchtower above an airport tarmac, when you spot a long rifle in the corner of the room.\n\n" +
                            $"You pick it up and check it for defects. Finding none, you sling it over your shoulder.\n\n" +
                            $"[Long Rifle Added]";
                        weaponsName = "long rifle";
                        break;
                    case 5:
                        weaponsLoot = "You pass a burned-up humvee on the road, and you're reminded of when the US Army was called in to try and quell this apocalypse.\n\n" +
                            $"You're shaken from your depressing reverie by the sight of a well-oiled M16 rifle laying under the humvee. You reach under and grab it, and inspect your new find.\n\n" +
                            $"[Military Rifle Added]";
                        weaponsName = "military rifle";
                        break;
                    case 6:
                        weaponsLoot = "You're walking along the freeway, peeking in abandoned cars, when you hit the jackpot: a very heavily-customized truck that looks like it could single-handedly deplete the ozone layer.\n\n" +
                            $"You check the door, find it unlocked, and check the backseat. Sure enough, where there are rednecks, there are guns, and you find a MAC-10 submachine gun in the back seat.\n\n" +
                            $"[Submachine Gun Added]";
                        weaponsName = "submachine gun";
                        break;
                    case 7:
                        weaponsLoot = "You slowly creak open the door to a small woodsy cottage, and above the fireplace you find an old-fashioned two-barrel Mossberg shotgun.\n\n" +
                            $"You pull the gun off the wall and check the barrel for blockages, before slinging it over your shoulder.\n\n" +
                            $"[Shotgun Added]";
                        weaponsName = "shotgun";
                        break;
                    case 8:
                        weaponsLoot = "You're exploring the forest when you find a hunting stand high up in a tree.\n\n" +
                            $"You climb to the stand and look inside, and are rewarded with a crossbow that's still in good condition.\n\n" +
                            $"[Crossbow Added]";
                        weaponsName = "crossbow";
                        break;
                    case 9:
                        weaponsLoot = "A SWAT van is overturned near a toll road. You tentatively walk forward to investigate.\n\n" +
                            $"When you get close, you hear loud banging coming from inside the van. You decide not to get any closer. However, just in front of you lies a Remington 700p sniper's rifle.\n\n" +
                            $"You grab it and run away from the van as fast as you can.\n\n" +
                            $"[Sniper Rifle added]";
                        weaponsName = "sniper rifle";
                        break;
                    case 10:
                        weaponsLoot = $"You're passing a smoldering convoy of military vehicles near the beach, trying not to think of the stench of burnt flesh all around.\n\n" +
                            $"You're waving the smell away from your face when you notice a gun barrel poking out of the window of one of the vehicles.\n\n" +
                            $"You grab the barrel and pull, and hear snarling on the other end. You pull again, sharply, and the gun rips free. A zombie snarls at you through the open window.\n\n" +
                            $"You point the barrel of your brand-new M249 SAW light machine gun into the vehicle and turn the zombie into paste.\n\n" +
                            $"[Light Machine Gun Added]";
                        weaponsName = "light machine gun";
                        break;
                }
            }
            else
            {
                int randomNum = rand.Next(1, 20);
                switch (randomNum)
                {
                    case 1:
                        weaponsLoot = "You're strolling along the road when you spot a particularly hefty tree branch. You shrug and grab it. It may make a half-decent weapon.\n\n" +
                            $"[Sturdy Branch Added]";
                        weaponsName = "sturdy branch";
                        break;
                    case 2:
                        weaponsLoot = "You pass an old baseball diamond near the suburbs. You peek into the dugout curiously, and find that there's still plenty of sturdy wood bats left.\n\n" +
                            $"You take one with you, and give it a few test swings.\n\n" +
                            $"[Baseball Bat Added]";
                        weaponsName = "baseball bat";
                        break;
                    case 3:
                        weaponsLoot = "You gingerly step over what appears to be half of a police officer. You pull a blood-covered baton from his belt loop.\n\n" +
                            $"It looks undamaged, so you clean it off on a clean part of the deceased officer's vest and put it in your pack.\n\n" +
                            $"[Police Baton Added]";
                        weaponsName = "police baton";
                        break;
                    case 4:
                        weaponsLoot = "You peek your head inside a Forest Ranger's office, looking for anything interesting.\n\n" +
                            $"A glint of metal catches your eye, and you go investigate. You pull out a decent-looking machete, probably used for clearing underbrush.\n\n" +
                            $"[Machete Added]";
                        weaponsName = "machete";
                        break;
                    case 5:
                        weaponsLoot = "There was a Japanese History museum in town, and when you visit, you find that most of the exhibits are destroyed.\n\n" +
                            $"However, you manage to find a katana, still sheathed, amongst the wreckage.\n\n" +
                            $"[Katana Added]";
                        weaponsName = "katana";
                        break;
                    case 6:
                        weaponsLoot = "You find an abandoned work truck on the side of the road, and decide to scavenge it.\n\n" +
                            $"You pull a hefty two-foot crowbar out of the truck bed and give it a few test swings.\n\n" +
                            $"[Crowbar Added]";
                        weaponsName = "crowbar";
                        break;
                    case 7:
                        weaponsLoot = "You see a Miami Sewer Dept. truck near a manhole cover, and shrug, deciding it's worth a look.\n\n" +
                            $"You end up pretty glad you looked, because inside the truck was a three-foot pipe wrench with a nice, heavy swing to it.\n\n" +
                            $"[Large Pipe Wrench Added]";
                        weaponsName = "large pipe wrench";
                        break;
                    case 8:
                        weaponsLoot = "You see a tractor laying on its side in the outskirts of the suburbs. Thinking that it was a curious sight, you decide to investigate.\n\n" +
                            $"You don't find much, except a rusty pitchfork laying on the ground.\n\n" +
                            $"[Pitchfork Added]";
                        weaponsName = "pitchfork";
                        break;
                    case 9:
                        weaponsLoot = "You step through the broken glass front of a local survivalist shop, and are delighted to find that there are still some items left.\n\n" +
                            $"Among other things, you leave with a shiny new hatchet.\n\n" +
                            $"[Hatchet Added]";
                        weaponsName = "hatchet";
                        break;
                    case 10:
                        weaponsLoot = "You're walking along the freeway when you spot what appears to be some construction up ahead.\n\n" +
                            $"Obviously, it's abandoned now, but there's still a nicely-sized sledgehammer left.\n\n" +
                            $"[Sledgehammer Added]";
                        weaponsName = "sledgehammer";
                        break;
                    case 11:
                        weaponsLoot = "Some poor bastards were repairing the roof of a fast food restaurant when this went down. You can tell they didn't make it out from the bloodstains all around.\n\n" +
                            $"However, there's a sturdy-looking wood plank nearby, so you decide to take that with you.\n\n" +
                            $"[2x4 Wood Plank Added]";
                        weaponsName = "2x4 Wood Plank";
                        break;
                    case 12:
                        weaponsLoot = "You see a very interesting sight for urban Miami: a puritanical lawn mower business. You decide to investigate.\n\n" +
                            $"Inside, you find the secret to their previous success in lawn-mowing without machinery: a very long, very sharp scythe.\n\n" +
                            $"[Scythe Added]";
                        weaponsName = "scythe";
                        break;
                    case 13:
                        weaponsLoot = "The Comic Convention was in town, as is evidenced by the dead cosplayers laying all around.\n\n" +
                            $"You find one with a more authentic-looking costume, and grab a pair of nunchucks off his belt. Sorry, cosplayer, but you need them more.\n\n" +
                            $"[Nunchucks Added]";
                        weaponsName = "nunchucks";
                        break;
                    case 14:
                        weaponsLoot = "You find a broken broom laying on the road, and you're struck with an idea. You tape a sharp piece of metal to the stick, and make yourself a makeshift spear.\n\n" +
                            $"It's proabbly not the most durable thing in the world, but it'll do in a pinch.\n\n" +
                            $"[Makeshift Spear Added]";
                        weaponsName = "makeshift spear";
                        break;
                    case 15:
                        weaponsLoot = "You're rummaging through a house near a dairy farm, looking for any dairy products that may, somehow, still be good enough to eat.\n\n" +
                            $"Instead, you find a nasty-looking cattle prod by the door. You shrug, and grab the prod. Maybe zombies don't like electricity\n\n" +
                            $"[Cattle Prod Added]";
                        weaponsName = "cattle prod";
                        break;
                    case 16:
                        weaponsLoot = "You find the headless corpse of what obviously used to be your standard Miami Beach tough guy.\n\n" +
                            $"You search his pockets and cringe as you find a set of brass knuckles. What on earth did this tool need these for?\n\n" +
                            $"You pocket them anyway, because while this corpse never needed them, you certainly do now.\n\n" +
                            $"[Brass Knuckles Added]";
                        weaponsName = "brass knuckles";
                        break;
                    case 17:
                        weaponsLoot = "You're walking past a graveyard, thinking of the irony, when you spot an open grave with dirt next to it.\n\n" +
                            $"You're curious, so you go look. Once there, you find a shovel stuck into the earth by the grave. Disconcertingly, the coffin inside is open, with no body in it.\n\n" +
                            $"[Shovel Added]";
                        weaponsName = "shovel";
                        break;
                    case 18:
                        weaponsLoot = "There's a quaint little landscaping business at the end of the street, and you stop by to see if you can find anything.\n\n" +
                            $"You don't find anything besides a dirty little hoe.\n\n" +
                            $"[Dirty Hoe Added]";
                        weaponsName = "dirty hoe";
                        break;
                    case 19:
                        weaponsLoot = "You step quietly through an abandoned Ikea, looking for.. Something? You're not quite sure.\n\n" +
                            $"All you really manage to find is a broken table leg. It'll be okay for a few solid swings on a zombie, but it's probably not the most trustworthy weapon.\n\n" +
                            $"[Table Leg Added]";
                        weaponsName = "table leg";
                        break;
                    case 20:
                        weaponsLoot = "Your jaw drops as you walk into the park, astounded to find that the Renaissance Faire was in town.\n\n" +
                            $"You find the corpse of what was once a very impressive knight in armor, and draw the sword from his scabbard.\n\n" +
                            $"You run your finger along the blade and lightly cut your finger. You grab the scabbard, tie it around your waist, and leave the faire feeling like King Fuckin' Arthur.\n\n" +
                            $"[Claymore Added]";
                        weaponsName = "claymore";
                        break;
                }
            }
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            InventoryAppend(id, weaponsName, 1);
            return weaponsLoot;
        }

        public  string AmmoLoot(Charsheet player, List<Charsheet> helpers)
        {
            string ammoLoot = "";
            int ammoQty = 0;
            Random rand = new Random();
            int randNum = rand.Next(1, 10);

            switch (randNum)
            {
                case 1:
                    ammoLoot = $"Walking along the road, you find a small pile of small-caliber rounds next to a gutter.\n\n" +
                        $"Upon examination, you find two good bullets.\n\n" +
                        $"[+2 Ammo]";
                    ammoQty = 2;
                    break;
                case 2:
                    ammoLoot = $"You stumble across someone's unfortunate corpse. A revolver is in their hand.\n\n" +
                        $"You take the revolver and examine it. The barrel is cracked, so it's useless; but in the cylinder you find four unfired rounds.\n\n" +
                        $"[+4 Ammo]";
                    ammoQty = 4;
                    break;
                case 3:
                    ammoLoot = $"You find the burned-out remains of a police cruiser by city hall.\n\n" +
                        $"When you approach, you see that there are a few shotgun shells laying by the open driver door.\n\n" +
                        $"[+6 Ammo]";
                    ammoQty = 6;
                    break;
                case 4:
                    ammoLoot = $"You nearly trip over a military service rifle laying on the ground outside of a failed military barricade.\n\n" +
                        $"You check the rifle and find that the barrel is bent. Useless. However, upon checking the magazine, you find eight good rounds still in it.\n\n" +
                        "[+8 Ammo]";
                    ammoQty = 8;
                    break;
                case 5:
                    ammoLoot = $"You're rooting through abandoned magazines in your settlement, trying to find some leftover shells.\n\n" +
                        $"You get lucky and find a half-full magazine for an M16.\n\n" +
                        $"[+10 Ammo]";
                    ammoQty = 10;
                    break;
                case 6:
                    ammoLoot = $"You're looting an old police station, trying not to make noise. Unfortunately, you aren't watching your feet, and you kick a box of shells across the room.\n\n" +
                        $"You cringe at the noise and wait, but, thankfully, you don't hear any approaching zombies. You grab the shells that you had kicked earlier and leave, to be safe.\n\n" +
                        $"[+12 Ammo]";
                    ammoQty = 12;
                    break;
                case 7:
                    ammoLoot = $"You find a pile of half-burnt weaponry in the rubble of the local National Guard outpost.\n\n" +
                        $"You assume that this used to be the armory. You manage to find quite a few rounds that weren't ruined by the fire.\n\n" +
                        $"[+14 Ammo]";
                    ammoQty = 14;
                    break;
                case 8:
                    ammoLoot = $"You find a bloody and abandoned backpack outside of a convenience store.\n\n" +
                        $"You wait around for about fifteen minutes, but the owner never shows. You consider them dead, and loot the bag.\n\n" +
                        $"[+16 Ammo]";
                    ammoQty = 16;
                    break;
                case 9:
                    ammoLoot = $"You walked to a nearby settlement to trade for some ammo, but when you got there, you found that they had been overrun.\n\n" +
                        $"You try not to think about how easily the same thing could happen to you as you scavenge amongst the rubble.\n\n" +
                        $"[+18 Ammo]";
                    ammoQty = 18;
                    break;
                case 10:
                    ammoLoot = $"You're looting an old convenience store when you feel cold steel press against your temple.\n\n" +
                        $"You freeze, and a voice to your right says, \"Drop everything and leave, or I'll shoot!\"\n\n" +
                        $"Your heart pounds in your chest. You duck, and a split second later, the man to your right's gun goes off. He misses you by inches, and you tackle him to the ground.\n\n" +
                        $"At some point in the melee, you get his gun, and turn it to his face. You pull the trigger without hesitation.\n\n" +
                        $"After some time digesting what just happened, you sigh, grab his pack, and leave. Inside the pack is quite a bit of ammo. You leave the gun, unable to stomach carrying it with you after that.\n\n" +
                        $"[+20 Ammo]";
                    ammoQty = 20;
                    break;
            }
            string name = player.GetName();
            ulong id = GetIDFromUsername(name);
            InventoryAppend(id, "Ammo", ammoQty);
            foreach (Charsheet helper in helpers)
            {
                string hname = helper.GetName();
                ulong h = GetIDFromUsername(hname);
                InventoryAppend(h, "Ammo", ammoQty);
            }
            return ammoLoot;
        }

        public  string SupplyCacheLoot(Charsheet player, List<Charsheet> helpers)
        {
            string supplyCacheLoot = "";
            Random rand = new Random();
            int randNum = rand.Next(1, 10);
            string username = player.GetName();
            ulong id = GetIDFromUsername(username);

            switch (randNum)
            {
                case 1:
                    supplyCacheLoot = $"You find a fanny pack with a cupcake and a water bottle in it\n\n" +
                        $"[+1 Water, +1 Food]";
                    InventoryAppend(id, "Water", 1);
                    InventoryAppend(id, "Food", 1);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 1);
                        InventoryAppend(h, "Food", 1);
                    }
                    break;
                case 2:
                    supplyCacheLoot = "In a semi along the freeway, you find a metal toolbox with a sandwich, a water bottle, and some metal scrap in it.\n\n" +
                        $"[+1 Water, +1 Food, +1 Scrap]";

                    player.scrap += 1;
                    
                    SavePlayerData(id, player);
                    foreach (Charsheet helper in helpers)
                    {
                        helper.scrap += 1;
                        ulong h = GetIDFromUsername(helper.GetName());
                        SavePlayerData(h, helper);
                    }

                    // Whenever you get around to InventoryAppend, the below is so you remember what values go where. The above is so the "CashScrapAppend" errors go away.
                    InventoryAppend(id, "Water", 1);
                    InventoryAppend(id, "Food", 1);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 1);
                        InventoryAppend(h, "Food", 1);
                    }
                    break;
                case 3:
                    supplyCacheLoot = "You root through an abandoned police cruiser and find the owner's lunch, as while as a loose shell and some aluminum foil.\n\n" +
                        $"[+1 Food, +1 Scrap, +1 Ammo]";

                    InventoryAppend(id, "Ammo", 1);
                    InventoryAppend(id, "Food", 1);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 1);
                        InventoryAppend(h, "Food", 1);
                    }
                    break;
                case 4:
                    supplyCacheLoot = "You dig through a food bank and find some abandoned chips, as well as a couple of water bottles.\n\n" +
                        $"[+2 Food, +2 Water]";
                    InventoryAppend(id, "Water", 2);
                    InventoryAppend(id, "Food", 2);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 2);
                        InventoryAppend(h, "Food", 2);
                    }
                    break;
                case 5:
                    supplyCacheLoot = "While looking through an abandoned mechanic, you find some soda and some scrap metal.\n\n" +
                        $"[+2 Water, +2 Scrap]";

                    player.scrap += 2;
                    
                    SavePlayerData(id, player);
                    foreach (Charsheet helper in helpers)
                    {
                        helper.scrap += 2;
                        ulong h = GetIDFromUsername(helper.GetName());
                        SavePlayerData(h, helper);
                    }

                    InventoryAppend(id, "Water", 2);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 2);
                    }
                    break;
                case 6:
                    supplyCacheLoot = "You're raiding a McDonald's when you find some still-good applesauces and bottled juice.\n\n" +
                        $"[+3 Food, +1 Water]";
                    InventoryAppend(id, "Food", 3);
                    InventoryAppend(id, "Water", 1);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 1);
                        InventoryAppend(h, "Food", 3);
                    }
                    break;
                case 7:
                    supplyCacheLoot = "You're rooting through a prison watchtower, and you find the absent watchman's water stash next to his weapon magazine.\n\n" +
                        $"[+2 Ammo, +3 Water]";
                    InventoryAppend(id, "Ammo", 2);
                    InventoryAppend(id, "Water", 3);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 3);
                        InventoryAppend(h, "Ammo", 2);
                    }
                    break;
                case 8:
                    supplyCacheLoot = "You raid a hotel vending machine, breaking it in the process. On the bright side, you find some food, water, and the stuff you broke would make decent scrap.\n\n" +
                        $"[+3 Food, +3 Water, +1 Scrap]";

                    player.scrap += 1;
                    
                    SavePlayerData(id, player);
                    foreach (Charsheet helper in helpers)
                    {
                        helper.scrap += 1;
                        ulong h = GetIDFromUsername(helper.GetName());
                        SavePlayerData(h, helper);
                    }

                    InventoryAppend(id, "Food", 3);
                    InventoryAppend(id, "Water", 3);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 3);
                        InventoryAppend(h, "Food", 3);
                    }
                    break;
                case 9:
                    supplyCacheLoot = "You stumble across someone's abandoned backpack. The owner is almost certainly dead.\n\n" +
                        $"You unzip the pack, and you're delighted to find that the previous owner had packed for a day trip.\n\n" +
                        $"[+2 Food, +2 Water, +2 Scrap, +2 Ammo]";

                    player.scrap += 2;
                    
                    SavePlayerData(id, player);
                    foreach (Charsheet helper in helpers)
                    {
                        helper.scrap += 2;
                        ulong h = GetIDFromUsername(helper.GetName());
                        SavePlayerData(h, helper);
                    }

                    InventoryAppend(id, "Food", 2);
                    InventoryAppend(id, "Water", 2);
                    InventoryAppend(id, "Ammo", 2);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 2);
                        InventoryAppend(h, "Food", 2);
                        InventoryAppend(h, "Ammo", 2);
                    }
                    break;
                case 10:
                    supplyCacheLoot = "While foraging in a gas station, you hear a noise outside. When you investigate, you see a large group with guns. One of them spots you, and yells out to his compatriots, and they point their weapons at you.\n\n" +
                        $"They yell for you to drop your loot. You quickly run and hide in the walk-in freezer. You hear them rummage around looking for you, but eventually, they give up and go to the next building.\n\n" +
                        $"You sneak out into the street, and try very hard not to laugh loudly when you find that one of the moronic would-be thieves left their bag in the street. You take it.\n\n" +
                        $"[+3 Food, +3 Water, +3 Ammo, +3 Scrap]";

                    player.scrap += 3;
                    
                    SavePlayerData(id, player);
                    foreach (Charsheet helper in helpers)
                    {
                        helper.scrap += 3;
                        ulong h = GetIDFromUsername(helper.GetName());
                        SavePlayerData(h, helper);
                    }

                    InventoryAppend(id, "Food", 3);
                    InventoryAppend(id, "Water", 3);
                    InventoryAppend(id, "Ammo", 3);
                    foreach (Charsheet helper in helpers)
                    {
                        ulong h = GetIDFromUsername(helper.GetName());
                        InventoryAppend(h, "Water", 3);
                        InventoryAppend(h, "Food", 3);
                        InventoryAppend(h, "Ammo", 3);
                    }
                    break;
            }
            return supplyCacheLoot;
        }

        public  string SettlementLoot(Charsheet player)
        {
            string settlementName = "";
            ulong id = GetIDFromUsername(player.GetName());
            Random rand = new Random();
            int randNum = rand.Next(1, 10);
            Settlement settlement = new Settlement();
            SettlementModule modOne = new SettlementModule("null", 0, 0, 1);
            SettlementModule modTwo = new SettlementModule("null", 0, 0, 2);
            SettlementModule modThree = new SettlementModule("null", 0, 0, 3);
            SettlementModule modFour = new SettlementModule("null", 0, 0, 4);
            SettlementModule modFive = new SettlementModule("null", 0, 0, 5);
            SettlementModule modSix = new SettlementModule("null", 0, 0, 6);
            SettlementModule modSeven = new SettlementModule("null", 0, 0, 7);
            SettlementModule modEight = new SettlementModule("null", 0, 0, 8);
            SettlementModule modNine = new SettlementModule("null", 0, 0, 9);
            SettlementModule modTen = new SettlementModule("null", 0, 0, 10);
            List<SettlementModule> modules = new List<SettlementModule>();
            switch (randNum)
            {
                case 1:

                    settlementName = "7-Eleven";
                    modules.Add(modOne);
                    settlement = new Settlement(settlementName, id, 1, 1, modules, 1, false);
                    break;
                case 2:
                    settlementName = "Duplex";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    settlement = new Settlement(settlementName, id, 2, 2, modules, 2, false);
                    break;
                case 3:
                    settlementName = "Church";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    settlement = new Settlement(settlementName, id, 3, 3, modules, 3, false);
                    break;
                case 4:
                    settlementName = "Farmhouse";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    settlement = new Settlement(settlementName, id, 4, 4, modules, 4, false);
                    break;
                case 5:
                    settlementName = "Apartment Complex";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    settlement = new Settlement(settlementName, id, 5, 5, modules, 5, false);
                    break;
                case 6:
                    settlementName = "Department Store";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    settlement = new Settlement(settlementName, id, 6, 6, modules, 6, false);
                    break;
                case 7:
                    settlementName = "Police Station";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    settlement = new Settlement(settlementName, id, 7, 7, modules, 7, false);
                    break;
                case 8:
                    settlementName = "Mini-Mall";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    modules.Add(modEight);
                    settlement = new Settlement(settlementName, id, 8, 8, modules, 8, false);
                    break;
                case 9:
                    settlementName = "Small Airport";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    modules.Add(modEight);
                    modules.Add(modNine);
                    settlement = new Settlement(settlementName, id, 9, 9, modules, 9, false);
                    break;
                case 10:
                    settlementName = "Prison";
                    modules.Add(modOne);
                    modules.Add(modTwo);
                    modules.Add(modThree);
                    modules.Add(modFour);
                    modules.Add(modFive);
                    modules.Add(modSix);
                    modules.Add(modSeven);
                    modules.Add(modEight);
                    modules.Add(modNine);
                    modules.Add(modTen);
                    settlement = new Settlement(settlementName, id, 10, 10, modules, 10, false);
                    break;
            }
            string name = player.GetName();
            InventoryAppend(id, settlementName + " Map", 1);
            string settlementLoot = "You find a building that isn't too run-down. Maybe you could fix it up and set up a base here?...\n\n" +
                    settlementName + " discovered.";
            //Keeping the below for archival purposes. Move this to a refurbish command, so that the player has to actually earn some scrap and fix it up to get the settlement.
            /*
            List<Settlement> settlements = new List<Settlement>();
            Dictionary<ulong, List<Settlement>> playerSettlementDictionary = DeserializeSettlementDictionary();
            if (playerSettlementDictionary.ContainsKey(id))
            {
                settlements = playerSettlementDictionary[id];
                playerSettlementDictionary.Remove(id);
            }
            settlements.Add(settlement);
            playerSettlementDictionary.Add(id, settlements);
            Serialize(playerSettlementDictionary, "\\Settlements\\PlayerSettlementDictionary");
            */
            return settlementLoot;
        }
    }
}
