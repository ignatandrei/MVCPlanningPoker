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
    // Users
    internal partial class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration(string schema = "")
        {
            ToTable("Users");
            HasKey(x => x.UsersId);

            Property(x => x.UsersId).HasColumnName("UsersID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().IsFixedLength().HasMaxLength(10);
            Property(x => x.Password).HasColumnName("Password").IsRequired().IsFixedLength().HasMaxLength(10);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
