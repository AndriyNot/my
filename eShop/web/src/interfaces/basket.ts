export interface BasketItemDto {
    catalogItemId: number;
    productName: string;
    unitPrice: number;
    quantity: number;
    pictureUrl: string;
    totalPrice?: number;
  }
  
  export interface BasketDto {
    userId: string;
    items: BasketItemDto[];
  }