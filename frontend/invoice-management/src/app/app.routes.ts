import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import(
            './pages/products/products.component'
        ).then()
    },
    {
        path: 'invoice',
        loadComponent: () => import(
            './pages/invoice/invoice.component'
        ).then()
    },
    {
        path: '**',
        loadComponent: () => import(
            './pages/not-found/not-found.component'
        ).then()
    }
];
