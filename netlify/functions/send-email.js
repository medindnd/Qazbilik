const mailgun = require("mailgun-js");

exports.handler = async (event) => {
  const data = JSON.parse(event.body);

  const mg = mailgun({
    apiKey: process.env.MAILGUN_API_KEY,
    domain: "sandbox76225123e95049c2b29bb2be84429f60.mailgun.org" // Убрал запятую и process.env
  });

  const emailData = {
    from: "no-reply@sandbox76225123e95049c2b29bb2be84429f60.mailgun.org",
    to: "tooqazbilik@mail.ru",
    subject: Новая заявка: ${data.name}, // Добавил обратные кавычки
    text: Имя: ${data.name}\nEmail: ${data.email}\nТелефон: ${data.phone} // Добавил обратные кавычки
  };

  try {
    await mg.messages().send(emailData);
    return { statusCode: 200, body: "Письмо отправлено!" };
  } catch (error) {
    return { statusCode: 500, body: "Ошибка: " + error.message };
  }
};
