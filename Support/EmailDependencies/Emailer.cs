namespace Support.EmailDependencies
{
    using MailKit;
    using MimeKit;
    using MailKit.Net.Imap;
    using MailKit.Search;
    using MailKit.Net.Smtp;
    using System.Drawing;

    public class Emailer
    {
        public static void recieveEmail(string username, string password,int ticketId)
        {
            string hostname = "imap.gmail.com";
            int port = 993;
            /*string username = "cxnnxrwalker18@gmail.com";
            string password = "hoqjplwdyusfvvju";*/
            //string uniqueIdentifier = "batu"; // This is the identifier you included in your sent emails
            using (var client = new ImapClient())
            {
                client.Connect(hostname, port, true);
                client.Authenticate(username, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // Search for all messages
                var messages = inbox.Search(SearchQuery.SubjectContains(ticketId.ToString()));

                foreach (var uid in messages)
                {
                    var message = inbox.GetMessage(uid);
                    if (IsReplyToOriginalEmail(message, message.Subject))
                    {

                        Console.WriteLine("Subject: " + message.Subject);
                        Console.WriteLine("From: " + message.From);
                        Console.WriteLine("Date: " + message.Date);
                        Console.WriteLine("Body: " + message.TextBody);
                        Console.WriteLine("-----------------------------------");
                    }
                }

                client.Disconnect(true);
            }
        }
    static bool IsReplyToOriginalEmail(MimeMessage replyMessage, string originalSubject)
    {
        var replySubject = replyMessage.Subject;
        return replySubject.StartsWith("Re:") || replySubject.StartsWith("Fwd:") || replySubject.Contains(originalSubject);
    }

        public void sendEmail(string sender, string reciever,string subject,string body, string password)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sender Name", sender));
            email.To.Add(new MailboxAddress("Receiver Name", reciever));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };
            

            //string smtpHost = "smtp.gmail.com";
            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(sender, password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
    
}
