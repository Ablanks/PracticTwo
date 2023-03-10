using System;
using Npgsql;


namespace ConsoleApp5
{
    public static class DatabaseRequests
    {
        public static string GetTypeCarQuery()
        {
            // Сохраняем в переменную запрос на получение всех данных и таблицы type_car
            var querySql = "SELECT * FROM type_car";
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
        public static string GetDriverQuery()
        {
            var querySql = "SELECT * FROM driver";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();
            string s = "";
            while (reader.Read())
            {
                s += $"Id: {reader[0]} Имя: {reader[1]} Фамилия: {reader[2]} Дата рождения: {reader[3]} \n";
            }

            return s;
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
        /// Метод GetRightsCategoryQuery
        /// отправляет запрос в БД на получение списка категории прав
        /// выводит в консоль данные о категории прав
        /// </summary>
        public static void GetRightsCategoryQuery()
        {
            var querySql = "SELECT * FROM rights_category";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader[0]} Название категории прав: {reader[1]}");
            }
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
        public static void GetDriverRightsCategoryQuery()
        {
            
            var querySql = "SELECT dr.first_name, dr.last_name, rc.name " +
                           "FROM driver_rights_category " +
                           "INNER JOIN driver dr on driver_rights_category.id_driver = dr.id " +
                           "INNER JOIN rights_category rc on rc.id = driver_rights_category.id_rights_category ";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Имя: {reader[0]} Фамилия: {reader[1]} Категория прав: {reader[2]}");
            }
        }
        
        
        public static void AddCarQuery(int type, string name, string stateName, int numberSeats)
        {
            var querySql =
                $"INSERT INTO car(id_type_car, name, state_number, number_passengers) VALUES ({type}, {name}, {stateName}, {numberSeats})";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
        }
        public static string GetCarQuery()
        {
            var querySql = "SELECT A.id, tc.name, A.name, state_number, number_passengers FROM car A" +
                           " INNER JOIN type_car tc on A.id_type_car = tc.id"; 

            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();
            string s = "";
            while (reader.Read())
            {
                s += $"Id {reader[0]}, Тип {reader[1]}, название {reader[2]}, штатный номер {reader[3]}, количество посадочных мест {reader[4]} \n";
            }

            return s;
        }
        
        public static void GetItineraryQuery()
        {
            var querySql = "SELECT * FROM Itinerary"; 

            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Id {reader[0]}, название маршрута {reader[1]}");
            }
        }
        
        public static void AddItineraryQuery(string name)
        {
            var querySql = $"INSERT INTO itinerary(name) VALUES ('{name}')";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();;
        }
        
        public static string GetRouteQuery()
        {
            var querySql = "SELECT A.id, dr.first_name, dr.last_name, cr.name, it.name, A.number_passengers From route A" +
                           " INNER JOIN driver dr on A.id_driver = dr.id " +
                           " INNER JOIN car cr on A.id_car = cr.id " +
                           " INNER JOIN itinerary it on A.id_itinerary = it.id "; 

            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();
            string s = "";
            while (reader.Read())
            {
                s += $"Id {reader[0]} Имя {reader[1]}, Фамилия {reader[2]}, Машина {reader[3]}, Маршрут {reader[4]}, Число пассажиров {reader[5]} \n";
            }

            return s;
        }
        
        public static void AddRouteQuery(int driverId, int carId,int itineraryId, int numberPassengers)
        {
            var querySql = $"INSERT INTO route(id_driver, id_car, id_itinerary, number_passengers) VALUES ('{driverId}, {carId}, {itineraryId}, {numberPassengers}')";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();;
        }
    }
}