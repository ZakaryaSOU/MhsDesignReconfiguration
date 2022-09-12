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
    public class Route : SQLObject
    {
        #region Member Variables
        protected string _idRoute;
        protected string _Length;
        protected string _Width;
        //protected string _RouteNetwork_MaterialHandlingSystem_idMaterialHandlingSystem;
        protected string _routeNetwork_idRouteNetworkcol;
        protected string _From_idNode;
        protected string _To_idNode;
        protected string _Direction;
        protected List<SQLObject> _Nodes;
        //protected List<SQLObject> _route_has_travelingpoint;
        #endregion
        #region Constructors
        public Route(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {


            OpenConnection();

            //Get Nodes
            _Nodes = new List<SQLObject>();

            command = new MySqlCommand("SELECT * FROM node WHERE idNode=" + _From_idNode, connection);


            reader = command.ExecuteReader();
            while (reader.Read())
                _Nodes.Add(new Node(project, reader));
            reader.Close();

            command = new MySqlCommand("SELECT * FROM node WHERE idNode=" + _To_idNode, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                _Nodes.Add(new Node(project, reader));
            reader.Close();

            //Cancel Connection
            CloseConnection();

            //  MessageBox.Show("Node DATA done");
            project.ListPhysicalObject.Add(this);

        }
        #endregion
        #region Public Properties
        public string IdRoute
        {
            get { return _idRoute; }
            set { _idRoute = value; }
        }
        public string Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        public string Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        /*public string RouteNetwork_MaterialHandlingSystem_idMaterialHandlingSystem
        {
            get { return _RouteNetwork_MaterialHandlingSystem_idMaterialHandlingSystem; }
            set { _RouteNetwork_MaterialHandlingSystem_idMaterialHandlingSystem = value; }
        }*/
        public string routeNetwork_idRouteNetworkcol
        {
            get { return _routeNetwork_idRouteNetworkcol; }
            set { _routeNetwork_idRouteNetworkcol = value; }
        }

        public string From_idNode
        {
            get { return _From_idNode; }
            set { _From_idNode = value; }
        }

        public string To_idNode
        {
            get { return _To_idNode; }
            set { _To_idNode = value; }
        }
        public List<SQLObject> Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        public string Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }



        /*internal void CreateSimioObject(IDesignContext context)
        {
            foreach (Node n in Nodes)
                n.CreateSimioObject(context); 
        }
        */
        internal new void CreateSimioLink(IDesignContext context, Project project)
        {
            //The code below works for travelingpoints


            /* 
             IIntelligentObject path;
            IIntelligentObject fromNode = ((Node)project.Nodes.Find(fn => ((Node)fn).idNode == From_idNode )).INode;
            IIntelligentObject toNode = ((Node)project.Nodes.Find(fn => ((Node)fn).idNode == To_idNode)).INode;
            ILinkObject pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode, (INodeObject)toNode, null) as ILinkObject;
            */
            Node fromNode, toNode = null;



            ILinkObject pathObject;
            fromNode = ((Node)project.Nodes.Find(fn => (((Node)fn).idNode == From_idNode)));
            toNode = ((Node)project.Nodes.Find(fn => (((Node)fn).idNode == To_idNode)));
            if (fromNode.Travelingpoint != null && toNode.Travelingpoint != null)
            {
                if (Direction == "uni")
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;

                else if (Direction == "bi")
                {
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;
                    pathObject.Properties["Type"].Value = "Bidirectional";
                }

            }
            else if (fromNode.Travelingpoint != null && toNode.Machine != null)
            {
                if (Direction == "uni")
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;

                else if (Direction == "bi")
                {
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;
                    pathObject.Properties["Type"].Value = "Bidirectional";
                }
            }

            else if (fromNode.Machine != null &&  toNode.Travelingpoint != null)
            {
                if (Direction == "uni")
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;
                else if (Direction == "bi")
                {
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;
                    pathObject.Properties["Type"].Value = "Bidirectional";
                }
            }
            else if (fromNode.Machine != null && toNode.Machine != null)
            {
                if (Direction == "uni")
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;

                else if (Direction == "bi")
                {
                    pathObject = context.ActiveModel.Facility.IntelligentObjects.CreateLink("Path", (INodeObject)fromNode.INode, (INodeObject)toNode.INode, null) as ILinkObject;
                    pathObject.Properties["Type"].Value = "Bidirectional";
                }
            }
            else
                throw new Exception("error");


            }
                #endregion
            }
            }
        
    

