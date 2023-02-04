using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register services for logging service
builder.Services.AddWatchDogServices(settings =>
{
    settings.IsAutoClear = true; //for preventing automatic log clearance set this flag to false
    settings.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;   //If you don't specify any schedule the default clearance of your logs will happen on a daily basis.

    //Setting up for forwarding the logs to an external database
    settings.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
    settings.SetExternalDbConnString = configuration["WatchDog:externalDB"];

});


var app = builder.Build();

////adding watch dog exception logging
//app.UseWatchDogExceptionLogger();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


    // Add middleware and setup username + password
    app.UseWatchDog(dog =>
    {
        dog.WatchPageUsername = configuration["WatchDog:username"];
        dog.WatchPagePassword = configuration["WatchDog:password"];
        dog.Blacklist = "api/users";    //doing blacklist this endpoints for security reasons.

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
