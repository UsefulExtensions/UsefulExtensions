﻿namespace UsefulExtensions.EntityFramework.Models
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}