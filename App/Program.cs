using App.Database;
using App.GraphQL.Mutation;
using App.GraphQL.Query;
using App.GraphQL.Scheme;
using App.GraphQL.Type;
using App.Repository;
using App.XMLStorage;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddSingleton<IXMLFactory, XMLFactory>();

builder.Services.AddSingleton<IRepositoryController, RepositoryController>();
builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddSingleton<IXMLRepository, XMLRepository>();

builder.Services.AddTransient<APIScheme>();

builder.Services.AddTransient<CategoryType>();
builder.Services.AddTransient<TaskType>();
builder.Services.AddTransient<TaskInputType>();

builder.Services.AddTransient<RootQuery>();
builder.Services.AddTransient<RootMutation>();

builder.Services.AddGraphQL(options => 
	options.AddAutoSchema<ISchema>()
	.AddSystemTextJson()
    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
    .AddDataLoader());

builder.Services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
builder.Services.AddSingleton<DataLoaderDocumentListener>();

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

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseRouting();

app.UseGraphQL<APIScheme>();
app.UseGraphQLGraphiQL();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
