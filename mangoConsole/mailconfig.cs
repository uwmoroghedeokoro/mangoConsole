using mangoservicetest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for mailconfig
/// </summary>
public class mailconfig
{
    public string smtphost = "", fromaddress = "", smtppassword = "", smtpusername = "";
    public int smtpport,configid;


    public mailconfig()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void init()
    {
        string sql = "select * from tblMailConfig";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Connection = utility.returnCon();
        cmd.Connection.Open();

        SqlDataReader dbReader = cmd.ExecuteReader();

        while (dbReader.Read())
        {
            smtphost = (string)dbReader["smtphost"];
            fromaddress = (string)dbReader["fromaddress"];
            smtppassword = (string)dbReader["smtppassword"];
            smtpusername = (string)dbReader["smtpusername"];
            smtpport = Convert.ToInt32(dbReader["smtpport"]);

            // smtpusername = "commerce-noreply@atlautomotive.com";
            smtpusername = "hrm-no-reply@atljamaica.com";
            // smtppassword= "W7s5AE5hKSTBKyPh";
            smtppassword = "M\"5hd_}a9N;=}%TKL<`RCEBr(!_XmuxDEjW&yG9G";
           // smtphost = "mail.atlautomotive.com";
           // smtpport = 587;
        }
        dbReader.Close();
    }
    public int sendMail(string subject,string message,string toAddress,bool ccHR=false)
    {
        int result = 0;
        System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
        SmtpClient smtpClient = new SmtpClient(smtphost, smtpport);
        System.Net.NetworkCredential creds = new NetworkCredential(smtpusername, smtppassword);
        Message.From = new MailAddress("hrm-no-reply@atljamaica.com", "IRAT HR");
        string[] recs = toAddress.Split(';');

        foreach (string rec in recs)
        {
            Message.To.Add(rec);
        }

        Console.Write("Attemptng to send via: " + creds.UserName + " " + creds.Password + " port: " + smtpport + " " + smtphost);

        Message.IsBodyHtml = true;
        smtpClient.EnableSsl = false;
        smtpClient.Port = smtpport;
        Message.Subject = subject;
        Message.Body = "<font style='font-family:arial;font-size:10pt'>";
        Message.Body += message;
        Message.Body += "</font>";
        try
        {
            smtpClient.Credentials = creds;
            smtpClient.Send(Message);
        }catch(Exception ex)
        {
            Console.Write(ex.ToString());
        }

        return result;
    }

  



}