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
    public class Node: SQLObject
    {
        #region Member Variables
        protected string _idNode;
        protected string _machine_idMachine;
        protected string _travelingpoint_idTravelingPoint;
        protected SQLObject _machine;
        protected SQLObject _travelingpoint;
        protected IIntelligentObject _INode;

        #endregion
        #region Constructors
        public Node(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {

            if (_machine_idMachine == "")
            {
                //Get TravelingPoints
                OpenConnection();

                command = new MySqlCommand("SELECT * FROM travelingpoint WHERE IdTravelingPoint =" + _travelingpoint_idTravelingPoint, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                    _travelingpoint = new Travelingpoint(project, reader);
                reader.Close();
                CloseConnection();

            }

            else
            {
                //Get Machines
                OpenConnection();


                command = new MySqlCommand("SELECT * FROM machine WHERE idMachine =" + _machine_idMachine, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                    _machine= new Machine(project, reader);
                reader.Close();
                CloseConnection();

            }

            //project.ListPhysicalObject.Add(this);
            project.AddPhysicalObject(this);


        }
        #endregion
        #region Public Properties
        public string idNode
        {
            get { return _idNode; }
            set { _idNode = value; }
        }
        public string machine_idMachine
        {
            get { return _machine_idMachine; }
            set { _machine_idMachine = value; }
        }
        public string travelingpoint_idTravelingPoint
        {
            get { return _travelingpoint_idTravelingPoint; }
            set { _travelingpoint_idTravelingPoint = value; }
        }
        public SQLObject Machine
        {
            get { return _machine; }
            set { _machine = value; }
        }
        public SQLObject Travelingpoint
        {
            get { return _travelingpoint; }
            set { _travelingpoint = value; }
        }
        public IIntelligentObject INode
        {
            get { return _INode; }
            set { _INode = value; }
        }

        internal new void CreateSimioObject(IDesignContext context)
        {
            if (machine_idMachine != "")
            {
                _INode = ((Machine)Machine).CreateSimioObject(context);
               //MessageBox.Show("Machine created");
            }
            else if (travelingpoint_idTravelingPoint != "")
            {
                _INode = ((Travelingpoint)Travelingpoint).CreateSimioObject(context);
                //MessageBox.Show("travelingpoint created");
            }
            else
                throw new Exception("ids");
        }
       
        #endregion
    }
}
