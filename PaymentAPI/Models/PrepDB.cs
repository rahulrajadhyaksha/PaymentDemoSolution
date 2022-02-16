using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentAPI.Models
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var servioceScope = app.ApplicationServices.CreateScope())
            {
                SendData(servioceScope.ServiceProvider.GetService<PaymentDetailContext>());
            }
        }

        private static void SendData(PaymentDetailContext context)
        {
            Console.WriteLine("Appling Migration...");
            context.Database.Migrate();

            if (context.PaymentDetails.Any())
            {
                context.PaymentDetails.AddRange(new PaymentDetail() { CardNumber = "1234567891234567", CardOwnerName = "Ram Sharma", ExpirationDate = "08/24", SecurityCode = "004", PaymentDetailId = 1 });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data- not seeding");
            }
        }
    }
}
