import { useEffect } from 'react'
import { Routes, Route, useLocation } from 'react-router-dom'
import LoginPage from './layout/Login/LoginPage'
import NotFound from './page/Error/NotFound'
import Forbidden from './page/Error/Forbidden'
import ServerError from './page/Error/ServerError'
import Header from './layout/Header/Header'
import Footer from './layout/Footer/Footer'
import UserRoutes from './routes/User Route'
import AdminRoutes from './routes/Admin Route'
import ProtectedRoute from './components/ProtectedRoute'
import { ToastProvider } from './components/ToastProvider'
import './App.css'

// Import Preline
import 'preline/preline'
import type { IStaticMethods } from 'preline/preline'
declare global {
  interface Window {
    HSStaticMethods: IStaticMethods
  }
}

function App() {
  const location = useLocation()

  // Initialize Preline on route change
  useEffect(() => {
    if (window.HSStaticMethods) {
      window.HSStaticMethods.autoInit()
    }
  }, [location.pathname])
  return (
    <ToastProvider>
    <Routes>
      {/* Admin Routes - No Header/Footer - Protected by role */}
      <Route
        path="/admin/*"
        element={
          <ProtectedRoute allowedRoles={['Admin', 'Facility_Manager']}>
            <AdminRoutes />
          </ProtectedRoute>
        }
      />
      
      {/* Login Page - Has Header/Footer */}
      <Route
        path="/login"
        element={
          <div className="flex min-h-screen flex-col bg-gray-50">
            <Header />
            <main className="flex-1">
              <LoginPage />
            </main>
            <Footer />
          </div>
        }
      />
      
      {/* Error Pages - Has Header/Footer */}
      <Route
        path="/404"
        element={
          <div className="flex min-h-screen flex-col bg-gray-50">
            <Header />
            <main className="flex-1">
              <NotFound />
            </main>
            <Footer />
          </div>
        }
      />
      <Route
        path="/403"
        element={
          <div className="flex min-h-screen flex-col bg-gray-50">
            <Header />
            <main className="flex-1">
              <Forbidden />
            </main>
            <Footer />
          </div>
        }
      />
      <Route
        path="/500"
        element={
          <div className="flex min-h-screen flex-col bg-gray-50">
            <Header />
            <main className="flex-1">
              <ServerError />
            </main>
            <Footer />
          </div>
        }
      />
      
      {/* User Routes - Has Header/Footer */}
      <Route
        path="/*"
        element={
          <div className="flex min-h-screen flex-col bg-gray-50">
            <Header />
            <main className="flex-1">
              <UserRoutes />
            </main>
            <Footer />
          </div>
        }
      />
      
      {/* Fallback - Has Header/Footer */}
      <Route
        path="*"
        element={
          <div className="flex min-h-screen flex-col bg-gray-50">
            <Header />
            <main className="flex-1">
              <NotFound />
            </main>
            <Footer />
          </div>
        }
      />
    </Routes>
    </ToastProvider>
  )
}

export default App
