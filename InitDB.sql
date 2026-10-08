USE master;
GO
IF DB_ID('shoeshop') IS NOT NULL
BEGIN
    ALTER DATABASE shoeshop SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE shoeshop;
END
GO
CREATE DATABASE shoeshop;
GO
USE shoeshop;
GO
CREATE TABLE roles (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(50) NOT NULL);
CREATE TABLE categories (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(100) NOT NULL);
CREATE TABLE manufacturers (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(100) NOT NULL);
CREATE TABLE suppliers (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(100) NOT NULL);
CREATE TABLE order_statuses (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(50) NOT NULL);
CREATE TABLE pickup_points (id INT IDENTITY(1,1) PRIMARY KEY, address NVARCHAR(MAX) NOT NULL);
CREATE TABLE users (id INT IDENTITY(1,1) PRIMARY KEY, login NVARCHAR(50) UNIQUE NOT NULL, password_hash NVARCHAR(64) NOT NULL, full_name NVARCHAR(150) NOT NULL, role_id INT REFERENCES roles(id));
CREATE TABLE products (id INT IDENTITY(1,1) PRIMARY KEY, product_article NVARCHAR(100) UNIQUE, title NVARCHAR(150) NOT NULL, category_id INT REFERENCES categories(id), description NVARCHAR(MAX), manufacturer_id INT REFERENCES manufacturers(id), supplier_id INT REFERENCES suppliers(id), cost DECIMAL(10,2) CHECK (cost >= 0), unit NVARCHAR(20), stock_quantity INT CHECK (stock_quantity >= 0), discount INT DEFAULT 0 CHECK (discount >= 0 AND discount <= 100), photo_path NVARCHAR(255));
CREATE TABLE orders (id INT IDENTITY(1,1) PRIMARY KEY, order_article NVARCHAR(50) UNIQUE NOT NULL, status_id INT REFERENCES order_statuses(id), pickup_point_id INT REFERENCES pickup_points(id), order_date DATE NOT NULL, delivery_date DATE NOT NULL, client_fio NVARCHAR(150), receive_code INT);
CREATE TABLE order_items (order_id INT REFERENCES orders(id) ON DELETE CASCADE, product_id INT REFERENCES products(id), count INT CHECK (count > 0), PRIMARY KEY(order_id, product_id));
GO

