import axios from 'axios';
import { OrderDto } from '../interfaces/order';
import { EmailRequest} from '../interfaces/email';

export async function createOrder(orderDto: OrderDto): Promise<number> {
    try {
      const response = await axios.post<number>(
        'http://www.alevelwebsite.com:5000/api/v1/Order/CreateOrder/create',
        orderDto,
        {
          headers: {
            'Content-Type': 'application/json',
            'accept': 'text/plain'
          }
        }
      );
      return response.data;
    } catch (error) {
      throw new Error('Failed to create order');
    }
  }
  
  export const sendEmail = async (emailRequest: EmailRequest): Promise<void> => {
    await axios.post('https://localhost:32768/api/Order/place-order', emailRequest);
  };
  