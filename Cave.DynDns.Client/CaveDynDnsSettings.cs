using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Cave.DynDns.Client
{
    public class CaveDynDnsSettings
    {
        public string DomainName;
        public string Password;
        public int IntervalMinutes;

        public CaveDynDnsSettings()
        {
            RegistryKey l_Key = Registry.CurrentUser.OpenSubKey(@"Software\CaveSystems\DynDns");
            if (l_Key != null)
            {
                DomainName = (string)l_Key.GetValue("Domainname");
                Password = (string)l_Key.GetValue("Password");
                int.TryParse((string)l_Key.GetValue("IntervalMinutes"), out IntervalMinutes);
                l_Key.Close();
            }
            if (DomainName == null) DomainName = "";
            if (Password == null) Password = "";
        }

        public void Save()
        {
            RegistryKey l_Key = Registry.CurrentUser.CreateSubKey(@"Software\CaveSystems\DynDns");
            l_Key.SetValue("Domainname", DomainName);
            l_Key.SetValue("Password", Password);
            l_Key.SetValue("IntervalMinutes", IntervalMinutes.ToString());
            l_Key.Close();
        }
    }
}
