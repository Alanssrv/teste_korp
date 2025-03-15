import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ApiProductService } from '../../services/api-product.service';
import {MatTableModule} from '@angular/material/table';
import { CurrencyPipe } from '@angular/common';
import { ProductModalComponent } from '../../components/product-modal/product-modal.component';
import { IProduct } from '../../models/IProduct.interface';
import { concatMap } from 'rxjs';

@Component({
  selector: 'app-products',
  imports: [MatTableModule, CurrencyPipe],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  
  #productApi = inject(ApiProductService);
  #dialog = inject(MatDialog);
  
  public getProductList = this.#productApi.getProductList;

  ngOnInit(): void {
    console.log(123);
    this.#productApi.httpProductList$().subscribe();
  }

  openModal() {
    const dialogRef = this.#dialog.open(ProductModalComponent, {
      width: '500px',
      data: null
    });

    dialogRef.afterClosed().subscribe((result: IProduct | undefined) => {
      if (result) {
        console.log('Produto Adicionado:', result);
        this.#productApi.httpAddProduct$(result).pipe(
          concatMap(() => this.#productApi.httpProductList$())
        ).subscribe();
      }
    });
  }
}
