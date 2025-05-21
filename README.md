To make the README more **clear, professional, and complete**, we can improve the formatting, integrate the React frontend setup more cleanly, and create a distinction between **backend** (ASP.NET Core API) and **frontend** (React app with Vite). Here's an improved version with that in mind:

---

```markdown
# 📦 Inventory Management API & Frontend

A full-stack inventory management solution with:

- ⚙️ **ASP.NET Core Web API** for backend operations (users, products, inventory, suppliers, payments)
- ⚛️ **React (Vite + TypeScript)** frontend with TailwindCSS and shadcn-ui
- 🔐 Role-based access control using ASP.NET Identity and JWT

---

## 🚀 Features

- User registration, login, and JWT authentication
- Role-based authorization (Admin, Manager, User)
- Product and inventory tracking
- Supplier and payment management
- Clean architecture (API, BLL, DAL)
- Modern frontend with instant preview and hot reloading

---

## 🧑‍💼 Roles & Permissions

| Role    | Permissions                                |
|---------|---------------------------------------------|
| Admin   | Full access to all features                 |
| Manager | Manage inventory and users                  |
| User    | View/update inventory only                  |

---

## 📁 Project Structure

```

InventoryManagement.V0/
├── InventoryManagement.API/        # ASP.NET Core API (Controllers, Startup)
├── InventoryManagement.BLL/        # Business Logic Layer
├── InventoryManagement.DAL/        # Data Access Layer (EF Core Repos)
├── Models/                         # Entity Models & DTOs
├── Services/                       # JWT Auth, Custom Services
├── frontend/                       # React (Vite + Tailwind + shadcn-ui)
└── README.md                       # Project Documentation

````

---

## 🔐 Authentication

- JWT-based authentication
- Roles assigned during user registration
- Unauthorized access returns friendly error message

> `"You are not logged in to use this endpoint"`

---

## 🛠️ Backend Setup (ASP.NET Core)

1. Clone the repository  
2. Update the database connection in `appsettings.json`  
3. Run database migrations:
   ```bash
   dotnet ef database update
````

4. Start the backend server:

   ```bash
   dotnet run
   ```

---

## 💻 Frontend Setup (React + Vite)

Make sure you have Node.js and npm installed. Use [nvm](https://github.com/nvm-sh/nvm#installing-and-updating) if needed.

1. Navigate to the `frontend/` directory:

   ```bash
   cd frontend
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Run the development server:

   ```bash
   npm run dev
   ```

> 💡 The frontend will auto-reload and preview changes instantly.

---

## 📌 Sample API Schemas

### 🏢 Company

```json
{
  "companyName": "string",
  "address": "string",
  "contactInfo": "string"
}
```

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

## 📬 Key API Endpoints

* `POST /api/auth/register`
* `POST /api/auth/login`
* `GET /api/inventory`
* `POST /api/products`
* *(more documented in Swagger UI)*

---

## 📄 License

MIT License — free to use and modify.

---

## 🙋‍♀️ Contributions

Pull requests are welcome!
Open an issue first to discuss major changes or feature proposals.

```

---

### ✅ Summary of Improvements

- Clearly separated **backend** and **frontend** setup
- Improved readability and structure with consistent section formatting
- Grouped related items (e.g., project structure, schemas, endpoints)
- Added links, tips, and usage hints

Let me know if you'd like this broken up into separate files (`README.md`, `frontend/README.md`, etc.) or need it converted to HTML or GitHub Pages format.
```
