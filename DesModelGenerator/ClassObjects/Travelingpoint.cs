using MySql.Data.MySqlClient;
using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserAddIn
{
    public class Travelingpoint : SQLObject
    {
        #region Member Variables
        protected string _idTravelingPoint;
        protected string _Level;
        protected string _XLocation;
        protected string _YLocation;
        protected IIntelligentObject _ITravelingPoint;
        protected List<SQLObject> _materialhandlingactivity;
        #endregion
        #region Constructors
        public Travelingpoint(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //SetConnection
            /* OpenConnection();

             //Get load unload activities
             _loadunloadactivities = new List<SQLObject>();
             command = new MySqlCommand("SELECT * FROM materialhandlingactibity WHERE travelingpoint_idTravelingPoint =" + _idTravelingPoint, connection);
             //command = new MySqlCommand("SELECT * FROM travelingpoint", connection);
             reader = command.ExecuteReader();
             while (reader.Read())
                 _materialhandlingactivity.Add(new Materialhandlingactvity(project, reader));
             reader.Close();

             CloseConnection();*/
        }
        #endregion
        #region Public Properties
        public string IdTravelingPoint
        {
            get { return _idTravelingPoint; }
            set { _idTravelingPoint = value; }
        }
        public string Level
        {
            get { return _Level; }
            set { _Level = value; }
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
        public List<SQLObject> Loadunloadactivities
        {
            get { return _materialhandlingactivity; }
            set { _materialhandlingactivity = value; }
        }
        public IIntelligentObject ITravelingPoint
        {
            get { return _ITravelingPoint; }
            set { _ITravelingPoint = value; }
        }

        internal IIntelligentObject CreateSimioObject(IDesignContext context)
        {
            IIntelligentObject item;

            item = context.ActiveModel.Facility.IntelligentObjects.CreateObject("TransferNode", new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation))); 
            return item;
        }
        #endregion
    }
}
