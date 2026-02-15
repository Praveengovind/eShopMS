namespace Discount.Grpc.Data;

public static class Extentions
{
    public static async Task<IApplicationBuilder> UseMigrationAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<DiscountContext>();
        await context?.Database.MigrateAsync();
        return app;
    }
}
