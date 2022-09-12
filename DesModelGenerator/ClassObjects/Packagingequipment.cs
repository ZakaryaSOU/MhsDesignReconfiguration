using MySql.Data.MySqlClient;
using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAddIn
{
    public class Packagingequipment : SQLObject
    {
        #region Member Variables
        protected string _idPackagingEquipment;
        protected string _Type;
        protected string _ProcessingTime;
        protected string _Capacity;
        protected string _XLocation;
        protected string _YLocation;
        #endregion
        #region Constructors
        public Packagingequipment(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
        }
        #endregion
        #region Public Properties
        public string IdPackagingEquipment
        {
            get { return _idPackagingEquipment; }
            set { _idPackagingEquipment = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string ProcessingTime
        {
            get { return _ProcessingTime; }
            set { _ProcessingTime = value; }
        }
        public string Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
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

        internal void CreateSimioObject(IDesignContext context)
        {
            context.ActiveModel.Facility.IntelligentObjects.CreateObject(Type, new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation)));

        }
        #endregion
    }
}
