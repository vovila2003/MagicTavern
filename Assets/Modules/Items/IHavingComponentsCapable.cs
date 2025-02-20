using System.Collections.Generic;

namespace Modules.Items
{
    public interface IHavingComponentsCapable
    {
        bool TryGet<T>(out T component);
        List<T> GetAll<T>();
        bool Has<T>();
    }
}