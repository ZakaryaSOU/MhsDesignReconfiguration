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
    public class Handlingunit: SQLObject
    {
        #region Member Variables
        protected string _idHandlingUnit;
        protected string _MaterialType;
        protected string _ContainerCapacity;
        protected string _ContainerSize;
        protected string _ContainerType;
        protected string _SimulationData_idSimulationData;
        protected ITable _Table; 
        #endregion
        #region Constructors
        public Handlingunit(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            project.ListPhysicalObject.Add(this);
        }

        #endregion
        #region Public Properties
        public virtual string IdHandlingUnit
        {
            get { return _idHandlingUnit; }
            set { _idHandlingUnit = value; }
        }
        public virtual string MaterialType
        {
            get { return _MaterialType; }
            set { _MaterialType = value; }
        }
        public virtual string ContainerCapacity
        {
            get { return _ContainerCapacity; }
            set { _ContainerCapacity = value; }
        }
        public virtual string ContainerSize
        {
            get { return _ContainerSize; }
            set { _ContainerSize = value; }
        }
        public virtual string ContainerType
        {
            get { return _ContainerType; }
            set { _ContainerType = value; }
        }
        public virtual string SimulationData_idSimulationData
        {
            get { return _SimulationData_idSimulationData; }
            set { _SimulationData_idSimulationData = value; }
        }

         public virtual ITable Table
         {
             get { return _Table; }
             set { _Table = value; }
         }


        internal void CreateSimioObject(IDesignContext context)
        {
            IIntelligentObject item = null;
            item = context.ActiveModel.Facility.IntelligentObjects.CreateObject("ModelEntity", new FacilityLocation(50, 0, 76));
            item.ObjectName = ContainerType;
        }

        internal void CreateSimioTable(IDesignContext context, Project project, ITable table)
        {
            
            IRow row = null;
            row = table.Rows.Create();
            row.Properties["Reference"].Value = ContainerType;
            row.Properties["Capacity"].Value = ContainerCapacity;

        }
        #endregion
    }
}
