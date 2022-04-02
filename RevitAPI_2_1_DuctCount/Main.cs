using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RevitAPI_2_1_DuctCount
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new DuctFilter(), "Выберите воздуховоды"); //список ссылок на элементы
            var ductList = new List<Duct>(); //присвоение элементам списка самих элементов из списка ссылок выше


            string info = string.Empty;
            foreach (var selectedElement in selectedElementRefList)
            {
                //Element element = doc.GetElement(selectedElement);  ПРОВЕРКА НА ТИП. НЕ НУЖНА КОГДА РЕАЛИЗОВАН iFilter
                //if(element is Wall)
                //{
                //    Wall oWall= (Wall)element;
                //    wallList.Add(oWall);
                //}

                Duct oDuct = doc.GetElement(selectedElement) as Duct;
                ductList.Add(oDuct);
                //var width = UnitUtils.ConvertFromInternalUnits(oWall.Width, DisplayUnitType.DUT_MILLIMETERS);//конвертируем в милимиетры. стандартно в ревите всегда футы
                //info += $"Name: {oWall.Name}, width: {width}";

            }
            info += $"Количество воздуховодов: {ductList.Count}";

            TaskDialog.Show("Selection", info); //вывод 

            return Result.Succeeded;
        }
    }

    
}
