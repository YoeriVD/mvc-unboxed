using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unboxed.Infrastructure.ServiceLayer
{
    public interface IRequest{}
    public interface IResponse{}
    public interface IReturn<TResponse> : IRequest where TResponse : IResponse {}
    public interface IRequestHandler<in TRequest, TResponse>
       where TRequest : IRequest
       where TResponse : IResponse
    {
        Task<TResponse> Handle(TRequest request);
    }
    public interface IRequestDispatcher
    {
        Task<TResponse> Execute<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse;
    }

    public class DefaultRequestDispatcher : IRequestDispatcher
    {
        
        public async Task<TResponse> Execute<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            var handlerType = GetType()
                .Assembly.GetTypes()
                .FirstOrDefault(t => t.GetInterfaces().Contains(typeof(IRequestHandler<TRequest, TResponse>)));

            if (handlerType == null) ThrowHandlerNotFoundException<TRequest, TResponse>();

            // ReSharper disable once AssignNullToNotNullAttribute (kan niet meer op dit punt)
            var handler = Activator.CreateInstance(handlerType) as IRequestHandler<TRequest, TResponse>;

            if (handler == null) ThrowHandlerNotFoundException<TRequest, TResponse>();
            
            // ReSharper disable once PossibleNullReferenceException (kan niet meer op dit punt)
            var response = await handler.Handle(request);
            
            return response;
        }

        private static void ThrowHandlerNotFoundException<TRequest, TResponse>() where TRequest : IRequest
            where TResponse : IResponse
        {
            throw new NotSupportedException(
                $"There is no handler defined for the Request: {typeof(TRequest)} and Response: {typeof(TResponse)}");
        }
    }
}
