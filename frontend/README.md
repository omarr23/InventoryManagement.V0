<<<<<<< HEAD
# Welcome to your Lovable project

## Project info

**URL**: https://lovable.dev/projects/52f7169e-4763-4b72-9208-cb4c54b67842

## How can I edit this code?

There are several ways of editing your application.

**Use Lovable**

Simply visit the [Lovable Project](https://lovable.dev/projects/52f7169e-4763-4b72-9208-cb4c54b67842) and start prompting.

Changes made via Lovable will be committed automatically to this repo.

**Use your preferred IDE**

If you want to work locally using your own IDE, you can clone this repo and push changes. Pushed changes will also be reflected in Lovable.

The only requirement is having Node.js & npm installed - [install with nvm](https://github.com/nvm-sh/nvm#installing-and-updating)

Follow these steps:

```sh
# Step 1: Clone the repository using the project's Git URL.
git clone <YOUR_GIT_URL>

# Step 2: Navigate to the project directory.
cd <YOUR_PROJECT_NAME>

# Step 3: Install the necessary dependencies.
npm i

# Step 4: Start the development server with auto-reloading and an instant preview.
npm run dev
```

**Edit a file directly in GitHub**

- Navigate to the desired file(s).
- Click the "Edit" button (pencil icon) at the top right of the file view.
- Make your changes and commit the changes.

**Use GitHub Codespaces**

- Navigate to the main page of your repository.
- Click on the "Code" button (green button) near the top right.
- Select the "Codespaces" tab.
- Click on "New codespace" to launch a new Codespace environment.
- Edit files directly within the Codespace and commit and push your changes once you're done.

## What technologies are used for this project?

This project is built with:

- Vite
- TypeScript
- React
- shadcn-ui
- Tailwind CSS

## How can I deploy this project?

Simply open [Lovable](https://lovable.dev/projects/52f7169e-4763-4b72-9208-cb4c54b67842) and click on Share -> Publish.

## Can I connect a custom domain to my Lovable project?

Yes, you can!

To connect a domain, navigate to Project > Settings > Domains and click Connect Domain.

Read more here: [Setting up a custom domain](https://docs.lovable.dev/tips-tricks/custom-domain#step-by-step-guide)
=======

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

InventoryManagement.V0/
├── 📂 InventoryManagement.API/       → API Controllers and Startup Configuration
├── 📂 InventoryManagement.BLL/       → Business Logic Layer (Services, Interfaces)
├── 📂 InventoryManagement.DAL/       → Data Access Layer (Repositories, EF Core)
├── 📂 Models/                        → Entity Models and Data Transfer Objects (DTOs)
├── 📂 Services/                      → Custom Services (e.g., Authentication, JWT)
└── 📄 README.md                      → Project Documentation


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

>>>>>>> 40374df87e1d0aa3e1e2ea3d9bfcb561b714ed45
