import { Component, Inject } from '@angular/core';
import { IProduct } from '../../models/IProduct.interface';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-add-invoice-modal',
  imports: [CurrencyPipe],
  templateUrl: './add-invoice-modal.component.html',
  styleUrl: './add-invoice-modal.component.scss'
})
export class AddInvoiceModalComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public productList: IProduct[], private dialogRef: MatDialogRef<AddInvoiceModalComponent>) { }

  decrease(item: IProduct): void {
    if (item.selectedAmount > 0) {
      item.selectedAmount--;
    }
  }
  
  increase(item: IProduct): void {
    if (item.selectedAmount < item.inventoryBalance) {
      item.selectedAmount++;
    }
  }

  createInvoice() {
    this.dialogRef.close(this.productList);
  }
}
