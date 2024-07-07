import React from 'react';
import { IconButton } from '@mui/material';
import { ShoppingCart } from '@mui/icons-material';
import { CatalogItem } from '../../interfaces/items';

interface Props {
  item: CatalogItem;
  onAddedToBasket: (item: CatalogItem) => void; 
}

const AddToBasketButton: React.FC<Props> = ({ item, onAddedToBasket }) => {
  const handleAddToBasket = async () => {
    const basketItem = {
      catalogItemId: item.id,
      productName: item.name,
      unitPrice: item.price,
      quantity: 1,
      pictureUrl: item.pictureUrl,
    };
    try {
      await onAddedToBasket(item); 
      console.log('Item added to basket:', basketItem);
      alert('Item added to basket');
    } catch (error) {
      console.error('Error adding item to basket:', error);
      alert('Error adding item to basket');
    }
  };

  return (
    <IconButton color="primary" onClick={handleAddToBasket}>
      <ShoppingCart />
    </IconButton>
  );
};

export default AddToBasketButton;