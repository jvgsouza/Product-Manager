using Dapper;
using System.Data.Common;
using Usuario.Application.Exceptions;
using Usuario.Domain.DTOs;
using Usuario.Domain.Entities;
using Usuario.Domain.Repositories;
using Usuario.Infra.Data;

namespace Usuario.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public User Login(Login login)
        {
            var connection = _connection.getConnection();
            connection.Open();

            try
            {
                var mock_user = new User
                {
                    Email = "teste@gmail.com",
                    Password = "123456"
                };

                if (login.Email == mock_user.Email && login.Password == mock_user.Password)
                {
                    return mock_user;
                }
                else
                {
                    throw new APIException("Email ou senha inválidos");
                }
                //var sql = "Select id, login, password from login where login = @login and password = @password";

                //var parameters = new DynamicParameters();
                //parameters.Add("@login", login.Email);
                //parameters.Add("@password", login.Password);

                //var user = connection.QueryFirst<User>(sql, parameters);

                //return user;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
