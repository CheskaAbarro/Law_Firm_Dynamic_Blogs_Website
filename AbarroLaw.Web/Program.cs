using AbarroLaw.Web.Data;
using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//For database connection ----------------------
builder.Services.AddDbContext<AbarroLawDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AbarroLawConnectionString")));

//For repository injections --------------------

//For Practice Repository
builder.Services.AddScoped<IPracticeRepository, PracticeRepository>();
//For Case Repository
builder.Services.AddScoped<ICaseRepository, CaseRepository>();
//For image upload repository (Local folder)
builder.Services.AddScoped<IImageRepository, FolderImageRepository>();
//For message inquiries
builder.Services.AddScoped<IMessageRepository, MessageRepository>();


//For accounts database connection ----------------------
builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));
//For log in
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //Default settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