INSERT INTO roles (name) VALUES (N'Гость');
INSERT INTO roles (name) VALUES (N'Авторизированный клиент');
INSERT INTO roles (name) VALUES (N'Менеджер');
INSERT INTO roles (name) VALUES (N'Администратор');
INSERT INTO categories (name) VALUES (N'Женская обувь');
INSERT INTO categories (name) VALUES (N'Мужская обувь');
INSERT INTO manufacturers (name) VALUES (N'Kari');
INSERT INTO manufacturers (name) VALUES (N'Marco Tozzi');
INSERT INTO manufacturers (name) VALUES (N'Рос');
INSERT INTO manufacturers (name) VALUES (N'Rieker');
INSERT INTO manufacturers (name) VALUES (N'Alessio Nesca');
INSERT INTO manufacturers (name) VALUES (N'CROSBY');
INSERT INTO suppliers (name) VALUES (N'Kari');
INSERT INTO suppliers (name) VALUES (N'Обувь для вас');
INSERT INTO order_statuses (name) VALUES (N'Завершен');
INSERT INTO order_statuses (name) VALUES (N'Новый ');
INSERT INTO pickup_points (address) VALUES (N'420151, г. Лесной, ул. Вишневая, 32');
INSERT INTO pickup_points (address) VALUES (N'125061, г. Лесной, ул. Подгорная, 8');
INSERT INTO pickup_points (address) VALUES (N'630370, г. Лесной, ул. Шоссейная, 24');
INSERT INTO pickup_points (address) VALUES (N'400562, г. Лесной, ул. Зеленая, 32');
INSERT INTO pickup_points (address) VALUES (N'614510, г. Лесной, ул. Маяковского, 47');
INSERT INTO pickup_points (address) VALUES (N'410542, г. Лесной, ул. Светлая, 46');
INSERT INTO pickup_points (address) VALUES (N'620839, г. Лесной, ул. Цветочная, 8');
INSERT INTO pickup_points (address) VALUES (N'443890, г. Лесной, ул. Коммунистическая, 1');
INSERT INTO pickup_points (address) VALUES (N'603379, г. Лесной, ул. Спортивная, 46');
INSERT INTO pickup_points (address) VALUES (N'603721, г. Лесной, ул. Гоголя, 41');
INSERT INTO pickup_points (address) VALUES (N'410172, г. Лесной, ул. Северная, 13');
INSERT INTO pickup_points (address) VALUES (N'614611, г. Лесной, ул. Молодежная, 50');
INSERT INTO pickup_points (address) VALUES (N'454311, г.Лесной, ул. Новая, 19');
INSERT INTO pickup_points (address) VALUES (N'660007, г.Лесной, ул. Октябрьская, 19');
INSERT INTO pickup_points (address) VALUES (N'603036, г. Лесной, ул. Садовая, 4');
INSERT INTO pickup_points (address) VALUES (N'394060, г.Лесной, ул. Фрунзе, 43');
INSERT INTO pickup_points (address) VALUES (N'410661, г. Лесной, ул. Школьная, 50');
INSERT INTO pickup_points (address) VALUES (N'625590, г. Лесной, ул. Коммунистическая, 20');
INSERT INTO pickup_points (address) VALUES (N'625683, г. Лесной, ул. 8 Марта');
INSERT INTO pickup_points (address) VALUES (N'450983, г.Лесной, ул. Комсомольская, 26');
INSERT INTO pickup_points (address) VALUES (N'394782, г. Лесной, ул. Чехова, 3');
INSERT INTO pickup_points (address) VALUES (N'603002, г. Лесной, ул. Дзержинского, 28');
INSERT INTO pickup_points (address) VALUES (N'450558, г. Лесной, ул. Набережная, 30');
INSERT INTO pickup_points (address) VALUES (N'344288, г. Лесной, ул. Чехова, 1');
INSERT INTO pickup_points (address) VALUES (N'614164, г.Лесной,  ул. Степная, 30');
INSERT INTO pickup_points (address) VALUES (N'394242, г. Лесной, ул. Коммунистическая, 43');
INSERT INTO pickup_points (address) VALUES (N'660540, г. Лесной, ул. Солнечная, 25');
INSERT INTO pickup_points (address) VALUES (N'125837, г. Лесной, ул. Шоссейная, 40');
INSERT INTO pickup_points (address) VALUES (N'125703, г. Лесной, ул. Партизанская, 49');
INSERT INTO pickup_points (address) VALUES (N'625283, г. Лесной, ул. Победы, 46');
INSERT INTO pickup_points (address) VALUES (N'614753, г. Лесной, ул. Полевая, 35');
INSERT INTO pickup_points (address) VALUES (N'426030, г. Лесной, ул. Маяковского, 44');
INSERT INTO pickup_points (address) VALUES (N'450375, г. Лесной ул. Клубная, 44');
INSERT INTO pickup_points (address) VALUES (N'625560, г. Лесной, ул. Некрасова, 12');
INSERT INTO pickup_points (address) VALUES (N'630201, г. Лесной, ул. Комсомольская, 17');
INSERT INTO pickup_points (address) VALUES (N'190949, г. Лесной, ул. Мичурина, 26');
GO

INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'94d5ous@gmail.com', N'd2820f125d23c3d0fafe06916a704986f3089e5fa0479b4c25161a272f00438a', N'Никифорова Весения Николаевна', 4);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'uth4iz@mail.com', N'38bdbfe589c9f06ae66e0239e387560ec6fd0f1e461bbd920d27acf00673177b', N'Сазонов Руслан Германович', 4);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'yzls62@outlook.com', N'fd08d92fd54f393f46a1fc4ffbfcf83ba802e83683c4d7eafe8b5178c053915e', N'Одинцов Серафим Артёмович', 4);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'1diph5e@tutanota.com', N'47893730c731bbab53f3e8c59d168ccc3267ddcc79aff8381f587db373c5ab4d', N'Степанов Михаил Артёмович', 3);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'tjde7c@yahoo.com', N'8ed59a1b6200df2d0961eebadedcd75ff2f2cf193a41e5bcfa5992f4d2d734c1', N'Ворсин Петр Евгеньевич', 3);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'wpmrc3do@tutanota.com', N'78eb1d08c2fdb9cb0a859a37e0e8604953f47a8819dc829d0efcc6f0699d0af7', N'Старикова Елена Павловна', 3);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'5d4zbu@tutanota.com', N'af40c4a08e1773a45a62c360eddb28141191c5ece01f7c596a76a12f119b18a5', N'Михайлюк Анна Вячеславовна', 2);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'ptec8ym@yahoo.com', N'717ee3bd45179a9ce91b03f015b98ba49a209ea704f0fbd8454f3c5e1b6c372d', N'Ситдикова Елена Анатольевна', 2);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'1qz4kw@mail.com', N'eb7e715f922a40093afa46d1e1522b1ae83bf7bc4a1c1c2775c4f3cd1f8505dd', N'Ворсин Петр Евгеньевич', 2);
INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'4np6se@mail.com', N'28fd0b15dd21c70fb93c7a8869bde5b3ea30b05d9000c8233f961666925697c2', N'Старикова Елена Павловна', 2);
GO

INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'А112Т4', N'Ботинки', 1, N'Женские Ботинки демисезонные kari', 1, 1, 4990, N'шт.', 6, 3, N'1.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'F635R4', N'Ботинки', 1, N'Ботинки Marco Tozzi женские демисезонные, размер 39, цвет бежевый', 2, 2, 3244, N'шт.', 13, 2, N'2.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'H782T5', N'Туфли', 2, N'Туфли kari мужские классика MYZ21AW-450A, размер 43, цвет: черный', 1, 1, 4499, N'шт.', 5, 4, N'3.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'G783F5', N'Ботинки', 2, N'Мужские ботинки Рос-Обувь кожаные с натуральным мехом', 3, 1, 5900, N'шт.', 8, 2, N'4.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'J384T6', N'Ботинки', 2, N'B3430/14 Полуботинки мужские Rieker', 4, 2, 3800, N'шт.', 16, 2, N'5.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'D572U8', N'Кроссовки', 2, N'129615-4 Кроссовки мужские', 3, 2, 4100, N'шт.', 6, 3, N'6.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'F572H7', N'Туфли', 1, N'Туфли Marco Tozzi женские летние, размер 39, цвет черный', 2, 1, 2700, N'шт.', 14, 2, N'7.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'D329H3', N'Полуботинки', 1, N'Полуботинки Alessio Nesca женские 3-30797-47, размер 37, цвет: бордовый', 5, 2, 1890, N'шт.', 4, 4, N'8.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'B320R5', N'Туфли', 1, N'Туфли Rieker женские демисезонные, размер 41, цвет коричневый', 4, 1, 4300, N'шт.', 6, 2, N'9.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'G432E4', N'Туфли', 1, N'Туфли kari женские TR-YR-413017, размер 37, цвет: черный', 1, 1, 2800, N'шт.', 15, 3, N'10.jpg');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'S213E3', N'Полуботинки', 2, N'407700/01-01 Полуботинки мужские CROSBY', 6, 2, 2156, N'шт.', 6, 3, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'E482R4', N'Полуботинки', 1, N'Полуботинки kari женские MYZ20S-149, размер 41, цвет: черный', 1, 1, 1800, N'шт.', 14, 2, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'S634B5', N'Кеды', 2, N'Кеды Caprice мужские демисезонные, размер 42, цвет черный', 6, 2, 5500, N'шт.', 0, 3, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'K345R4', N'Полуботинки', 2, N'407700/01-02 Полуботинки мужские CROSBY', 6, 2, 2100, N'шт.', 3, 2, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'O754F4', N'Туфли', 1, N'Туфли женские демисезонные Rieker артикул 55073-68/37', 4, 2, 5400, N'шт.', 18, 4, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'G531F4', N'Ботинки', 1, N'Ботинки женские зимние ROMER арт. 893167-01 Черный', 1, 1, 6600, N'шт.', 9, 12, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'J542F5', N'Тапочки', 2, N'Тапочки мужские Арт.70701-55-67син р.41', 1, 1, 500, N'шт.', 0, 13, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'B431R5', N'Ботинки', 2, N'Мужские кожаные ботинки/мужские ботинки', 4, 2, 2700, N'шт.', 5, 2, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'P764G4', N'Туфли', 1, N'Туфли женские, ARGO, размер 38', 6, 1, 6800, N'шт.', 15, 15, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'C436G5', N'Ботинки', 1, N'Ботинки женские, ARGO, размер 40', 5, 1, 10200, N'шт.', 9, 15, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'F427R5', N'Ботинки', 1, N'Ботинки на молнии с декоративной пряжкой FRAU', 4, 2, 11800, N'шт.', 11, 15, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'N457T5', N'Полуботинки', 1, N'Полуботинки Ботинки черные зимние, мех', 6, 1, 4600, N'шт.', 13, 3, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'D364R4', N'Туфли', 1, N'Туфли Luiza Belly женские Kate-lazo черные из натуральной замши', 1, 1, 12400, N'шт.', 5, 16, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'S326R5', N'Тапочки', 2, N'Мужские кожаные тапочки Профиль С.Дали ', 6, 2, 9900, N'шт.', 15, 17, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'L754R4', N'Полуботинки', 1, N'Полуботинки kari женские WB2020SS-26, размер 38, цвет: черный', 1, 1, 1700, N'шт.', 7, 2, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'M542T5', N'Кроссовки', 2, N'Кроссовки мужские TOFA', 4, 2, 2800, N'шт.', 3, 18, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'D268G5', N'Туфли', 1, N'Туфли Rieker женские демисезонные, размер 36, цвет коричневый', 4, 2, 4399, N'шт.', 12, 3, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'T324F5', N'Сапоги', 1, N'Сапоги замша Цвет: синий', 6, 1, 4699, N'шт.', 5, 2, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'K358H6', N'Тапочки', 2, N'Тапочки мужские син р.41', 4, 1, 599, N'шт.', 2, 20, N'');
INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'H535R5', N'Ботинки', 1, N'Женские Ботинки демисезонные', 4, 2, 2300, N'шт.', 7, 2, N'');
GO

