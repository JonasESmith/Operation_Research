using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using OptGui.Services;
using OptGui.Views;

namespace OptGui
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<Services.IProblemType1918, Services.KnapsackSolver>();
            containerRegistry.Register<Services.IKnapsackRow, Services.KnapsackRow>();
        }

        protected override Window CreateShell()
        {
            Views.MainWindow w = Container.Resolve<Views.MainWindow>();
            return w;
        }
    }
}
