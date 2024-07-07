import React from 'react';
import { Grid, Card, CardContent, CardMedia, Typography, CardActions, Button } from '@mui/material';
import { CatalogItem } from '../../interfaces/items';

interface ItemsListProps {
  items: CatalogItem[];
  onAddToBasket: (item: CatalogItem) => void;
}

const ItemsList: React.FC<ItemsListProps> = ({ items, onAddToBasket }) => {
  return (
    <>
      {items.map((item) => (
        <Grid item xs={12} sm={6} md={4} key={item.id}>
          <Card>
            <CardMedia
              component="img"
              height="140"
              image={item.pictureUrl}
              alt={item.name}
            />
            <CardContent>
              <Typography gutterBottom variant="h5" component="div">
                {item.name}
              </Typography>
              <Typography variant="body2" color="text.secondary">
                Price: ${item.price.toFixed(2)}
              </Typography>
            </CardContent>
            <CardActions>
              <Button
                size="small"
                color="primary"
                onClick={() => onAddToBasket(item)}
              >
                Add to Basket
              </Button>
            </CardActions>
          </Card>
        </Grid>
      ))}
    </>
  );
};

export default ItemsList;