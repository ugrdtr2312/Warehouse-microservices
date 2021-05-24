using System;
using System.Linq;
using System.Text;
using DAL.Contexts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WarehouseService.Handlers;

namespace API.Handlers
{
    public static class RabbitMqHandler
    {
        public static void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory() { HostName = "192.168.39.162" };
            var connection = factory.CreateConnection();
            var brandsChannel = connection.CreateModel();
            var categoriesChannel = connection.CreateModel();
            var suppliersChannel = connection.CreateModel();
            var brandsConsumer = new EventingBasicConsumer(brandsChannel);
            var categoriesConsumer = new EventingBasicConsumer(categoriesChannel);
            var suppliersConsumer = new EventingBasicConsumer(suppliersChannel);

            var connectionString = VaultHandler.GetDbDataFromVault();

            brandsConsumer.Received += (model, ea) =>
            {
                var contextOptions = new DbContextOptionsBuilder<WarehouseContext>()
                                     .UseSqlServer(connectionString)
                                     .Options;
                var dbContext = new WarehouseContext(contextOptions);

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received from brands {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;

                Console.WriteLine(type);

                if (type == "brands.update")
                {
                    var brand = dbContext.Brands.First(a => a.Id == data["id"].Value<int>());
                    brand.BrandName = data["brandName"].Value<string>();
                    dbContext.SaveChanges();
                }

                if (type == "brands.add")
                {
                    dbContext.Brands.Add(new Brand()
                    {
                        Id = data["id"].Value<int>(),
                        BrandName = data["brandName"].Value<string>()
                    });
                    dbContext.SaveChanges();
                }
            };

            categoriesConsumer.Received += (model, ea) =>
            {
                var contextOptions = new DbContextOptionsBuilder<WarehouseContext>()
                                     .UseSqlServer(connectionString)
                                                         .Options;
                var dbContext = new WarehouseContext(contextOptions);

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received from categories {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;

                Console.WriteLine(type);

                if (type == "categories.update")
                {
                    var category = dbContext.Categories.First(a => a.Id == data["id"].Value<int>());
                    category.CategoryName = data["categoryName"].Value<string>();
                    dbContext.SaveChanges();
                }

                if (type == "categories.add")
                {
                    dbContext.Categories.Add(new Category()
                    {
                        Id = data["id"].Value<int>(),
                        CategoryName = data["categoryName"].Value<string>()
                    });
                    dbContext.SaveChanges();
                }
            };

            suppliersConsumer.Received += (model, ea) =>
            {
                var contextOptions = new DbContextOptionsBuilder<WarehouseContext>()
                                     .UseSqlServer(connectionString)
                                     .Options;
                var dbContext = new WarehouseContext(contextOptions);

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received from suppliers {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;

                Console.WriteLine(type);

                if (type == "suppliers.update")
                {
                    var supplier = dbContext.Suppliers.First(a => a.Id == data["id"].Value<int>());
                    supplier.CompanyName = data["companyName"].Value<string>();
                    dbContext.SaveChanges();
                }

                if (type == "suppliers.add")
                {
                    dbContext.Suppliers.Add(new Supplier()
                    {
                        Id = data["id"].Value<int>(),
                        CompanyName = data["companyName"].Value<string>()
                    });
                    dbContext.SaveChanges();
                }
            };

            brandsChannel.BasicConsume(queue: "brands.productservice",
                                       autoAck: true,
                                       consumer: brandsConsumer);

            categoriesChannel.BasicConsume(queue: "categories.productservice",
                                           autoAck: true,
                                           consumer: categoriesConsumer);

            suppliersChannel.BasicConsume(queue: "suppliers.productservice",
                                          autoAck: true,
                                          consumer: suppliersConsumer);
        }
    }
}