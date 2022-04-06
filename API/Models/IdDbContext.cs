using System.Data;
using System.Data.SqlClient;

namespace API.Models
{
    public class IdDbContext
    {
        public List<Id> getid()
        {
            List<Id> idl = new List<Id>();
            string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "ak";
            SqlCommand cmd = new SqlCommand(Q, con);
            //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Id idd = new Id();
                idd.column_1 = Convert.ToInt32(dr.GetValue(0).ToString());
                idl.Add(idd);



            }
            con.Close();
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //return Ok(ds);

            return idl;
        }
    }
}