using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FClam5.Models
{
    public class ErrorReport
    {
        [Key]
        [Column(Order=1)]
        public int errorNumber { get; set; }
        [Key]
        [Column(Order=2)]
        public int reportNumber { get; set; }
        public String URL { get; set; }
        public String parentURL { get; set; }
        public String errorType { get; set; }
    }

    public class ErrorReportContext : DbContext
    {
        public DbSet<ErrorReport> ErrorReports { get; set; }
    }
}