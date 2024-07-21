using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WorkoutTracker.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Initialize Firebase Admin SDK
        var firebaseCredential = GoogleCredential.FromFile("./workout-tracker-c97b4-firebase-adminsdk-kcvbq-3a81ecd94d.json");
        FirebaseApp.Create(new AppOptions { Credential = firebaseCredential });

        // Register Firestore client
        services.AddSingleton(sp =>
        {
            return FirestoreDb.Create("workout-tracker-c97b4");
        });

        // Register WorkoutService
        services.AddScoped<WorkoutService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
