using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuttingLayer.CommonDefinitions.Record
{
    public class Console
    {
        public bool IncludeScopes { get; set; }
        public LogLevel LogLevel { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public class Debug
    {
        public LogLevel LogLevel { get; set; }
    }

    public class Environment
    {
        public string Name { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }

        [JsonProperty("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }

        [JsonProperty("Microsoft.Hosting")]
        public string MicrosoftHosting { get; set; }

        [JsonProperty("Microsoft.Extensions.Hosting")]
        public string MicrosoftExtensionsHosting { get; set; }
    }

    public class RabbitMQ
    {
        public string RabbitMqRootUri { get; set; }
        public string RabbitMqUri { get; set; }
        public string RabbitMqUriChallenge { get; set; }
        public string RabbitMqUri_Coach { get; set; }
        public string RabbitMqUri_Nutritionist { get; set; }
        public string RabbitMqUri_Physiotherapist { get; set; }
        public string RabbitMqUri_PlayerEnergyPoint { get; set; }
        public string RabbitMqUri_NotificationChallenge { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class PaymentToken
    {
        public string CompanyToken { get; set; }
        public string Request { get; set; }
        public string CompanyRef { get; set; }
        public string RedirectURL { get; set; }
        public string BackURL { get; set; }
        public string PaymentUrl { get; set; }
        public string DirectPayUrl { get; set; }
        public int CompanyRefUnique { get; set; }
        public int PTL { get; set; }
        public int ServiceType { get; set; }
        public string PTL_Type { get; set; }
        public string ServiceDescription { get; set; }
    }
    public class Urls
    {
        public string Challenge { get; set; }
        public string Venue { get; set; }
        public string Coach { get; set; }
        public string Nutritionist { get; set; }
        public string Physiotherapist { get; set; }
    }
    public class AppSettingsRecord
    {
        public DatabaseSettings DatabaseSettings { get; set; }
        public Logging Logging { get; set; }
        public RabbitMQ RabbitMQ { get; set; }
        public Environment Environment { get; set; }
        public Debug Debug { get; set; }
        public Console Console { get; set; }
        public PaymentToken PaymentToken { get; set; }
        public Urls Urls { get; set; }

    }
}
