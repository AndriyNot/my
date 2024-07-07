import axios from 'axios';
import { CatalogItem, PaginatedItemsResponse} from '../interfaces/items';


export async function getByPageItems(page: number): Promise<PaginatedItemsResponse<CatalogItem>> {
    const response = await axios.post<PaginatedItemsResponse<CatalogItem>>(
      'http://localhost:5000/api/v1/catalogbff/items',
      {
        pageSize: 6,
        pageIndex: page - 1,
        filters: {}
      }
    );
    return response.data;
  }
  
  export const getItemsByType = async (typeId: number) => {
    try {
      console.log('Запит на отримання айтемів за типом з typeId:', typeId); // Додаємо логування запиту
      const response = await axios.post(`http://localhost:5000/api/v1/catalogbff/getitemsbytype?typeId=${typeId}`);
      console.log('Відповідь сервера:', response.data); // Додаємо логування відповіді
      return response.data;
    } catch (error) {
      console.error('Помилка отримання айтемів за типом:', error);
      throw error; // Можна обробити помилку тут або прокинути її далі
    }
  };