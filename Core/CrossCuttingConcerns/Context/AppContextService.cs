using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Context
{

    public class AppContextService : IAppContextService
    {
        private Extensions.BabuAppContext _appContext;

        public void SetAppContext(Extensions.BabuAppContext appContext)
        {
            _appContext = appContext;
        }

        public Extensions.BabuAppContext GetAppContext()
        {
            return _appContext;
        }
    }

}
