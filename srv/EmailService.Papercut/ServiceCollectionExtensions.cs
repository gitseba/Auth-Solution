using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace EmailService.Papercut
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPapercutEmailService(this IServiceCollection services)
        {
            services.AddMailKit(config =>
            {
                config.UseMailKit(new MailKitOptions
                {
                    Server = "127.0.0.1",
                    Port = 25,
                    SenderName = "Sebs",
                    SenderEmail = "sebs@testo.com"
                });
            });

            return services;
        }
    }
}
