# 🌍 BAS Mart | Authentic Afro-Caribbean E-Commerce

**BAS Mart** is a specialized web platform designed to bridge the gap between authentic African and Caribbean suppliers and local consumers.  
Built with **ASP.NET Core MVC**, it delivers a fast, reliable, and user-friendly shopping experience tailored for food products, spices, and cultural goods.

---

## 🎯 Project Goal

The main goal of this project is to design and implement a complete full-stack e-commerce system using **ASP.NET Core MVC** that demonstrates:

- real-world online shopping workflows,
- product and order management,
- secure user authentication,
- and scalable web application architecture.

This project was developed as a **final-year academic project** and also serves as a portfolio application to demonstrate practical skills in modern web development and software engineering.

---

## 📸 Project Gallery

### 🏠 Home & Discovery

The landing page presents high-quality banners and featured categories to help users easily discover authentic Afro-Caribbean products.

![Home Page](screenshots/home_page.png)

---

### 🛒 The Shop (Product Grid)

A clean and responsive product grid with a **custom icon-based “Add to Cart” system**.

> AJAX is used to allow users to add products to the cart without reloading the page.

![Shop Page](screenshots/shop_page.png)

---

### 🛍️ Shopping Cart & Checkout

A simple and intuitive interface for managing product quantities and reviewing totals before checkout.

![Cart Page](screenshots/cart_page.png)

---
### 🛠️ Admin Dashboard

The admin panel allows authorized staff to manage products and monitor customer orders through a dedicated management interface.

![Admin Dashboard](screenshots/admin_dashboard.png)

---
## 🛠️ Key Technical Features

### 1. Seamless Shopping Experience (AJAX & Fetch API)

Instead of full page reloads, BAS Mart uses background asynchronous requests.

When a user clicks the **Add to Cart** icon:

- a `POST` request is sent to the `CartController`,
- the server updates the cart stored in the session,
- and the cart badge in the navigation bar is updated instantly using JavaScript.

---

### 2. Multi-Language Foundation

The project architecture is prepared for a multilingual interface and future expansion to:

- 🇬🇧 English  
- 🇷🇺 Russian  
- 🇫🇷 French  

---

### 3. Dynamic Product and User Management

- **Entity Framework Core** manages product data such as:
  - weight and quantity,
  - pricing and discounts,
  - image paths and categories.
- **ASP.NET Core Identity** provides:
  - secure authentication,
  - user session handling,
  - administrator roles and access control.

---

## 💻 Tech Stack

| Layer | Technology |
|------|----------|
| Backend | .NET 8.0 (C#) |
| Frontend | Razor Views, JavaScript (ES6+), Bootstrap 5 |
| Database | Microsoft SQL Server |
| ORM | Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| Icons | Bootstrap Icons |

---

## 🚀 Installation & Setup

### 1. Clone the repository

```bash
git clone https://github.com/TekNanya/Emart.git
````markdown
# 🌍 BAS Mart | Authentic Afro-Caribbean E-Commerce

**BAS Mart** is a specialized web platform designed to bridge the gap between authentic African and Caribbean suppliers and local consumers.  
Built with **ASP.NET Core MVC**, it delivers a fast, reliable, and user-friendly shopping experience tailored for food products, spices, and cultural goods.

---

## 🎯 Project Goal

The main goal of this project is to design and implement a complete full-stack e-commerce system using **ASP.NET Core MVC** that demonstrates:

- real-world online shopping workflows,
- product and order management,
- secure user authentication,
- and scalable web application architecture.

This project was developed as a **final-year academic project** and also serves as a portfolio application to demonstrate practical skills in modern web development and software engineering.

---

## 📸 Project Gallery

### 🏠 Home & Discovery

The landing page presents high-quality banners and featured categories to help users easily discover authentic Afro-Caribbean products.

![Home Page](screenshots/home_page.png)

---

### 🛒 The Shop (Product Grid)

A clean and responsive product grid with a **custom icon-based “Add to Cart” system**.

> AJAX is used to allow users to add products to the cart without reloading the page.

![Shop Page](screenshots/shop_page.png)

---

### 🛍️ Shopping Cart & Checkout

A simple and intuitive interface for managing product quantities and reviewing totals before checkout.

![Cart Page](screenshots/cart_page.png)

---

## 🛠️ Key Technical Features

### 1. Seamless Shopping Experience (AJAX & Fetch API)

Instead of full page reloads, BAS Mart uses background asynchronous requests.

When a user clicks the **Add to Cart** icon:

- a `POST` request is sent to the `CartController`,
- the server updates the cart stored in the session,
- and the cart badge in the navigation bar is updated instantly using JavaScript.

---

### 2. Multi-Language Foundation

The project architecture is prepared for a multilingual interface and future expansion to:

- 🇬🇧 English  
- 🇷🇺 Russian  
- 🇫🇷 French  

---

### 3. Dynamic Product and User Management

- **Entity Framework Core** manages product data such as:
  - weight and quantity,
  - pricing and discounts,
  - image paths and categories.
- **ASP.NET Core Identity** provides:
  - secure authentication,
  - user session handling,
  - administrator roles and access control.

---
### 4. Admin Dashboard & Order Tracking

The system includes an administrator panel that allows authorized users to manage the store and monitor customer activity.

Admin users can:

- view and manage all customer orders,
- track order status (new, processed, completed),
- manage products (create, edit, update and remove items),
- monitor basic store activity through the dashboard interface.

Customers are also able to:

- view their order history,
- track the status of their orders after checkout.

Access to the dashboard is protected using role-based authorization provided by ASP.NET Core Identity.
---

## 💻 Tech Stack

| Layer | Technology |
|------|----------|
| Backend | .NET 8.0 (C#) |
| Frontend | Razor Views, JavaScript (ES6+), Bootstrap 5 |
| Database | Microsoft SQL Server |
| ORM | Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| Icons | Bootstrap Icons |

---

## 🚀 Installation & Setup

### 1. Clone the repository


git clone https://github.com/TekNanya/Emart.git
````

---

### 2. Configure the database

Open the `appsettings.json` file and update the connection string:

```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BASMartDB;Trusted_Connection=True;"
```

---

### 3. Apply database migrations

Open **Package Manager Console** in Visual Studio and run:

```
Update-Database
```

---

### 4. Run the application

Press **F5** in Visual Studio to start the project.

---

## 📁 Core Directory Structure

* `/Controllers` – application logic (Cart, Shop, Accounts, etc.)
* `/Models` – data models (Products, Orders, CartItems, Users)
* `/Views` – Razor views and shared layouts
* `/wwwroot` – static files (CSS, JavaScript, images)

---

## 🧪 Testing

At this stage, the project focuses on functional and manual testing of:

* cart operations,
* authentication flows,
* product browsing and filtering,
* and checkout behaviour.

Automated tests can be added in future iterations.

---

## 📦 Future Improvements

* Online payment gateway integration
* Full multilingual user interface
* Product reviews and ratings


---

## 👩‍💻 Author

**Developed by TekNanya**

*Quality authentic goods, delivered with modern technology.*
