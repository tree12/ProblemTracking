using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using ProblemTracking.Repository;

namespace ProblemTracking.Web.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly Dictionary<string, object> _services; 
        private readonly Entity.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;
        public ServiceFactory(Entity.ApplicationDbContext applicationDbContext, IConfiguration configuration, UnitOfWork unitOfWork)
        {
            this._services = new Dictionary<string, object>();
            this._context = applicationDbContext;
            this._configuration = configuration;
            this._unitOfWork = unitOfWork;
        }
        public T GetService<T>() where T : class
        {
            string serviceName = typeof(T).Name;

            if (!this._services.ContainsKey(serviceName))
            {
                ConstructorInfo cInfo = typeof(T).GetConstructors().First();
                List<object> parameters = new List<object>();
                foreach (ParameterInfo parameterInfo in cInfo.GetParameters())
                {
                    if (parameterInfo.ParameterType.IsAssignableFrom(typeof(Entity.ApplicationDbContext))) parameters.Add(_context);
                    else if (parameterInfo.ParameterType.IsAssignableFrom(typeof(IConfiguration))) parameters.Add(this._configuration);
                    else if (parameterInfo.ParameterType.IsAssignableFrom(typeof(UnitOfWork))) parameters.Add(this._unitOfWork);
                    else
                    {
                        throw new ArgumentException($"Cannot inject type {parameterInfo.ParameterType.FullName} to Service {typeof(T).FullName}");
                    }
                }

                var serviceInstance = (T)Activator.CreateInstance(typeof(T), parameters.ToArray());
                this._services.Add(serviceName, serviceInstance);

                return serviceInstance;
            }
            else
            {
                return this._services[serviceName] as T;
            }
        }
    }
    public interface IServiceFactory
    {
        T GetService<T>() where T : class;
    }
}
