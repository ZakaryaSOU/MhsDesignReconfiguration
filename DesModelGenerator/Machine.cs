using MySql.Data.MySqlClient;
using SimioAPI.Extensions;
using SimioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserAddIn
{
    public class Machine : SQLObject
    {
        #region Member Variables
        protected string _idMachine;
        protected string _Name;
        protected string _Type;
        protected string _Capacity;
        protected string _ScStrategy;
        protected string _ProcessingTime;
        protected string _XLocation;
        protected string _YLocation;
        protected string _idManufacturingSystem;
        protected List<SQLObject> _queues;
        protected List<SQLObject> _machinelogs;
        protected IIntelligentObject _IMachine;
        #endregion
        #region Constructors
        public Machine(Project project, MySqlDataReader parentReader):base(project, parentReader)
        {

            //Get Queues
            OpenConnection();
            _queues = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM queue WHERE Machine_idMachine = " + _idMachine, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _queues.Add(new Queue(project, reader));
            reader.Close();

            //Get MachineLogs
            _machinelogs = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM machinelogs WHERE machine_idMachine = " + _idMachine, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _machinelogs.Add(new Machinelog(project, reader));
            reader.Close();
            CloseConnection();
        }

        #endregion
        #region Public Properties
        public string IdMachine
        {
            get { return _idMachine; }
            set { _idMachine = value; }
        }
        public string ScStrategy
        {
            get { return _ScStrategy; }
            set { _ScStrategy = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string ProcessingTime
        {
            get { return _ProcessingTime; }
            set { _ProcessingTime = value; }
        }
        public string IdManufacturingSystem
        {
            get { return _idManufacturingSystem; }
            set { _idManufacturingSystem = value; }
        }
        public string XLocation
        {
            get { return _XLocation; }
            set { _XLocation = value; }
        }
        public string YLocation
        {
            get { return _YLocation; }
            set { _YLocation = value; }
        }
        public IIntelligentObject IMachine
        {
            get { return _IMachine; }
            set { _IMachine = value; }
        }

        internal IIntelligentObject CreateSimioObject(IDesignContext context)
        {
            IIntelligentObject item, item5, item6;
            item = null;
            item5 = null;
            if (ScStrategy == "push" && Type == "Source")
            {
                
                item6 = context.ActiveModel.Facility.IntelligentObjects.CreateObject(Type, new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation)));
                item6.ObjectName = Name;
                item6.Properties["EntityType"].Value = item6.ObjectName + "_Logs.Container_Type";
                item6.Properties["ArrivalTimeProperty"].Value = item6.ObjectName + "_Logs.Date";


                // a enlenver
                item = ((IFixedObject)item6).Nodes[0];
            }
            else if (ScStrategy == "push" && Type == "Server")
            {
                item5 = context.ActiveModel.Facility.IntelligentObjects.CreateObject(Type, new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation) - 10));
                item5.ObjectName = Name;
                item = context.ActiveModel.Facility.IntelligentObjects.CreateObject("TransferNode", new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation) ));
                context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)item, ((IFixedObject)item5).Nodes[0], null);
                context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", ((IFixedObject)item5).Nodes[1], (INodeObject)item, null);
            }

            else if (ScStrategy == "pull")
            {
                MessageBox.Show("this a pull mmachibe" + Name + " and " + Type); 
                IIntelligentObject item1, item2, item3, item4;

                item2 = context.ActiveModel.Facility.IntelligentObjects.CreateObject("Source", new FacilityLocation(double.Parse(XLocation) - 6, 0, double.Parse(YLocation)));
                item2.ObjectName = Name;
                item2.Properties["EntityType"].Value = item2.ObjectName + "_Logs.Container_Type";
                item2.Properties["ArrivalTimeProperty"].Value = item2.ObjectName + "_Logs.Date";

                item1 = context.ActiveModel.Facility.IntelligentObjects.CreateObject("Separator", new FacilityLocation(double.Parse(XLocation) - 3, 0, double.Parse(YLocation)));
                item1.Properties["SeparationMode"].Value = "Make Copies";
                item1.Properties["CopyQuantity"].Value = "1";
                item1.Properties["CopyEntityType"].Value = "Same As Original Entity";
                item6 = context.ActiveModel.Facility.IntelligentObjects.CreateObject("Combiner", new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation)));
                item6.Properties["BatchQuantity"].Value = "1";
                item6.Properties["MatchingRule"].Value = "Match Members";
                item3 = context.ActiveModel.Facility.IntelligentObjects.CreateObject("Sink", new FacilityLocation(double.Parse(XLocation) + 3, 0, double.Parse(YLocation) + 2));
                item4 = context.ActiveModel.Facility.IntelligentObjects.CreateObject("Sink", new FacilityLocation(double.Parse(XLocation) + 3, 0, double.Parse(YLocation) - 2));


                context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", ((IFixedObject)item2).Nodes[0], ((IFixedObject)item1).Nodes[0], null);
                context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", ((IFixedObject)item1).Nodes[2], ((IFixedObject)item6).Nodes[1], null);
                context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", ((IFixedObject)item6).Nodes[2], ((IFixedObject)item3).Nodes[0], null);
                context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", ((IFixedObject)item6).Nodes[2], ((IFixedObject)item4).Nodes[0], null);
                
                item = ((IFixedObject)item6).Nodes[0];
            }
            else
                throw new Exception("error");
            return item;
            
            }
        public List<SQLObject> Queues
        {
            get { return _queues; }
            set { _queues = value; }
        }

        public List<SQLObject> MachineLogs
        {
            get { return _machinelogs; }
            set { _machinelogs = value; }
        }



        internal void CreateSimioTable(IDesignContext context, Project p)
        {
            ITable table;
            
            table = context.ActiveModel.Tables.Create(Name+"_Logs");
            table.Columns.AddIntegerColumn("idlog", 0);
            table.Columns.AddDateTimeColumn("Date", DateTime.Now);
            table.Columns.AddObjectReferenceColumn("Container_Type");
            
            foreach (Machinelog obj in MachineLogs)
            {
                obj.AddNewLog(context, table, Name) ;
            }

           


        }
        
        #endregion
    }
}
