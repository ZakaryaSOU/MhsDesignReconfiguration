using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserAddIn
{
    class UserAddIn : IDesignAddIn
    {
        #region IDesignAddIn Members

        /// <summary>
        /// Property returning the name of the add-in. This name may contain any characters and is used as the display name for the add-in in the UI.
        /// </summary>
        public string Name
        {
            get { return "UserAddIn"; }
        }

        /// <summary>
        /// Property returning a short description of what the add-in does.
        /// </summary>
        public string Description
        {
            get { return "Automatic Generation of DES models"; }
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
            // This example code places some new objects from the Standard Library into the active model of the project.
            if (context.ActiveModel != null)
            {
                // Example of how to place some new fixed objects into the active model.
                // This example code places three new fixed objects: a Source, a Server, and a Sink.
                IIntelligentObjects intelligentObjects = context.ActiveModel.Facility.IntelligentObjects;
                IFixedObject sourceObject = intelligentObjects.CreateObject("Source", new FacilityLocation(-10, 0, -10)) as IFixedObject;
               
                IFixedObject serverObject = intelligentObjects.CreateObject("Server", new FacilityLocation(0, 0, 0)) as IFixedObject;
                serverObject.ObjectName = "server1";
                
                IFixedObject sinkObject = intelligentObjects.CreateObject("Sink", new FacilityLocation(10, 0, 10)) as IFixedObject;



                IFixedObject Sep = intelligentObjects.CreateObject("Separator", new FacilityLocation(50, 0, 50)) as IFixedObject;
                IFixedObject Comb = intelligentObjects.CreateObject("Combiner", new FacilityLocation(60, 0, 50)) as IFixedObject;

                IIntelligentObject node1 = context.ActiveModel.Facility.IntelligentObjects.CreateObject("Node", new FacilityLocation(5, 5, 5));

                // Example of how to place some new link objects into the active model (to add network paths between nodes).
                // This example code places two new link objects: a Path connecting the Source 'output' node to the Server 'input' node,
                // and a Path connecting the Server 'output' node to the Sink 'input' node.
               // INodeObject sourceOutputNode = sourceObject.Nodes[0];
                INodeObject serverInputNode = serverObject.Nodes[0];
                INodeObject serverOutputNode = serverObject.Nodes[1];


                INodeObject IN1 = Sep.Nodes[0];
                INodeObject IN12 = Sep.Nodes[1];
                INodeObject OUT1 = Sep.Nodes[2];
                INodeObject IN2 = Comb.Nodes[0];
                INodeObject IN21 = Comb.Nodes[1];
                INodeObject OUT2 = Comb.Nodes[2];
                //INodeObject travelingpoint = node1.Nodes[0];
                INodeObject sinkInputNode = sinkObject.Nodes[0];
                ILinkObject pathObject1 = intelligentObjects.CreateLink("Path", sourceObject.Nodes[0], (INodeObject)node1, null) as ILinkObject;
                ILinkObject pathObject3 = intelligentObjects.CreateLink("Path", (INodeObject)node1, serverInputNode, null) as ILinkObject;
                ILinkObject pathObject2 = intelligentObjects.CreateLink("Path", serverOutputNode, sinkInputNode, null) as ILinkObject;

                intelligentObjects.CreateLink("Path", OUT1, IN2, null);
                intelligentObjects.CreateLink("Path", OUT2, IN1, null);

                IPropertyDefinition propertyDefinition = context.ActiveModel.PropertyDefinitions.AddStringProperty("TableName", "null");
                propertyDefinition.CategoryName = "OUPrpperties";

                IPropertyDefinition propertyDefinition1 = context.ActiveModel.PropertyDefinitions.AddStringProperty("ObjectsRefColumnName", "null");
                propertyDefinition1.CategoryName = "OUPrpperties";

                IPropertyDefinition propertyDefinition2 = context.ActiveModel.PropertyDefinitions.AddStringProperty("ObjectsColumnName", "null");
                propertyDefinition2.CategoryName = "OUPrpperties";

                IPropertyDefinition propertyDefinition3 = context.ActiveModel.PropertyDefinitions.AddStringProperty("XLocation", "null");
                propertyDefinition3.CategoryName= "OUPrpperties";

                IPropertyDefinition propertyDefinition4 = context.ActiveModel.PropertyDefinitions.AddStringProperty("ZLocation", "null");
                propertyDefinition4.CategoryName = "OUPrpperties";
                ITable table = null;
                ITableColumn tableColumn = null;
                IRow row = null;
                table = context.ActiveModel.Tables.Create("what_the_hell");
                table.Columns.AddActivityReferenceColumn("test");
                table.Columns.AddActivityReferenceColumn("test2");
                row = table.Rows.Create();
                
                row.Properties["test"].Value = "test";


                
                //  MessageBox.Show(context.ActiveModel.Properties["Source1"].Name);

                //objectname = context.ActiveModel.Facility.IntelligentObjects[objectProperties.Item1];
                //objectname.properties[objectProperties.Item2].Value = objectProperties.Item3; 
                // Example of how to edit the property of an object.
                // This example code edits the 'ProcessingTime' property of the added Server object.
                serverObject.Properties["ProcessingTime"].Value = "100";
                sourceObject.Properties["EntitiesPerArrival"].Value = "50";

                //if travelingpoint machine id machine not null, avoid

            }// trouver comment commencer la simulation ''IMODEL.''
        }

        #endregion
    }
}
