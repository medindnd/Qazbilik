using System;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;

// ====================== Чтение .env вручную ======================
var envVariables = new Dictionary<string, string>();
if (File.Exists(".env"))
{
    foreach (var line in File.ReadAllLines(".env"))
    {
        var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 2)
        {
            envVariables[parts[0].Trim()] = parts[1].Trim().Trim('"');
        }
    }
}
else
{
    Console.WriteLine("❌ Файл .env не найден!");
    return;
}

// ====================== Получение переменных ======================
var apiKey = envVariables["MAILGUN_API_KEY"];
var domain = envVariables["MAILGUN_DOMAIN"];
var from = envVariables.GetValueOrDefault("MAILGUN_FROM", $"postmaster@{domain}");
var to = envVariables["MAILGUN_TO"];

// ====================== Отправка письма ======================
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
    request.AddParameter("subject", "Письмо без DotNetEnv");
    request.AddParameter("text", "Работает даже без сторонних пакетов!");

    var response = await client.ExecuteAsync(request);
    Console.WriteLine(response.IsSuccessful ? "✅ Письмо отправлено!" : "❌ Ошибка: " + response.ErrorMessage);
}
catch (Exception ex) 
{
    Console.WriteLine("🔥 Ошибка: " + ex.Message);
}
