import React, { useState, useEffect } from 'react';
import { AppBar, Toolbar, Typography, Container, CircularProgress, Pagination, Grid, IconButton, Badge, Button } from '@mui/material';
import { ShoppingCart } from '@mui/icons-material';
import { addItemToBasket } from '../api/basket';
import { getByPageItems, getItemsByType} from '../api/item';
import { CatalogItem } from '../interfaces/items';
import ItemsList from './components/ItemsList';
import TypesMenu from '../type/components/TypesMenu';
import { Link } from 'react-router-dom';
import LoginButton from '../auth/LoginButton'; // Імпортуємо новий компонент

const ItemsPage: React.FC = () => {
  const [items, setItems] = useState<CatalogItem[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [page, setPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  const handleAddToBasket = async (item: CatalogItem) => {
    const basketItem = {
      catalogItemId: item.id,
      productName: item.name,
      unitPrice: item.price,
      quantity: 1,
      pictureUrl: item.pictureUrl,
    };
    try {
      await addItemToBasket(basketItem);
      console.log('Item added to basket:', basketItem);
      alert('Item added to basket');
    } catch (error) {
      console.error('Error adding item to basket:', error);
      alert('Error adding item to basket');
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const itemsResponse = await getByPageItems(page);
        setItems(itemsResponse.data);
        setTotalPages(Math.ceil(itemsResponse.count / 6));
      } catch (error) {
        console.error('Error loading items:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [page]);

  const handlePageChange = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  const handleTypeSelect = async (typeId: number) => {
    setLoading(true);
    try {
      const itemsResponse = await getItemsByType(typeId);
      setItems(itemsResponse);
      setTotalPages(1);
    } catch (error) {
      console.error('Error loading items by type:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            My Catalog
          </Typography>
          <TypesMenu onTypeSelect={handleTypeSelect} />
          <Button component={Link} to="/" color="inherit">
            Home
          </Button>
          <IconButton component={Link} to="/basket" color="inherit">
            <Badge color="secondary">
              <ShoppingCart />
            </Badge>
          </IconButton>
          <LoginButton /> {/* Додаємо компонент кнопки аутентифікації */}
        </Toolbar>
      </AppBar>
      <Container style={{ marginTop: '2em' }}>
        {loading ? (
          <CircularProgress />
        ) : (
          <Grid container spacing={3}>
            <ItemsList items={items} onAddToBasket={handleAddToBasket} />
          </Grid>
        )}
        <Pagination
          count={totalPages}
          page={page}
          onChange={handlePageChange}
          style={{ marginTop: '2em' }}
        />
      </Container>
    </div>
  );
};

export default ItemsPage;