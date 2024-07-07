import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Typography, Button, CircularProgress, Grid, Card, CardContent, CardMedia, TextField } from '@mui/material';
import { sendEmail } from '../api/api';
import { getBasket, clearBasket} from '../api/basket';
import { createOrder } from '../api/order';
import { BasketDto, BasketItemDto} from '../interfaces/basket';
import { OrderDto } from '../interfaces/order';
import { getEmailContentAsString } from '../EmailContent';

const OrderPage: React.FC = () => {
  const [basket, setBasket] = useState<BasketDto | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [userData, setUserData] = useState({
    name: '',
    city: '',
    address: '',
    phoneNumber: '',
    email: ''
  });
  const navigate = useNavigate();

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

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setUserData({
      ...userData,
      [name]: value
    });
  };

  const handlePlaceOrder = async () => {
    if (basket) {
      try {
        const orderDto: OrderDto = {
          id: 0,
          userId: basket.userId,
          fullName: userData.name,
          city: userData.city,
          address: userData.address,
          phoneNumber: userData.phoneNumber,
          email: userData.email,
          orderDate: new Date(),
          orderDetails: basket.items.map(item => ({
            id: 0,
            orderId: 0,
            userId: basket.userId,
            catalogItemId: item.catalogItemId,
            amount: item.quantity
          }))
        };

        const orderId = await createOrder(orderDto);
        alert(`Order created with ID: ${orderId}`);
        
        // Відправка емейлу з введеними даними
        const emailContent = getEmailContentAsString({
          recipientName: userData.name,
          phoneNumber: userData.phoneNumber,
          address: userData.address,
          basket: basket
        });

        const emailRequest = {
          recipientName: userData.name,
          recipientEmail: userData.email,
          subject: 'Order Confirmation',
          message: emailContent // Передача готового HTML-рядка
        };
        await sendEmail(emailRequest);

        await clearBasket(); // Очистити кошик після оформлення замовлення
        navigate('/');
      } catch (error) {
        console.error('Error creating order:', error);
        alert('Error creating order');
      }
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
    <Container>
      <Typography variant="h4" gutterBottom>
        Order Summary
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12} md={8}>
          <Typography variant="h5">Enter your details</Typography>
          <form noValidate autoComplete="off">
            <TextField
              label="Name"
              name="name"
              fullWidth
              margin="normal"
              value={userData.name}
              onChange={handleInputChange}
            />
            <TextField
              label="City"
              name="city"
              fullWidth
              margin="normal"
              value={userData.city}
              onChange={handleInputChange}
            />
            <TextField
              label="Address"
              name="address"
              fullWidth
              margin="normal"
              value={userData.address}
              onChange={handleInputChange}
            />
            <TextField
              label="Phone Number"
              name="phoneNumber"
              fullWidth
              margin="normal"
              value={userData.phoneNumber}
              onChange={handleInputChange}
            />
            <TextField
              label="Email"
              name="email"
              fullWidth
              margin="normal"
              value={userData.email}
              onChange={handleInputChange}
            />
          </form>
        </Grid>
        <Grid item xs={12} md={4}>
          <Typography variant="h5">Items in your basket</Typography>
          <Grid container spacing={3}>
            {basket.items.map((item: BasketItemDto) => (
              <Grid item xs={12} key={item.catalogItemId}>
                <Card>
                  <CardMedia
                    component="img"
                    height="60"
                    image={item.pictureUrl}
                    alt={item.productName}
                  />
                  <CardContent>
                    <Typography variant="body2" color="text.secondary">
                      {item.productName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      Quantity: {item.quantity}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Grid>
      </Grid>
      <Button
        variant="contained"
        color="primary"
        onClick={handlePlaceOrder}
        style={{ marginTop: '2em' }}
      >
        Place Order
      </Button>
    </Container>
  );
};

export default OrderPage;