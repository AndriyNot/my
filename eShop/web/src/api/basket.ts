import axios from 'axios';
import { BasketItemDto, BasketDto } from '../interfaces/basket';

const baseUrl = 'http://www.alevelwebsite.com:5003/api/v1/BasketBff';

export const getBasket = async (): Promise<BasketDto> => {
    let basketId = getBasketId();
    if (!basketId) {
      basketId = generateBasketId();
      setBasketId(basketId);
    }
  
    try {
      const response = await axios.post<BasketDto>(`${baseUrl}/GetBasket`, { basketId }, {
        headers: {
          'Content-Type': 'application/json',
          'accept': 'application/json',
        }
      });
      console.log('Basket data:', response.data);
      return response.data;
    } catch (error) {
      console.error('Error fetching basket:', error);
      throw error;
    }
  };
  
  export const addItemToBasket = async (item: BasketItemDto) => {
    let basketId = getBasketId();
    if (!basketId) {
      basketId = generateBasketId();
      setBasketId(basketId);
    }
  
    try {
      const response = await axios.post(`${baseUrl}/AddItemsToBasket`, { basketId, items: [item] }, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      console.log('Item added to basket:', response.data);
      return response.data;
    } catch (error) {
      console.error('Error adding item to basket:', error);
      throw error;
    }
  };
  
  export const removeItemFromBasket = async (basketId: string, catalogItemId: number) => {
    try {
      const response = await axios.post(`${baseUrl}/RemoveItem`, { basketId, catalogItemId }, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      console.log('Item removed from basket:', response.data);
      return response.data;
    } catch (error) {
      console.error('Error removing item from basket:', error);
      throw error;
    }
  };
  
  export const clearBasket = async (): Promise<void> => {
    await axios.post('http://www.alevelwebsite.com:5000/api/v1/Basket/ClearBasket');
  };

  // Додаємо функції для роботи з локальним сховищем
export const getBasketId = (): string | null => {
    return localStorage.getItem('basketId');
  };
  
  export const setBasketId = (basketId: string) => {
    localStorage.setItem('basketId', basketId);
  };
  
  export const generateBasketId = (): string => {
    return '_' + Math.random().toString(36).substr(2, 9);
  };