const sgMail = require('@sendgrid/mail');
sgMail.setApiKey(process.env.SENDGRID_API_KEY);

exports.handler = async (event) => {
  const data = JSON.parse(event.body);
  
  const msg = {
    to: 'tooqazbilik@mail.ru',
    from: 'no-reply@yourdomain.com',
    subject: Новая заявка: ${data.name},
    text: Имя: ${data.name}\nEmail: ${data.email}\nТелефон: ${data.phone}
  };

  await sgMail.send(msg);
  return { statusCode: 200 };
};
