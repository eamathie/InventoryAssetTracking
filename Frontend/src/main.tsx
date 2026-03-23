import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route} from 'react-router'
import App from './App.tsx'
import './index.css'
import RegisterLoginUser from './components/auth/RegisterLoginUser.tsx'
import Categories from './components/categories/Categories.tsx'
import Assets from './components/assets/Assets.tsx'
import Navbar from './components/layout/Navbar.tsx'
import { AuthProvider } from './tools/AuthProvider.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AuthProvider>
      <BrowserRouter>
      <Navbar />
        <Routes>
          <Route path ="/" element={<App />} >
            <Route path = "auth" element={<RegisterLoginUser />} />
          </Route>
          <Route path="/assets" element={<Assets />} />
          <Route path="/categories" element={<Categories />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  </StrictMode>,
)
