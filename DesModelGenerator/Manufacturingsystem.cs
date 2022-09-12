using MySql.Data.MySqlClient;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAddIn
{
    public class Manufacturingsystem : SQLObject
    {
        #region Member Variables
        protected string _idManufacturingSystem;
        protected string _SimulationData_idSimulationData;
       // protected List<SQLObject> _machines;
        
        #endregion
        #region Constructors
        public Manufacturingsystem(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //Set connection
            /* OpenConnection();

             //Get Machines

             _machines = new List<SQLObject>();
             command = new MySqlCommand("SELECT * FROM machine", connection);
             reader = command.ExecuteReader();
             while (reader.Read())
                 _machines.Add(new Machine(project, reader));
             reader.Close();

             CloseConnection(); */
        }
        #endregion
        #region Public Properties
        public string IdManufacturingSystem
        {
            get { return _idManufacturingSystem; }
            set { _idManufacturingSystem = value; }
        }
        public string SimulationData_idSimulationData
        {
            get { return _SimulationData_idSimulationData; }
            set { _SimulationData_idSimulationData = value; }
        }
        /*public List<SQLObject> Machines
        {
            get { return _machines; }
            set { _machines = value; }
        }*/

       /* internal void CreateSimioObject(IDesignContext context)
        {
            foreach (Machine mch in Machines)
                mch.CreateSimioObject(context) ;
        }*/
        #endregion
    }
}
