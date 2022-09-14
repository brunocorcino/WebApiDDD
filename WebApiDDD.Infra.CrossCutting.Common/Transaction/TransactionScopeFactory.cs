using System.Reflection;
using System.Transactions;

namespace WebApiDDD.Infra.CrossCutting.Common.Transaction
{
    public static class TransactionScopeFactory
    {
        private readonly static string cachedMaxTimeout = "_cachedMaxTimeout";
        private readonly static string maximumTimeout = "_maximumTimeout";

        /// <summary>
        /// Cria um TransactionScope sempre com os parametros padrão de Isolation e Timeout
        /// </summary>
        /// <returns>objeto TransactionScope</returns>
        public static TransactionScope Create(bool enableTransactionScopeAsyncFlowOption = false)
        {
            IsolationLevel isolation = GetDefaultIsolationLevel();

            TimeSpan timeout = GetDefaultTimeout();

            return Create(isolation, timeout, enableTransactionScopeAsyncFlowOption);
        }

        /// <summary>
        /// Cria um TransactionScope sempre com os parametros padrão de Isolation e Timeout definido
        /// </summary>
        /// <param name="timeout">TimeSpan definido</param>
        /// <returns>objeto TransactionScope</returns>
        public static TransactionScope Create(TimeSpan timeout)
        {
            IsolationLevel isolation = GetDefaultIsolationLevel();

            return Create(isolation, timeout, false);
        }

        /// <summary>
        /// Cria um TransactionScope sempre com os parametros de Isolation e Timeout definidos
        /// </summary>
        /// <param name="isolation">nível específico</param>
        /// <param name="timeout">timeout definido</param>
        /// <returns>Objeto TransactionScope</returns>
        public static TransactionScope Create(IsolationLevel isolation, TimeSpan timeout, bool enableTransactionScopeAsyncFlowOption)
        {
            return Create(TransactionScopeOption.Required, new TransactionOptions()
            {
                IsolationLevel = isolation,
                Timeout = timeout,
            },
            enableTransactionScopeAsyncFlowOption);
        }

        /// <summary>
        /// Cria um TransactionScope com opção de configuração de todas as opções
        /// </summary>
        /// <param name="scopeOption">Scope Option. O padrão é Required</param>
        /// <param name="options">Opções de Isolation e TImeout</param>
        /// <returns>objeto TransactionScope</returns>
        public static TransactionScope Create(TransactionScopeOption scopeOption, TransactionOptions options, bool enableTransactionScopeAsyncFlowOption)
        {
            return enableTransactionScopeAsyncFlowOption
                ? new TransactionScope(scopeOption, options, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(scopeOption, options);
        }

        private static TimeSpan GetDefaultTimeout()
        {
            TimeSpan timeout = TimeSpan.FromSeconds(120);

            return timeout;
        }

        private static IsolationLevel GetDefaultIsolationLevel()
        {
            IsolationLevel isolation = IsolationLevel.ReadCommitted;

            return isolation;
        }

        public static void OverrideMaximumTimeout(TimeSpan timeout)
        {
            //TransactionScope inherits a *maximum* timeout from Machine.config.  There's no way to override it from
            //code unless you use reflection.  Hence this code!
            //TransactionManager._cachedMaxTimeout
            var type = typeof(TransactionManager);
            var cachedMaxTimeout = type.GetField(TransactionScopeFactory.cachedMaxTimeout, BindingFlags.NonPublic | BindingFlags.Static);
            cachedMaxTimeout.SetValue(null, true);
            //TransactionManager._maximumTimeout
            var maximumTimeout = type.GetField(TransactionScopeFactory.maximumTimeout, BindingFlags.NonPublic | BindingFlags.Static);
            maximumTimeout.SetValue(null, timeout);
        }
    }
}
