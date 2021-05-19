using ConfectApp.adminMenu;
using ConfectApp.List;
using Plugin.DeviceInfo;
using System;
using System.Data.SqlClient;

namespace ConfectApp
{
    public class Msg
    {
        public string header;
        public string txt;
        public DateTime date;
    }
    public class Review
    {
        public int speed;
        public int kitchen;
        public int service;
        public string text;
        public int sum;
        public DateTime date;
        public int id;
    }
    public class Order
    {
        public int id;
        public string Id;
        public string payment;
        public string comment;
        public string time;
        public string nomination;
        public string status;
        public int total;
        public string GPS;
        public string products;
        public DateTime date;
        public int point;
        public string phone;
        public string firstName;
    }
    public class User
    {
        public int IOApp;
        public int id;
        public string Id;
        public int role;
        public string Role;
        public string firstName;
        public string lastName;
        public string password;
        public string phone;
        public string gender;
        public DateTime DOB;
        public string Date;
        public string dob;
        public string zone;
        public int point;
        public int total;
        public int count;
        public string Point;
        public string tranc;
    }
    public class Product
    {
        public int productId;
        public int basketAmount;
        public string productName;
        public string productCharacter;
        public string productAmount;
        public int productPrice;
        public int basketPrice;
        public string productPhoto;
    }
    static class DbWorking
    {
        public static string deviceId = CrossDeviceInfo.Current.Id;

