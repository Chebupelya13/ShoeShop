-- admin   : 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
-- manager : 1e8a6f3796d421714b4deebcd7f2b1899eebba98e6c4664db27c7d4eaec9dc00
-- client  : 948fe603f6aba037ed107af56172551ecdf656a47ce4e5ba00874e402b932fc6

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

CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
);

CREATE TABLE categories (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL
);

CREATE TABLE manufacturers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL
);

CREATE TABLE suppliers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL
);

CREATE TABLE order_statuses (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
);

CREATE TABLE pickup_points (
    id INT IDENTITY(1,1) PRIMARY KEY,
    address NVARCHAR(MAX) NOT NULL
);

CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    login NVARCHAR(50) UNIQUE NOT NULL,
    password_hash NVARCHAR(64) NOT NULL,
    full_name NVARCHAR(150) NOT NULL,
    role_id INT REFERENCES roles(id)
);

CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(150) NOT NULL,
    category_id INT REFERENCES categories(id),
    description NVARCHAR(MAX),
    manufacturer_id INT REFERENCES manufacturers(id),
    supplier_id INT REFERENCES suppliers(id),
    cost DECIMAL(10,2) CHECK (cost >= 0),
    unit NVARCHAR(20),
    stock_quantity INT CHECK (stock_quantity >= 0),
    discount INT DEFAULT 0 CHECK (discount >= 0 AND discount <= 100),
    photo_path NVARCHAR(255)
);

CREATE TABLE orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    order_article NVARCHAR(50) UNIQUE NOT NULL,
    status_id INT REFERENCES order_statuses(id),
    pickup_point_id INT REFERENCES pickup_points(id),
    order_date DATE NOT NULL,
    delivery_date DATE NOT NULL
);

CREATE TABLE order_items (
    order_id INT REFERENCES orders(id) ON DELETE CASCADE,
    product_id INT REFERENCES products(id),
    count INT CHECK (count > 0),
    PRIMARY KEY(order_id, product_id)
);
GO

INSERT INTO roles (name) VALUES 
(N'Гость'),
(N'Клиент'),
(N'Менеджер'),
(N'Администратор');

INSERT INTO categories (name) VALUES 
(N'Кроссовки'),
(N'Кеды'),
(N'Ботинки'),
(N'Туфли');

INSERT INTO manufacturers (name) VALUES 
(N'Nike'),
(N'Adidas'),
(N'Puma'),
(N'Reebok');

INSERT INTO suppliers (name) VALUES 
(N'ООО СпортОпт'),
(N'ОбувьСервис'),
(N'ГлавОбувь');

INSERT INTO order_statuses (name) VALUES 
(N'Новый'),
(N'В обработке'),
(N'Доставлен'),
(N'Отменен');

INSERT INTO pickup_points (address) VALUES 
(N'г. Москва, ул. Пушкина, д. 10'),
(N'г. Санкт-Петербург, Невский пр., д. 45'),
(N'г. Казань, ул. Баумана, д. 21');

INSERT INTO users (login, password_hash, full_name, role_id) VALUES 
(N'admin', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'Иванов Иван Иванович (Админ)', 4),
(N'manager', N'1e8a6f3796d421714b4deebcd7f2b1899eebba98e6c4664db27c7d4eaec9dc00', N'Петров Петр Петрович (Менеджер)', 3),
(N'client', N'948fe603f6aba037ed107af56172551ecdf656a47ce4e5ba00874e402b932fc6', N'Сидоров Сидор Сидорович (Клиент)', 2);

INSERT INTO products (title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES 
(N'Nike Air Max', 1, N'Удобные беговые кроссовки', 1, 1, 12000.00, N'пар', 15, 10, NULL),
(N'Adidas Superstar', 2, N'Классические кеды', 2, 2, 9500.00, N'пар', 5, 0, NULL),
(N'Puma Suede', 2, N'Замшевые кеды', 3, 1, 8000.00, N'пар', 0, 20, NULL),
(N'Reebok Classic', 1, N'Кроссовки для повседневной носки', 4, 3, 11000.00, N'пар', 20, 5, NULL),
(N'Nike Cortez', 2, N'Винтажные кеды', 1, 2, 10500.00, N'пар', 8, 16, NULL),
(N'Adidas Terrex', 3, N'Трекинговые ботинки', 2, 3, 15000.00, N'пар', 12, 0, NULL),
(N'Puma RS-X', 1, N'Модные кроссовки', 3, 1, 13000.00, N'пар', 30, 25, NULL),
(N'Reebok Zig Kinetica', 1, N'Спортивные кроссовки с амортизацией', 4, 2, 14500.00, N'пар', 7, 0, NULL),
(N'Nike Air Force 1', 2, N'Популярные кеды из кожи', 1, 3, 12500.00, N'пар', 2, 5, NULL),
(N'Adidas Stan Smith', 2, N'Легендарные теннисные кеды', 2, 1, 8500.00, N'пар', 18, 10, NULL);

INSERT INTO orders (order_article, status_id, pickup_point_id, order_date, delivery_date) VALUES 
(N'ORD-001', 1, 1, '2026-10-01', '2026-10-05'),
(N'ORD-002', 2, 2, '2026-10-02', '2026-10-06'),
(N'ORD-003', 3, 3, '2026-10-03', '2026-10-07'),
(N'ORD-004', 4, 1, '2026-10-04', '2026-10-08'),
(N'ORD-005', 1, 2, '2026-10-05', '2026-10-09');

INSERT INTO order_items (order_id, product_id, count) VALUES 
(1, 1, 2),
(1, 2, 1),
(2, 3, 1),
(3, 4, 1),
(3, 5, 2),
(4, 6, 1),
(5, 7, 3),
(5, 8, 1);
GO
