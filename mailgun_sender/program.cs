using RestSharp;
using RestSharp.Authenticators;
using DotNetEnv; // Не забудьте установить пакет!

// Загружаем переменные из .env
Env.Load();

// Получаем настройки
var apiKey = Environment.GetEnvironmentVariable("MAILGUN_API_KEY");
var domain = Environment.GetEnvironmentVariable("MAILGUN_DOMAIN");
var from = Environment.GetEnvironmentVariable("MAILGUN_FROM");
var to = Environment.GetEnvironmentVariable("MAILGUN_TO");

if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(domain))
{
    Console.WriteLine("❌ Ошибка: Проверьте переменные в .env файле");
    return;
}

try 
{
    var client = new RestClient(
        new RestClientOptions("https://api.mailgun.net") 
        { 
            Authenticator = new HttpBasicAuthenticator("api", apiKey) 
        }
    );

    var request = new RestRequest($"/v3/{domain}/messages", Method.Post);
    request.AddParameter("from", from);
    request.AddParameter("to", to);
    request.AddParameter("subject", "Письмо из .env");
    request.AddParameter("text", "Этот вариант безопасен!");

    var response = await client.ExecuteAsync(request);
    Console.WriteLine(response.IsSuccessful ? "✅ Письмо отправлено!" : "❌ Ошибка: " + response.ErrorMessage);
}
catch (Exception ex) 
{
    Console.WriteLine("🔥 Ошибка: " + ex.Message);
}
