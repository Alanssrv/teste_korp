import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IInvoiceProduct } from '../../models/IInvoiceProduct.interface';
import { CurrencyPipe } from '@angular/common';
import { GroupedProduct } from '../../models/GroupedProduct.interface';

@Component({
  selector: 'app-detail-invoice',
  imports: [CurrencyPipe],
  templateUrl: './detail-invoice.component.html',
  styleUrl: './detail-invoice.component.scss'
})
export class DetailInvoiceComponent {

  public groupedProducts: GroupedProduct[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public invoiceProductList: IInvoiceProduct[]) {
    var grouped = invoiceProductList.reduce((acc: { [key: string]: any }, curr) => {
      const code = curr.product.code;
    
      if (!acc[code]) {
        acc[code] = {
          id: curr.invoice.id,
          code: code,
          name: curr.product.name,
          totalQuantity: 1,
          price: curr.product.price,
          totalPrice: curr.product.price,
        };
      } else {
      
        acc[code].totalQuantity += 1;
        acc[code].totalPrice += curr.product.price;
      }
    
      return acc;
    }, {});

    this.groupedProducts = Object.values(grouped) as GroupedProduct[];
  }
}
