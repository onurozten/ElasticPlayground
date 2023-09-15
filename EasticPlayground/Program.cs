using EasticPlayground.Elastic;
using Elastic.Clients.Elasticsearch;
using ElasticPlayground.Elastic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IElasticClient, ElasticClient>();

var elasticBuilder = new ElasticSearchClientBuilder("people");

var elasticClient = await elasticBuilder
        .AddCertificateFingerprint("d384ed12052a194841de434021e3d890158a645cc2bf30d4a34c15cdfb8e8448")
        .AddAuthentication("elastic", "qZformijdcb+32amzuwO")
        .Build();

builder.Services.AddSingleton(typeof(ElasticsearchClient), elasticClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
