using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarsExploration.Common.Registry
{
    public abstract class RegistryBase
    {
        private readonly List<ServiceDescriptor> descriptors;

        public RegistryBase()
        {
            descriptors = new List<ServiceDescriptor>();
        }

        protected void Register(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            descriptors.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
        }

        public IEnumerable<ServiceDescriptor> GetDescriptors()
        {
            return descriptors;
        }
    }
}