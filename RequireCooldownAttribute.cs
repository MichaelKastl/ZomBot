using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Zombie_Apocalypse_Discord_Bot
{
    // Inherit from PreconditionAttribute
    public class RequireCooldownAttribute : PreconditionAttribute
    {
        public RequireCooldownAttribute()
        {

        }
        // Override the CheckPermissions method
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var user = context.User;
            // Check if this user is a Guild User, which is the only context where roles exist
            if (context.User is SocketGuildUser gUser)
            {
                // If this command was executed by a user with the appropriate role, return a success
                ulong serverId = context.Guild.Id;
                Dictionary<ulong, PlayerVariable> playVarDict = DeserializePlayerVariableDictionary(serverId);
                PlayerVariable pv = new PlayerVariable();
                if (playVarDict.ContainsKey(user.Id))
                {
                    pv = playVarDict[user.Id];
                }
                bool isForaging = pv.GetIsForaging();
                // bool isForaging = false;
                DateTimeOffset lastForage = pv.GetLastForage();
                DateTimeOffset now = DateTimeOffset.Now;
                TimeSpan diff = now - lastForage;
                double minBetween = diff.TotalMinutes;
                if (!isForaging && minBetween >= 3) // 3
                {
                    // Since no async work is done, the result has to be wrapped with `Task.FromResult` to avoid compiler errors
                    return Task.FromResult(PreconditionResult.FromSuccess());
                }
                // Since it wasn't, fail
                else if (isForaging && minBetween < 3)
                { 
                    return Task.FromResult(PreconditionResult.FromError(user.Username + ", you're currently participating in another forage; end that forage before you start this one!"));
                }
                else if (isForaging && minBetween >= 5)
                {
                    isForaging = false;
                    pv.isForaging = false;
                    playVarDict.Remove(user.Id);
                    playVarDict.Add(user.Id, pv);
                    Serialize(playVarDict, "PlayerVariables", serverId);
                    Participant p = DeserializeAllParticipants(user.Id, serverId);
                    string forager = p.forager.ToString();
                    File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\" + forager + "_forage.xml");
                    File.Delete("D:\\Zombot Files\\Servers\\" + serverId + "\\Active Fights\\" + forager + "_combatants.xml");
                    return Task.FromResult(PreconditionResult.FromError(user.Username + ", there was an incongruency between your last forage time and your current foraging state. The incongruency has been forced to resolve. Please try again."));
                }
                else
                {
                    return Task.FromResult(PreconditionResult.FromError(user.Username + ", you have to wait until tomorrow to forage again. The zombies are still active from your last outing."));
                }    
            }
            else
                return Task.FromResult(PreconditionResult.FromError("You must be in a guild to run this command."));
        }

        public Dictionary<ulong, PlayerVariable> DeserializePlayerVariableDictionary(ulong serverId)
        {
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

        public static void Serialize(object o, string fileName, ulong serverId)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream s = File.Open("D:\\Zombot Files\\Servers\\" + serverId + "\\" + fileName + ".xml", FileMode.OpenOrCreate);
            formatter.Serialize(s, o);
            s.Close();
        }

        public Participant DeserializeAllParticipants(ulong ID, ulong serverId)
        {
            Participant o = new Participant();
            int fileCount = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\").Length;
            if (fileCount != 0)
            {
                string[] participantFiles = Directory.GetFiles("D:\\Zombot Files\\Servers\\" + serverId + "\\Participants\\");
                foreach (string participantFile in participantFiles)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Stream s = File.Open(participantFile, FileMode.OpenOrCreate);
                    List<Participant> x = (List<Participant>)formatter.Deserialize(s);
                    IEnumerable<Participant> y = x.Where(z => z.GetID() == ID);
                    if (y.Count() != 0)
                    {
                        o = y.FirstOrDefault();
                    }
                    s.Close();
                }
            }
            return o;
        }
    }
}
