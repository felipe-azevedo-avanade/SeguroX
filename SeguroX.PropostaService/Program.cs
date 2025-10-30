using SeguroX.PropostaService.Application;
using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;
using SeguroX.PropostaService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// 🔧 CONFIGURAÇÃO DE SERVIÇOS
// ----------------------------

// MVC e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Injeção de dependências (DI)
builder.Services.AddSingleton<IPropostaRepository, PropostaRepositoryInMemory>();
builder.Services.AddScoped<IPropostaValidator, PropostaValidator>();
builder.Services.AddScoped<ICreditoService, MotorCreditoSimulado>();
builder.Services.AddScoped<PropostaAppService>();

// ----------------------------
// 🚀 CONSTRUÇÃO DO APP
// ----------------------------
var app = builder.Build();

// ----------------------------
// 🌐 MIDDLEWARES
// ----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.MapControllers();

// ----------------------------
// 🖥️ MENSAGEM DE INICIALIZAÇÃO
// ----------------------------
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("---------------------------------------------------");
Console.WriteLine("✅ SeguroX.PropostaService iniciado com sucesso!");
Console.ResetColor();
Console.WriteLine($"🌍 URL base: http://localhost:5000");
Console.WriteLine("---------------------------------------------------");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("❌ ERRO AO INICIAR A APLICAÇÃO:");
    Console.ResetColor();
    Console.WriteLine(ex.ToString());
}