        static SqlConnection connection;
        static DbWorking()
        {
            string cs = $"Data Source=192.168.0.105,1433;Initial Catalog=dbConfectApp;User ID=test;Password=test";
            connection = new SqlConnection(cs);
            connection.Open();
        }
        // проверка пользователя при входе
        static public bool CheckUser(string phone, string password)
        {
            string sql = $"SELECT Phone, Password FROM Users WHERE Phone = N'{phone}' AND Password = N'{password}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        // проверка уникальности имени при регистрации
        static public bool CheckPhone(string phone)
        {
            string sql = $"SELECT Phone FROM Users WHERE Phone = N'{phone}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User check = new User();
            while (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        // добавление пользователя в базу данных
        static public bool RegUser(User register)
        {
            if (!(DbWorking.CheckPhone(register.phone)))
            {
                var sql = @"INSERT INTO Users (Role,FirstName,LastName, Password, Phone, Gender, DOB)" +
                    $"VALUES({register.role}, N'{register.firstName}', N'{register.lastName}', N'{register.password}', N'{register.phone}'" +
                    $", N'{register.gender}', N'{register.DOB.ToString("yyyy-MM-dd")}')";

                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        // вывод количества клиентов 
        static public User ViewClients()
        {
            string sql = "SELECT * FROM Users WHERE Role = 0";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User view = new User();
            int i = 0;
            while (reader.Read())
            {
                view.Id = $"{view.Id}\n{++i}";
                view.firstName = view.firstName + "\n" + reader.GetString(reader.GetOrdinal("FirstName"));
                view.lastName = view.lastName + "\n" + reader.GetString(reader.GetOrdinal("LastName"));
                view.phone = view.phone + "\n" + reader.GetString(reader.GetOrdinal("Phone"));
                view.gender = view.gender + "\n" + reader.GetString(reader.GetOrdinal("Gender"));
                view.dob = view.dob + "\n" + reader.GetDateTime(reader.GetOrdinal("DOB")).ToShortDateString();
            }
            reader.Close();
            return view;
        }
        // проверка на наличие id-телефона в бд
        static public bool checkDeviceID()
        {
            string sql = $"SELECT DeviceId FROM Device WHERE DeviceId = N'{deviceId}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        // добавляет id-телефона пользователя при регистрации
        static public void AddDeviceID()
        {
            if (!(DbWorking.checkDeviceID()))
            {
                var sql = @"INSERT INTO Device (DeviceId)" +
                       $"VALUES(N'{deviceId}')";

                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.ExecuteNonQuery();
            }
        }
        // проверка IO пользователя
        static public int CheckIO()
        {
            string sql = $"SELECT * FROM Device D JOIN Users U ON U.Id = D.UserId WHERE D.DeviceId = N'{deviceId}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                return 1;
            }
            reader.Close();
            return 0;
        }
        // наличие прав пользователя
        static public int isCheckMenu()
        {
            string sql = $"SELECT U.Role FROM Users U JOIN Device D ON U.Id = D.UserId WHERE D.DeviceId = N'{deviceId}' AND U.Id = D.UserId";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.role = reader.GetInt32(reader.GetOrdinal("Role"));
                reader.Close();
                return user.role;
            }
            reader.Close();
            return -1;
        }
        // выбор меню пользователю
        static public int UserMenu(string phone)
        {
            string sql = $"SELECT D.deviceId,U.Role, U.Phone FROM Users U JOIN Device D ON U.Id = D.UserId " +
                $"WHERE D.DeviceId = N'{deviceId}' AND U.Phone = N'{phone}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.role = reader.GetInt32(reader.GetOrdinal("Role"));
                reader.Close();
                return user.role;
            }
            reader.Close();
            return -1;
        }
        // обновляет данные о входе, выходе из аккаунта
        static public void UpdateToIO(int check, string phone)
        {
            if (check == 1)
            {
                var sql = $"UPDATE Device SET UserId=(SELECT Id FROM Users WHERE Phone = N'{phone}') WHERE DeviceId = N'{deviceId}'";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                var sql = $"UPDATE Device SET UserId = NULL WHERE DeviceId = N'{deviceId}'";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.ExecuteNonQuery();
            }
        }
        // добавление товара в корзину 
        static public void AddToBasket(int productId)
        {
            var sql = $"SELECT * FROM Basket WHERE N'{productId}' = ProductId AND (SELECT DeviceId FROM Device WHERE DeviceId = N'{deviceId}') = N'{deviceId}'";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Product pr = new Product();
            while (reader.Read())
            {
                pr.productId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                if (pr.productId == productId)
                {
                    pr.basketAmount = reader.GetInt32(reader.GetOrdinal("BasketAmount"));
                    pr.basketAmount = ++pr.basketAmount;
                    reader.Close();
                    sql = $"UPDATE Basket SET BasketAmount = {pr.basketAmount}";
                    sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.ExecuteNonQuery();
                    reader.Close();
                    return;
                }
            }
            reader.Close();

            sql = @"INSERT INTO Basket (DeviceId, productId, Price)" +
                      $"VALUES((SELECT Id FROM Device WHERE DeviceId = N'{deviceId}'), {productId}, (SELECT Price FROM Products WHERE Id= N'{productId}'))";

            sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // манипулирование товаров в корзине
        static public void ManipulationBasket(int productId, int check)
        {
            var sql = $"SELECT * FROM Basket WHERE N'{productId}'=ProductId AND DeviceId = (SELECT Id FROM Device WHERE DeviceId = N'{deviceId}')";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Product pr = new Product();
            while (reader.Read())
            {
                pr.productId = reader.GetInt32(reader.GetOrdinal("productId"));
                if (pr.productId == productId)
                {
                    if (check == 1)
                    {
                        pr.basketAmount = reader.GetInt32(reader.GetOrdinal("BasketAmount"));
                        pr.basketAmount = ++pr.basketAmount;
                    }
                    else
                    {
                        pr.basketAmount = reader.GetInt32(reader.GetOrdinal("BasketAmount"));
                        pr.basketAmount = --pr.basketAmount;
                    }
                    reader.Close();
                    sql = $"UPDATE Basket SET BasketAmount = {pr.basketAmount} WHERE N'{productId}' = productId ";
                    sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.ExecuteNonQuery();
                    reader.Close();
                    return;
                }
            }
        }
        // выводит список товаров в корзине
        static public Product ViewToBasket(int click)
        {
            string sql = $"SELECT * FROM Products P JOIN Basket B ON P.Id = B.ProductId WHERE (SELECT DeviceId FROM Device WHERE DeviceId = N'{deviceId}') = N'{deviceId}'";
            bool check = true;
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Product p = new Product();
            while (reader.Read())
            {
                p.productPhoto = reader.GetString(reader.GetOrdinal("Photo"));
                p.productName = reader.GetString(reader.GetOrdinal("Title"));
                p.productCharacter = reader.GetString(reader.GetOrdinal("Description"));
                p.productAmount = reader.GetString(reader.GetOrdinal("ProductAmount"));
                p.productPrice = reader.GetInt32(reader.GetOrdinal("Price"));
                p.basketAmount = 0 + reader.GetInt32(reader.GetOrdinal("BasketAmount"));
                p.productId = reader.GetInt32(reader.GetOrdinal("Id"));
                p.basketPrice = 1;

                if (click == 1)
                {
                    BasketList.listPoducts(p);
                }
                else
                {
                    ProductBasket.Prod(p);
                }
                check = false;
            }
            reader.Close();
            if (check)
            {
                p = null;
            }
            return p;
        }
        // заказ товара пользователем
        static public void OrdersAdd(Order userPay)
        {
            var sql = $"INSERT INTO Orders (Nomination, UserId, Products, Comment, GPS,Time,Payment, Total) VALUES(N'{userPay.nomination}',(SELECT Id FROM Users WHERE Phone = N'{userPay.phone}'), N'{userPay.products}',N'{userPay.comment}', N'{userPay.GPS}'," +
                $"N'{userPay.time}',N'{userPay.payment}',{userPay.total})";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();

            // добавление баллов
            sql = $"INSERT INTO Point (UserId, Point) VALUES((SELECT Id FROM Users WHERE Phone = N'{userPay.phone}'),{userPay.point})";

            sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
            return;
        }
        // вывод истории заказов пользователю
        static public void ViewHistory()
        {
            var sql = $"SELECT * FROM Orders O JOIN Users U ON O.UserId = U.Id WHERE UserId = (SELECT UserId FROM Device WHERE DeviceId = N'{deviceId}') ORDER BY O.Id DESC";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Order history = new Order();
            while (reader.Read())
            {
                history.nomination = reader.GetString(reader.GetOrdinal("Nomination"));
                history.GPS = reader.GetString(reader.GetOrdinal("GPS"));
                history.comment = reader.GetString(reader.GetOrdinal("Comment"));
                history.time = reader.GetString(reader.GetOrdinal("Time"));
                history.products = reader.GetString(reader.GetOrdinal("Products"));
                history.payment = reader.GetString(reader.GetOrdinal("Payment"));
                history.total = 0 + reader.GetInt32(reader.GetOrdinal("Total"));
                history.date = reader.GetDateTime(reader.GetOrdinal("Date"));
                history.status = reader.GetString(reader.GetOrdinal("Status"));
                History.CreateForms(history);
            }
            reader.Close();
        }
        // выводит статистику получения баллов пользователями
        static public User LogPoint()
        {
            string sql = $"SELECT * FROM Point P JOIN Users U ON P.UserId = U.Id";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User p = new User();
            while (reader.Read())
            {
                p.Date = Convert.ToString(p.Date + "\n" + reader.GetDateTime(reader.GetOrdinal("Date")).ToString("g"));
                p.Point = Convert.ToString(p.Point + "\n" + ("+" + reader.GetInt32(reader.GetOrdinal("Point")) + " балл."));
                p.phone = p.phone + "\n" + reader.GetString(reader.GetOrdinal("Phone"));
                p.tranc = p.tranc + "\n" + "Кэшбэк";
            }
            reader.Close();
            return p;
        }
        // просмотр кол-ва баллов пользователем
        static public void ViewPoint(string phone)
        {
            string sql = $"SELECT * FROM Point P JOIN Users U ON P.UserId = U.Id WHERE Phone = N'{phone}'";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Order p = new Order();
            while (reader.Read())
            {
                p.date = reader.GetDateTime(reader.GetOrdinal("Date"));
                p.point = reader.GetInt32(reader.GetOrdinal("Point"));
                Profile.LogPoint(p);
            }
            reader.Close();
        }
        // удаление всех товаров в корзине
        static public void DeleteBasket()
        {
            var sql = $"DELETE FROM Basket WHERE (SELECT Id FROM Device WHERE DeviceId = N'{deviceId}') = DeviceId";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // удаление товара в корзине по индексу
        static public void DeleteBasketIndex(string name)
        {
            var sql = $"DELETE FROM Basket WHERE (SELECT Id FROM Device WHERE DeviceId = N'{deviceId}') = DeviceId AND " +
                $"(SELECT Id FROM Products WHERE Title = N'{name}') = ProductId";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // удаление отзыва по индексу
        static public void DeleteReviewIndex(int id)
        {
            var sql = $"DELETE FROM Reviews WHERE Id = {id}";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // выводит данные о пользователе пользователю
        static public User ViewUser()
        {
            string sql = "SELECT FirstName, LastName, Phone ,DOB, 2 * COUNT(O.Id) AS Point, COUNT(O.Id) AS Count FROM Users U " +
                $"JOIN Device D ON U.Id = D.UserId LEFT JOIN Orders O ON O.UserId = U.Id WHERE D.DeviceId = N'{deviceId}' " +
                "GROUP BY FirstName, LastName, Phone, DOB; ";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User u = new User();
            while (reader.Read())
            {
                u.firstName = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
                u.lastName = reader.GetString(reader.GetOrdinal("FirstName"));
                u.phone = reader.GetString(reader.GetOrdinal("Phone"));
                u.DOB = reader.GetDateTime(reader.GetOrdinal("DOB"));
                u.point = reader.GetInt32(reader.GetOrdinal("Point"));
                u.count = reader.GetInt32(reader.GetOrdinal("Count"));
                reader.Close();
                return u;
            }
            reader.Close();
            return u;
        }

        static public User User()
        {
            string sql = $"SELECT * FROM Users U JOIN Device D ON U.Id = D.UserId WHERE D.DeviceId = N'{deviceId}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User us = new User();
            while (reader.Read())
            {
                us.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                us.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                us.gender = reader.GetString(reader.GetOrdinal("Gender"));
                us.DOB = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("DOB")).ToShortDateString());
                us.phone = reader.GetString(reader.GetOrdinal("Phone"));
                reader.Close();
                return us;
            }
            reader.Close();
            return us;
        }
        // редактирование профиля пользователем
        static public void EditProfile(string phone, string firstName, string lastName, string gender)
        {
            var sql = $"UPDATE Users SET FirstName = N'{firstName}', LastName = N'{lastName}', Gender = N'{gender}' WHERE Phone = N'{phone}'";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // создать рассылку для пользователей
        static public void AddPush(string header, string text)
        {
            var sql = @"INSERT INTO Push (Header,Text)" +
                     $"VALUES(N'{header}', N'{text}')";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // вывод пуш-рассылки
        static public void ViewMsg()
        {
            string sql = $"SELECT * FROM Push";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Msg msg = new Msg();
            while (reader.Read())
            {
                msg.date = reader.GetDateTime(reader.GetOrdinal("Date"));
                msg.header = reader.GetString(reader.GetOrdinal("Header"));
                msg.txt = reader.GetString(reader.GetOrdinal("Text"));
                News.CreateForm(msg);
            }
            reader.Close();
        }
        // просмотр отзывов
        static public void ViewReviews(int choice)
        {
            string sql = $"SELECT * FROM Reviews";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Review rev = new Review();
            while (reader.Read())
            {
                rev.date = reader.GetDateTime(reader.GetOrdinal("Date"));
                rev.sum = (reader.GetInt32(reader.GetOrdinal("Speed")) + reader.GetInt32(reader.GetOrdinal("Kitchen")) + reader.GetInt32(reader.GetOrdinal("Service"))) / 3;
                rev.text = reader.GetString(reader.GetOrdinal("Text"));
                rev.id = reader.GetInt32(reader.GetOrdinal("Id"));
                if (choice == 1)
                {
                    Reviews.CreateFormUser(rev);
                }
                else
                {
                    Reviews.CreateFormAdmin(rev);
                }
            }
            reader.Close();
        }
        // удаление курьера
        static public void DeleteCourier(string phone)
        {
            var sql = $"DELETE FROM Courier WHERE Phone = N'{phone}'";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // добавление, обновление данных о курьерах
        static public bool AddCourier(User user)
        {
            if (DbWorking.CheckCourier(user.phone))
            {
                var sql = $"UPDATE Courier SET Name = N'{user.firstName}', Zone = N'{user.zone}' WHERE Phone = N'{user.phone}'";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.ExecuteNonQuery();
                return false;
            }
            else
            {
                var sql = $"INSERT INTO Courier VALUES(N'{user.firstName}', N'{user.phone}', N'{user.zone}')";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
        }
        // просмотр на наличие уже занесенного в бд курьера
        static public bool CheckCourier(string phone)
        {
            User us = new User();
            string sql = $"SELECT Phone FROM Courier WHERE Phone = N'{phone}'";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        // просмотр списка курьеров
        static public User ViewCourier()
        {
            User u = new User();
            string sql = $"SELECT * FROM Courier";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                u.firstName = u.firstName + "\n" + reader.GetString(reader.GetOrdinal("Name"));
                u.phone = u.phone + "\n" + reader.GetString(reader.GetOrdinal("Phone"));
                u.zone = u.zone + "\n" + reader.GetString(reader.GetOrdinal("Zone"));
            }
            reader.Close();
            return u;
        }
        // вывод товаров в список заказов
        static public Order ViewOrders()
        {
            Order u = new Order();
            string sql = $"SELECT * FROM Orders O JOIN Users U ON O.UserId = U.Id";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                u.id = reader.GetInt32(reader.GetOrdinal("Id"));
                u.date = reader.GetDateTime(reader.GetOrdinal("Date"));
                u.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                u.phone = reader.GetString(reader.GetOrdinal("Phone"));
                u.total = reader.GetInt32(reader.GetOrdinal("Total"));
                u.status = reader.GetString(reader.GetOrdinal("Status"));
                u.products = reader.GetString(reader.GetOrdinal("Nomination")) + ": " + reader.GetString(reader.GetOrdinal("GPS")) + "\n" + u.phone + "\n" + u.firstName +
                    "\n---\n" + "Комментарий: " + reader.GetString(reader.GetOrdinal("Comment")) + "\n---\n" + "Заберу заказ через " + reader.GetString(reader.GetOrdinal("Time")) + "\n--\n" + reader.GetString(reader.GetOrdinal("Products")) + "\n--\n" +
                    "Форма оплаты: " + reader.GetString(reader.GetOrdinal("Payment")) + "\nСумма заказа: " + u.total;
                Orders.CreateForm(u);
            }
            reader.Close();
            return u;
        }
        // добавление отзыва
        static public void AddReview(Review review)
        {
            var sql = $"INSERT INTO Reviews(Speed, Kitchen, Service, Text) VALUES ({review.speed},{review.kitchen},{review.service},N'{review.text}')";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        static public void UpdateOrder(string status, string phone, int id)
        {
            var sql = $"UPDATE Orders SET Status = N'{status}' WHERE (SELECT Phone FROM Users WHERE Phone = N'{phone}') = N'{phone}' AND Id = {id}";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.ExecuteNonQuery();
        }
        // статистика по отзывам
        static public Review StatsReviews()
        {
            bool check = false;
            string sql = $"SELECT * FROM Reviews";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Review rev = new Review();
            int i = 0;
            while (reader.Read())
            {
                ++i;
                rev.speed = rev.speed + reader.GetInt32(reader.GetOrdinal("Speed"));
                rev.kitchen = rev.kitchen + reader.GetInt32(reader.GetOrdinal("Kitchen"));
                rev.service = rev.service + reader.GetInt32(reader.GetOrdinal("Service"));
                check = true;
            }
            if (!check)
            {
                rev.speed = 0;
                rev.kitchen = 0;
                rev.service = 0;
                rev.sum = 0;
                reader.Close();
                return rev;
            }
            rev.speed = rev.speed / i;
            rev.kitchen = rev.kitchen / i;
            rev.service = rev.service / i;
            rev.sum = (rev.speed + rev.kitchen + rev.service) / 3;
            reader.Close();
            return rev;
        }
        // статистика кол-ва пользователей в приложении
        static public int StatsUsers()
        {
            string sql = $"SELECT COUNT(Id) AS Count FROM Users";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            int i = 0;
            User us = new User();
            while (reader.Read())
            {
                us.total = reader.GetInt32(reader.GetOrdinal("Count"));
                reader.Close();
                return us.total;
            }
            reader.Close();
            return 0;
        }
        // статистика кол-ва заказов в приложении
        static public int StatsOrders()
        {
            string sql = $"SELECT COUNT(id) AS Count FROM Orders";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User us = new User();
            while (reader.Read())
            {
                us.total = reader.GetInt32(reader.GetOrdinal("Count"));
                reader.Close();
                return us.total;
            }
            reader.Close();
            return 0;
        }
        // статистика прибыли
        static public int StatsProfit()
        {
            string sql = $"SELECT SUM(Total) AS Total, Status FROM Orders WHERE Status = 'Доставлен' GROUP BY Status";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User us = new User();

            while (reader.Read())
            {
                us.total = reader.GetInt32(reader.GetOrdinal("Total"));
                reader.Close();
                return us.total;
            }
            reader.Close();
            return 0;
        }
        // статистика заказов по датам
        static public int StatsNumberOfOrders(int check)
        {
            var sql = $"SELECT COUNT(Id) AS Count FROM Orders WHERE GETDATE()-1 <= Date";

            if (check == 1)
            {
                sql = $"SELECT COUNT(Id) AS Count FROM Orders WHERE DATEADD(day, -7, GETDATE()) <= GETDATE()";
            }
            else if (check == 2)
            {
                sql = $"SELECT COUNT(Id) AS Count FROM Orders WHERE DATEADD(month, -1, GETDATE()) <= GETDATE()";
            }
            else if (check == 3)
            {
                sql = $"SELECT COUNT(Id) AS Count FROM Orders WHERE DATEADD(year, -1, GETDATE()) <= GETDATE()";

            }
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            User us = new User();
            while (reader.Read())
            {
                us.count = reader.GetInt32(reader.GetOrdinal("Count"));
                reader.Close();
                return us.count;
            }
            reader.Close();
            return 0;
        }
        static public void Free() { connection.Close(); connection.Dispose(); }
    }
}
