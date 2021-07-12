using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuanLiRapPhim.App
{
    public class Email
    {
        //public static async Task<String> SendEmail(String _from,String _to,String _subject,string _body)
        //{
        //    MailMessage mailMessage = new MailMessage(_from,_to,_subject,_body);
        //    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.ReplyToList.Add(new MailAddress(_from));
        //    mailMessage.Sender = new MailAddress(_from);

        //    using var smptClient = new SmtpClient("localhost");
        //    try
        //    {
        //        await smptClient.SendMailAsync(mailMessage);
        //        return "Suscess";
        //    }
        //    catch(Exception e)
        //    {
        //        return "Fail";
        //    }

        //}

        public static async Task<bool> SendMailLocalSmtp(string _from, string _to, string _subject, string _body)
        {
            using (SmtpClient client = new SmtpClient("localhost"))
            {
                return await SendMail(_from, _to, _subject, _body, client);
            }
        }
        public static async Task<bool> SendMail(string _from, string _to, string _subject, string _body, SmtpClient client)
        {
            // Tạo nội dung Email
            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);


            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static async Task<bool> SendMailGoogleSmtp(string _from, string _to, string _subject,
                                                            string _body)
        {

            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential("0306181113@caothang.edu.vn","285727901");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                return await SendMail(_from, _to, _subject, _body, client);
            }

        }
    }
}
