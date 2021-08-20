import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SearchCountryField, CountryISO } from 'ngx-intl-tel-input';
import { ProductService } from '../Services/product.service';
import { OrderModel } from '../Common/Models/OrderModel';
import { CommandModel } from '../Common/Models/CommandModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products-order',
  templateUrl: './products-order.component.html',
  styleUrls: ['./products-order.component.css']
})
export class ProductsOrderComponent implements OnInit {
  separateDialCode = true;
  SearchCountryField = SearchCountryField;
  CountryISO = CountryISO;
  preferredCountries: CountryISO[] = [CountryISO.Egypt];
  phoneForm = new FormGroup({
    phone: new FormControl(undefined, [Validators.required])
  });

  userName = '';

  OrderModel = {
    address: '',
    email: '',
    telephone: ''
  }


  selectedProducts = [];
  order: OrderModel;
  commands = [];

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit() {
    this.userName = localStorage.getItem('Name');
    this.selectedProducts = JSON.parse(localStorage.getItem('cart'));
  }
  addQuantity(Product) {
    var index = this.selectedProducts.findIndex(a => a.id == Product.id);
    if (index > -1) {
      this.selectedProducts[index].quantity++;
      this.selectedProducts[index].totalPrice = this.selectedProducts[index].quantity * this.selectedProducts[index].price;
    }
  }

  SubstQuantity(Product) {
    var index = this.selectedProducts.findIndex(a => a.id == Product.id);
    if (index > -1) {
      if (this.canReduce(Product.quantity)) {
        this.selectedProducts[index].quantity--;
        this.selectedProducts[index].totalPrice = this.selectedProducts[index].quantity * this.selectedProducts[index].price;
      }
    }
  }

  CalculateData(product, event) {
    if (event.target.value > 0) {
      var index = this.selectedProducts.findIndex(a => a.id == product.id);
      if (index > -1) {
        this.selectedProducts[index].quantity = event.target.value;
        this.selectedProducts[index].totalPrice = this.selectedProducts[index].quantity * this.selectedProducts[index].price;
      }
    } else {
      Swal.fire('Oops...', "Quantity cannot go lower than 1", 'error');
    }
  }

  canReduce(quantity) {
    if (quantity - 1 > 0)
      return true;
    else {
      Swal.fire('Oops...', "Quantity cannot go lower than 1", 'error');
      return false;
    }
  }

  Submitorder(Order) {
    if (Order.address.length == 0)
      this.ErrorDisplay('Address cannot be empty');
    else if (!this.validateEmail(Order.email))
      this.ErrorDisplay('Email format is not valid');
    else if (Order.telephone ==  null)
      this.ErrorDisplay('Phone number canot be empty');
    else {
      for (var i = 0; i < this.selectedProducts.length; i++) {
        this.commands.push(new CommandModel(this.selectedProducts[i].id, this.selectedProducts[i].quantity));
      }
      Order.telephone = Order.telephone.dialCode + Order.telephone.number
      this.productService.submitOrder(Order, this.commands).subscribe((data: any) => {
        if (data != undefined && data.result != undefined && data.result) {
          Swal.fire('Sucess', "Order submitted", 'success');
          this.selectedProducts = [];
          localStorage.setItem('cart', '[]');
        }
      });
    }
  }

  goBack() {
    this.router.navigate(['ProductList']);
  }

  //TODO : method to be implemented in View model base class (called only from one place)
   //#region "commonMethods"
  disconnect() {
    localStorage.clear();
    this.router.navigate(['/']);
  }

  validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\u0600-\u06FF\-0-9]+\.)+[a-zA-Z\u0600-\u06FF]{2,}))$/;
    return re.test(email);
  }

  ErrorDisplay(Message) {
    Swal.fire('Oops...', Message, 'error')
  }
    //#endregion "commonMethods"
}
