using System;

namespace GrillMaster.Core.Entities
{
    public interface IIdentify
    {
        Guid Id { get; }
        string Name { get; }
    }
}