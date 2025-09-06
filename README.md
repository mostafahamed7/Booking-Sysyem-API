# 🏨 Hotel Booking System

A hotel booking system built with **ASP.NET Core** and **Entity Framework Core**, designed to manage hotels, rooms, and reservations.  
This project provides a RESTful API for hotel and room management, as well as booking handling.

---

## 🚀 Features
- Hotel management (create, update, delete hotels).
- Room management (types, availability, pricing).
- Room booking and reservation system.
- Authentication & Authorization with JWT.
- Clean Architecture (Services, Repository, DTOs).
- Database access via Entity Framework Core.
- API documentation with Swagger.

---

## 🛠️ Tech Stack
- **Backend:** ASP.NET Core 9
- **Database:** SQL Server (EF Core)
- **Authentication:** JWT-based authentication
- **Architecture:** Layered / Clean Architecture
- **Tools:** Swagger, GitHub Desktop, Visual Studio

---

## 📂 Project Structure
E-Commerce │── Core # Entities, Interfaces, Specifications │── Infrastructure # Data Access, EF Core Configurations, Redis │── API # Controllers, DTOs, Endpoints │── Application # Services, Business Logic

---
## ⚡ Getting Started

### Prerequisites
Make sure you have installed:
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server
- Visual Studio 2022 / VS Code

### Setup & Run
1. Clone the repository:
   ```bash
   git clone https://github.com/mostafahamed7/HotelBookingSystem.git

---

## 📖 API Endpoints (Examples)

### 🔐 Authentication
- `POST /api/auth/register` → Register a new user  
- `POST /api/auth/login` → Authenticate & get JWT token  

---

### 🏨 Hotels
- `GET /api/hotels` → Get all hotels  
- `POST /api/hotels` → Add a new hotel  
- `PUT /api/hotels/{id}` → Update a hotel  
- `DELETE /api/hotels/{id}` → Delete a hotel  

---

### 🛏️ Rooms
- `GET /api/rooms/{hotelId}` → Get rooms for a hotel  
- `POST /api/rooms` → Create a new room  
- `DELETE /api/rooms/{id}` → Delete a room 

---

### 📅 Bookings
- `POST /api/bookings` → Create a new booking  
- `GET /api/bookings/{userId}` → Get bookings for a user  

---

## 👨‍💻 Author
Developed by **Mostafa Hamed**  

- 📧 Email: [your.email@example.com](mostfahmed770@gmail.com)  
- 💼 LinkedIn: [https://www.linkedin.com/in/yourprofile](https://www.linkedin.com/in/mostafa-hamed-82532423b/)  
- 🐙 GitHub: [https://github.com/mostafahamed7](https://github.com/mostafahamed7) 