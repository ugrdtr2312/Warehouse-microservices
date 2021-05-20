import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { BrandDetailsComponent } from './components/brand-details/brand-details.component';
import { CategoryDetailsComponent } from './components/category-details/category-details.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { SupplierDetailsComponent } from './components/supplier-details/supplier-details.component';
import { LoginComponent } from './components/user/login/login.component';
import { UserComponent } from './components/user/user.component';
import { AuthGuard } from './helpers/auth/auth.guard';
import { ForbiddenComponent } from './helpers/forbidden/forbidden.component';

const routes: Routes = [
    {path:'',redirectTo:'/user/login',pathMatch:'full'},
    {
        path: 'user', component: UserComponent,
        children: [
            { path: 'login', component: LoginComponent }
        ]
    },
    {path:'forbidden',component: ForbiddenComponent},
    {path:'adminpanel',component: AdminPanelComponent, canActivate:[AuthGuard], data:{permittedRoles:['Admin']}}, 
    { path:'categories', component: CategoryDetailsComponent, canActivate:[AuthGuard]},
    { path:'products', component: ProductDetailsComponent, canActivate:[AuthGuard]},
    { path:'brands', component: BrandDetailsComponent, canActivate:[AuthGuard]},
    { path:'suppliers', component: SupplierDetailsComponent, canActivate:[AuthGuard]}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
export const routingComponents = [CategoryDetailsComponent, ProductDetailsComponent, BrandDetailsComponent, SupplierDetailsComponent]