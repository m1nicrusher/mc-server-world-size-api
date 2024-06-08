long GetSizeRecursive(DirectoryInfo dirInfo)
{
    long sum = 0;
    foreach (var file in dirInfo.GetFiles())
        sum += file.Length;
    foreach (var dir in dirInfo.GetDirectories())
        sum += GetSizeRecursive(dir);
    return sum;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://*:5000");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var folderName = builder.Configuration.GetValue<string>("FolderPath");
app.MapGet("/size", () =>
    {
        return GetSizeRecursive(new DirectoryInfo(folderName));
    })
    .WithName("GetWorldSize")
    .WithOpenApi();

app.Run();
