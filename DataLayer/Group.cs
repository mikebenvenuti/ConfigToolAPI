using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{

    public static class CommandExtensions
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (name == null) throw new ArgumentNullException("name");

            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
        }
    }
    public class Groups   {
               
        
       public static DataTable GetTable(string TableName ) {
           DataTable table = new DataTable(TableName);
           SqlDataAdapter da  = null;

           using (SqlConnection conn = DB.GetSqlConnection()) {

               SqlCommand cmd = new SqlCommand("Select * from " + TableName, conn );

               da = new SqlDataAdapter(cmd);
               int res = da.Fill(table);
               
           }
           return table;

       }

       
       public static void AddGroup(string GroupName )
       {
           using (var command = DB.CreateCommand())
           {
               command.CommandText = @"INSERT INTO Groups (GroupId, grpName) VALUES(100, @GroupName)";
               command.AddParameter("GroupName", GroupName);
               command.ExecuteNonQuery();
            }

       }
      
 //object[] ParameterValues = new object[] {"1",DateTime.Now, 12, "Completed", txtNotes.Text};
 //Database db = DatabaseFactory.CreateDatabase("ConnectionStringName");
 //DataSet ds =  = db.ExecuteDataSet("StoredProcName", ParameterValues);

       public static void Map(IDataRecord record, Group group)
       {
           group.groupID = (int)record["groupID"];
           group.Name = (string)record["grpName"];
       }

       public static IEnumerable<Group> FindGroups()
       {
           using (var command = DB.CreateCommand())
           {
               command.CommandText = @"SELECT * FROM Groups";

               using (var reader = command.ExecuteReader())
               {
                   List<Group> grouplist = new List<Group>();

                   while (reader.Read())
                   {
                       var thisgroup = new Group();
                       Map(reader, thisgroup);
                       grouplist.Add(thisgroup);

                   }
                   return grouplist;
               }

               
           }
       }
        
    }

    public class Group
    {
        public int groupID { get; set; }
        public string Name { get; set; }
    }  

    
   
}
