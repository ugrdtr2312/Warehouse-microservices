import { BrandDetail } from '../brand/brand-detail.model';
import { Injectable } from '@angular/core';
import { ProductDetail } from './product-detail.model';
import { HttpClient } from "@angular/common/http"
import { CategoryDetail } from '../category/category-detail.model';
import { SupplierDetail } from '../supplier/supplier-detail.model';

@Injectable({
  providedIn: 'root'
})
export class ProductDetailService {

  constructor(private http:HttpClient) { }

  readonly baseURL = "http://192.168.39.162:80/products/api/products"
  formData:ProductDetail = new ProductDetail();
  list:ProductDetail[];
  categoryList:CategoryDetail[];
  brandList:BrandDetail[];
  supplierList:SupplierDetail[];

  postProductDetails(){
    if (this.formData.brandId == "null")
    this.formData.brandId = null;
    if (this.formData.categoryId == "null")
    this.formData.categoryId = null;
    if (this.formData.supplierId == "null")
    this.formData.supplierId = null;

    return this.http.post(this.baseURL, this.formData)
  }

  putProductDetails(){
    if (this.formData.brandId == "null")
    this.formData.brandId = null;
    if (this.formData.categoryId == "null")
    this.formData.categoryId = null;
    if (this.formData.supplierId == "null")
    this.formData.supplierId = null;

    return this.http.put(`${this.baseURL}`, this.formData)
  }

  deleteProductDetails(id:number){
    return this.http.delete(`${this.baseURL}/${id}`)
  }

  refreshList(){
    this.http.get(this.baseURL).toPromise()
    .then(res => {
      this.list = res as ProductDetail[];
      this.list.forEach(element => {
        if (element.brandName == null)
          element.brandName = "-";
      });
    });
    this.http.get("http://192.168.39.162:80/categories/api/categories").toPromise()
    .then(res => this.categoryList = res as CategoryDetail[]);
    this.http.get("http://192.168.39.162:80/brands/api/brands").toPromise()
    .then(res => this.brandList = res as BrandDetail[]);
    this.http.get("http://192.168.39.162:80/suppliers/api/suppliers").toPromise()
    .then(res => this.supplierList = res as SupplierDetail[]);
  }
}
