using MailKit.Net.Smtp;
using MimeKit;

namespace ProductManagement.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOrderConfirmationAsync(string toEmail, string customerName, int orderId, decimal total)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Titan Store", _config["Email:From"]!));
                message.To.Add(new MailboxAddress(customerName, toEmail));
                message.Subject = $"✅ Pedido #{orderId} confirmado - Titan Store";

                var totalStr = total.ToString("C0");

                var body = new BodyBuilder
                {
                    HtmlBody = $@"
                    <div style='font-family:Arial,sans-serif;max-width:600px;margin:0 auto;'>
                        <div style='background:linear-gradient(135deg,#1e293b,#3b82f6);padding:30px;text-align:center;border-radius:12px 12px 0 0;'>
                            <h1 style='color:white;margin:0;'>📦 Titan Store</h1>
                        </div>
                        <div style='background:#f8fafc;padding:30px;border-radius:0 0 12px 12px;'>
                            <h2 style='color:#1e293b;'>¡Hola {customerName}!</h2>
                            <p style='color:#64748b;'>Tu pedido ha sido confirmado exitosamente.</p>
                            <div style='background:white;border-radius:8px;padding:20px;margin:20px 0;border-left:4px solid #3b82f6;'>
                                <p style='margin:0;'><strong>Número de orden:</strong> #{orderId}</p>
                                <p style='margin:8px 0 0;'><strong>Total:</strong> <span style='color:#3b82f6;font-size:1.2rem;font-weight:bold;'>{totalStr}</span></p>
                            </div>
                            <div style='background:#fef3c7;border-radius:8px;padding:15px;margin:20px 0;'>
                                <p style='margin:0;color:#92400e;'><strong>⚠️ Recuerda:</strong> Envía tu comprobante de pago Nequi por WhatsApp para procesar tu pedido más rápido.</p>
                            </div>
                            <a href='https://wa.me/573004051875' style='display:block;background:#25d366;color:white;text-align:center;padding:12px;border-radius:8px;text-decoration:none;font-weight:bold;'>📱 Enviar comprobante por WhatsApp</a>
                            <p style='color:#94a3b8;font-size:0.85rem;text-align:center;margin-top:20px;'>Gracias por comprar en Titan Store 🛒</p>
                        </div>
                    </div>"
                };
                message.Body = body.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_config["Email:Host"]!, int.Parse(_config["Email:Port"]!), false);
                await client.AuthenticateAsync(_config["Email:Username"]!, _config["Email:Password"]!);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando email: {ex.Message}");
            }
        }
    }
}