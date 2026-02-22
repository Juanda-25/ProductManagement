# 🛒 ProductManagement - API REST + Dashboard + Tienda

Sistema web completo de gestión de productos desarrollado con ASP.NET Core Web API (.NET 10), dashboard administrativo y tienda para clientes.

![Dashboard](https://img.shields.io/badge/Dashboard-Admin-blue) ![API](https://img.shields.io/badge/API-REST-green) ![JWT](https://img.shields.io/badge/Auth-JWT-orange) ![MySQL](https://img.shields.io/badge/DB-MySQL-blue)

## 🚀 Características

- ✅ API REST con autenticación JWT
- ✅ CRUD completo de productos con fotos y categorías
- ✅ Dashboard administrativo con gráficas y estadísticas
- ✅ Gestión de órdenes con cambio de estado
- ✅ Tienda para clientes con carrito de compras
- ✅ Pago con QR Nequi + notificación por WhatsApp
- ✅ Modo oscuro en la tienda
- ✅ Página de contacto
- ✅ Notificación por email al confirmar pedido
- ✅ Documentación con Scalar UI

## 🛠️ Tecnologías

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core + Pomelo (MySQL)
- JWT Authentication
- MailKit (emails)
- Bootstrap 5
- Scalar UI (documentación API)
- MySQL

## ⚙️ Instalación

### Requisitos previos
- .NET 10 SDK
- MySQL Server
- Git

### Pasos

**1. Clona el repositorio:**
```bash
git clone https://github.com/TU_USUARIO/ProductManagement.git
cd ProductManagement
```

**2. Configura `appsettings.json`:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProductManagementDB;User=root;Password=TU_PASSWORD;"
  },
  "Jwt": {
    "Key": "TU_CLAVE_SECRETA_MUY_LARGA",
    "Issuer": "ProductManagement",
    "Audience": "ProductManagementUsers"
  },
  "Email": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "TU_CORREO@gmail.com",
    "Password": "TU_APP_PASSWORD",
    "From": "TU_CORREO@gmail.com"
  }
}
```

**3. Ejecuta las migraciones:**
```bash
dotnet ef database update
```

**4. Corre el proyecto:**
```bash
dotnet run
```

## 🌐 URLs

| Página | URL |
|--------|-----|
| Tienda | http://localhost:5200/shop.html |
| Panel Admin | http://localhost:5200 |
| API Docs | http://localhost:5200/scalar/v1 |
| Contacto | http://localhost:5200/contacto.html |

## 📡 Endpoints API

| Método | Endpoint | Descripción | Auth |
|--------|----------|-------------|------|
| POST | /api/Auth/register | Registrar usuario | No |
| POST | /api/Auth/login | Iniciar sesión | No |
| GET | /api/Products/public | Listar productos (público) | No |
| GET | /api/Products | Listar productos | JWT |
| POST | /api/Products | Crear producto | JWT |
| GET | /api/Products/{id} | Obtener producto | JWT |
| PUT | /api/Products/{id} | Editar producto | JWT |
| DELETE | /api/Products/{id} | Eliminar producto | JWT |
| POST | /api/Products/{id}/imagen | Subir imagen | JWT |
| POST | /api/Orders | Crear orden | No |
| GET | /api/Orders | Listar órdenes | JWT |
| PUT | /api/Orders/{id}/status | Actualizar estado | JWT |

## 👤 Autor

**Juan Guevara**  
[GitHub](https://github.com/Juanda-25)