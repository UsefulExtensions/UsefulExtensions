using System;

namespace UsefulExtensions.EntityFramework.Models
{
    public interface IAudit
    {
        string LastModifiedBy { get; set; }
        string CreatedBy { get; set; }

        DateTime LastModifiedDate { get; set; }
        DateTime CreateDate { get; set; }
    }
}