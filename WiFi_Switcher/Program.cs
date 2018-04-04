using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace WiFi_Switcher
{
    class Program
    {

        static void Main(string[] args)

        {
            string macType = string.Empty;
            int WiFi_count = 0;
            long maxSpeed = 1;
            string onOff = args[0].ToString();
            onOff = onOff.ToUpper();
            
            //invalid command line
            if (!onOff.Equals("OFF") && !onOff.Equals("ON"))

                {
                Console.WriteLine("Invalid command line argument");
                Console.WriteLine("To trun wiFi Off type \"WifI_Switcher OFF\"");
                Console.WriteLine("To trun wiFi on type \"WifI_Switcher ON\"");
                Environment.Exit(1);
                }

            //look at all the adapters
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                var temptype = nic.NetworkInterfaceType.ToString();
                

                //found a WiFi adapter 
                if (nic.Speed > maxSpeed && temptype == "Wireless80211")
                {
                    Console.WriteLine("WiFi connection Found.");
                    Console.WriteLine("Name:" + nic.Name);
                    Console.WriteLine("Speed: " + nic.Speed);
                    Console.WriteLine("");
                    WiFi_count++;

                    //Call netsh to enable/disable the card 
                    
                    if (onOff == "OFF")
                    {
                        Console.WriteLine("Disabling WiFi connection.");
                        //netsh interface set interface "nic.name" Disable
                    }

                    if (onOff == "ON")
                    {
                        Console.WriteLine("Enabling WiFi connection.");
                        //netsh interface set interface "nic.name" enable
                    }
                }                
            }
            
            //handel machine with no WiFi card 
            if (WiFi_count ==0)
                {
                Console.WriteLine("No wiFi adapter Found");
                Environment.Exit(0);
                }
            
        }
        
    }
}
