// See https://aka.ms/new-console-template for more information
using Solid._05_DIP.Good;

Console.WriteLine("Hello, World!");

var emailService = new EmailService();
var userService = new UserService(emailService);