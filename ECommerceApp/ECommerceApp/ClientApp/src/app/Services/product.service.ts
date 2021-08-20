import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { ProductModel } from '../Common/Models/ProductModel';
import { throwError } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class ProductService {
  readonly BaseURI = 'https://localhost:44357/api';//TODO: add this value in config file 

  constructor(private http: HttpClient, private router: Router) { }

  Init() {
    var host = this.BaseURI + '/Product/Init';
    return this.http.post(host, {})
      .subscribe();
  }

  FindProduct() {
    var host = this.BaseURI + '/Product/Find';
    return this.http.post(host, {}).pipe(
      map((data: ProductModel[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  submitOrder(order, commands) {
    var host = this.BaseURI + '/Product/SubmitOrder';
    return this.http.post(host, {
      order: order,
      commands: commands
    });
  }
}
