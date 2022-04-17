using System;
using Rhino.Mocks;

namespace Utils.XUnit
{
    public abstract class SpecificationBase : IContextSpecification, IDisposable
    {
        protected abstract void Because();
        protected virtual void EstablishContext()
        {
        }

        public void Dispose()
        {
            this.AfterEachSpec();
        }

        protected virtual void AfterEachSpec()
        {
            throw new NotImplementedException();
        }

        void IContextSpecification.Because() => this.Because();
        
        void IContextSpecification.EstablishContext() => this.EstablishContext();

        protected T Stub<T>() where T : class => MockRepository.GenerateStub<T>();

        protected T Mock<T>() where T : class => MockRepository.GenerateMock<T>();
    }
}