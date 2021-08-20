export class OrderModel {
  constructor(adress, email, tel) {
    this.address = adress;
    this.email = email;
    this.telephone = tel;
  }
  id: number;
  address: string;
  email: string;
  telephone: string;
}
