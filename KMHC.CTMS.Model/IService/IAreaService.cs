using System.Collections.Generic;
using kmhc.hr.domain.model;

namespace kmhc.hr.domain.IService
{
    public interface IAreaService
    {
        IEnumerable<Area> GetAreas(int pid);
    }
}
