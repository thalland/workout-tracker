using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
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
        var firebaseCredential = GoogleCredential.FromFile("serviceAccountKey.json");
        FirebaseApp.Create(new AppOptions { Credential = firebaseCredential });

        // Register Firestore client
        services.AddSingleton<FirestoreDb>(sp =>
        {
            return FirestoreDb.Create("your-project-id");  // Replace with your actual project ID
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
