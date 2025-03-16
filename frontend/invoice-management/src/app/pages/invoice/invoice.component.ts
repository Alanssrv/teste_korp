import { Component, inject, OnInit } from '@angular/core';
import { ApiInvoiceService } from '../../services/api-invoice.service';
import { MatIconModule } from '@angular/material/icon';
import { DetailInvoiceComponent } from '../../components/detail-invoice/detail-invoice.component';
import { MatDialog } from '@angular/material/dialog';
import { IInvoiceProduct } from '../../models/IInvoiceProduct.interface';
import { IInvoice } from '../../models/IInvoice.interface';
import { concatMap } from 'rxjs';

@Component({
  selector: 'app-invoice',
  imports: [MatIconModule],
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.scss'
})
export default class InvoiceComponent implements OnInit {
  #invoiceApi = inject(ApiInvoiceService);
  #dialog = inject(MatDialog);

  public getInvoiceList = this.#invoiceApi.getInvoiceList;
  public getProductList = this.#invoiceApi.getProductList;
  
  ngOnInit(): void {
    this.#invoiceApi.httpInvoiceList$().subscribe();
    this.#invoiceApi.httpProductList$().subscribe();
  }
  
  openDetailModal(invoice: IInvoice) {
    this.#invoiceApi.httpInvoiceProductListByInvoiceId$(invoice.id).subscribe(
      (response) => {
        const dialogRef = this.#dialog.open(DetailInvoiceComponent, {
          width: '500px',
          data: response
        });
      }
    );
  }
}
