using Npgsql;

namespace Discount.API.Extensions;

public static class DbInitializerExtension
{
    public static IServiceCollection MigrateDatabase<TContext>(this IServiceCollection services, int? retry = 0)
    {
        int retryForAvailability = retry.Value;
 
        var serviceProvider = services.BuildServiceProvider();
 
        using (var scope = serviceProvider.CreateScope())
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migration postgresql database.");
                logger.LogInformation(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                
                using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = "drop table if exists coupon";
                command.ExecuteNonQuery();

                command.CommandText =
                    @"create table coupon(Id serial primary key, ProductName varchar(24) not null, Description text, Amount int)";
                command.ExecuteNonQuery();

                command.CommandText =
                    "insert into coupon(ProductName, Description, amount) values ('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();
                
                command.CommandText =
                    "insert into coupon(ProductName, Description, amount) values ('Samsung 10', 'Samsung Discount', 100);";
                command.ExecuteNonQuery();
                
                logger.LogInformation("Migrated postresql database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the postresql database");
            
                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(services, retryForAvailability);
                }
            }
        }
 
        // RETURN SERVICES INSTEAD OF HOST
        return services;
    }
}