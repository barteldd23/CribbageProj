﻿namespace Cribbage.PL.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string SortField { get; }
    }
}
