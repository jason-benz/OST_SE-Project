using MediaHub.Areas.Identity;
using MediaHub.Data;
using MediaHub.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Data.ChatModule.Model;
using MediaHub.Data.UserSuggestionModule.ViewModel;
using MediaHub.Data.ProfileModule.ViewModel;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ChatModule.ViewModel;
using MediaHub.Data.ProfileModule.Persistency;
using MediaHub.Data.ChatModule.Persistency;
using MediaHub.Data.MediaModule.Persistency;
using MediaHub.Data.UserSuggestionModule.Persistency;
using MediaHub.Data.FeedModule.ViewModel;
using MediaHub.Data.FeedModule.Persistency;
using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.ContactsModule.Persistency;
using MediaHub.Data.UserSuggestionModule.Model;
using MediaHub.Data.ContactsModule.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Logging

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
LogService.Singleton = new SerilogService(logger);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString!));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddScoped<IIdentityService>(_ => new IdentityService());
builder.Services.AddTransient<IEmailSender, EmailSender>();
var profileManager = new UserProfileDataManager();
var userSuggestionDataManager = new UserSuggestionDataManager();
var contactDataManager = new ContactDataManager();
var feedService = new FeedService(new FeedDataManager(), contactDataManager);
IChatDataManager chatDataManager = new ChatDataManager();
var mediaApi = new TmdbApi();
builder.Services.AddScoped<IUserProfileViewModel>(_ => new UserProfileViewModel(profileManager, feedService));
builder.Services.AddScoped<IUserSuggestionsViewModel>(_ => new UserSuggestionsViewModel(new UserSuggestionDataManager(), contactDataManager));
builder.Services.AddScoped<IMediaSearchViewModel>(_ => new MediaSearchViewModel(mediaApi));
builder.Services.AddSingleton(LogService.Singleton);
builder.Services.AddScoped<IRatingViewModel>(_ => new RatingViewModel(profileManager, new UserSuggestionEngine(userSuggestionDataManager, contactDataManager), feedService));
builder.Services.AddScoped<IMediaTableViewModel>(_ => new MediaTableViewModel(mediaApi, profileManager));
builder.Services.AddScoped<IChatViewModel>(_ => new ChatViewModel(chatDataManager, profileManager, contactDataManager));
builder.Services.AddScoped<IFeedViewModel>(_ => new FeedViewModel(feedService));
builder.Services.AddScoped<IMediaCommentViewModel>(_ => new MediaCommentViewModel(new MediaCommentDataManager()));
builder.Services.AddScoped<IContactViewModel>(_ => new ContactViewModel(new ContactDataManager()));

var app = builder.Build();
app.UseResponseCompression();

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
