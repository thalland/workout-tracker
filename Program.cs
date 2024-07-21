using System;
using System.IO; // Import this namespace for Path.Combine
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Define the path to the Firebase credentials file
var firebaseCredentialPath = Path.Combine(AppContext.BaseDirectory, "serviceAccount.json");

Console.WriteLine($"Firebase Credential Path: {firebaseCredentialPath}");

// Initialize Firebase Admin SDK
try
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(firebaseCredentialPath),
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing Firebase: {ex.Message}");
    throw;
}

// Register Firestore client
builder.Services.AddSingleton(sp =>
{
    try
    {
        // Ensure you replace "your-project-id" with your actual Google Cloud project ID
        var firestoreDb = FirestoreDb.Create("workout-tracker-c97b4");
        Console.WriteLine("Firestore client has been initialized.");
        return firestoreDb;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating Firestore client: {ex.Message}");
        throw; // Consider how you want to handle initialization failures
    }
});

// Register WorkoutService
builder.Services.AddScoped<WorkoutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
