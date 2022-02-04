import {CustomObjectsApi, KubeConfig, Watch} from "@kubernetes/client-node";

import { getState, setState } from "./dapr-state";
import publish from "./dapr-publish";

require("regenerator-runtime");
const k8s = require('@kubernetes/client-node');


const kc: KubeConfig = new k8s.KubeConfig();
kc.loadFromCluster();

const watch: Watch = new k8s.Watch(kc);

let timeout :NodeJS.Timeout|any = null;

/**
 * getAllResources will get all resources in the Kubernetes cluster with the plural name.
 * @param objectType  objectType is the plural kubernetes resource type defined by the CRD (pages)
 */
const getAllResources = async (objectType:string): Promise<any> => {
    return new Promise((resolve) => {
        const customObjectsApi :CustomObjectsApi = kc.makeApiClient(k8s.CustomObjectsApi);
        const listFn = () => customObjectsApi.listClusterCustomObject("medulla.recro.com", "v1", objectType)
        const path = '/';
        const w = new k8s.Watch(kc);
        const cache = new k8s.ListWatch(path, w, listFn);

        setTimeout(() => {
            const list = cache.list();
            resolve(list);
        }, 2000);
    })
}

/**
 * watchUrlRecompile will watch a url for event changes and recompile.
 * @param url  url is the watch url for which when an event attached to that url is received then recompile
 */
const watchUrlRecompile = (url :string) => {
    watch.watch(url,
        {
            allowWatchBookmarks: false,
        },
        (type :any, apiObj :any, watchObj :any) => {
            console.log(type, apiObj, watchObj);
            console.log('starting 10 second timer to recompile');
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                clearTimeout(timeout);
                console.log('recompiling');
                recompile();
            }, 10000);
        },
        (err :Error) => {
            console.log(err);
        })
        .then((req :any) => {
            console.log(req);
        });
};

/**
 * convertKubernetesNameToRazorFileName will convert a kubernetes object name to a C# File name with .razor extension.
 * @param name is a kubernetes name formatted string like my-custom-object which will be converted to MyCustomObject.razor
 */
const convertKubernetesNameToRazorFileName = (name :string) => {
    let _name = '';
    let isUppercase = false;
    let isDash = false;
    for (let i = 0; i < name.length; i++) {
        if (i === 0) isUppercase = true;
        if (name[i] === '-') isDash = true;

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

/**
 * recompile will get Pages Kubernetes resource and set the state in DAPR redis, then send a publish event to DAPR.
 */
const recompile = async () => {
    console.log("recompiling pages")
    const pages = await getAllResources("pages");
    await setState("pages", pages);
    await publish("pages_compile_pub_sub", "compile", "compiled");

    const pagesInState = await getState("pages");
    if (pagesInState !== pages) {
        throw new Error("pages in state does not match pages from resources");
    }
};

(async () => {
    watchUrlRecompile('/apis/medulla.recro.com/v1/pages');
    watchUrlRecompile('/apis/medulla.recro.com/v1/apps');
})();


