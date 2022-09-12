using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAddIn
{
    public class Materialhandlingactvity : SQLObject
    {
        #region Member Variables
        protected string _idMaterialHandlingActivity;
        protected string _ProcessingTime;
        protected string _travelingpoint_idTravelingPoint;
        protected string _machine_idMachine;
        protected string _materialhandlingequipment_idMaterialHandlingEquipment;
        #endregion
        #region Constructors
        public Materialhandlingactvity(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
        }
        #endregion
        #region Public Properties
        public string IdMaterialHandlingActivity
        {
            get { return _idMaterialHandlingActivity; }
            set { _idMaterialHandlingActivity = value; }
        }
        public string ProcessingTime
        {
            get { return _ProcessingTime; }
            set { _ProcessingTime = value; }
        }
        public string Travelingpoint_idTravelingPoint
        {
            get { return _travelingpoint_idTravelingPoint; }
            set { _travelingpoint_idTravelingPoint = value; }
        }
        public string Machine_idMachine
        {
            get { return _machine_idMachine; }
            set { _machine_idMachine = value; }
        }
        public string Materialhandlingequipment_idMaterialHandlingEquipment
        {
            get { return _materialhandlingequipment_idMaterialHandlingEquipment; }
            set { _materialhandlingequipment_idMaterialHandlingEquipment = value; }
            #endregion
        }
    }
}
