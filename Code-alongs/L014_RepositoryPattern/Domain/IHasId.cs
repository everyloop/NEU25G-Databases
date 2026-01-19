using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L014_RepositoryPattern.Domain
{
    internal interface IHasId<TId>
    {
        TId Id { get; }
    }
}
