using System;

namespace Core.CrossCuttingConcerns.Context
{
    public interface IAppContextService
    {
        void SetAppContext(Extensions.BabuAppContext appContext);
        Extensions.BabuAppContext GetAppContext();
    }

}
