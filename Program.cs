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

// Set the GOOGLE_APPLICATION_CREDENTIALS environment variable
string firebaseCredentialPath = Path.Combine(AppContext.BaseDirectory, "serviceAccount.json");
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebaseCredentialPath);

// Initialize Firebase
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.GetApplicationDefault(),
});

// Register FirestoreDb as a singleton
builder.Services.AddSingleton(sp =>
{
    try
    {
        // Ensure you replace "workout-tracker-c97b4" with your actual Google Cloud project ID
        var firestoreDb = FirestoreDb.Create("workout-tracker-c97b4");
        return firestoreDb;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating Firestore client: {ex.Message}");
        throw;
    }
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Enable API exploration features
builder.Services.AddSwaggerGen(); // Add Swagger generation support
builder.Services.AddScoped<WorkoutService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware
    app.UseSwaggerUI(); // Enable Swagger UI middleware
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing middleware is added
app.UseAuthorization(); // Ensure authorization middleware is added

app.MapControllers(); // Map controller routes

app.Run();


/*

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
builder.Services.AddControllers(); // Adds support for controllers to the application
builder.Services.AddEndpointsApiExplorer(); // Enables API exploration features
builder.Services.AddSwaggerGen(); // Adds Swagger generation support

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
    Console.WriteLine("Firebase initialized successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing Firebase: {ex.Message}");
    throw; // Rethrows the exception to halt the application startup if Firebase initialization fails
}

// Register Firestore client as a singleton service
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
        throw; // Rethrows the exception to halt the application startup if Firestore client creation fails
    }
});

// Register WorkoutService as a scoped service
builder.Services.AddScoped<WorkoutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger
    app.UseSwaggerUI(); // Enables Swagger UI
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS
app.UseAuthorization(); // Adds authorization middleware to the request pipeline
app.MapControllers(); // Maps controller routes

app.Run(); // Runs the application
*/