using System.Data;
using System.Data.SqlClient;

namespace API.Models
{
    public class LevelDbContext
    {
        public List<Level> getLevel()
        {
            List<Level> LevelList = new List<Level>();
            string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "sp_Level_details";
            SqlCommand cmd = new SqlCommand(Q, con);
            //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Level leveldetail = new Level();
                leveldetail.LEVEL_Code = dr.GetValue(0).ToString();
                leveldetail.Description = dr.GetValue(1).ToString();
                leveldetail.Active = dr.GetValue(2).ToString();
                LevelList.Add(leveldetail);



            }
            con.Close();
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //return Ok(ds);

            return LevelList;
        }

        public bool AddLevel(Level level)
        {
            string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "sp_Level_detailsInsert";
            SqlCommand cmd = new SqlCommand(Q, con);
            //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LEVEL_Code", level.LEVEL_Code);
            cmd.Parameters.AddWithValue("@Description", level.Description);
            cmd.Parameters.AddWithValue("@Active", level.Active);


            con.Open();
            int returnValue = cmd.ExecuteNonQuery();

            if (returnValue > 0)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public bool UpdateLevel(Level level)
        {
            string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "sp_Level_detailsUpdate";
            SqlCommand cmd = new SqlCommand(Q, con);
            //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LEVEL_Code", level.LEVEL_Code);
            cmd.Parameters.AddWithValue("@Description", level.Description);
            cmd.Parameters.AddWithValue("@Active", level.Active);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteLevel(String levelCode)
        {
            string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "sp_Level_detailsDelete";
            SqlCommand cmd = new SqlCommand(Q, con);
            //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LEVEL_Code", levelCode);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}