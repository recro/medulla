// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Operator;

public class MedullaPage : IKubernetesSaveable
{

    public string PageTitle = "";

    public void SaveToKubernetes()
    {
        // load kube config file
        // KubernetesApi.loadFromLocal()

        // build your crd object
        // var object = (your object)

        //KubernetesApi.SaveCustomResourceOBject(object)

    }
}
