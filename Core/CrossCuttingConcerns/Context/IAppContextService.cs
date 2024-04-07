using System;

namespace Core.CrossCuttingConcerns.Context
{
    public interface IAppContextService
    {
        void SetAppContext(Extensions.AppContext appContext);
        Extensions.AppContext GetAppContext();
    }

}
