using SeguroX.ContratacaoService.Application;
using SeguroX.ContratacaoService.Infrastructure;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // -----------------------------
    // Serviços principais
    // -----------------------------
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // -----------------------------
    // Injeção de dependências
    // -----------------------------
    builder.Services.AddSingleton<IContratacaoRepository, ContratacaoRepositoryInMemory>();
    builder.Services.AddScoped<ContratacaoAppService>();

    // -----------------------------
    // Configuração do HttpClient (para comunicação com PropostaService)
    // -----------------------------
    builder.Services.AddHttpClient<ContratacaoAppService>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5000/"); // PropostaService
    });

    var app = builder.Build();

    // -----------------------------
    // Middlewares
    // -----------------------------
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();
    app.MapControllers();

    // -----------------------------
    // Logs de inicialização
    // -----------------------------
    Console.WriteLine("---------------------------------------------------");
    Console.WriteLine("SeguroX.ContratacaoService iniciado com sucesso");
    Console.WriteLine("URL base: http://localhost:5001");
    Console.WriteLine("---------------------------------------------------");

    app.Run();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ERRO AO INICIAR A APLICAÇÃO:");
    Console.ResetColor();
    Console.WriteLine(ex.ToString());
}
