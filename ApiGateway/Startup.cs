using System;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace ApiGateway
{
    public class Startup
    {
        private BusHandle _busHandle;

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            services.AddSingleton<IBus>(bus);

            _busHandle = TaskUtil.Await(() => bus.StartAsync());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.ApplicationServices
                .GetRequiredService<IApplicationLifetime>()
                .ApplicationStopping
                .Register(OnShutdown);
        }

        private void OnShutdown()
        {
            _busHandle.Stop(TimeSpan.FromSeconds(30));
        }
    }
}
