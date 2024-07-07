import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ItemsPage from './Items/ItemsPage';
import Callback from "./auth/callback";
import TypeItemsPage from './type/TypeItemsPage';
import BasketPage from './Basket/BasketPage';
import OrderPage from './order/OrderPage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
      <Route path="/callback" element={<Callback />} />
        <Route path="/" element={<ItemsPage />} />
        <Route path="/items/:typeId" element={<TypeItemsPage />} />
        <Route path="/basket" element={<BasketPage />} />
        <Route path="/order" element={<OrderPage />} />
      </Routes>
    </Router>
  );
};

export default App;