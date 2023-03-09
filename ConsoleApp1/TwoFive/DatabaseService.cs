using Npgsql;

namespace ConsoleApp5
{
    public static class DatabaseService
    {
        private static NpgsqlConnection? _connection;
        /// <summary>
        /// Метод GetConnectionString()
        /// возвращает строку подключения к БД
        /// </summary>
        private static string GetConnectionString()
        {
            return @"Host=10.30.0.137;Port=5432;Database=gr621_praev;Username=gr621_praev;Password=QWEasd12";
        }

        /// <summary>
        /// Метод GetSqlConnection()
        /// проверяет есть ли уже открытое соединение с БД
        /// если нет, то открывает соединение с БД
        /// </summary>
        /// <returns></returns>
        public static NpgsqlConnection GetSqlConnection()
        {
            if (_connection is null)
            {
                _connection = new NpgsqlConnection(GetConnectionString());
                _connection.Open();
            }
        
            return _connection;
        }
    }
}