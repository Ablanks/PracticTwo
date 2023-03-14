using System;
using Npgsql;


namespace ConsoleApp2
{
    public static class DatabaseRequests
    {
        public static string GetTypeCarQuery()
        {
            // Сохраняем в переменную запрос на получение всех данных и таблицы type_car
            var querySql = "SELECT * FROM users";
            // Создаем команду(запрос) cmd, передаем в нее запрос и соединение к БД
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            // Выполняем команду(запрос)
            // результат команды сохранится в переменную reader
            using var reader = cmd.ExecuteReader();
            string s = "";
            // Выводим данные которые вернула БД
            while (reader.Read())
            {
                s += $"Id: {reader[0]} Название: {reader[1]} \n";
            }

            return s;
        }

        /// <summary>
        /// Метод AddTypeCarQuery
        /// отправляет запрос в БД на добавление типа машины
        /// </summary>
        public static void AddUser(long a)
        {
            var querySql = 
                $"INSERT INTO users (chat_id, default_city, notification, time_notification) VALUES ({a}, '{""}', {false}, {0.0}) ON CONFLICT DO NOTHING";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }

        public static void UpdUser(string m, long a)
        {
            var querySql = $"UPDATE users SET default_city = '{m}' WHERE chat_id = {a}";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }
    }
}