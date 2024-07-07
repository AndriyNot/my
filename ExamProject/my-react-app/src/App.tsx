import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ProductListPage from './Product/components/ProductsListPage'; // Перевірте правильний шлях до вашого компонента ProductListPage

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <Routes>
          {/* Інші маршрути */}
          <Route path="/products" element={<ProductListPage />} />
          {/* Інші маршрути */}
        </Routes>
      </div>
    </Router>
  );
};

export default App;