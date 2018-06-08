using System;
using System.Web.Http;

namespace ReferenceProject
{
    public static class OptionsMessageHandlerConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.MessageHandlers.Add(new Handlers.OptionsHttpMessageHandler());
        }
    }
}
