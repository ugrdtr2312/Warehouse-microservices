import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { UserService } from './shared/user/user.service';
import { ProductDetailFormComponent } from './components/product-details/product-detail-form/product-detail-form.component';
import { CategoryDetailFormComponent } from './components/category-details/category-detail-form/category-detail-form.component';
import { SupplierDetailFormComponent } from './components/supplier-details/supplier-detail-form/supplier-detail-form.component';
import { BrandDetailFormComponent } from './components/brand-details/brand-detail-form/brand-detail-form.component';
import { SortDirective } from './helpers/util/sort.directive';
import { UserComponent } from './components/user/user.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { LoginComponent } from './components/user/login/login.component';
import { NavigationComponent } from './helpers/navigation/navigation.component';
import { AuthInterceptor } from './helpers/auth/auth.interceptor';
@NgModule({
  declarations: [
    AppComponent,
    ProductDetailFormComponent,
    CategoryDetailFormComponent,
    SupplierDetailFormComponent,
    BrandDetailFormComponent,
    SortDirective,
    routingComponents,
    UserComponent,
    AdminPanelComponent,
    LoginComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    Ng2SearchPipeModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
