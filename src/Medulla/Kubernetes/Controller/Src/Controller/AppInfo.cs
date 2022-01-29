
using System;
using Utils;

namespace App;

public sealed class AppInfo {

    public const string APP_NAME = "Medulla";
    public const string VERSION = "1.0.0";

    public const string COMPANY = "Recro";

    public const string DATE = "01/29/2022";

    public static void PrintInfo() {
        Logger.Message($""+
            $"\n\n\nWelcome to the Kubernetes Controller for {AppInfo.APP_NAME}\n"+
            "This Controller manages your Medulla Application on your Kubernetes Cluster through the CustomResourceDefinitions.\n\n\n"+

            $"App: {AppInfo.APP_NAME}\n"+
            $"Version: {AppInfo.VERSION}\n"+
            $"Company: {AppInfo.COMPANY}\n"+
            $"Date: {AppInfo.DATE}\n\n\n\n");
    }



}
