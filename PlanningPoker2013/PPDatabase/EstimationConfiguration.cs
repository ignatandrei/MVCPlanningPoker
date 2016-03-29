// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using System.Threading.Tasks;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace PPDatabase
{
    // Estimations
    internal partial class EstimationConfiguration : EntityTypeConfiguration<Estimation>
    {
        public EstimationConfiguration(string schema = "")
        {
            ToTable("Estimations");
            HasKey(x => x.EstmId);

            Property(x => x.EstmId).HasColumnName("EstmID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RoundId).HasColumnName("RoundID").IsRequired();
            Property(x => x.UserId).HasColumnName("UserID").IsRequired();
            Property(x => x.Value).HasColumnName("Value").IsRequired();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
