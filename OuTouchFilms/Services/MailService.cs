using OuTouchFilms.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security;

namespace OuTouchFilms.Services
{
    public class MailService : IMailService
    {
        private string GetBodyMail(string header, string userName, string text)
        {
            StringBuilder body = new StringBuilder();
            string backImg = "https://kartinkin.net/uploads/posts/2021-10/1633784785_33-kartinkin-net-p-beskonechnoe-leto-miku-art-krasivo-39.jpg";

            body.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n    <head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>OuTouch letter</title><style type=\"text/css\">@media only screen and (min-device-width: 601px) {.content {width: 600px !important;}}\r\n\t\tbody[yahoo] .class {}\r\n\t\t.button {text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px;}\r\n\t\t.button a {color: #ffffff!important; text-decoration: none;}\r\n\t\t.button a:hover {text-decoration: underline;}\r\n\r\n\t\t@media only screen and (max-width: 550px), screen and (max-device-width: 550px) {}\r\n\t\tbody[yahoo] .buttonwrapper {background-color: transparent!important;}\r\n\t\tbody[yahoo] .button a {background-color: #e05443; padding: 15px 15px 13px!important; display: block!important;}\r\n        </style>\r\n    </head><body yahoo bgcolor=\"#f6f8f1\" style=\"margin: 0; padding: 0; min-width: 100%; background-color: #f6f8f1;\"><table class=\"content\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width: 100%; max-width: 600px;\"><!--Header--><tr><td bgcolor=\"#a7a7a7\" style=\"padding: 40px 30px 20px 30px; background-image: url(");

            body.Append(backImg);
            
            body.Append("); background-size: cover;\"><!--LOGO--><table width=\"95\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td height=\"70\" style=\"padding: 0 20px 20px 0;\"><img src=\"http://outouch.ru/images/outouch.png\" width=\"100\"  border=\"0\" alt=\"\" /></td></tr></table><!--END-LOGO--><!--Заглавие--><table class=\"col425\" align=\"left\" border=\"0\" cellpadding=\"0\" style=\"width: 100%; max-width: 400px;\"><tr><td height=\"70\"><table width=\"100%\" border=\"0\" cellspacing=\"0\"><tr><td style=\"padding: 0 0 0 3px; font-size: 20px; color: #ffffff; font-family: sans-serif; letter-spacing: 5px; font-weight: bold;\">OuTouch Films</td></tr><tr><td class=\"h1\" style=\"padding: 5px 0 0 0; font-size: 33px; line-height: 38px; font-weight: bold; color: #25d5b3; font-family: sans-serif;\">");
            
            body.Append(header);

            body.Append("</td></tr></table></td></tr></table><!--END-ЗАГЛАВИЕ--></tr><!--ТЕЛО ПИСЬМА--><td class=\"content\" bgcolor=\"#ffffff\" style=\"width: 100%; max-width: 600px; padding: 30px 30px 30px 30px; border-bottom: 1px solid #f2eeed;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><!--ВСТУПЛЕНИЕ--><tr><td style=\"color: #153643; font-family: sans-serif; padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold;\">");

            body.Append("Уважаемый(-ая) " + userName + "!");

            body.Append("</td></tr><!--/ВСТУПЛЕНИЕ--><!--НАЧАЛО--><tr><td style=\"color: #153643; font-family: sans-serif; font-size: 16px; line-height: 22px;\"><p>");

            body.Append(text+"</p></td></tr>");

            body.Append("<!--ОКОНЧАНИЕ ПИСЬМА--><tr><td style=\"color: #153643; font-family: sans-serif; font-size: 16px; line-height: 22px;\"><p>С наилучшими пожеланиями, <br /><strong>Администрация сайта OuTouch</strong></p></td></tr><!--/ОКОНЧАНИЕ ПИСЬМА--></table></td></tr><!--Footer--><tr><td class=\"footer\" bgcolor=\"#44525f\" style=\"padding: 20px 30px 15px 30px;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" style=\"font-family: sans-serif; font-size: 14px; color: #ffffff;\">&reg;All rights reserved<br/><a href=\"http://films.outouch.ru\" style=\"color: #ffffff; text-decoration: underline;\">OuTouch Films</a></td></tr></table></body></html>");


            return body.ToString();
        }
            
        private async Task SendAsync(string bodyCode,string subject, string emailReceiver)
        {
            MailAddress from = new MailAddress("outouch_anime@mail.ru", "OuTouch Films");
            // кому отправляем
            MailAddress to = new MailAddress(emailReceiver);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = subject;
            // текст письма
            m.Body = bodyCode;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("outouch_anime@mail.ru", "QZDAHHfu5mZd7Ph4WTUZ");
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(m);
        }


        public async Task NewPostLetter(News news,string userNameReceiver,string emailReceiver)
        {
            await SendAsync(GetBodyMail("Новый пост с новостями",
                                            userNameReceiver,
                                            "Недавно на сайте вышел новый пост с названием: " + news.Title + ". Рекомендуем его к прочтению, ведь там возможно содержится очень важная информация")
                                , "Новый пост с новостями!"
                                , emailReceiver);
        }
        public async Task RegistrationLetter(User user)
        {
            await SendAsync(GetBodyMail("Регистрация",
                                user.Login,
                                "Спасибо за регистрацию на нашем сайте, " + user.Login + ". Рады, что вы выбрали именно наш сервис. Приятного просмотра фильмов)")
                                , "Спасибо за регистрацию на нашем сервисе!"
                                , user.Email);
        }
    }
}
