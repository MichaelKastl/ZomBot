using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;
using Zombie_Apocalypse_Discord_Bot.Modules;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot.Services
{
    public class CommandHandler
    {
        // setup fields to be set later in the constructor
        private readonly IConfiguration _config;
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services)
        {
            // juice up the fields with these services
            // since we passed the services in, we can use GetRequiredService to pass them into the fields set earlier
            _config = services.GetRequiredService<IConfiguration>();
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            // take action when we execute a command
            _commands.CommandExecuted += CommandExecutedAsync;

            // take action when we receive a message (so we can process it, and see if it is a valid command)
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            // register modules that are public and inherit ModuleBase<T>.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        // this class is where the magic starts, and takes actions upon receiving messages
        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            var chnl = rawMessage.Channel as SocketGuildChannel;
            ulong serverId = chnl.Guild.Id;

            // ensures we don't process system/other bot messages
            if (!(rawMessage is SocketUserMessage message))
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            // sets the argument position away from the prefix we set
            var argPos = 0;

            // get prefix from the XML file
            string prefix;
            string prefixDefault = "=";
            string prefixCustom = PrefixGetter(serverId);
            if (prefixCustom == null)
            {
                prefix = prefixDefault;
            }
            else
            {
                prefix = prefixCustom;
            }
            DateTimeOffset now = DateTimeOffset.Now;
            ServerData sd = DeserializeServerData(serverId);
            if (sd.lastBackup == null)
            {
                sd.lastBackup = DateTimeOffset.MinValue;
            }
            TimeSpan since = now - sd.lastBackup;
            if (since.TotalHours >= 1)
            {
                sd.lastBackup = DateTimeOffset.Now;
                Serialize(sd, "Server Data\\" + serverId, serverId);
                Commands.Backup(serverId);
            }
            // determine if the message has a valid prefix, and adjust argPos based on prefix
            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasStringPrefix(prefix, ref argPos)))
            {
                now = DateTimeOffset.Now;
                sd = DeserializeServerData(serverId);
                since = now - sd.lastHordeAttack;
                //since = new TimeSpan(2, 20, 0);
                if (since.TotalHours >= 2)
                {
                    TimeSpan sinceCheck = now - sd.lastHordeCheck;
                    // TimeSpan sinceCheck = new TimeSpan(0, 45, 0);
                    if (sinceCheck.TotalMinutes >= 30)
                    {
                        sd.lastHordeCheck = DateTimeOffset.Now;
                        Serialize(sd, "Server Data\\" + serverId, serverId);
                        Random rand = new Random();
                        int hordeRoll = rand.Next(1, 20);
                        // int hordeRoll = 1;
                        if (hordeRoll == 1)
                        {
                            await HordeAttack(sd);
                        }
                    }
                }
                return;
            }

            var context = new SocketCommandContext(_client, message);
            // execute command if one is found that matches
            await _commands.ExecuteAsync(context, argPos, _services);
        }

        public async Task HordeAttack(ServerData sd)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // build an embed, because they're shinier
            var embed = new EmbedBuilder();
            sb.AppendLine($"A horde of zombies has swarmed Miami!");
            Dictionary<ulong, List<Settlement>> sDict = DeserializeSettlementDictionary(sd.serverId);
            List<Settlement> underAttack = new List<Settlement>();
            List<ulong> usersAttacked = new List<ulong>();
            foreach (KeyValuePair<ulong, List<Settlement>> kvp in sDict)
            {
                if (kvp.Value.Count == 1)
                {
                    
                    foreach (Settlement s in kvp.Value)
                    {
                        if (!usersAttacked.Contains(s.owner))
                        {
                            if (!s.isUnderAttack)
                            {
                                s.isUnderAttack = true;
                                underAttack.Add(s);
                                usersAttacked.Add(s.owner);
                            }
                        }
                    }
                }
                else
                {
                    foreach (Settlement s in kvp.Value)
                    {
                        Random rand = new Random();
                        int isAttacked = rand.Next(1, 2);
                        if (isAttacked == 1)
                        {
                            if (!usersAttacked.Contains(s.owner))
                            {
                                if (!s.isUnderAttack)
                                {
                                    s.isUnderAttack = true;
                                    underAttack.Add(s);
                                    usersAttacked.Add(s.owner);
                                }
                            }
                        }
                    }
                }
            }
            if (underAttack.Count > 1)
            {
                sb.AppendLine($"It looks like the horde is splitting into multiple parts...");
            }
            else if (underAttack.Count == 1)
            {
                sb.AppendLine($"The whole horde is heading to one location!");
            }
            else
            {
                sb.AppendLine($"The horde is going past the entire city! Miami lucked out, this time...");
            }
            Serialize(sDict, "Settlements\\PlayerSettlementDictionary", sd.serverId);
            foreach (Settlement s in underAttack)
            {
                if (underAttack.Count > 1)
                {
                    var g = _client.GetGuild(sd.serverId);
                    var user = g.GetUser(s.owner);
                    string username = user.Username;
                    sb.AppendLine($"One section is heading towards " + username + "'s " + s.name + " settlement!");
                }
                else
                {
                    var g = _client.GetGuild(sd.serverId);
                    var user = g.GetUser(s.owner);
                    string username = user.Username;
                    sb.AppendLine($"The horde is heading straight for " + username + "'s " + s.name + " settlement!");
                }
            }
            if (underAttack.Count > 1)
            {
                sb.AppendLine($"It looks like the rest of the settlements in the area were spared!");
            }
            else
            {
                sb.AppendLine($"It looks like all settlements in the area were spared!");
            }
            if (underAttack.Count >= 1)
            {
                sb.AppendLine($"To defend your settlement, type =defend [settlement name]!");
                sb.AppendLine($"(image credit to Joakim Olofsson. deviantart: https://www.deviantart.com/joakimolofsson)");
                embed.WithImageUrl("https://i.imgur.com/gykt6Gs.jpg");
                embed.Title = "Horde Attack!";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                var guild = _client.GetGuild(sd.serverId);
                var hChannel = guild.GetChannel(sd.hordeChannel);
                ISocketMessageChannel hordeChannel = hChannel as ISocketMessageChannel;
                await hordeChannel.SendMessageAsync(null, false, embed.Build());
                string x = "";
                foreach (ulong user in usersAttacked)
                {
                    var u = guild.GetUser(user) as IUser;
                    x += $"{u.Mention} ";
                }
                if (x == "")
                {
                    x = "You lucked out this time, Miami!";
                }
                await hordeChannel.SendMessageAsync(x, false, null, null);
            }
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // if a command isn't found, log that info to console and exit this method
            if (!command.IsSpecified)
            {
                Console.WriteLine($"Command failed to execute for [" + context.Message + "] <-> [" + context.User + "]!");
                return;
            }
             

            // log success to the console and exit this method
            if (result.IsSuccess)
            {
                Console.WriteLine($"Command [" + context.Message + "] executed for -> [" + context.User + "]");
                return;
            }


            var user = context.User;
            ulong serverId = context.Guild.Id;
            Dictionary<ulong, PlayerVariable> playVarDict = DeserializePlayerVariableDictionary(serverId);
            PlayerVariable pv = new PlayerVariable();
            if (playVarDict.ContainsKey(user.Id))
            {
                pv = playVarDict[user.Id];
            }
            DateTimeOffset lastForage = pv.GetLastForage();
            DateTimeOffset now = DateTimeOffset.Now;
            TimeSpan diff = now - lastForage;
            double minBetween = diff.TotalMinutes;
            if (command.Value.Name == "forage" && minBetween < 3 && !result.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(result.ErrorReason);
                if (minBetween < 3)
                {
                    TimeSpan cd = new TimeSpan(0, 3, 0);
                    TimeSpan timeLeft = cd - diff;
                    int min = timeLeft.Minutes;
                    int sec = timeLeft.Seconds;
                    if (min == 1)
                    {
                        sb.AppendLine($"Time to the next day: " + min + " minute, " + sec + " seconds.");
                    }
                    else
                    {
                        sb.AppendLine($"Time to the next day: " + min + " minutes, " + sec + " seconds.");
                    }
                }
                EmbedBuilder embed = new EmbedBuilder();
                embed.Title = "Foraging...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await context.Channel.SendMessageAsync(null, false, embed.Build());
                return;
            }

            if (command.Value.Name == "forage" && !pv.isForaging && !result.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(result.ErrorReason);
                EmbedBuilder embed = new EmbedBuilder();
                embed.Title = "Foraging...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await context.Channel.SendMessageAsync(null, false, embed.Build());
                return;
            }

            if (command.Value.Name == "forage" && pv.isForaging && !result.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(result.ErrorReason);
                EmbedBuilder embed = new EmbedBuilder();
                embed.Title = "Foraging...";
                embed.Description = sb.ToString();
                embed.WithColor(new Color(0, 255, 0));
                await context.Channel.SendMessageAsync(null, false, embed.Build());
                return;
            }

            // failure scenario, let's let the user know
            StringBuilder stringB = new StringBuilder();
            stringB.AppendLine(result.ErrorReason);
            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = "Something went wrong!";
            eb.Description = stringB.ToString();
            eb.WithColor(new Color(0, 255, 0));
            await context.Channel.SendMessageAsync(null, false, eb.Build());
            StringBuilder errorSB = new StringBuilder();
            errorSB.AppendLine(result.ErrorReason);
            errorSB.AppendLine(result.Error.ToString());
            if (!Directory.Exists("D:\\Zombot Files\\Servers\\" + serverId + "\\Error Logs"))
            {
                Directory.CreateDirectory("D:\\Zombot Files\\Servers\\" + serverId + "\\Error Logs");
            }
            using (StreamWriter file = new StreamWriter("D:\\Zombot Files\\Servers\\" + serverId + "\\Error Logs\\" + DateTimeOffset.Now.Date.Day + "-" + DateTimeOffset.Now.Date.Month + "-" + DateTimeOffset.Now.Date.Year + ".txt"))
            {
                file.Write(errorSB.ToString());
            }
        }

        public Dictionary<ulong, List<Settlement>> DeserializeSettlementDictionary(ulong serverId)
        {
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

        public string PrefixGetter(ulong serverId)
        {
            string prefix = "null";
            ServerData sd = DeserializeServerData(serverId);
            try
            { prefix = sd.GetPrefix(); }
            catch
            { prefix = "null"; }
            return prefix;
        }

        public ServerData DeserializeServerData(ulong serverId)
        {
            ServerData o = new ServerData();
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("D:\\Zombot Files\\Servers\\" + serverId + "\\Server Data\\" + serverId + ".xml"))
            {
                Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\Server Data\\" + serverId + ".xml", FileMode.OpenOrCreate);
                if (s.Length != 0)
                {
                    o = (ServerData)formatter.Deserialize(s);
                }
                s.Close();
            }
            return o;
        }

        public Dictionary<ulong, PlayerVariable> DeserializePlayerVariableDictionary(ulong serverId)
        {
            Dictionary<ulong, PlayerVariable> o = new Dictionary<ulong, PlayerVariable>();
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("D:\\Zombot Files\\Servers\\" + serverId + "\\PlayerVariables.xml"))
            {
                Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\PlayerVariables.xml", FileMode.OpenOrCreate);
                if (s.Length != 0)
                {
                    o = (Dictionary<ulong, PlayerVariable>)formatter.Deserialize(s);
                }
                s.Close();
            }
            return o;
        }

        public void Serialize(object o, string fileName, ulong serverId)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\" + fileName + ".xml", FileMode.OpenOrCreate);
            formatter.Serialize(s, o);
            s.Close();
        }
    }
}

