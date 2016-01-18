using System;

namespace FriendFinder.ServiceBase
{
    public abstract class FriendFinderServiceBase : IFriendFinderServiceBase
    {
        public static Response GetResponse<TParam>(TParam param, Action<TParam, Result> action)
        {
            var response = new Response();

            Execute(result => action(param, result), response);

            return response;
        }

        public static Response GetResponse<TParam>(Request<TParam> request, Action<TParam, Result> action)
        {
            var response = new Response();

            Execute(result => action(request.Parameter, result), response);

            return response;

        }

        public static Response<TData> GetResponse<TData>(Func<Result, TData> function)
        {
            var response = new Response<TData>();

            Execute(result => response.Data = function(result), response);

            return response;
        }

        public static Response<TData> GetResponse<TParam, TData>(TParam param, Func<TParam, Result, TData> function)
        {
            var response = new Response<TData>();

            Execute(result => response.Data = function(param, result), response);

            return response;
        }
        public static Response<TData> GetResponse<TParam, TData>(Request<TParam> request, Func<TParam, Result, TData> function)
        {
            var response = new Response<TData>();
           
            Execute(result=> response.Data = function(request.Parameter, result), response);

            return response;
        }

   
        private static void Execute(Action<Result> operation, Response response)
        {
            var result = new Result();
         
            try
            {
                operation(result);
            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = ex.Message;
            }
            finally
            {
                response.Result = result;
            }

        }

    }
}
