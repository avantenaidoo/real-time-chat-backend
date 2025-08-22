# Real-Time Chat Backend

ASP.NET Core backend for a real-time chat app with SignalR, REST APIs, and SQLite (local) or PostgreSQL (production).

## Setup
1. Install .NET 8 SDK.
2. Set environment variables: `JWT_SECRET`, `ConnectionStrings__DefaultConnection` (SQLite or PostgreSQL).
3. Run: `dotnet restore`, `dotnet ef database update`, `dotnet run`.

## APIs
- POST /Account/Register: Register user.
- POST /Account/Login: Login and get JWT.
- GET /api/Chat/rooms: List rooms.
- POST /api/Chat/rooms: Create room.
- GET /api/Chat/messages/{roomId}: Get messages.

## Deployment
Hosted on Render: https://chat-app.onrender.com