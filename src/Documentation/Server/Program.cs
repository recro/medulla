// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}

app.UseBlazorFrameworkFiles(new PathString("/docs"));
app.UseStaticFiles(new StaticFileOptions()
{
    ServeUnknownFileTypes = true,
});
app.UseStaticFiles(new PathString("/docs"));

app.MapFallbackToFile("/docs/{*path:nonfile}", "docs/index.html");

app.Run();
