Posts Schema

company
{
  "companyName": "string",
  "address": "string",
  "contactInfo": "string"
}

user
{
  "userId": "string",
  "username": "string",
  "password": "string",
  "role": "string"
}
product
{
  "name": "string",
  "sku": "string",
  "description": "string",
  "price": 1000000
}

inventory
{
  "ownerType": "COMPANY",
  "ownerId": 2147483647,
  "name": "string",
  "isPublic": true,
  "inventoryProducts": [
    {
      "inventoryId": 2147483647,
      "productId": 2147483647,
      "quantity": 2147483647
    }
  ]
}

inventory_product
{
  "inventoryId": 2147483647,
  "productId": 2147483647,
  "quantity": 2147483647
}

payments
{
  "userId": 0,
  "amount": 999999999.99,
  "paymentDate": "2025-05-06T05:13:13.864Z",
  "paymentMethod": "string",
  "status": "string",
  "stripePaymentIntentId": "string",
  "stripeChargeId": "string"
}

supplier
{
  "name": "string",
  "address": "string",
  "phone": "string",
  "email": "user@example.com"
}

supplier_product
{
  "supplierId": 2147483647,
  "productId": 2147483647,
  "defaultCost": 0
}
