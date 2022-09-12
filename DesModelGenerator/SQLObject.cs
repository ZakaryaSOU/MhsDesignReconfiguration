using MySql.Data.MySqlClient;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserAddIn
{
    public abstract class SQLObject
    {
        protected MySqlConnection connection;
        protected MySqlCommand command;
        protected MySqlDataReader reader;

      

        public SQLObject(Project project, MySqlDataReader parentReader)
        {
            if (parentReader == null || project == null)
                return;
            //Get local properties
            GetLocalProperties(parentReader, this);
        }
        public SQLObject()
        {

        }

        protected void OpenConnection()
        {
            //connection = new MySqlConnection("server=sql11.freesqldatabase.com;port=3306;database=sql11470136;Uid=sql11470136;Pwd=gZHghibljB");
           connection = new MySqlConnection("server=localhost;database=mydb3_sim;uid=root;pwd=owned.123");
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void CloseConnection()
        {
            connection.Close();
        }

        protected string GetString(MySqlDataReader Reader, string key)
        {
            try
            {
                return Reader.GetString(key);
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void GetLocalProperties(MySqlDataReader parentReader, object classObj)
        {
            //Get local properties
            foreach (PropertyInfo variable in classObj.GetType().GetProperties())
                if (variable.PropertyType.Name == "String")
                    variable.SetValue(classObj, GetString(parentReader, variable.Name));
        }

        /*protected void ReadListProperties(Project project, ref List<SQLObject> ObjectList, Type T, string SQLCommand)
        {
            //Get list
            ObjectList = new List<SQLObject>();
            command = new MySqlCommand(SQLCommand, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
                ObjectList.Add((SQLObject)Activator.CreateInstance(T, reader));
            reader.Close();
        }*/

        /* protected SQLObject ReadObjectProperties(Project project, Type T, string SQLCommand)
         {
             //Get list
             SQLObject Object = null;
             command = new MySqlCommand(SQLCommand, connection);
             reader = command.ExecuteReader();
             int dummyCounter = 0;
             while (reader.Read())
             {
                 if (dummyCounter > 0)
                     throw new Exception("Erreur Commande SQL");
                 Object = ((SQLObject)Activator.CreateInstance(T, reader));
                 dummyCounter++;
             }
             reader.Close();
             return Object;
         }*/

        internal void CreateSimioObject(IDesignContext context)
        {

        }
       

        public void Execute(IDesignContext context)
        {
            throw new NotImplementedException();
        }


        

    }
}
