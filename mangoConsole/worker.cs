using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MangoASAservice
{
    class worker
    {
      public struct alertz
        {
           public string alertName;
           public DateTime alertDue;
           public string recipients;
           public string employeeName;
           public string location;
        }
        public worker()
        {

        }

        public static SqlConnection returnCon()
        {
            SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=mangohrm;Integrated Security=True;Connection Timeout=5");

            try
            {
               // con.Open();
            }catch (Exception ex)
            {

            }
            return con;
                
           
        }
    }

}
