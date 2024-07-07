import React, { useState, useEffect } from 'react';
import { Grid, Container, CircularProgress } from '@mui/material';
import { useParams } from 'react-router-dom';
import { CatalogItem } from '../interfaces/items';
import { getItemsByType } from '../api/item';
import ItemsList from '../Items/components/ItemsList'; // Імпортуємо тільки сам компонент

interface TypeItemsPageProps {
  // Опціональні пропси, якщо потрібно
}

const TypeItemsPage: React.FC<TypeItemsPageProps> = () => {
  const [items, setItems] = useState<CatalogItem[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const { typeId } = useParams<{ typeId?: string }>(); // typeId може бути рядком або undefined

  useEffect(() => {
    const fetchItemsByType = async () => {
      try {
        if (typeId) {
          const itemsResponse = await getItemsByType(parseInt(typeId, 10));
          setItems(itemsResponse.data);
        }
      } catch (error) {
        console.error('Error fetching items by type:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchItemsByType();
  }, [typeId]);

  const handleAddToBasket = (item: CatalogItem) => {
    // Додайте вашу логіку для додавання товару в кошик тут
    console.log('Adding item to basket:', item);
  };

  return (
    <Container>
      {loading ? (
        <CircularProgress />
      ) : (
        <Grid container spacing={3}>
          {/* Передаємо масив елементів та обробник додавання до кошика */}
          <ItemsList items={items} onAddToBasket={handleAddToBasket} />
        </Grid>
      )}
    </Container>
  );
};

export default TypeItemsPage;