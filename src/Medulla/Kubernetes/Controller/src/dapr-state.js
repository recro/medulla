"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.setState = exports.getState = void 0;
const axios_1 = __importDefault(require("axios"));
const setState = (key, value) => __awaiter(void 0, void 0, void 0, function* () {
    yield axios_1.default.post("http://localhost:3500/v1.0/state/statestore", [{ "key": key, "value": value }]);
});
exports.setState = setState;
const getState = (key) => __awaiter(void 0, void 0, void 0, function* () {
    const response = yield axios_1.default.get(`http://localhost:3500/v1.0/state/statestore/${key}`);
    return response.data;
});
exports.getState = getState;
