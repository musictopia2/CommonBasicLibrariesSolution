using System.Net; //not common enough.
using System.Net.Mail; //not common enough.

namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.SendEmailClasses;
public class SendEmailService
{
    private readonly ISmptService _smptService;
    public SendEmailService(ISmptService smptService)
    {
        _smptService = smptService;
    }
    public async Task SendEmailAsync(EmailMessage email)
    {
        SmptInfo info = await _smptService.GetSmptInfoAsync();
        var smtpClient = new SmtpClient(info.Smtp)
        {
            Port = info.Port,
            EnableSsl = true,
            Credentials = new NetworkCredential(info.UserName, info.Password)
        };
        MailMessage mail = new()
        {
            From = new(info.UserName),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = email.IsHtml
        };
        mail.To.Add(email.EmailAddress);
        await smtpClient.SendMailAsync(mail);
    }
}