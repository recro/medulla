import {CustomObjectsApi, KubeConfig, Watch} from "@kubernetes/client-node";

import publish from "./dapr-publish";
const homedir = require('os').homedir();
const fs = require('fs')


require("regenerator-runtime");
const k8s = require('@kubernetes/client-node');


const kc: KubeConfig = new k8s.KubeConfig();




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
};


/**
 * recompile will get Pages Kubernetes resource and set the state in DAPR redis, then send a publish event to DAPR.
 */
const recompile = async () => {
    console.log("recompiling pages")
    const pages = await getAllResources("pages");
    await publish("pages_compile_pub_sub", "compile", pages);
};

/**
 * waits for some time
 */
const wait = async (time : number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, time)
    })
}

/**
 * loads env
 */
async function loadEnv() {
    console.log("loading env")
    if (process.env.LOAD_FROM_CLUSTER) {
        while (true) {
            if (fs.existsSync( "/var/run/secrets/kubernetes.io/serviceaccount/ca.crt" )) {
                console.log("/var/run/secrets/kubernetes.io/serviceaccount/ca.crt found")
                break;
            } else {
                console.log("/var/run/secrets/kubernetes.io/serviceaccount/ca.crt not found")
                await wait(1000);
                continue;
            }
        }
        console.log("loading from cluster")
        kc.loadFromCluster();
    } else {
        const homedir = require('os').homedir();
        while (true) {
            if (fs.existsSync( `${homedir}/.kube/config` )) {
                console.log("kube config file found")
                break;
            } else {
                console.log("kube config file not found")
                await wait(1000);
                continue;
            }
        }
        console.log("loading from default")
        kc.loadFromDefault();
    }
    await (async () => {
        watchUrlRecompile('/apis/medulla.recro.com/v1/pages');
    })();
}

(async () => {
    await loadEnv()
})()
