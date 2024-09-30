import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '', redirectTo: '/calendar', pathMatch: 'full'
    },
    {
        path: 'calendar',
        loadComponent: () => import('./calendar/overview/overview.component').then((m) => m.OverviewComponent),
    },
    {
        path: 'information',
        loadComponent: () => import('./pinboard/overview/overview.component').then((m) => m.OverviewComponent),
    },
    {
        path: 'display/edit',
        loadComponent: () => import('./monitor/edit/edit.component').then((m) => m.EditComponent),
    },
    {
        path: 'display/:displayId',
        loadComponent: () => import('./monitor/display/display.component').then((m) => m.DisplayComponent),
    },
    {
        path: 'settings',
        loadComponent: () => import('./settings/overview/overview.component').then((m) => m.OverviewComponent),
    },
];
