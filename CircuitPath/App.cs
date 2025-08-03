using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace CircuitPath
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private RibbonPanel ribbonPanel;

        public Result OnStartup(UIControlledApplication application)
        {
            const string tabName = "RK Tools";
            const string panelName = "Small Tools";

            try { application.CreateRibbonTab(tabName); } catch { }
            ribbonPanel = application.CreateOrSelectPanel(tabName, panelName);

            // Create the buttons
            var btn1 = ribbonPanel.CreatePushButton<Command>("Set circuit path")
                .SetLargeImage("Resources/AllDevices.tiff")
                .SetToolTip("Set circuit path mode to \"All Devices\" for all circuits in the project.")
                .SetContextualHelp("https://raulkalev.github.io/rktools/");


            // Stack them (they will appear as small buttons in a row)
            ribbonPanel.RowStackedItems(btn1);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }
}
