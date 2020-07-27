using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Geex.ElasticSearch.Zero.Logging.Elasticsearch;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace ElasticSearch
{
    public static class Extensions
    {
        public static IPAddress GetLocalIPv4(this IEnumerable<IPAddress> addressList)
        {
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Configures <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" /> to use Application Insights services.
        /// </summary>
        /// <param name="webHostBuilder">The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" /> instance.</param>
        /// <returns>The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" />.</returns>
        public static IWebHostBuilder ConfigEsLogging(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, EsLoggerProvider>());
                services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<EsLoggerOptions>, EsLoggerOptionsSetup>());
                services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<EsLoggerOptions>, LoggerProviderOptionsChangeTokenSource<EsLoggerOptions, EsLoggerProvider>>());
                services.AddLogging(builder => builder.AddElasticsearch());
            });
            return webHostBuilder;
        }
    }
}
