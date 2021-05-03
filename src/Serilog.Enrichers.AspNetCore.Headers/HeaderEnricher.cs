using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Enrichers.AspNetCore.Headers;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Enrichers.AspNetCore.Headers
{
    public class HeaderEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _header;
        private readonly string _propertyName;

        public HeaderEnricher(IHttpContextAccessor contextAccessor, string header, string propertyName = null)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _header = header ?? throw new ArgumentNullException(nameof(header));
            _propertyName = propertyName ?? _header;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_contextAccessor.HttpContext == null)
                return;

            var value = _contextAccessor.HttpContext.Request.Headers[_header].FirstOrDefault();
            logEvent.AddOrUpdateProperty(new LogEventProperty(_propertyName, new ScalarValue(value)));
        }
    }
}
