import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { IProduct } from '../models/IProduct.interface';
import { catchError, Observable, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiProductService {
  
  #httpClient = inject(HttpClient);
  #url = signal(environment.productApiUrl);

  #setProductList = signal<IProduct[] | null>(null);
  get getProductList() {
    return this.#setProductList.asReadonly();
  }
  public httpProductList$() : Observable<IProduct[]> {
    this.#setProductList.set(null);
    
    return this.#httpClient.get<IProduct[]>(`${this.#url()}/all`).pipe(
      tap((res) => this.#setProductList.set(res)),
      catchError( (error: HttpErrorResponse) => {
        return throwError(() => error);
      })
    );
  }

  public httpAddProduct$(product: IProduct) : Observable<IProduct> {
    return this.#httpClient.post<IProduct>(`${this.#url()}/create`, {
      Name: product.name,
      Code: product.code,
      Price: product.price,
      InventoryBalance: product.inventoryBalance
    }).pipe(
      catchError( (error: HttpErrorResponse) => {
        return throwError(() => error);
      })
    );
  }
}
