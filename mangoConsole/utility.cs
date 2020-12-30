using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mangoservicetest
{
    class utility
    {
        public utility()
        {

        }

       

        public static SqlConnection returnCon()
        {
            // SqlConnection con = new SqlConnection("Data Source=localhost\\devapps;Initial Catalog=IRL_HRM;Integrated Security=true;Connection Timeout=5");
            SqlConnection con = new SqlConnection("Data Source=172.18.12.117;Initial Catalog=HRM;user=atl_hrm_admin;password=@ut04177;Connection Timeout=5");

            try
            {
                // con.Open();
            }
            catch (Exception ex)
            {
                Console.Write("SQL Connection Exception " + ex.ToString());
            }
            return con;


        }

        public static List<MangoASAservice.worker.alertz> alertzDue()
        {
            SqlCommand cmd = new SqlCommand();
            List<MangoASAservice.worker.alertz> alertD = new List<MangoASAservice.worker.alertz>();
            try
            {
                cmd.Connection = utility.returnCon();
                cmd.Connection.Open();

                cmd.CommandText = "select * from view_empAlerts where datediff(day,'" + DateTime.Today.ToShortDateString() + "',alertdate) >-1 and datediff(day,'" + DateTime.Today.ToShortDateString() + "',alertdate) <=3";

                SqlDataReader dbread = cmd.ExecuteReader();
                while (dbread.Read())
                {
                    MangoASAservice.worker.alertz alerto = new MangoASAservice.worker.alertz();
                    alerto.alertDue = Convert.ToDateTime(dbread["alertdate"]);
                    alerto.alertName = (string)dbread["alertType"];
                    alerto.employeeName = (string)dbread["fullname"];
                    alerto.location = (string)dbread["locName"];
                    alerto.recipients = (string)dbread["alertRecipient"];
                    alertD.Add(alerto);
                }
            }
            finally
            {
                cmd.Connection.Close();
            }

            return alertD;
        }

    
    }


}
