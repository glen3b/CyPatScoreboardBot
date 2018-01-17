using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberPatriot.DiscordBot.Results
{
    public class UserArgumentError : RuntimeResult
    {
        public UserArgumentError(string reason) : base(null, "Invalid Argument: " + reason)
        {
        }
    }
}
