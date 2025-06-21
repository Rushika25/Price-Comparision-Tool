import { Component } from '@angular/core';
import { Product } from '../../services/product';


@Component({
  selector: 'app-product',
  templateUrl: './product.html'
})
export class ProductComponent {
  productName = '';
  product: Product | null = null;

  constructor(private productService: Product) { }

  fetchProduct() {
    this.productService.getProduct(this.productName).subscribe({
      next: (data:any) => this.product = data,
      error: () => alert('Product not found!')
    });
  }

  addSampleProduct() {
    const newProduct: Product = {
      name: 'Pixel 9',
      lastUpdated: new Date().toISOString(),
      prices: [
        { store: 'Amazon', amount: 899.99, url: 'https://amazon.com/pixel9' },
        { store: 'Flipkart', amount: 879.49, url: 'https://flipkart.com/pixel9' }
      ]
    };

    this.productService.addProduct(newProduct).subscribe({
      next: () => alert('Product added!'),
      error: () => alert('Error adding product')
    });
  }
}

export { Product };
