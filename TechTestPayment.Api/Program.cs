using TechTestPayment.Api.Setup;

var builder = WebApplication.CreateBuilder(args);
var setup = new Setup(builder.Services, builder.Configuration, builder.Host);

setup.ConfigurePreBuild();

var app = builder.Build();

setup.ConfigurePostBuild(app);

app.Run();
