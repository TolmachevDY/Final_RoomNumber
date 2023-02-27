using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

namespace RoomNumber
{
    [Transaction(TransactionMode.Manual)]
    public class AddRoomNumber : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //обращаемся к документу
            Document doc = commandData.Application.ActiveUIDocument.Document;
            
            List<Room> rooms = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .OfType<Room>()
                .ToList();

            int number = 1;
            Transaction transaction = new Transaction(doc);
            transaction.Start("Нумерация комнат");
            foreach (Room room in rooms)
            {
                room.get_Parameter(BuiltInParameter.ROOM_NUMBER).Set(number.ToString());
                number++;
            }
            transaction.Commit();
            return Result.Succeeded;
        }
    }
}
