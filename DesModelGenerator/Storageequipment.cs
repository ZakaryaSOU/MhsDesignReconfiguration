using MySql.Data.MySqlClient;
using SimioAPI.Extensions;
using SimioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserAddIn
{
    public class Storageequipment : SQLObject
    {
        #region Member Variables
        protected string _idStorageEquipment;
        protected string _Type;
        protected string _Size;
        protected string _Capacity;
        protected string _XLocation;
        protected string _YLocation;
        
        #endregion
        #region Constructors
        public Storageequipment(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            project.ListPhysicalObject.Add(this);


        }
        #endregion
        #region Public Properties

        public virtual string Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public virtual string Capacity
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

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string IdStorageEquipment
        {
            get { return _idStorageEquipment; }
            set { _idStorageEquipment = value; }
        }

        internal void CreateSimioObject(IDesignContext context)
        {

          context.ActiveModel.Facility.IntelligentObjects.CreateObject(Type, new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation)));
        }
        #endregion
    }
}
