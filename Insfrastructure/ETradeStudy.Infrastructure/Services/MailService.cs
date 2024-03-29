﻿using ETradeStudy.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:UserName"], " E-Ticaret", Encoding.UTF8);

            SmtpClient smtpClient = new();
            smtpClient.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
            smtpClient.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtpClient.EnableSsl = true;
            smtpClient.Host = _configuration["Mail:Host"];
            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br>Eğer şifre yenileme talebinde bulunduysanız aşağıdaki link üzerinden şifrenizi yenileyebilirsiniz" +
                "<br><strong><a target=\"_blank\" href=\" ");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayanız</a></strong><br><br><br><span style=\" font-size:12px;\">Eğer ki bu talep sizin tarafınızdan gerçekleştirilmediyse bu maili ciddiye almayınız</span>" +
                "<br>Saygılarımızla...<br><br>Mini|E-Ticaret ");
            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());

        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Sayın {userName}  Merhaba<br> {orderDate} tarihli {orderCode} kodlu siparişiniz kargoya verilmiştir.<br>İyi günlerde kullanınız";
            await SendMailAsync(to, $"{orderCode} Kodlu Siparişiniz Tamamlandı", mail);

        }
    }
}
