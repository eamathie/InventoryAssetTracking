import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route} from 'react-router'
import App from './App.tsx'
import './index.css'
import RegisterLoginUser from './components/auth/RegisterLoginUser.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path ="/" element={<App />} >
          <Route path = "auth" element={<RegisterLoginUser />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
