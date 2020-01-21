using System;
using System.Collections.Generic;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Slackers.Services.Bus.MassTran.RabbitMQ;

namespace Slackers.Services.Bus.MassTransit.RabbitMQ
{
    public static class Extensions
    {
        private static string _hostName;

        private static List<Action<IServiceProvider, IRabbitMqBusFactoryConfigurator, IRabbitMqHost>> _addEndpointAndConsumerActionList;

        static Extensions()
        {
            _addEndpointAndConsumerActionList = new List<Action<IServiceProvider, IRabbitMqBusFactoryConfigurator, IRabbitMqHost>>();
        }

        public static IServiceCollection AddHandler<TEventMessage, TInterface, TImpplimentationClass>(this IServiceCollection services)
            where TEventMessage : EventMessage
            where TInterface : class, IEventHandler<TEventMessage>
            where TImpplimentationClass : class, TInterface

        {
            if (string.IsNullOrEmpty(_hostName))
            {
                throw new InvalidOperationException("Must Set Host Name before adding Handler");
            }

            var queueName = $"{_hostName}_{typeof(TEventMessage).Name}";
            services.AddTransient<TInterface, TImpplimentationClass>();
            services.AddTransient(provider => provider.GetRequiredService<IBus>().CreateRequestClient<TEventMessage>());
            _addEndpointAndConsumerActionList.Add((provider, cfg, host) =>
            {
                cfg.ReceiveEndpoint(queueName, e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(x => x.Interval(2, 1000));
                    e.Consumer(() =>
                    {
                        var handlerImplementation = provider.GetRequiredService<TInterface>();
                        return new TransitConsumer<TEventMessage>(handlerImplementation);
                    });
                    EndpointConvention.Map<TEventMessage>(e.InputAddress);
                });
            });

            return services;
        }

        public static IServiceCollection AddBus(this IServiceCollection services, string section, IConfiguration configuration)
        {
            var configurationOptions = configuration.GetSection(section);
            var options = new RabbitMQOptions();
            configurationOptions.Bind(options);

            _hostName = options.HostName;
            services.AddSingleton(provider => global::MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(options.Server, options.VirtualHost, h =>
                {
                    h.Username(options.UserName);
                    h.Password(options.Password);
                });

                cfg.SetLoggerFactory(provider.GetService<ILoggerFactory>());
                foreach (var handlerAction in _addEndpointAndConsumerActionList)
                {
                    handlerAction(provider, cfg, host);
                }
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddScoped<IEventBus, TransitBus>();
            services.AddSingleton<IHostedService, BusService>();
            return services;
        }

    }
}