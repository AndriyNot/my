import React from 'react';
import { styled } from '@mui/material/styles';
import Pagination from '@mui/material/Pagination';
import ProductCard from '../ProductsCard';
import { getProducts } from '../../api/modules/products';

const StyledContainer = styled('div')({
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  marginTop: '20px',
});

const PAGE_SIZE = 10;

const ProductListPage: React.FC = () => {
  const [products, setProducts] = React.useState<any[]>([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [currentPage, setCurrentPage] = React.useState(1);

  React.useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await getProducts(currentPage, PAGE_SIZE);
        setProducts(data.products);
        setTotalPages(Math.ceil(data.totalCount / PAGE_SIZE));
      } catch (error) {
        console.error('Failed to fetch products:', error);
      }
    };

    fetchProducts();
  }, [currentPage]);

  const handlePageChange = (event: React.ChangeEvent<unknown>, page: number) => {
    setCurrentPage(page);
  };

  return (
    <StyledContainer>
      <h1>Products</h1>
      {products.map(product => (
        <ProductCard key={product.id} product={product} />
      ))}
      <Pagination
        count={totalPages}
        page={currentPage}
        onChange={handlePageChange}
        variant="outlined"
        shape="rounded"
        style={{ marginTop: '20px' }}
      />
    </StyledContainer>
  );
};

export default ProductListPage;