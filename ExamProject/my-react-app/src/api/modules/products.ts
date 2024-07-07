export async function getProducts(page: number, pageSize: number) {
    const response = await fetch(`http://localhost:5000/api/products?page=${page}&pageSize=${pageSize}`);
    if (!response.ok) {
      throw new Error("Failed to fetch products");
    }
    const data = await response.json();
    return data;
  }