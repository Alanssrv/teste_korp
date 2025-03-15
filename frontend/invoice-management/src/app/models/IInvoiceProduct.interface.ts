import { IInvoice } from "./IInvoice.interface";
import { IProduct } from "./IProduct.interface";

export interface IInvoiceProduct {
    product: IProduct,
    invoice: IInvoice,
    id: number,
    creationDate: Date
}