using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UserAddIn
{
    class AddInClass : IDesignAddIn
    {
        public string Name
        {
            get { return "AddInClass"; }
        }

        /// <summary>
        /// Property returning a short description of what the add-in does.
        /// </summary>
        public string Description
        {
            get { return "Automatic Generation of DES models."; }
        }

        /// <summary>
        /// Property returning an icon to display for the add-in in the UI.
        /// </summary>
        public System.Drawing.Image Icon
        {
            get { return null; }
        }

        /// <summary>
        /// Method called when the add-in is run.
        /// </summary>

        public void Execute(IDesignContext context)
        {
            Project p = new Project(null, null);

            if (context.ActiveModel != null)
            {
                context.ActiveModel.BulkUpdate((IModel model) =>
                {
                    //Import Handling Unit properties
                    ITable table;
                    table = context.ActiveModel.Tables.Create("Handling_Unit");
                    table.Columns.AddEntityReferenceColumn("Reference");
                    table.Columns.AddIntegerColumn("Capacity", 0);

                    foreach (SQLObject obj in p.HandlingUnits)
                        ((Handlingunit)obj).CreateSimioTable(context, p, table);

                    //Import layout and resource data

                    foreach (SQLObject obj in p.Nodes)
                        ((Node)obj).CreateSimioObject(context);

                    foreach (SQLObject obj in p.Nodes)
                    {
                        if (((Node)obj).Machine != null)
                            ((Machine)((Node)obj).Machine).CreateSimioTable(context, p);
                    }
                    foreach (SQLObject obj in p.StorageEquipments)
                        ((Storageequipment)obj).CreateSimioObject(context);

                    foreach (SQLObject obj in p.MovingEquipments)
                        ((Movingequipment)obj).CreateSimioObject(context);

                    foreach (SQLObject obj in p.HandlingUnits)
                        ((Handlingunit)obj).CreateSimioObject(context);

                    foreach (SQLObject obj in p.Routes)
                        ((Route)obj).CreateSimioLink(context,p);
                    
                });

            }
        }


    }
}
