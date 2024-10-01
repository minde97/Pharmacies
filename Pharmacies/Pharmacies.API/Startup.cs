using Pharmacies.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(x =>
		x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
	);
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
