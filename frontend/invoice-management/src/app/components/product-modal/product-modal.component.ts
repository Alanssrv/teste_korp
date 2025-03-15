import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'app-product-modal',
  imports: [ReactiveFormsModule],
  templateUrl: './product-modal.component.html',
  styleUrl: './product-modal.component.scss',
})
export class ProductModalComponent {

  public productForm: FormGroup;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<ProductModalComponent>) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      code: [
        '',
        [Validators.required,Validators.pattern(/^\d{20}$/)]
      ],
      price: [
        '',
        [Validators.required, Validators.min(0.01)]
      ],
      inventoryBalance: [
        '',
        [Validators.required, Validators.min(1)]
      ]
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      this.dialogRef.close(this.productForm.value);
    }
  }
}
