-- =============================================
-- Tạo Database
-- =============================================
CREATE DATABASE smartphone_store_db;
GO

USE smartphone_store_db;
GO

-- =============================================
-- Bảng Categories
-- Đồng bộ với: Category.cs
-- =============================================
CREATE TABLE Categories (
    category_id   INT            PRIMARY KEY IDENTITY(1,1),
    category_name NVARCHAR(255)  NOT NULL UNIQUE,
    category_date DATETIME       DEFAULT GETDATE()
);
GO

CREATE TABLE Users (
    user_id      INT          PRIMARY KEY IDENTITY(1,1),
    first_name   NVARCHAR(255) NOT NULL,
    last_name    NVARCHAR(255) NOT NULL,
    email        VARCHAR(50)   NOT NULL UNIQUE,
    password     VARCHAR(50)   NOT NULL,
    phone_number VARCHAR(20)   NOT NULL UNIQUE,
    create_date  DATETIME      DEFAULT GETDATE()
    role INT NOT NULL DEFAULT 0;
);
GO

-- =============================================
-- Bảng Products
-- Đồng bộ với: Product.cs
-- =============================================
CREATE TABLE Products (
    product_id   INT            PRIMARY KEY IDENTITY(1,1),
    product_name NVARCHAR(50)   NOT NULL UNIQUE,
    descriptions NVARCHAR(255)  NULL,
    price        DECIMAL(10, 2) NOT NULL,
    image_url    VARCHAR(MAX)   NULL,
    quantity     INT            DEFAULT 0,
    category_id  INT            NULL,
    product_date DATETIME       DEFAULT GETDATE(),
    FOREIGN KEY (category_id) REFERENCES Categories(category_id)
);
-- =============================================
-- Bảng Carts
-- Đồng bộ với: Cart.cs
-- =============================================
CREATE TABLE Carts (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NULL UNIQUE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
GO

-- =============================================
-- Bảng CartItems
-- Đồng bộ với: CartItem.cs
-- =============================================
CREATE TABLE CartItems (
    cart_item_id INT PRIMARY KEY IDENTITY(1,1),
    cart_id      INT NULL,
    product_id   INT NULL,
    quantity     INT DEFAULT 1,
    FOREIGN KEY (cart_id)    REFERENCES Carts(cart_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);
GO

-- =============================================
-- Bảng Orders
-- Đồng bộ với: Order.cs
-- =============================================
CREATE TABLE Orders (
    order_id      INT            PRIMARY KEY IDENTITY(1,1),
    user_id       INT            NULL,
    address       NVARCHAR(255)  NOT NULL,
    phone_number  VARCHAR(20)    NOT NULL,
    order_date    DATETIME       DEFAULT GETDATE(),
    total_amount  DECIMAL(10, 2) NULL,
    orders_status NVARCHAR(50)   DEFAULT N'Chờ xác nhận',
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
GO

-- =============================================
-- Bảng OrderDetails
-- Đồng bộ với: OrderDetail.cs
-- =============================================
CREATE TABLE OrderDetails (
    order_detail_id INT            PRIMARY KEY IDENTITY(1,1),
    order_id        INT            NULL,
    product_id      INT            NULL,
    quantity        INT            NOT NULL,
    price           DECIMAL(10, 2) NOT NULL,
    total_price     DECIMAL(10, 2) NULL,
    FOREIGN KEY (order_id)   REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);
GO

-- =============================================
-- Dữ liệu mẫu - Categories
-- =============================================
INSERT INTO Categories (category_name) VALUES
(N'Apple'),
(N'Samsung'),
(N'Xiaomi'),
(N'Oppo'),
(N'Vivo');
GO
INSERT INTO Users (first_name, last_name, email, password, phone_number, role) 
VALUES 
(N'Quản trị', N'Viên', 'admin@gmail.com', '123456', '0123456789', 1),
(N'Người', N'Dùng', 'user@gmail.com', '123456', '0987654321', 0);
GO
INSERT INTO Products (product_name, descriptions, price, image_url, quantity, category_id) 
VALUES 
(N'Iphone 17 Promax', N'8GB-256GB', 38000000, 'http://cdn2.cellphones.com.vn/358x/media/catalog/product/i/p/iphone-17-pro-max.jpg', 10000, 1),
(N'Iphone 15 Promax', N'8GB-256GB', 25000000, 'https://cdn2.cellphones.com.vn/358x/media/catalog/product/i/p/iphone15-pro-max-titan-nau.jpg', 10000, 1);
(N'Iphone 16 Promax', N'8GB-256GB', 32000000, 'https://cdn2.cellphones.com.vn/358x/media/catalog/product/i/p/iphone-16-pro-max-titan-trang.png', 10000, 1);
GO
