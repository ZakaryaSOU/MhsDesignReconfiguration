using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimioAPI;
using SimioAPI.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace UserAddIn
{
    public class Project: SQLObject
    {
        List<SQLObject> _listPhysicalObject;
        List<SQLObject> _listSimulationData;
        public Project(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            ListPhysicalObject = new List<SQLObject>();
            //Establish connection
            OpenConnection();
            //Get Simulation Data
            //ReadListProperties(this, ref _listSimulationData, typeof(Simulationdata), "Select * From simulationdata") ;
            _listSimulationData = new List<SQLObject>();
            command = new MySqlCommand("Select * From simulationdata", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _listSimulationData.Add(new Simulationdata(this, reader));
            reader.Close();
            CloseConnection(); 
        }

        public List<SQLObject> ListSimulationData
        {
            get { return _listSimulationData; }
            set { _listSimulationData = value; }
        }
        public List<SQLObject> ListPhysicalObject
        {
            get { return _listPhysicalObject; }
            set { _listPhysicalObject = value; }
        }

        public List<SQLObject> SimulationData => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Simulationdata));

        public List<SQLObject> TravelingPoints => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Travelingpoint));

        public List<SQLObject> Routes => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Route));

        public List<SQLObject> s => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Queue));

        public List<SQLObject> Machines => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Machine));

        public List<SQLObject> StorageEquipments => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Storageequipment));

        public List<SQLObject> MovingEquipments => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Movingequipment));

        public List<SQLObject> HandlingUnits => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Handlingunit));

        public List<SQLObject> MachineLogs => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Machinelog));

        public List<SQLObject> Nodes => ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Node));

        public void AddPhysicalObject(SQLObject obj)
        {
            List<SQLObject> dummy = new List<SQLObject>();
            switch (obj.GetType().Name)
            {
                case "Travelingpoint":
                    dummy = ListPhysicalObject.FindAll(tp => tp.GetType() == typeof(Travelingpoint));
                    if (dummy.Count == 0 || dummy.Find(n => ((Travelingpoint)n).IdTravelingPoint == ((Travelingpoint)obj).IdTravelingPoint) == null)
                        ListPhysicalObject.Add(obj);
                    break;
                case "Queue":
                    dummy = ListPhysicalObject.FindAll(q => q.GetType() == typeof(Queue));
                    if (dummy.Count == 0 || dummy.Find(n => ((Queue)n).IdQueue == ((Queue)obj).IdQueue) == null)
                        ListPhysicalObject.Add(obj);
                    break;
                case "Node":
                    dummy = ListPhysicalObject.FindAll(q => q.GetType() == typeof(Node));
                    if (dummy.Count == 0 || dummy.Find(n => ((Node)n).idNode == ((Node)obj).idNode) == null)
                        ListPhysicalObject.Add(obj);
                    break;
                case "Machinelogs":
                    dummy = ListPhysicalObject.FindAll(q => q.GetType() == typeof(Machinelog));
                    if (dummy.Count == 0 || dummy.Find(n => ((Machinelog)n).IdMachineLogs == ((Machinelog)obj).IdMachineLogs) == null)
                        ListPhysicalObject.Add(obj);
                    break;
                case "Machine":
                    dummy = ListPhysicalObject.FindAll(q => q.GetType() == typeof(Machine));
                    if (dummy.Count == 0 || dummy.Find(n => ((Machine)n).IdMachine == ((Machine)obj).IdMachine) == null)
                        ListPhysicalObject.Add(obj);
                    break;
                default:
                    ListPhysicalObject.Add(obj);
                    break;

            }

        }
    }
}

