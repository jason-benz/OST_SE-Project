using MediaHub.Areas.Identity;
using MediaHub.Data;
using MediaHub.Data.Persistency;
using MediaHub.Data.ViewModel;
using MediaHub.Data.Model;
using MediaHub.Pages;
using MediaHub.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddSingleton<IdentityService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
var profileManager = new UserProfileDataManager();
builder.Services.AddSingleton<IUserProfileViewModel>(new UserProfileViewModel(profileManager));
var suggestionManager = new UserSuggestionDataManager();
builder.Services.AddSingleton<IUserSuggestionsViewModel>(new UserSuggestionsViewModel(suggestionManager));
builder.Services.AddSingleton<IMediaSearchViewModel>(new MediaSearchViewModel(new TmdbApi()));
builder.Services.AddSingleton<IRatingViewModel>(new RatingViewModel(profileManager));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
