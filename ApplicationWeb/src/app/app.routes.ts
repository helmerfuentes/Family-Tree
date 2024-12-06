import { Routes } from '@angular/router';
import { NotFoundComponent } from './Pages/notFound/notFound.component';

export const routes: Routes = [
   
    {
        path: 'relationship',
        loadComponent: () => import('./Pages/relationship/relationship.component'),
    },
    {
        path: 'designer',
        loadComponent: () => import('./Pages/designer/designer.component'),
    },
    {
        path: '',
        redirectTo: '/relationship',
        pathMatch: 'full'
    },
    {
        path: '**',  // Ruta comodín para redirigir a la página no encontrada
        component: NotFoundComponent
    }
];
