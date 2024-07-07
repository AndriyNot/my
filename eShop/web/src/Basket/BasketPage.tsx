import React, { useState, useEffect } from 'react';
import { Container, Grid, Card, CardContent, CardMedia, Typography, CircularProgress, Button, AppBar, Toolbar, IconButton, Badge } from '@mui/material';
import { ShoppingCart } from '@mui/icons-material';
import { getBasket, removeItemFromBasket, getBasketId } from '../api/basket';
import { BasketDto, BasketItemDto } from '../interfaces/basket';
import { Link } from 'react-router-dom';

const BasketPage: React.FC = () => {
  const [basket, setBasket] = useState<BasketDto | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchBasket = async () => {
      try {
        const basketData = await getBasket();
        setBasket(basketData);
      } catch (error) {
        console.error('Error fetching basket:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchBasket();
  }, []);

  const handleRemoveItem = async (catalogItemId: number) => {
    try {
      const basketId = getBasketId();
      if (basketId) {
        await removeItemFromBasket(basketId, catalogItemId);
        const updatedBasket = await getBasket();
        setBasket(updatedBasket);
        alert('Item removed from basket');
      } else {
        console.error('BasketId is null or undefined.');
      }
    } catch (error) {
      console.error('Error removing item from basket:', error);
      alert('Error removing item from basket');
    }
  };

  if (loading) {
    return <CircularProgress />;
  }

  if (!basket || basket.items.length === 0) {
    return (
      <Container>
        <Typography variant="h6">Your basket is empty</Typography>
      </Container>
    );
  }

  return (
    <div>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            My Catalog
          </Typography>
          <Button component={Link} to="/" color="inherit">
            Home
          </Button>
          <IconButton component={Link} to="/basket" color="inherit">
            <Badge color="secondary">
              <ShoppingCart />
            </Badge>
          </IconButton>
          <Button component={Link} to="/order" color="inherit">
            Place Order
          </Button>
        </Toolbar>
      </AppBar>
      <Container style={{ marginTop: '2em' }}>
        <Typography variant="h4" gutterBottom>
          Your Basket
        </Typography>
        <Grid container spacing={3}>
          {basket.items.map((item: BasketItemDto) => (
            <Grid item xs={12} sm={6} md={4} key={item.catalogItemId}>
              <Card>
                <CardMedia
                  component="img"
                  height="140"
                  image={item.pictureUrl}
                  alt={item.productName}
                />
                <CardContent>
                  <Typography gutterBottom variant="h5" component="div">
                    {item.productName}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Price: ${item.unitPrice.toFixed(2)}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Quantity: {item.quantity}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Total: ${(item.unitPrice * item.quantity).toFixed(2)}
                  </Typography>
                  <Button
                    variant="contained"
                    color="secondary"
                    onClick={() => handleRemoveItem(item.catalogItemId)}
                    style={{ marginTop: '1em' }}
                  >
                    Remove
                  </Button>
                </CardContent>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
    </div>
  );
};

export default BasketPage;