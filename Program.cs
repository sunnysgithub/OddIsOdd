using OddIsOdd;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseOddIsOddMiddleware();

app.MapGet(Constants.PathToEven, () => $"Even {DateTime.UtcNow.ToLongTimeString()}");

app.MapGet(Constants.PathToOdd, () => $"Odd {DateTime.UtcNow.ToLongTimeString()}");

app.Run();