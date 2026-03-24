import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route} from 'react-router'
import './index.css'
import RegisterLoginUser from './components/auth/RegisterLoginUser.tsx'
import Categories from './components/categories/Categories.tsx'
import Assets from './components/assets/Assets.tsx'
import Navbar from './components/layout/Navbar.tsx'
import { AuthProvider } from './tools/AuthProvider.tsx'
import AdminPanels from './components/admin/AdminPanels.tsx'
import Background from './components/layout/Background.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AuthProvider>
      <BrowserRouter>
        <div className="flex flex-col max-h-[100vh] h-screen">
          <Background />
          <Navbar />
          <Routes>
            <Route path="/auth" element={<RegisterLoginUser />} />
            <Route path="/admin" element={<AdminPanels />} />
            <Route path="/assets" element={<Assets />} />
            <Route path="/categories" element={<Categories />} />
          </Routes>
        </div>
      </BrowserRouter>
    </AuthProvider>
  </StrictMode>
)



