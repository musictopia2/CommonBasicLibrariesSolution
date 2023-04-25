namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.SendEmailClasses;
public record struct EmailMessage(string EmailAddress, string Subject, string Body, bool IsHtml = false);