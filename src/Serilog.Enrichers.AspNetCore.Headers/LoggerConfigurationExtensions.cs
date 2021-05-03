using Microsoft.Extensions.DependencyInjection;
using Serilog.Configuration;
using Serilog.Enrichers.AspNetCore.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Enrichers
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithHeader(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceProvider sp, string headerName, string propertyName = null)
        {
            if (enrichmentConfiguration == null) 
                throw new ArgumentNullException(nameof(enrichmentConfiguration));


            return enrichmentConfiguration.With(ActivatorUtilities.CreateInstance<HeaderEnricher>(sp, headerName, propertyName));
        }

        public static LoggerConfiguration WithCorrelationId(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceProvider sp, 
            string headerName = "X-Correlation-ID",
            string propertyName = "CorrelationId")
            => WithHeader(enrichmentConfiguration, sp, headerName, propertyName);
    }
}
