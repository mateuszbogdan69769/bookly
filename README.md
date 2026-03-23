# 📅 Booking App — Reservation System

A web application for managing reservations and guest information efficiently and securely. Built with a scalable architecture using .NET 8 (backend) and Vue 3 + TypeScript (frontend).

## 🔗 Demo

- **Frontend (User App)**: [https://bookly.mbogdan.pl](https://bookly.mbogdan.pl)
- **Swagger API**: [https://bookly-api.mbogdan.pl/swagger/index.html](https://bookly-api.mbogdan.pl/swagger/index.html)

## 🧪 Test Credentials

**App Access**

- Email: `test@test.com`
- Password: `test123`

**Swagger Access**

- Username: `API`
- Password: `API123`

---

## 🧰 Features

- 🔐 **User Authentication** — Register and login with secure password hashing (SHA256 + salt)
- 📆 **Reservation Management** — Create, update, and delete reservations with support for notes and guest linkage
- 🧑‍🤝‍🧑 **Guest Handling** — Track guest details and manage their reservation history
- 📊 **Statistics Dashboard** — View dynamic statistics such as current reservations and guest counts
- 🟢 **Reservation Statuses** — Define custom statuses with colors and order, including drag-and-drop sorting

---

## 🛠 Technologies

### Backend (.NET 8, C#)

- **Entity Framework Core** — ORM and migrations
- **CQRS + Mediator Pattern** — clean separation of logic between read and write operations
- **Ardalis Specification** — standardized and reusable database query specifications
- **Supabase** — used for user authentication and PostgreSQL database management
- **Azure App Service** — automatic deployment and hosting via GitHub CI/CD

### Frontend (Vue 3, TypeScript)

- **Vite** — blazing fast development server and build tool
- **Vuetify** — Material Design UI components
- **Pinia** — centralized state management
- **Axios** — for API communication
- **Luxon** — modern date and time handling
- **class-transformer** — converting between JSON and TypeScript classes
- **vue-draggable-plus** — drag-and-drop support for reordering statuses
- **Vue Router** — routing with guard protection for unauthorized access

---

## 🚀 Getting Started

### Backend

1. Clone the repository
2. Create a `appsettings.Development.json` based on `appsettings.json`
3. Configure PostgreSQL and Supabase credentials
4. Run the backend with .NET CLI

### Frontend

1. Install dependencies:
   ```bash
   npm install
   ```
2. Start the dev server:
   ```bash
   npm run dev
   ```
