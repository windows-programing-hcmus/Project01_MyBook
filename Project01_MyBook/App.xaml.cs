using Project01_MyBook.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project01_MyBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Username { get; set; }
        public static string OnStart { get; set; } = AppConfig.GetValue("OnStart") ?? " ";
        public static string LastScreen { get; set; } = AppConfig.GetValue("LastScreen") ?? " ";
        protected override void OnStartup(StartupEventArgs e)
        {

            if (OnStart.Equals("last"))
            {
                this.StartupUri = new System.Uri(LastScreen, System.UriKind.Relative);
                Username = AppConfig.GetValue(AppConfig.Username) ?? " ";
            }
            else if (OnStart.Equals("main"))
            {
                this.StartupUri = new System.Uri("GUI/mainwindow.xaml", System.UriKind.Relative);
                Username = AppConfig.GetValue(AppConfig.Username) ?? " ";
            }
            else
            {
                this.StartupUri = new System.Uri("GUI/loginwindow.xaml", System.UriKind.Relative);
            }
        }
    }
}
