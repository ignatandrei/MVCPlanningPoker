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
    // Rounds
    internal partial class RoundConfiguration : EntityTypeConfiguration<Round>
    {
        public RoundConfiguration(string schema = "")
        {
            ToTable("Rounds");
            HasKey(x => x.RoundId);

            Property(x => x.TableId).HasColumnName("TableID").IsRequired();
            Property(x => x.RoundName).HasColumnName("RoundName").IsRequired().IsFixedLength().HasMaxLength(10);
            Property(x => x.Result).HasColumnName("Result").IsOptional().IsFixedLength().HasMaxLength(10);
            Property(x => x.RoundId).HasColumnName("RoundID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
