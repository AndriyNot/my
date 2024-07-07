import React from 'react';
import { BasketDto, BasketItemDto } from './interfaces/basket';

interface EmailContentProps {
  recipientName: string;
  phoneNumber: string;
  address: string;
  basket: BasketDto;
}

const EmailContent: React.FC<EmailContentProps> = ({ recipientName, phoneNumber, address, basket }) => {
  return (
    <>
      <p>Name: {recipientName}</p>
      <p>Phone Number: {phoneNumber}</p>
      <p>Address: {address}</p>
      <p>Items:</p>
      <ul>
        {basket.items.map((item: BasketItemDto) => (
          <li key={item.catalogItemId}>
            {item.productName} - Quantity: {item.quantity}
          </li>
        ))}
      </ul>
    </>
  );
};

export const getEmailContentAsString = ({ recipientName, phoneNumber, address, basket }: EmailContentProps): string => {
    let content = `
      Your Order Has Been Placed
        
        Thank you, ${recipientName}! Your order has been successfully placed.
        
        Order Details:
        - Name: ${recipientName}
        - Phone Number: ${phoneNumber}
        - Address: ${address}
        
        Items in Your Order:
    `;
  
    basket.items.forEach((item: BasketItemDto, index: number) => {
      content += `
        ${index + 1}. ${item.productName} - Quantity: ${item.quantity}
      `;
    });
  
    return content;
  };

export default EmailContent;