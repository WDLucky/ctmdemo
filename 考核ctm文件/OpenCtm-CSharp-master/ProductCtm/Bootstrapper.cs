using CommonServiceLocator;
using Microsoft.Practices.Prism.MefExtensions;
using ProductCtm.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductCtm
{
    /// <summary>
    /// 初始化，设置启动页面
    /// </summary>
    class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<CreateCtm>();
        }

        /// <summary>
        /// 注册容器
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CreateCtm).Assembly));

        }


        protected override void InitializeShell()
        {
            var mainWindow = ServiceLocator.Current.GetInstance<CreateCtm>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }
    }
}
