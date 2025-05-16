
---

```markdown
# 📦 Inventory Management API

An ASP.NET Core Web API project for managing users, products, inventory, suppliers, and payments in an organization. Built with role-based authorization using ASP.NET Identity.

---

## 🚀 Features

- User registration, login, and authentication with JWT
- Role-based authorization: Admin, Manager, and User
- Inventory and product tracking
- Supplier and payment management
- Clean architecture with separate layers (API, BLL, DAL)

---

## 👥 Roles & Permissions

| Role    | Description                                 |
|---------|---------------------------------------------|
| Admin   | Full access to all features                 |
| Manager | Can manage inventory and users              |
| User    | Can view/update inventory only              |

---

## 📂 Project Structure

```

InventoryManagement.V0/
├── InventoryManagement.API/        # Web API controllers
├── InventoryManagement.BLL/        # Business logic layer
├── InventoryManagement.DAL/        # Data access layer
├── Models/                         # DTOs and Entity Models
├── Services/                       # Custom services like Auth
└── README.md

````

---

## 🔒 Authentication

- JWT-based authentication
- Roles are assigned during user creation
- Middleware handles unauthorized access with a friendly message: `"You are not logged in to use this endpoint"`

---

## 📌 API Schemas

### 🏢 Company
```json
{
  "companyName": "string",
  "address": "string",
  "contactInfo": "string"
}
````

### 👤 User

```json
{
  "userId": "string",
  "username": "string",
  "password": "string",
  "role": "Admin"
}
```

### 📦 Product

```json
{
  "name": "string",
  "sku": "string",
  "description": "string",
  "price": 100.0
}
```

### 📥 Inventory

```json
{
  "ownerType": "COMPANY",
  "ownerId": 1,
  "name": "Main Warehouse",
  "isPublic": true,
  "inventoryProducts": [
    {
      "inventoryId": 1,
      "productId": 1,
      "quantity": 100
    }
  ]
}
```

### 💰 Payments

```json
{
  "userId": 1,
  "amount": 999.99,
  "paymentDate": "2025-05-06T05:13:13Z",
  "paymentMethod": "CreditCard",
  "status": "Completed",
  "stripePaymentIntentId": "pi_xxx",
  "stripeChargeId": "ch_xxx"
}
```

### 🚚 Supplier

```json
{
  "name": "string",
  "address": "string",
  "phone": "string",
  "email": "supplier@example.com"
}
```

---

## 🛠️ Setup Instructions

1. Clone the repository
2. Set up your database connection in `appsettings.json`
3. Run database migrations:

   ```bash
   dotnet ef database update
   ```
4. Build and run the project:

   ```bash
   dotnet run
   ```

---

## 📬 API Endpoints

* `POST /api/auth/register`
* `POST /api/auth/login`
* `GET /api/inventory`
* `POST /api/products`
* ...more documented via Swagger

---

## 📄 License

MIT License — free to use and modify.

---

## 🙋‍♂️ Contributions

Pull requests are welcome. Please open an issue first to discuss changes.

```

