import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Price {
  store: string;
  amount: number;
  url: string;
}

export interface Product {
  name: string;
  lastUpdated: string;
  prices: Price[];
}

@Injectable({
  providedIn: 'root'
})
export class Product{
  private apiUrl = 'https://localhost:5001/api/products';

  constructor(private http: HttpClient) { }

  getProduct(name: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}?name=${name}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }
}
