using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System.Linq;

namespace CircuitPath
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            var circuits = new FilteredElementCollector(doc)
                .OfClass(typeof(ElectricalSystem))
                .Cast<ElectricalSystem>()
                .ToList();

            if (circuits.Count == 0)
            {
                TaskDialog.Show("Set All Circuits", "No electrical circuits found.");
                return Result.Succeeded;
            }

            int setCount = 0;

            using (Transaction tx = new Transaction(doc, "Set Circuits Path Mode to All Devices"))
            {
                tx.Start();
                foreach (var circuit in circuits)
                {
                    // Check current value to avoid unnecessary sets
                    if (circuit.CircuitPathMode != ElectricalCircuitPathMode.AllDevices)
                    {
                        circuit.CircuitPathMode = ElectricalCircuitPathMode.AllDevices;
                        setCount++;
                    }
                }
                tx.Commit();
            }

            TaskDialog.Show("Set All Circuits",
                $"Updated {setCount} circuit(s) to Path Mode = All Devices.");

            return Result.Succeeded;
        }
    }
}
