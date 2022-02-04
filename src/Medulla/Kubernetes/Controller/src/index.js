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
const dapr_state_1 = require("./dapr-state");
const dapr_publish_1 = __importDefault(require("./dapr-publish"));
require("regenerator-runtime");
const k8s = require('@kubernetes/client-node');
const fs = require('fs');
const axios = require('axios').default;
const kc = new k8s.KubeConfig();
kc.loadFromDefault();
const watch = new k8s.Watch(kc);
let timeout = null;
const getAllResources = (objectType) => __awaiter(void 0, void 0, void 0, function* () {
    return new Promise((resolve, reject) => {
        const customObjectsApi = kc.makeApiClient(k8s.CustomObjectsApi);
        const listFn = () => customObjectsApi.listClusterCustomObject("medulla.recro.com", "v1", objectType);
        const path = '/';
        const w = new k8s.Watch(kc);
        const cache = new k8s.ListWatch(path, w, listFn);
        setTimeout(() => {
            const list = cache.list();
            resolve(list);
        }, 2000);
    });
});
const watchUrlRecompile = (url) => {
    watch.watch(url, {
        allowWatchBookmarks: false,
    }, (type, apiObj, watchObj) => {
        // console.log(type, apiObj, watchObj);
        console.log('starting 10 second timer to recompile');
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            clearTimeout(timeout);
            console.log('recompiling');
            recompile();
        }, 10000);
    }, (err) => {
        console.log(err);
    })
        .then((req) => {
        // setTimeout(() => { req.abort(); }, 10 * 1000);
    });
};
const createDir = (dir) => {
    fs.rmSync(dir, { recursive: true });
    console.log(`${dir} is deleted!`);
    if (!fs.existsSync(dir)) {
        console.log(dir, "does not exist creating");
        fs.mkdirSync(dir, { recursive: true });
    }
};
const convertKubernetesNameToRazorFileName = (name) => {
    let _name = '';
    let isUppercase = false;
    let isDash = false;
    for (let i = 0; i < name.length; i++) {
        if (i === 0)
            isUppercase = true;
        if (name[i] === '-')
            isDash = true;
        if (isDash) {
            isUppercase = true;
            isDash = false;
            continue;
        }
        if (isUppercase) {
            _name += name[i].toUpperCase();
            isUppercase = false;
            continue;
        }
        _name += name[i];
    }
    return `${_name}.razor`;
};
const recompile = () => __awaiter(void 0, void 0, void 0, function* () {
    console.log("recompiling pages");
    const pages = yield getAllResources("pages");
    yield (0, dapr_state_1.setState)("pages", pages);
    yield (0, dapr_publish_1.default)(1);
    // console.log(await getState("pages"));
});
(() => __awaiter(void 0, void 0, void 0, function* () {
    watchUrlRecompile('/apis/medulla.recro.com/v1/pages');
    watchUrlRecompile('/apis/medulla.recro.com/v1/apps');
}))();
