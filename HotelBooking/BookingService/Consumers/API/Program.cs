using Application.Booking;
using Application.Booking.Ports;
using Application.Guest;
using Application.Guest.Ports;
using Payment.Application.MercadoPago;
using Application.Payment.Ports;
using Application.Room;
using Application.Room.Ports;
using Data;
using Data.Booking;
using Data.Guest;
using Data.Room;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Payment.Application;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(typeof(BookingManager));
#region IoC
// guest
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

// room
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// booking
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepositoy, BookingRepository>();

// payment service
//builder.Services.AddScoped<IMercadoPagoPaymentService, MercadoPagoAdapter>();
builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();


#endregion

#region DB writing up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(
    options => options.UseSqlServer(connectionString));
#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseHttpsRedirection();
app.Run();