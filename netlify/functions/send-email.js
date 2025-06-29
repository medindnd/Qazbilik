const mailgun = require("mailgun-js");

exports.handler = async (event) => {
  const data = JSON.parse(event.body);

  const mg = mailgun({
    apiKey: process.env.MAILGUN_API_KEY,
    domain: process.env.MAILGUN_DOMAIN, // Например: "sandbox123.mailgun.org"
  });

  const emailData = {
    from: "no-reply@sandbox76225123e95049c2b29bb2be84429f60.mailgun.org",
    to: "tooqazbilik@mail.ru",
    subject: Новая заявка: ${data.name},
    text: Имя: ${data.name}\nEmail: ${data.email}\nТелефон: ${data.phone},
  };

  try {
    await mg.messages().send(emailData);
    return { statusCode: 200, body: "Письмо отправлено!" };
  } catch (error) {
    return { statusCode: 500, body: "Ошибка: " + error.message };
  }
};
