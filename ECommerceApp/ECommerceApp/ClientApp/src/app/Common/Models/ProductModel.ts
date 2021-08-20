export class ProductModel {
  constructor(ProductModel) {
    this.id = ProductModel.id;
    this.name = ProductModel.name;
    this.category = ProductModel.category;
    this.image = ProductModel.image;
    this.price = ProductModel.price;
    this.quantity = 1;
    this.totalPrice = this.price * this.quantity;
  }
  id: number;
  name: string;
  category: string;
  price: number;
  quantity: number;
  image: string;
  totalPrice: number;
}
