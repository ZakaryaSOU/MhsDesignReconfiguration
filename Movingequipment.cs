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
    public class Movingequipment : SQLObject
    {
        #region Member Variables
        protected string _idMovingEquipment;
        protected string _Type;
        protected string _Speed;
        protected string _Capacity;
        protected string _Size;
        protected string _InitialXLocation;
        protected string _InitialYLocation;
        #endregion
        #region Constructors
        public Movingequipment(Project project, MySqlDataReader parentReader) : base(project, parentReader)
        {
            //set Connection
/* OpenConnection();
 CloseConnection();
 //Get load unload activities
  _loadunloadactivities = new List<SQLObject>();
  command = new MySqlCommand("SELECT * FROM loadunloadactivity WHERE MovingEquipment_idMovingEquipment =" + _idMovingEquipment, connection);
  reader = command.ExecuteReader();
  while (reader.Read())
      _loadunloadactivities.Add(new Loadunloadactivity(project, reader));
  reader.Close();

  CloseConnection();*/

  project.ListPhysicalObject.Add(this); 

}
#endregion
#region Public Properties
public string IdMovingEquipment
{
get { return _idMovingEquipment; }
set { _idMovingEquipment = value; }
}
public string Type
{
get { return _Type; }
set { _Type = value; }
}
public string Speed
{
get { return _Speed; }
set { _Speed = value; }
}
public string Capacity
{
get { return _Capacity; }
set { _Capacity = value; }
}
public string Size
{
get { return _Size; }
set { _Size = value; }
}
public string InitialXLocation
{
get { return _InitialXLocation; }
set { _InitialXLocation = value; }
}
public string InitialYLocation
{
get { return _InitialYLocation; }
set { _InitialYLocation = value; }
}

internal void CreateSimioObject(IDesignContext context)
{
context.ActiveModel.Facility.IntelligentObjects.CreateObject("Vehicle", new FacilityLocation(double.Parse(InitialXLocation), 0, double.Parse(InitialYLocation)));

}
#endregion
}
}
