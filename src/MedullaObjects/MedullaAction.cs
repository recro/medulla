// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Operator;

public class MedullaAction : IKubernetesSaveable
{
    public string Uuid = "";
    public string ActionName = "medulla-action";//input field in ui
    public string DockerImage = "mysql";// input field;
    public MedullaAction? NextActionToRun; // dropdown of available actions

    //trigger

    public void SaveToKubernetes()
    {
        // load kube config file
        // KubernetesApi.loadFromLocal()

        // build your crd object
        // var object = (your object)

        //KubernetesApi.SaveCustomResourceOBject(object)
    }

    public static MedullaAction LoadClassFromKubernetesWithUuid(string uuid)
    {
        // look in the cluster for that uuid

        // instantiate a MedullaAction object

        //return the object
        return new MedullaAction();
    }

    public static string CreateNewMedullaActionAndSerialize()
    {
        /*MedullaAction obj = new MyObject();
        obj.n1 = 1;
        obj.n2 = 24;
        obj.str = "Some String";
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, obj);
        stream.Close();  */

        // return serialized class as string
        return "";
    }




}
