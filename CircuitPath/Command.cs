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
                TaskDialog.Show("Set Circuits Path Mode", "No electrical circuits found.");
                return Result.Succeeded;
            }

            int updatedCount = 0;
            int skippedManual = 0;

            using (Transaction tx = new Transaction(doc, "Set Farthest Device → All Devices"))
            {
                tx.Start();

                foreach (var circuit in circuits)
                {
                    // "Manual" path in UI corresponds to Custom in the API.
                    // Also guard with HasCustomCircuitPath, just in case.
                    if (circuit.CircuitPathMode == ElectricalCircuitPathMode.Custom || circuit.HasCustomCircuitPath)
                    {
                        skippedManual++;
                        continue;
                    }

                    // Only change circuits currently set to Farthest Device
                    if (circuit.CircuitPathMode == ElectricalCircuitPathMode.FarthestDevice)
                    {
                        circuit.CircuitPathMode = ElectricalCircuitPathMode.AllDevices;
                        updatedCount++;
                    }
                    // If already AllDevices (or anything else non-custom), leave as is
                }

                tx.Commit();
            }

            TaskDialog.Show(
                "Set Circuits Path Mode",
                $"Updated {updatedCount} circuit(s) from Farthest Device → All Devices.\n" +
                $"Skipped {skippedManual} circuit(s) with manual/custom path."
            );

            return Result.Succeeded;
        }
    }
}
