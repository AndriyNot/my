export interface OrderDto {
    id?: number; 
    userId: string;
    fullName: string;
    city: string;
    address: string;
    phoneNumber: string;
    email: string;
    orderDate: Date;
    orderDetails: OrderDetailDto[];
  }
  
  export interface OrderDetailDto {
    id: number; 
    orderId: number; 
    userId: string;
    catalogItemId: number;
    amount: number;
  }