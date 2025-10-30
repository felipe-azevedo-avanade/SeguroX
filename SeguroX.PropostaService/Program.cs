using SeguroX.PropostaService.Application;
using SeguroX.PropostaService.Infrastructure;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Serviços principais
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Injeção de dependências
    builder.Services.AddSingleton<IPropostaRepository, PropostaRepositoryInMemory>();
    builder.Services.AddScoped<PropostaAppService>();

    var app = builder.Build();

    // Middlewares
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();

    // Mapear controladores
    app.MapControllers();

    // Mensagens no console
    Console.WriteLine("---------------------------------------------------");
    Console.WriteLine("SeguroX.PropostaService iniciado com sucesso");
    Console.WriteLine("URL base: http://localhost:5000");
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
