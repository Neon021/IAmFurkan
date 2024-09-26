﻿using Microsoft.Extensions.Options;
using My3rdBlog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace My3rdBlog.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;
        private readonly SmtpClient _client;
        public EmailService(IOptions<SmtpSettings> options)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _settings = options.Value;
            _client = new SmtpClient(_settings.Server)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true,
            };
        }
        public async void SendEmail(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(
                _settings.From,
                email,
                subject,
                message);
            try
            {
                await _client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {

            return _client.SendMailAsync(mailMessage);
        }
    }
}
