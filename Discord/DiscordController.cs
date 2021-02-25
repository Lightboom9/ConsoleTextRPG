using System.Collections;
using System.Collections.Generic;
using System;

using Discord;

namespace Controllers
{
    public static class DiscordController
    {
        private static readonly long _clientId = 814461765496602694;

        private static bool _usable = true;

        private static Discord.Discord _discord;
        private static long _unixTimestamp;
        public static Discord.Discord DiscordInstance
        {
            get
            {
                if (_usable)
                {
                    if (!Alive)
                    {
                        CreateDiscord();
                    }
                    return _discord;
                }
                else return null;
            }
        }

        public static void CreateDiscord()
        {
            if (!Alive)
            {
                try
                {
                    _discord = new Discord.Discord(_clientId, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
                }
                catch (Discord.ResultException)
                {
                    _usable = false;
                }

                if (_usable)
                {
                    _unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    Activity baseActivity = new Activity
                    {
                        Timestamps =
                        {
                            Start = _unixTimestamp,
                        },
                        Assets =
                        {
                            LargeImage = "logo"
                        }
                    };
                    _discord.GetActivityManager().UpdateActivity(baseActivity, (res) => { });
                    Alive = true;
                }
            }
        }
        public static void CreateDiscord(string defaultActivityLine)
        {
            if (!Alive)
            {
                try
                {
                    _discord = new Discord.Discord(_clientId, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
                }
                catch (Discord.ResultException)
                {
                    _usable = false;
                }

                if (_usable)
                {
                    _unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    Activity baseActivity = new Activity
                    {
                        State = defaultActivityLine,
                        Timestamps =
                        {
                            Start = _unixTimestamp,
                        },
                        Assets =
                        {
                            LargeImage = "logo"
                        }
                    };
                    _discord.GetActivityManager().UpdateActivity(baseActivity, (res) => { });
                    Alive = true;
                }
            }
        }

        public static bool Alive { get; private set; } = false;

        public static void DisposeDiscord()
        {
            if (_usable)
            {
                if (Alive)
                {
                    _discord.GetActivityManager().ClearActivity((res) => { });
                    _discord.Dispose();
                    _discord = null;
                }
            }
        }

        public static void SetStatus(string line)
        {
            if (_usable && Alive)
            {
                Activity activity = new Activity
                {
                    State = line,
                    Timestamps =
                        {
                            Start = _unixTimestamp,
                        },
                    Assets =
                        {
                            LargeImage = "logo"
                        }
                };
                _discord.GetActivityManager().UpdateActivity(activity, (res) => { });
            }
        }
        public static void SetStatus(string line1, string line2)
        {
            if (_usable && Alive)
            {
                Activity activity = new Activity
                {
                    Details = line1,
                    State = line2,
                    Timestamps =
                        {
                            Start = _unixTimestamp,
                        },
                    Assets =
                        {
                            LargeImage = "logo"
                        }
                };
                _discord.GetActivityManager().UpdateActivity(activity, (res) => { });
            }
        }

        public static void RunCallbacks()
        {
            _discord.RunCallbacks();
        }
    }
}