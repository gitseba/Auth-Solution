namespace EmailService.Papercut.Templates
{
    public static class MailMessage
    {
        public static string GetMailMessage()
        {
            return $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    " +
                "<meta charset=\"UTF-8\">\r\n    " +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n" +
                "<title>Welcome</title>\r\n   " +
                "<style>\r\n" +
                "body { font-family: Arial, sans-serif; line-height: 1.6; background-color: #f5f5f5; padding: 20px; }" +
                ".container { max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);}" +
                "h1 { color: #333; }" +
                "p { margin-bottom: 20px; }" +
                ".button { display: inline-block; background-color: #007bff; color: #fff; text-decoration: none; padding: 10px 20px; border-radius: 3px; }" +
                " </style> " +
                " </head>" +
                "<body>" +
                " <div class=\"container\"><h1>Welcome!</h1>" +
                "   <p>Dear [Username],</p>" +
                "  <p>Thank you for registering with us. We're excited to have you on board!</p>\r\n" +
                "  <p>To get started, please <a href=\"[ActivationLink]\" class=\"button\">activate your account</a>.</p>\r\n" +
                " <p>If you have any questions or need assistance, feel free to contact us.</p>\r\n      " +
                "  <p>Best regards,<br> [Your Company Name]</p>\r\n   " +
                " </div>\r\n</body>\r\n</html>";
        }
    }
}
