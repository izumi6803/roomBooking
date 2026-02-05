# FPTU Multi-campus Facility Booking System

A comprehensive facility booking system for FPT University campuses, featuring real-time booking management, check-in/check-out functionality, and multi-campus support.

## Project Structure

```
Clone/
├── FPTUHCMMulti-campusFacilityBookingSystem---BE/    # Backend (.NET 8)
└── FPTUHCMMulti-campusFacilityBookingSystem---FE/    # Frontend (React + Vite)
```

## Tech Stack

### Backend
- .NET 8
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Google OAuth

### Frontend
- React 19
- TypeScript
- Vite
- TailwindCSS
- React Router

## Local Development

### Backend Setup

1. Navigate to backend directory:
```bash
cd FPTUHCMMulti-campusFacilityBookingSystem---BE
```

2. Create `.env` file (copy from `.env.example`)
3. Update database connection string
4. Run migrations and start the server:
```bash
dotnet ef database update
dotnet run --project Controller
```

The backend will run on `http://localhost:5252`

### Frontend Setup

1. Navigate to frontend directory:
```bash
cd FPTUHCMMulti-campusFacilityBookingSystem---FE
```

2. Install dependencies:
```bash
npm install
```

3. Create `.env` file (copy from `.env.example`)
4. Start development server:
```bash
npm run dev
```

The frontend will run on `http://localhost:5173`

## Deployment on Railway

### Prerequisites
- Railway account (https://railway.app)
- GitHub repository connected to Railway

### Deploy Backend

1. **Create New Project in Railway**
   - Go to Railway Dashboard
   - Click "New Project"
   - Select "Deploy from GitHub repo"
   - Choose your repository

2. **Configure Backend Service**
   - Root Directory: `FPTUHCMMulti-campusFacilityBookingSystem---BE`
   - Railway will auto-detect the Dockerfile
   - Service will use port 8080 (already configured in Dockerfile)

3. **Add Database**
   - In your project, click "New"
   - Select "Database" → "PostgreSQL"
   - Railway will automatically create a database and set `DATABASE_URL`

4. **Set Environment Variables**
   Go to your backend service → Variables tab and add:
   ```
   DATABASE_URL=(automatically set by Railway)
   JWT_SECRET=your-secure-jwt-secret-here
   JWT_EXPIRY=3600
   GOOGLE_CLIENT_ID=your-google-client-id
   GOOGLE_CLIENT_SECRET=your-google-client-secret
   ASPNETCORE_ENVIRONMENT=Production
   ASPNETCORE_URLS=http://0.0.0.0:8080
   ALLOWED_ORIGINS=https://your-frontend-url.railway.app
   ```

5. **Deploy**
   - Railway will automatically build and deploy
   - Note your backend URL (e.g., `https://your-backend-service.railway.app`)

### Deploy Frontend

1. **Create Frontend Service**
   - In the same project, click "New" → "GitHub Repo"
   - Select your repository again
   - Choose a different service name (e.g., "frontend")

2. **Configure Frontend Service**
   - Root Directory: `FPTUHCMMulti-campusFacilityBookingSystem---FE`
   - Railway will auto-detect the Dockerfile
   - Service will use port 80 (nginx)

3. **Set Environment Variables**
   Go to your frontend service → Variables tab and add:
   ```
   VITE_API_BASE_URL=https://your-backend-service.railway.app
   VITE_GOOGLE_CLIENT_ID=your-google-client-id
   ```

   **IMPORTANT**: For Vite environment variables to work in Docker:
   - Railway will inject these during build time
   - The Dockerfile is configured to handle this

4. **Deploy**
   - Railway will automatically build and deploy
   - Your frontend will be available at `https://your-frontend-service.railway.app`

### Update CORS Settings

After frontend deployment, update backend environment variables:
- Go to backend service → Variables
- Update `ALLOWED_ORIGINS` to include your frontend URL:
  ```
  ALLOWED_ORIGINS=https://your-frontend-service.railway.app
  ```

### Deployment Checklist

- [ ] Backend service deployed and running
- [ ] PostgreSQL database created and connected
- [ ] Backend environment variables set
- [ ] Frontend service deployed and running
- [ ] Frontend environment variables set
- [ ] CORS configured with frontend URL
- [ ] Database migrations applied (automatic on startup)
- [ ] Test login functionality
- [ ] Test booking functionality

## Environment Variables Reference

### Backend (.env)
```bash
DATABASE_URL=postgresql://user:password@host:port/database
JWT_SECRET=your-jwt-secret
JWT_EXPIRY=3600
GOOGLE_CLIENT_ID=your-client-id
GOOGLE_CLIENT_SECRET=your-client-secret
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:8080
ALLOWED_ORIGINS=https://your-frontend-url
```

### Frontend (.env)
```bash
VITE_API_BASE_URL=https://your-backend-url
VITE_GOOGLE_CLIENT_ID=your-client-id
```

## Troubleshooting

### Backend Issues
- **Database connection failed**: Check `DATABASE_URL` environment variable
- **CORS errors**: Ensure `ALLOWED_ORIGINS` includes your frontend URL
- **Port issues**: Railway uses port 8080 (configured in Dockerfile)

### Frontend Issues
- **API calls failing**: Verify `VITE_API_BASE_URL` is set correctly
- **Build fails**: Ensure all dependencies are in `package.json`
- **Runtime errors**: Check browser console for environment variable issues

## Features

- Multi-campus facility management
- Real-time booking system
- Check-in/check-out with image upload
- Role-based access control (Student, Manager, Admin)
- Google OAuth authentication
- Booking history and status tracking
- Feedback and rating system

## License

This project is developed for FPT University.

