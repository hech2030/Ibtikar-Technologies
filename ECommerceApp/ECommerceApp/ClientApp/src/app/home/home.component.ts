import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { UserService } from '../Services/user.service';
import Swal from 'sweetalert2';
import { UserModel } from '../Common/Models/UserModel';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private userService: UserService) {

  }

  LoginModel = {
    email: '',
    password: ''
  }

  RegisterModel = {
    email: '',
    userName: '',
    password: '',
    passwordConfirm: ''
  }


  ngOnInit() {
  }

  toggleSignUp() {
    $('#logreg-forms .form-signin').toggle(); // display:block or none
    $('#logreg-forms .form-signup').toggle(); // display:block or none
  }
  Login(LoginModel) {
    this.userService.login(LoginModel);
  }
  Register(RegisterModel) {
    if (!this.validateEmail(RegisterModel.email))
      this.ErrorDisplay('Email format is not valid');
    else if (RegisterModel.userName.indexOf(' ') != -1 || RegisterModel.userName == '')
      this.ErrorDisplay('User name format is not correct or empty !');
    else if (!this.validatePassword(RegisterModel.password))
      this.ErrorDisplay('Password should contain t least 8 characters contains upper and lowercase letters + at least 1 number + at least 1 special character');
    else if (RegisterModel.password != RegisterModel.passwordConfirm)
      this.ErrorDisplay('Passwords don\'t match');
    else {
      let user = new UserModel(RegisterModel.userName, RegisterModel.email, RegisterModel.password);
      this.userService.register(user);
    }
  }


  validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\u0600-\u06FF\-0-9]+\.)+[a-zA-Z\u0600-\u06FF]{2,}))$/;
    return re.test(email);
  }

  validatePassword(password) {
    const re = new RegExp("^(?=.*?[A-Z\u0600-\u06FF])(?=.*?[a-z\u0600-\u06FF])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
    return re.test(password);
  }

  ErrorDisplay(Message) {
    Swal.fire('Oops...', Message, 'error')
  }
}
