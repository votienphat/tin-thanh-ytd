using System.Configuration;

namespace MyConfig
{
    public class RedisElement : ConfigurationElement
    {
        [ConfigurationProperty("Host")]
        public string Host
        {
            get { return (string)this["Host"]; }
        }

        [ConfigurationProperty("Password")]
        public string Password
        {
            get { return (string)this["Password"]; }
        }

        [ConfigurationProperty("PreKeyBossMini", DefaultValue = "lb"),]
        public string PreKeyBossMini
        {
            get { return (string)this["PreKeyBossMini"]; }
        }
       
    }
}
