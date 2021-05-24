import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http"
import { BrandDetail } from './brand-detail.model';

@Injectable({
  providedIn: 'root'
})
export class BrandDetailService {

  constructor(private http:HttpClient) { }

  readonly baseURL = "http://192.168.39.162:80/brands/api/brands"
  formData:BrandDetail = new BrandDetail();
  list:BrandDetail[];

  postBrandDetails(){
    return this.http.post(this.baseURL, this.formData)
  }

  putBrandDetails(){
    return this.http.put(`${this.baseURL}`, this.formData)
  }

  deleteBrandDetails(id:number){
    return this.http.delete(`${this.baseURL}/${id}`)
  }

  refreshList(){
    this.http.get(this.baseURL).toPromise()
    .then(res => this.list = res as BrandDetail[]);
  }
}
