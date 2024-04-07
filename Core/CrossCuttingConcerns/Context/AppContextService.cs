using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Context
{

    public class AppContextService : IAppContextService
    {
        private Extensions.AppContext _appContext;

        public void SetAppContext(Extensions.AppContext appContext)
        {
            _appContext = appContext;
        }

        public Extensions.AppContext GetAppContext()
        {
            return _appContext;
        }
    }

}
