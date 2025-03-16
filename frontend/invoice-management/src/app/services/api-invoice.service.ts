import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { IProduct } from '../models/IProduct.interface';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { IInvoiceProduct } from '../models/IInvoiceProduct.interface';
import { IInvoice } from '../models/IInvoice.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiInvoiceService {

  #httpClient = inject(HttpClient);
  #productUrl = signal(environment.productApiUrl);
  #invoiceUrl = signal(environment.invoiceApiUrl);

  #setProductList = signal<IProduct[] | null>(null);
  get getProductList() {
    return this.#setProductList.asReadonly();
  }
  public httpProductList$() : Observable<IProduct[]> {
    this.#setProductList.set(null);
    
    return this.#httpClient.get<IProduct[]>(`${this.#productUrl()}/all`).pipe(
      tap((res) => this.#setProductList.set(res)),
      catchError( (error: HttpErrorResponse) => {
        return throwError(() => error);
      })
    );
  }

  #setInvoiceList = signal<IInvoice[] | null>(null);
  get getInvoiceList() {
    return this.#setInvoiceList.asReadonly();
  }
  public httpInvoiceList$() : Observable<IInvoice[]> {
    this.#setInvoiceList.set(null);
    
    return this.#httpClient.get<IInvoice[]>(`${this.#invoiceUrl()}/allInvoices`).pipe(
      tap((res) => this.#setInvoiceList.set(res)),
      catchError( (error: HttpErrorResponse) => {
        return throwError(() => error);
      })
    );
  }

  #setInvoiceProductsList = signal<IInvoiceProduct[] | null>(null);
  get getInvoiceProductsList() {
    return this.#setInvoiceProductsList.asReadonly();
  }
  public httpInvoiceProductListByInvoiceId$(id: number) : Observable<IInvoiceProduct[]> {
    this.#setInvoiceProductsList.set(null);
    
    return this.#httpClient.get<IInvoiceProduct[]>(`${this.#invoiceUrl()}/invoiceProductsByInvoiceId/${id}`).pipe(
      tap((res) => this.#setInvoiceProductsList.set(res)),
      catchError( (error: HttpErrorResponse) => {
        return throwError(() => error);
      })
    );
  }
}
