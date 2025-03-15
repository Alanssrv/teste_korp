import { Routes } from '@angular/router';
import InvoiceComponent from './pages/invoice/invoice.component';
import NotFoundComponent from './pages/not-found/not-found.component';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import(
            './pages/products/products.component'
        ).then(p => p.ProductsComponent)
    },
    {
        path: 'invoice',
        component: InvoiceComponent
    },
    {
        path: '**',
        component: NotFoundComponent
    }
];
