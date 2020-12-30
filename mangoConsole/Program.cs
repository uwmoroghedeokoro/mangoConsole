using MangoASAservice;
using mangoservicetest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mangoConsole
{
    class Program
    {
       static SqlConnection conz;
        static void Main(string[] args)
        {
            Timer t = new Timer(TimerCallback, null, 0, (1000 * 60*60)*6);
            Console.WriteLine("Running........ waiting for schedule");
           // doAction();
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            Console.WriteLine("Last scheduled timer: " + DateTime.Now.ToString());
           // if (DateTime.Now.Hour == 9)
            {
                doAction();
            }
           
        }

        private static void doAction()
        {
            Console.Write("Execute Event : HR Service : " + DateTime.Now.ToString() + "\n");
            //reset leave
            SqlCommand sqlcom = new SqlCommand();
            string result = "Successful";
            mailconfig mailC = new mailconfig();
            try
            {
                conz = utility.returnCon();

                sqlcom.Connection = conz;
               // sqlcom.CommandText = "resetEmployeeEntitlement";
               // sqlcom.CommandType = System.Data.CommandType.StoredProcedure;

                sqlcom.Connection.Open();
               // sqlcom.ExecuteNonQuery();


                // check for emp alerts
                // Console.WriteLine("ready");

                mailC.init();

                foreach (worker.alertz emAlert in utility.alertzDue())
                {
                    string txt= emAlert.alertDue.ToShortDateString()==(DateTime.Today.ToShortDateString())?"Today" : " in " + (emAlert.alertDue-DateTime.Today).TotalDays+ " days" ;
                    string msg = "<span style='font-size:12pt'>This is to notify you that the below Employee alert is due " + txt + ". <br><br>";
                    msg += "Employee: <b>" + emAlert.employeeName + "</b><br>";
                    msg += "Alert: <b>" + emAlert.alertName + "</b><br>";
                    msg += "Due Date: <b>" + emAlert.alertDue.ToShortDateString() + "</b></span><br>";
                    mailC.sendMail("Employee Reminder for " + emAlert.employeeName + " - " + emAlert.alertName, msg, emAlert.recipients);
                    Console.WriteLine("Alert: " + emAlert.employeeName + " " + emAlert.alertName + " " + emAlert.alertDue.ToShortDateString());
                }


            }
            catch (Exception ex)
            {
                result = "Failed : " + ex.Message.ToString();
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                sqlcom.CommandType = System.Data.CommandType.Text;
                sqlcom.CommandText = "insert into tblServiceLog (result) values ('" + result + "')";
                sqlcom.ExecuteNonQuery();

                sqlcom.Connection.Close();
               // mailC.sendMail("IRAT HR-Service Event", "<br><br>Service Execution time : " + DateTime.Now.ToString() + "<br>Errors : " + result, "it-support@islandroutes.com");
            }
        }
    }
}
