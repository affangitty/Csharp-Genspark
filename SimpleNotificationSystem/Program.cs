using SimpleNotificationSystem.Interfaces;
using SimpleNotificationSystem.Models;
using SimpleNotificationSystem.Services;

var user = new User
{
    Name = "Affan Ahammed",
    Email = "Affan.Ahammed@example.com",
    PhoneNumber = "+91 9873451453"
};

var emailChannel = new EmailNotification();
var smsChannel = new SmsNotification();
var whatsAppChannel = new WhatsAppNotification();
var notificationService = new NotificationService(emailChannel, smsChannel, whatsAppChannel);

Console.WriteLine("--- Send via NotificationService (typed helpers) ---");
var welcome = new Notification { Message = "Welcome to the company portal." };
notificationService.SendEmail(user, welcome);

var alert = new Notification { Message = "Your verification code is 482910." };
notificationService.SendSms(user, alert);

var appLink = new Notification { Message = "Open the app: https://example.com/app" };
notificationService.SendWhatsApp(user, appLink);

Console.WriteLine();
Console.WriteLine("--- Polymorphism: same method, different INotificationSender implementations ---");
var reminder = new Notification { Message = "Team meeting at 3 PM." };
notificationService.Send(user, reminder, emailChannel);

var promo = new Notification { Message = "Flash sale ends tonight!" };
notificationService.Send(user, promo, smsChannel);

var status = new Notification { Message = "Your order has shipped." };
notificationService.Send(user, status, whatsAppChannel);

Console.WriteLine();
Console.WriteLine("--- Iterate channels as INotificationSender ---");
INotificationSender[] channels = { emailChannel, smsChannel, whatsAppChannel };
var broadcast = new Notification { Message = "Office closed Monday (holiday)." };
foreach (var channel in channels)
{
    notificationService.Send(user, broadcast, channel);
}
