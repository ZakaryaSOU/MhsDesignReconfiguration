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
    public class Routenetwork : SQLObject
    {
        #region Member Variables
        protected string _MaterialHandlingSystem_idMaterialHandlingSystem;
        protected string _idRouteNetworkcol;
        protected string _Width;
        protected string _Length;
        protected List<SQLObject> _routes;
        #endregion
        #region Constructors
        public Routenetwork(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //SetConnection
            OpenConnection();

            //Get Routes
            _routes = new List<SQLObject>();
            command = new MySqlCommand("SELECT * FROM route WHERE routenetwork_idRouteNetworkcol = " + _idRouteNetworkcol, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _routes.Add(new Route(project, reader));
            reader.Close();

            CloseConnection();
        }
        #endregion
        #region Public Properties
        public string MaterialHandlingSystem_idMaterialHandlingSystem
        {
            get { return _MaterialHandlingSystem_idMaterialHandlingSystem; }
            set { _MaterialHandlingSystem_idMaterialHandlingSystem = value; }
        }
        public string IdRouteNetworkcol
        {
            get { return _idRouteNetworkcol; }
            set { _idRouteNetworkcol = value; }
        }
        public string Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public string Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        public List<SQLObject> Routes
        {
            get { return _routes; }
            set { _routes = value; }
        }

        internal void CreateSimioObject(IDesignContext context)
        {
            foreach (Route rt in Routes)
                rt.CreateSimioObject(context);
        }
        #endregion
    }
}
