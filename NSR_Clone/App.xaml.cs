using Microsoft.EntityFrameworkCore;
using NSR_Clone.Data;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NSR_Clone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var db = new AppDbContext();
            db.Database.Migrate(); // sets Migrates at startup
        }
    }
}
