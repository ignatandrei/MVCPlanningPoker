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
    // Tables
    internal partial class TableConfiguration : EntityTypeConfiguration<Table>
    {
        public TableConfiguration(string schema = "")
        {
            ToTable("Tables");
            HasKey(x => x.TableId);

            Property(x => x.TableId).HasColumnName("TableID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ModeratorId).HasColumnName("ModeratorID").IsRequired();
            Property(x => x.TableName).HasColumnName("TableName").IsRequired().IsFixedLength().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
