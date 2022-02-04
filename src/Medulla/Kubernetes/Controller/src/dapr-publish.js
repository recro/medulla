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
Object.defineProperty(exports, "__esModule", { value: true });
//dependencies
const dapr_client_1 = require("dapr-client");
const daprHost = "127.0.0.1";
const publish = (orderId) => __awaiter(void 0, void 0, void 0, function* () {
    const PUBSUB_NAME = "order_pub_sub";
    const TOPIC_NAME = "orders";
    const client = new dapr_client_1.DaprClient(daprHost, process.env.DAPR_HTTP_PORT || "3500", dapr_client_1.CommunicationProtocolEnum.HTTP);
    console.log("Published data:" + orderId);
    yield client.pubsub.publish(PUBSUB_NAME, TOPIC_NAME, orderId);
});
exports.default = publish;
