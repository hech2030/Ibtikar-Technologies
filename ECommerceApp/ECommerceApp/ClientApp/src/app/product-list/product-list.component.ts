import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../Common/Models/ProductModel';
import { ProductService } from '../Services/product.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  constructor(private productService: ProductService, private router: Router) { }

  AddedToCart = 0;
  selectedProducts = JSON.parse(localStorage.getItem('cart'));

  products: ProductModel[];
  userName = '';

  ngOnInit() {
    this.userName = localStorage.getItem('Name');
    this.productService.FindProduct()
      .subscribe((data: any) => {
        console.log(data.result);
        this.products = data.result.map(function (item) {
          return new ProductModel(item);
        })
      });

    if (localStorage.getItem('cart') == undefined) {
      localStorage.setItem('cart', '[]');
      this.selectedProducts = [];
    }
    else {
      var existingCart = JSON.parse(localStorage.getItem('cart'));
      this.AddedToCart = existingCart.length;
      this.selectedProducts = existingCart;
    }
  }

  AddToCart(Product) {
    //if the item is not added already
    if (this.selectedProducts.find(a => a.id == Product.id) == undefined) {
      this.AddedToCart = this.AddedToCart + 1;
      this.selectedProducts.push(Product);
      var existingLocalStorage = JSON.parse(localStorage.getItem('cart'));
      existingLocalStorage.push(Product);
      localStorage.setItem('cart', JSON.stringify(existingLocalStorage));
    } else {
      Swal.fire('Oops...', "You have already added this prouduct to cart", 'error')
    }
  }
  DeleteProduct(id) {
    this.AddedToCart = this.AddedToCart - 1;
    this.selectedProducts = this.selectedProducts.filter(a => a.id != id);
    var existingLocalStorage = JSON.parse(localStorage.getItem('cart'));
    existingLocalStorage = existingLocalStorage.filter(a => a.id != id);
    localStorage.setItem('cart', JSON.stringify(existingLocalStorage));
  }

  RedirectToOrder() {
    this.router.navigate(['/ProductsOrder']);
  }


  //TODO : method to be implemented in View model base class (called only from one place)
  disconnect() {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
