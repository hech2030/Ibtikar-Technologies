"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModel = void 0;
var UserModel = /** @class */ (function () {
    function UserModel(userName, email, password) {
        this.userName = userName,
            this.email = email,
            this.passwordHash = password;
    }
    return UserModel;
}());
exports.UserModel = UserModel;
//# sourceMappingURL=UserModel.js.map