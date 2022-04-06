using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class HomeController: Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet("getlevel")]
        public List<Level> getlevel()
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

            return LevelList;

        }
        

        [HttpPost("Adduser")]
        public bool Adduser()
        {
            string cs = "Data Source =localhost ;Initial Catalog= Level ;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "sp_Level_detailsInsert";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            string levelid = HttpContext.Request.Form["LEVEL_Code"];
            string description = HttpContext.Request.Form["Description"];
            string status = HttpContext.Request.Form["Active"];
            cmd.Parameters.AddWithValue("@LEVEL_Code", levelid);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Active", status);

            con.Open();
            int returnValue = cmd.ExecuteNonQuery();

            if (returnValue > 0)
            {
            return true;
            }
            con.Close();
            return false;

        }

    

        [HttpPost("edit")]
        public ActionResult Edit(string levelcode, Level level)
        {
            if (ModelState.IsValid == true)
            {
                LevelDbContext context = new LevelDbContext();
                bool check = context.UpdateLevel(level);
                if (check == true)
                {
                    //TempData["UpdateMessage"] = "Data has been Updated Successfully.";
                    ModelState.Clear();
                    //return RedirectToAction("Index");
                    var row = context.getLevel().Find(model => model.LEVEL_Code == levelcode);
                    return Ok(row);
                }

            }

            return Ok();
        }



        // [HttpPost("Updatelevel")]
        // public bool UpdateLevel(String levelcode, String description, String active)
        // {
        //     string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
        //     SqlConnection con = new SqlConnection(cs);
        //     string Q = "sp_Level_detailsUpdate";
        //     SqlCommand cmd = new SqlCommand(Q, con);
        //     //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
        //     cmd.CommandType = CommandType.StoredProcedure;
        //     cmd.Parameters.AddWithValue("@LEVEL_Code", levelcode);
        //     cmd.Parameters.AddWithValue("@Description", description);
        //     cmd.Parameters.AddWithValue("@Active", active);
        //     con.Open();
        //     int i = cmd.ExecuteNonQuery();
        //     con.Close();

        //     if (i > 0)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }

        // }

        [HttpPost("updateuser")]
            public bool UpdateLevel()
            {
            string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
            SqlConnection con = new SqlConnection(cs);
            string Q = "sp_Level_DetailsUpdate";
            SqlCommand cmd = new SqlCommand(Q, con);
            //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
            string levelcode = HttpContext.Request.Form["LEVEL_Code1"];
            string description = HttpContext.Request.Form["Description1"];
            string active = HttpContext.Request.Form["Active1"];
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LEVEL_Code", levelcode);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Active", active);
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


        [HttpDelete("Deletelevel")]
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










    // public class HomeController : Controller
    // {

    //     public IActionResult Index()
    //     {
    //         LevelDbContext db = new LevelDbContext();
    //         List<Level> obj = db.getLevel();

    //         return View(obj);
    //     }

        
        // [HttpPost("Addlevel")]
        // public bool AddLevel(String levelcode, String description, String active)
        // {
        //     string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
        //     SqlConnection con = new SqlConnection(cs);
        //     string Q = "sp_Level_detailsInsert";
        //     SqlCommand cmd = new SqlCommand(Q, con);
        //     //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
        //     cmd.CommandType = CommandType.StoredProcedure;

        //     cmd.Parameters.AddWithValue("@LEVEL_Code", levelcode);
        //     cmd.Parameters.AddWithValue("@Description", description);
        //     cmd.Parameters.AddWithValue("@Active", active);


        //     con.Open();
        //     int returnValue = cmd.ExecuteNonQuery();

        //     if (returnValue > 0)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }

        // }


        // [HttpDelete("Deletelevel")]
        // public bool DeleteLevel(String levelCode)
        // {

        //     string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
        //     SqlConnection con = new SqlConnection(cs);
        //     string Q = "sp_Level_detailsDelete";
        //     SqlCommand cmd = new SqlCommand(Q, con);
        //     //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
        //     cmd.CommandType = CommandType.StoredProcedure;
        //     cmd.Parameters.AddWithValue("@LEVEL_Code", levelCode);
        //     con.Open();
        //     int i = cmd.ExecuteNonQuery();
        //     con.Close();

        //     if (i > 0)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }

        // }

        // [HttpPost("Updatelevel")]
        // public bool UpdateLevel(String levelcode, String description, String active)
        // {
        //     string cs = "Data Source =localhost ;Initial Catalog=Level;User Id=sa;Password=Strong@1234;";
        //     SqlConnection con = new SqlConnection(cs);
        //     string Q = "sp_Level_detailsUpdate";
        //     SqlCommand cmd = new SqlCommand(Q, con);
        //     //SqlDataAdapter sda = new SqlDataAdapter(Q, con);
        //     cmd.CommandType = CommandType.StoredProcedure;
        //     cmd.Parameters.AddWithValue("@LEVEL_Code", levelcode);
        //     cmd.Parameters.AddWithValue("@Description", description);
        //     cmd.Parameters.AddWithValue("@Active", active);
        //     con.Open();
        //     int i = cmd.ExecuteNonQuery();
        //     con.Close();

        //     if (i > 0)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }

        // }

    //     public IActionResult Privacy()
    //     {
    //         return Ok();
    //     }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }



    }

}