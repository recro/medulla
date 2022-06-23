// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using k8s;
using k8s.Models;
using KubeOps.Operator.Controller;
using KubeOps.Operator.Controller.Results;
using KubeOps.Operator.Entities.Extensions;
using KubeOps.Operator.Rbac;
using  Medulla.Kubernetes.Operator.Entities;

namespace Medulla.Kubernetes.Operator.Controller;


/// <summary>
/// Application class for KubeOps Application CRD
/// </summary>
[EntityRbac(typeof(V1Alpha1ApplicationEntity), Verbs = RbacVerb.All)]
public class ApplicationCtrl : IResourceController<V1Alpha1ApplicationEntity>
{
    /// <summary>
    /// CreatedAsync called by KubeOps when CRD created
    /// </summary>
    /// <param name="resource">crd resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult?> CreatedAsync(V1Alpha1ApplicationEntity resource)
    {
        resource.WithOwnerReference(resource.Organization);
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// ReconcileAsync called by KubeOps when resource updated
    /// </summary>
    /// <param name="resource">KubeOps CRD resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult?> ReconcileAsync(V1Alpha1ApplicationEntity resource)
    {
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// StatusModifiedAsync called by KubeOps when status modified
    /// </summary>
    /// <param name="resource">KubeOps CRD resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult> StatusModifiedAsync(V1Alpha1ApplicationEntity resource)
    {
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// DeletedAsync Called by KubeOps when resource deleted
    /// </summary>
    /// <param name="resource">KubeOps CRD Resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult> DeletedAsync(V1Alpha1ApplicationEntity resource)
    {
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }
}
