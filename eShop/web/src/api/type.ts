import axios from 'axios';
import { CatalogType } from '../interfaces/items';

export async function getTypes(): Promise<CatalogType[]> {
    const response = await axios.post<CatalogType[]>('http://localhost:5000/api/v1/catalogbff/gettypes');
    return response.data;
  }