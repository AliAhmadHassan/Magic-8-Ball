
using Serilog;
using Serilog.Events;


namespace Magic8Ball.Api
{
    public static class LogExtension
    {
        public static WebApplicationBuilder ConfigureLog(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureAppConfiguration((builderContext, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);

                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                string DbPath = System.IO.Path.Join(path, "magic8ball_Log.db");

                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(LogEventLevel.Information)
                    .WriteTo.SQLite(sqliteDbPath: DbPath, tableName:"TbLog", batchSize: 1)
               .CreateLogger();
            })
            .UseSerilog();

            Log.Warning("Iniciando API Magic 8 Ball");

            return builder;
        }
    }
}
