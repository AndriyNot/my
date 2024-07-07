export interface CatalogItem {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  catalogType: CatalogType;
  catalogBrand: CatalogBrand;
  availableStock: number;
}
  
  export interface CatalogType {
    id: number;
    type: string;
  }

  export interface CatalogBrand {
    id: number;
    brand: string;
  }
  
  export interface PaginatedItemsResponse<T> {
    data: T[];
    pageIndex: number;
    pageSize: number;
    count: number;
  }
  
  export {};

