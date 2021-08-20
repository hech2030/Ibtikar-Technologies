export class CommandModel {
  constructor(productId, quantity) {
    this.productId = productId;
    this.quantity = quantity;
  }
  id: number;
  productId: string;
  quantity: string;
}
