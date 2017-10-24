using Prism.Mef;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace SecondCtm
{
    class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            MyBootstrapper.Container = Container;
            return Container.GetExportedValue<MainWindow>();
        }

        /// <summary>
        /// 注册mef容器
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.LoadFrom("Cube.Impl.dll")));         
        }

        /// <summary>
        /// 让页面显示出来
        /// </summary>
        protected override void InitializeShell()
        {
            var main = Container.GetExportedValue<MainWindow>();
            Application.Current.MainWindow = main;
            Application.Current.MainWindow.Show();
        }

    }
}
