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
    public class Machinelog : SQLObject
    {
        #region Member Variables
        protected string _idmachinelogs;
        protected string _Date;
        protected string _machine_idMachine;
        protected string _Name;
        protected string _handlingunit_IdHandlingUnit;
        protected string _ContainerType;
        
         



        #endregion
        #region Constructors
        public Machinelog(Project project, MySqlDataReader parentReader) : base(project, parentReader)
    {
        
    }

    #endregion
    #region Public Properties
    public string IdMachineLogs
    {
        get { return _idmachinelogs; }
        set { _idmachinelogs = value; }
    }
    public string Date
    {
        get { return _Date; }
        set { _Date = value; }
    }

        public string machine_IdMachine
            {
            get { return _machine_idMachine; }
            set { _machine_idMachine = value; }
        }
        public string Handlingunit_IdHandlingUnit
        {
            get { return _handlingunit_IdHandlingUnit; }
            set { _handlingunit_IdHandlingUnit = value; }
        }
        
        
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string ContainerType
        {
            get { return _ContainerType; }
            set { _ContainerType = value; }
        }

        internal void AddNewLog(IDesignContext context, ITable table, string tmachine)
        {
            // MessageBox.Show("This type is from the machinelogs: 1= " +tmachine+" 2:"+ MachineType);



            if (tmachine.Equals(Name))
            {
                IRow row2 = null;
                row2 = table.Rows.Create();
                row2.Properties["idlog"].Value = IdMachineLogs;
                row2.Properties["Date"].Value = Date;
                row2.Properties["Container_Type"].Value = ContainerType;
            }                             
             
            






        }

        /* internal void CreateSimioObject(IDesignContext context)
         {
             context.ActiveModel.Facility.IntelligentObjects.CreateObject("Source", new FacilityLocation(double.Parse(XLocation), 0, double.Parse(YLocation)));
         }

         /* public string TravelingPoint_idTravelingPoint
          {
              get { return _TravelingPoint_idTravelingPoint; }
              set { _TravelingPoint_idTravelingPoint = value; }
          } */

        /* public SQLObject TravelingPoint
         {
             get { return _TravelingPoint; }
             set { _TravelingPoint = value; }
         } */
        #endregion
    }
}