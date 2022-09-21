using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Api
{
    public class ApiResult
    {
        public ActionReturn Status { get; set; } = new ActionReturn();
    }

    public class ApiResult<TData> : ApiResult
    {
        public TData Data { get; set; }
    }
}
