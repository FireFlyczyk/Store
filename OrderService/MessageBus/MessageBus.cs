using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using OrderService.Dtos;
using RabbitMQ.Client;

namespace OrderService.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBus(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory(){
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
              _connection = factory.CreateConnection();
              _channel = _connection.CreateModel();
              _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
              _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            }
            catch (Exception)
            {
                
               Console.WriteLine("Could not connect to RabbitMQ");
            }
     
        }
        public void PublishNewOrder(OrderSendDto orderSendDto)
        {
          var message = JsonSerializer.Serialize(orderSendDto);
          if (_connection.IsOpen)
          {
            Console.WriteLine("RabbitMQ Connection Open, sending message...");
            SendMessage(message);
          }
          else{
            Console.WriteLine("RabbitMQ connection is closed, not sending");
          }
        }

        private void SendMessage(string message)
        {
           var body = Encoding.UTF8.GetBytes(message);
           _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
           Console.WriteLine($"We have sent {message}");
           
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e){
           Console.WriteLine("Connection Shutdown"); 
        }

        public void Dispose(){
            Console.WriteLine("MessageBus disposed");
            if (_channel.IsOpen){
                _channel.Close();
                _connection.Close();
            }
        
        }
    }
}