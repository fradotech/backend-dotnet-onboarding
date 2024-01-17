using System;
using DotNetEnv;

public class Config
{
    public static string ConnectionString;

    static Config()
    {
        DotNetEnv.Env.Load();
        ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Host=127.0.0.1;Database=dotnet_onboarding;Username=postgres;Password=frado201";
    }
}