SET IDENTITY_INSERT orders ON;
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (1, N'1', 1, 1, '2025-02-27', '2025-04-20', N'Степанов Михаил Артёмович', 901);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (2, N'2', 1, 11, '2022-09-28', '2025-04-21', N'Никифорова Весения Николаевна', 902);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (3, N'3', 1, 2, '2025-03-21', '2025-04-22', N'Сазонов Руслан Германович', 903);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (4, N'4', 1, 11, '2025-02-20', '2025-04-23', N'Одинцов Серафим Артёмович', 904);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (5, N'5', 1, 2, '2025-03-17', '2025-04-24', N'Степанов Михаил Артёмович', 905);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (6, N'6', 1, 15, '2025-03-01', '2025-04-25', N'Никифорова Весения Николаевна', 906);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (7, N'7', 1, 3, '2025-02-28', '2025-04-26', N'Сазонов Руслан Германович', 907);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (8, N'8', 2, 19, '2025-03-31', '2025-04-27', N'Одинцов Серафим Артёмович', 908);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (9, N'9', 2, 5, '2025-04-02', '2025-04-28', N'Степанов Михаил Артёмович', 909);
INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES (10, N'10', 2, 19, '2025-04-03', '2025-04-29', N'Степанов Михаил Артёмович', 910);
SET IDENTITY_INSERT orders OFF;
GO

INSERT INTO order_items (order_id, product_id, count) SELECT 1, id, 2 FROM products WHERE product_article = N'А112Т4';
INSERT INTO order_items (order_id, product_id, count) SELECT 1, id, 2 FROM products WHERE product_article = N'F635R4';
INSERT INTO order_items (order_id, product_id, count) SELECT 2, id, 1 FROM products WHERE product_article = N'H782T5';
INSERT INTO order_items (order_id, product_id, count) SELECT 2, id, 1 FROM products WHERE product_article = N'G783F5';
INSERT INTO order_items (order_id, product_id, count) SELECT 3, id, 10 FROM products WHERE product_article = N'J384T6';
INSERT INTO order_items (order_id, product_id, count) SELECT 3, id, 10 FROM products WHERE product_article = N'D572U8';
INSERT INTO order_items (order_id, product_id, count) SELECT 4, id, 5 FROM products WHERE product_article = N'F572H7';
INSERT INTO order_items (order_id, product_id, count) SELECT 4, id, 4 FROM products WHERE product_article = N'D329H3';
INSERT INTO order_items (order_id, product_id, count) SELECT 5, id, 2 FROM products WHERE product_article = N'А112Т4';
INSERT INTO order_items (order_id, product_id, count) SELECT 5, id, 2 FROM products WHERE product_article = N'F635R4';
INSERT INTO order_items (order_id, product_id, count) SELECT 6, id, 1 FROM products WHERE product_article = N'H782T5';
INSERT INTO order_items (order_id, product_id, count) SELECT 6, id, 1 FROM products WHERE product_article = N'G783F5';
INSERT INTO order_items (order_id, product_id, count) SELECT 7, id, 10 FROM products WHERE product_article = N'J384T6';
INSERT INTO order_items (order_id, product_id, count) SELECT 7, id, 10 FROM products WHERE product_article = N'D572U8';
INSERT INTO order_items (order_id, product_id, count) SELECT 8, id, 5 FROM products WHERE product_article = N'F572H7';
INSERT INTO order_items (order_id, product_id, count) SELECT 8, id, 4 FROM products WHERE product_article = N'D329H3';
INSERT INTO order_items (order_id, product_id, count) SELECT 9, id, 5 FROM products WHERE product_article = N'B320R5';
INSERT INTO order_items (order_id, product_id, count) SELECT 9, id, 1 FROM products WHERE product_article = N'G432E4';
INSERT INTO order_items (order_id, product_id, count) SELECT 10, id, 5 FROM products WHERE product_article = N'S213E3';
INSERT INTO order_items (order_id, product_id, count) SELECT 10, id, 5 FROM products WHERE product_article = N'E482R4';
GO

