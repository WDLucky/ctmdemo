using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SecondCtm
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            base.OnStartup(e);

        }
    }

    public static class MyBootstrapper
    {
        public static CompositionContainer Container { get; set; }

    }
}
