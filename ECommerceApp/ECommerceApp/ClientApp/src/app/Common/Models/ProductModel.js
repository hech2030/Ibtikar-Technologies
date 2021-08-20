"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProductModel = void 0;
var ProductModel = /** @class */ (function () {
    function ProductModel(ProductModel) {
        this.id = ProductModel.id;
        this.name = ProductModel.name;
        this.category = ProductModel.category;
        this.image = ProductModel.image;
        this.price = ProductModel.price;
        this.quantity = 1;
        this.totalPrice = this.price * this.quantity;
    }
    return ProductModel;
}());
exports.ProductModel = ProductModel;
//# sourceMappingURL=ProductModel.js.map