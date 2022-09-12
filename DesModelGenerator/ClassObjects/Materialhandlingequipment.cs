using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimioAPI;
using SimioAPI.Extensions;

namespace UserAddIn
{
    public class Materialhandlingequipment : SQLObject
    {
        #region Member Variables
        protected string _idMaterialHandlingEquipment;
        protected string _MaterialHandlingSystem_idMaterialHandlingSystem;
        protected string _MaterialHandlingSystem_SimulationData_idSimulationData;
        protected List<SQLObject> _packagingequipments;
        protected List<SQLObject> _movingequipments;
        protected List<SQLObject> _storageequipments;
        #endregion
        #region Constructors
        public Materialhandlingequipment(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //set connection
            OpenConnection();

            //Get material handling equipments
            _packagingequipments = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM packagingequipment WHERE idPackagingEquipment = " + _idMaterialHandlingEquipment, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _packagingequipments.Add(new Packagingequipment(project, reader));
            reader.Close();
            //CloseConnection();

            //OpenConnection();
            //Get moving equipments

            _movingequipments = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM movingequipment WHERE idMovingEquipment = " + _idMaterialHandlingEquipment, connection) ;
            reader = command.ExecuteReader();
            while (reader.Read())
                _movingequipments.Add(new Movingequipment(project, reader));
            reader.Close();
            //CloseConnection();


           //OpenConnection();
            //Get storage equipments

            _storageequipments = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM storageequipment WHERE idStorageEquipment ="+ _idMaterialHandlingEquipment, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _storageequipments.Add(new Storageequipment(project, reader));
            reader.Close();

            CloseConnection();
        }
        #endregion
        #region Public Properties
        public string IdMaterialHandlingEquipment
        {
            get { return _idMaterialHandlingEquipment; }
            set { _idMaterialHandlingEquipment = value; }
        }
       
        

        internal void CreateSimioObject(IDesignContext context)
        {
            foreach (Movingequipment me in _movingequipments)
            {
                me.CreateSimioObject(context) ;
            }
            foreach (Packagingequipment pe in _packagingequipments)
            {
                pe.CreateSimioObject(context);
            }
            foreach (Storageequipment se in _storageequipments)
            {
                se.CreateSimioObject(context);
            } 
            
        }

        public string MaterialHandlingSystem_idMaterialHandlingSystem
        {
            get { return _MaterialHandlingSystem_idMaterialHandlingSystem; }
            set { _MaterialHandlingSystem_idMaterialHandlingSystem = value; }
        }
        public string MaterialHandlingSystem_SimulationData_idSimulationData
        {
            get { return _MaterialHandlingSystem_SimulationData_idSimulationData; }
            set { _MaterialHandlingSystem_SimulationData_idSimulationData = value; }
        }
        public List<SQLObject> Packagingequipments
        {
            get { return _packagingequipments; }
            set { _packagingequipments = value; }
        }
        public List<SQLObject> Movingequipments
        {
            get { return _movingequipments; }
            set { _movingequipments = value; }
        }
        public List<SQLObject> Storageequipments
        {
            get { return _storageequipments; }
            set { _storageequipments = value; }
        }
        #endregion
    }
}
