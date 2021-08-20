import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly BaseURI = 'https://localhost:44357/api';//TODO: add this value in config file 

  constructor(private http: HttpClient, private router: Router, private productService: ProductService) { }


  login(LoginModel) {
    var host = this.BaseURI + '/fw/Users/Login';
    return this.http.post(host, LoginModel)
      .subscribe(
        (res: any) => {
          if (res.result == null)
            Swal.fire('Oops...', "User name or password incorrect", 'error')
          else {
            this.productService.Init();
            localStorage.setItem('Name', res.result.userName);
            this.router.navigate(['/ProductList'])
              .then(() => {
                //window.location.reload();
              });
          }
        },
        err => {
          if (err.status == 400) {
            Swal.fire('Oops...', "User name or password incorrect", 'error')
          }
          else {
            Swal.fire('Oops...', 'Something went wrong, please contact the administrator', 'error')
            console.log(err);
          }
        }
      );
  }
  register(user) {
    var host = this.BaseURI + '/fw/Users/Register';
    return this.http.post(host, user)
      .subscribe(
        (res: any) => {
            Swal.fire('Oops...', 'User added', 'success');
        },
        err => {
          Swal.fire('Oops...', err.error.message, 'error')
            console.log(err);
        }
      );
  }}
