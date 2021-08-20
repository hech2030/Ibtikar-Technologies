export class UserModel {
  constructor(userName, email, password) {
    this.userName = userName,
      this.email = email,
      this.passwordHash = password
  }
  id: number;
  userName: string;
  email: string;
  passwordHash: string;
  phoneNumber: string;
}
