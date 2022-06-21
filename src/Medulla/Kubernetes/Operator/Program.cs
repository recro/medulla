// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using KubeOps.Operator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddKubernetesOperator();
var app = builder.Build();
app.UseKubernetesOperator();
app.RunOperatorAsync(args);
