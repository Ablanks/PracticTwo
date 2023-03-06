using System;
using Npgsql;


namespace ConsoleApp5
{
    public static class DatabaseRequests
    {
        public static void GetTypeCarQuery()
        {
            // Сохраняем в переменную запрос на получение всех данных и таблицы type_car
            var querySql = "SELECT * FROM type_car";
            // Создаем команду(запрос) cmd, передаем в нее запрос и соединение к БД
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            // Выполняем команду(запрос)
            // результат команды сохранится в переменную reader
            using var reader = cmd.ExecuteReader();

            // Выводим данные которые вернула БД
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader[0]} Название: {reader[1]}");
            }
        }

        /// <summary>
        /// Метод AddTypeCarQuery
        /// отправляет запрос в БД на добавление типа машины
        /// </summary>
        public static void AddTypeCarQuery(string name)
        {
            var querySql = $"INSERT INTO type_car(name) VALUES ('{name}')";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Метод AddDriverQuery
        /// отправляет запрос в БД на добавление водителей
        /// </summary>
        public static void AddDriverQuery(string firstName, string lastName, DateTime birthdate)
        {
            var querySql =
                $"INSERT INTO driver(first_name, last_name, birthdate) VALUES ('{firstName}', '{lastName}', '{birthdate}')";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Метод GetDriverQuery
        /// отправляет запрос в БД на получение списка водителей
        /// выводит в консоль данные о водителях
        /// </summary>
        public static void GetDriverQuery()
        {
            var querySql = "SELECT * FROM driver";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader[0]} Имя: {reader[1]} Фамилия: {reader[2]} Дата рождения: {reader[3]}");
            }
        }

        /// <summary>
        /// Метод AddRightsCategoryQuery
        /// отправляет запрос в БД на добавление категорий прав
        /// </summary>
        public static void AddRightsCategoryQuery(string name)
        {
            var querySql = $"INSERT INTO rights_category(name) VALUES ('{name}')";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Метод AddDriverRightsCategoryQuery
        /// отправляет запрос в БД на добавление категории прав водителю
        /// </summary>
        public static void AddDriverRightsCategoryQuery(int driver, int rightsCategory)
        {
            var querySql =
                $"INSERT INTO driver_rights_category(id_driver, id_rights_category) VALUES ({driver}, {rightsCategory})";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Метод GetDriverRightsCategoryQuery
        /// отправляет запрос в БД на получение категорий водителей
        /// выводит в консоль информацию о категориях прав водителей
        /// </summary>
        public static void GetDriverRightsCategoryQuery(int driver)
        {
            var querySql = "SELECT dr.first_name, dr.last_name, rc.name " +
                           "FROM driver_rights_category " +
                           "INNER JOIN driver dr on driver_rights_category.id_driver = dr.id " +
                           "INNER JOIN rights_category rc on rc.id = driver_rights_category.id_rights_category " +
                           $"WHERE dr.id = {driver};";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Имя: {reader[0]} Фамилия: {reader[1]} Категория прав: {reader[2]}");
            }
        }
    }
}