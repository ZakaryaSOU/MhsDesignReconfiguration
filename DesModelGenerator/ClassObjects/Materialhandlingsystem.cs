using MySql.Data.MySqlClient;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserAddIn
{
    public class Materialhandlingsystem : SQLObject
    {
        #region Member Variables
        protected string _idMaterialHandlingSystem;
        protected string _SimulationData_idSimulationData;
        protected List<SQLObject> _materialhandlingequipments;
        protected List<SQLObject> _routenetworks;
        
        
        #endregion
        #region Constructors
        public Materialhandlingsystem(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //Set Connection
            OpenConnection();
            //Get material handling equipments
            _materialhandlingequipments = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM materialhandlingequipment WHERE MaterialHandlingSystem_idMaterialHandlingSystem = " + _idMaterialHandlingSystem, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _materialhandlingequipments.Add(new Materialhandlingequipment(project, reader));
            reader.Close();
            CloseConnection();
           
            
            
            OpenConnection();
            //Get route networks
            _routenetworks = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM routenetwork WHERE MaterialHandlingSystem_idMaterialHandlingSystem = " + _idMaterialHandlingSystem, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _routenetworks.Add(new Routenetwork(project, reader));
            reader.Close();
            CloseConnection();
            
            
        }
        #endregion
            #region Public Properties
        public string IdMaterialHandlingSystem 
        {
            get { return _idMaterialHandlingSystem; }
            set { _idMaterialHandlingSystem = value; }
        }
        public string SimulationData_idSimulationData
        {
            get { return _SimulationData_idSimulationData; }
            set { _SimulationData_idSimulationData = value; }
        }
        public List<SQLObject> Materialhandlingequipments
        {
            get { return _materialhandlingequipments; }
            set { _materialhandlingequipments = value; }
        }
        public List<SQLObject> Routenetworks
        {
            get { return _routenetworks; }
            set { _routenetworks = value; }
        }

        
        #endregion
        public void CreateSimioObject (IDesignContext context)
        { 
          foreach (Materialhandlingequipment MHE in Materialhandlingequipments)
                MHE.CreateSimioObject(context);
          foreach (Routenetwork rn in Routenetworks)
                rn.CreateSimioObject(context);
           
        }
         
        


}
}
