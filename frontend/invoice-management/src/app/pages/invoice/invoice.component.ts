import { Component, inject, OnInit } from '@angular/core';
import { ApiInvoiceService } from '../../services/api-invoice.service';
import { MatIconModule } from '@angular/material/icon';
import { DetailInvoiceComponent } from '../../components/detail-invoice/detail-invoice.component';
import { MatDialog } from '@angular/material/dialog';
import { IInvoiceProduct } from '../../models/IInvoiceProduct.interface';
import { IInvoice } from '../../models/IInvoice.interface';
import { concatMap } from 'rxjs';
import { AddInvoiceModalComponent } from '../../components/add-invoice-modal/add-invoice-modal.component';
import { IProduct } from '../../models/IProduct.interface';

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

  openNewInvoiceModal() {
    const dialogRef = this.#dialog.open(AddInvoiceModalComponent, {
      data: this.getProductList()?.map(product => ({...product, selectedAmount: 0}))
    });

    dialogRef.afterClosed().subscribe((result: IProduct[] | undefined) => {
      result = result!.filter(product => product.selectedAmount > 0);
      if (result!.length > 0){
        var codes: string[] = result!.flatMap(item => Array(item.selectedAmount).fill(item.code));
        this.#invoiceApi.httAddInvoice$(codes).pipe(
          concatMap(() => this.#invoiceApi.httpInvoiceList$())
        ).subscribe();
      }
    });
  }
}
