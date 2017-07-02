using System.Configuration;

namespace MyConfig.Content
{
    public class MailElement : ConfigurationElement
    {
        [ConfigurationProperty("Port", DefaultValue = 0)]
        public int Port
        {
            get { return (int)this["Port"]; }
        }

        [ConfigurationProperty("HostMail", DefaultValue = "")]
        public string HostMail
        {
            get { return (string)this["HostMail"]; }
        }

        [ConfigurationProperty("SendingMail", DefaultValue = "")]
        public string SendingMail
        {
            get { return (string)this["SendingMail"]; }
        }

        [ConfigurationProperty("SendingMailName", DefaultValue = "")]
        public string SendingMailName
        {
            get { return (string)this["SendingMailName"]; }
        }

        [ConfigurationProperty("SendingMailTitle", DefaultValue = "")]
        public string SendingMailTitle
        {
            get { return (string)this["SendingMailTitle"]; }
        }

        [ConfigurationProperty("SendingMail2")]
        public string SendingMail2
        {
            get { return (string)this["SendingMail2"]; }
        }

        [ConfigurationProperty("ReceiveMails")]
        public string ReceiveMails
        {
            get { return (string)this["ReceiveMails"]; }
        }

       
    }
}
