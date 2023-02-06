using Core.Configuration;
using Core.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public OrdersRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings?.Value ??
            throw new ArgumentNullException(nameof(connectionStrings));
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            using(var connection = CreateConnection(_connectionStrings.MainDB))
            {
                IEnumerable<Order> ordersRequest;
                try
                {
                    var ordersQuery = $@"SELECT * FROM Orders";
                    ordersRequest = await connection.QueryAsync<Order>(ordersQuery);

                    connection.Close();
                    return ordersRequest;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return ordersRequest = new List<Order>();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUsername(string username)
        {
            using (var connection = CreateConnection(_connectionStrings.MainDB))
            {
                IEnumerable<Order> ordersRequest;
                try
                {
                    var parameters = new
                    {
                        CreatedByUsername = username
                    };
                    var ordersQuery = $@"SELECT * FROM Orders WHERE CreatedByUsername = @CreatedByUsername;";
                    ordersRequest = await connection.QueryAsync<Order>(ordersQuery, parameters);

                    connection.Close();
                    return ordersRequest;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return ordersRequest = new List<Order>();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByType(string orderType)
        {
            using (var connection = CreateConnection(_connectionStrings.MainDB))
            {
                IEnumerable<Order> ordersRequest;
                try
                {
                    var parameters = new
                    {
                        OrderType = orderType
                    };
                    var ordersQuery = $@"SELECT * FROM Orders WHERE OrderType = @OrderType;";
                    ordersRequest = await connection.QueryAsync<Order>(ordersQuery, parameters);

                    connection.Close();
                    return ordersRequest;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return ordersRequest = new List<Order>();
            }
        }

        public async Task<string> CreateOrder(Order order)
        {
            using (var connection = CreateConnection(_connectionStrings.MainDB))
            {
                try
                {
                    var parameters = new
                    {
                        Id = Guid.NewGuid(),
                        OrderType = order.OrderType,
                        CustomerName = order.CustomerName,
                        CreatedByUsername = order.CreatedByUsername
                    };

                    var insertOrderQuery = $@"INSERT INTO Orders 
                    (Id, OrderType, CustomerName, CreatedDate, CreatedByUsername)
                    VALUES
                    (@Id, @OrderType, @CustomerName, GETDATE(), @CreatedByUsername);

                    SELECT Id FROM Orders WHERE Id = @Id;
                    ";

                    string responseId = (await connection.QueryFirstOrDefaultAsync<Guid>(insertOrderQuery, parameters)).ToString();
                    return responseId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return "";
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            using (var connection = CreateConnection(_connectionStrings.MainDB))
            {
                Order result = new Order();
                try
                {
                    var Params = new
                    {
                        Id = order.Id,
                        CustomerName = order.CustomerName,
                        OrderType = order.OrderType
                    };

                    string selectQuery = $@"SELECT Id FROM Orders WHERE Id = CAST(@Id AS uniqueidentifier);";
                    var searchResult = await connection.QueryFirstOrDefaultAsync<Guid>(selectQuery, Params);
                    if (!(searchResult == new Guid()))
                    {
                        string updateQuery = $@"UPDATE Orders SET CustomerName = @CustomerName, OrderType = @OrderType WHERE Id = CAST(@Id AS uniqueidentifier);
                                                SELECT * FROM  Orders WHERE Id = CAST(@Id AS uniqueidentifier);";
                        result = await connection.QueryFirstOrDefaultAsync<Order>(updateQuery, Params);
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return result;
            }
        }

        public async Task<string> DeleteOrder(string id)
        {
            using(var connection = CreateConnection(_connectionStrings.MainDB))
            {
                string curatedResult = "";
                try
                {
                    var parameters = new
                    {
                        Id = id
                    };

                    string selectQuery = $@"SELECT Id FROM Orders WHERE Id = CAST(@Id AS uniqueidentifier);";
                    var result = await connection.QueryFirstOrDefaultAsync<Guid>(selectQuery, parameters);
                    if (result == new Guid())
                    {
                        return curatedResult;
                    }
                    string deleteQuery = $@"DELETE FROM Orders WHERE Id = CAST(@Id AS uniqueidentifier);";
                    await connection.QueryFirstOrDefaultAsync(deleteQuery, parameters);
                    curatedResult = result.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return curatedResult;
            }
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
