using System.Collections.Generic;
using kmhc.hr.domain.model;
using kmhc.hr.infrastructure.utility;

namespace kmhc.hr.domain.IService
{
    public interface IFamilyService
    {
        IEnumerable<Family> GetFamilys(PageInfo page, string name);

        Family Get(int id);

        bool Add(Family entity);

        bool Edit(Family entity);
    }
}
