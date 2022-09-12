using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SimioAPI.Extensions;

namespace UserAddIn
{
    public class Simulationdata: SQLObject
    {
        #region Member Variables
        protected string _idSimulationData;
        protected List<SQLObject> _handlingunits;
        protected List<SQLObject> _manufacturingsystems;
        protected List<SQLObject> _materialhandlingsystems;
        
        
        #endregion
        #region Constructors
        public Simulationdata(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //Set connection
            OpenConnection();

            //Get handling units
            _handlingunits = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM handlingunit", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _handlingunits.Add(new Handlingunit(project, reader));
            reader.Close();
            //CloseConnection();
            //MessageBox.Show("Handling data done");

            //Get manufacturing systems 
            //OpenConnection();
            _manufacturingsystems = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM manufacturingsystem", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _manufacturingsystems.Add(new Manufacturingsystem(project, reader));
            reader.Close();
            //CloseConnection();
            //MessageBox.Show("Manufacturing data done");

            //Get materialhandling systems 
            //OpenConnection();
            _materialhandlingsystems = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM materialhandlingsystem", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _materialhandlingsystems.Add(new Materialhandlingsystem(project, reader));
            reader.Close();
            CloseConnection();
            //MessageBox.Show("MHS DATA done");
        }
        public Simulationdata():base()
        {
        }
        #endregion
            #region Public Properties
        public string IdSimulationData
        {
            get { return _idSimulationData; }
            set { _idSimulationData = value; }
        }

        public List<SQLObject> Handlingunits
        {
            get { return _handlingunits; }
            set { _handlingunits = value; }
        }
        public List<SQLObject> Manufacturingsystems
        {
            get { return _manufacturingsystems; }
            set { _manufacturingsystems = value; }
        }
        public List<SQLObject> Materialhandlingsystems
        {
            get { return _materialhandlingsystems; }
            set { _materialhandlingsystems = value; }
        }
        //rajouté pour tester
        

        internal void CreateSimioObject(IDesignContext context)
        {
            /* IIntelligentObject item = context.ActiveModel.Facility.IntelligentObjects.[row.Properties[str].Value];
                            item = context.ActiveModel.Facility.IntelligentObjects.CreateObject(row.Properties[value1].Value, new FacilityLocation(double.Parse(row.Properties[str1].Value), 0, double.Parse(row.Properties[value2].Value)));
                            item.ObjectName = row.Properties[str].Value;*/
            foreach (Handlingunit hu in _handlingunits)
            {
                hu.CreateSimioObject(context);
            }
            /*foreach (Manufacturingsystem mu in _manufacturingsystems)
            {
                mu.CreateSimioObject(context);
            }*/
            foreach (Materialhandlingsystem mh in _materialhandlingsystems)
            {
                mh.CreateSimioObject(context);
            }
           
        }
        #endregion
    }
}
