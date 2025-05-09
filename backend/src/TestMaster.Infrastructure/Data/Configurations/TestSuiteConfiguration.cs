using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestMaster.Core.TestManagement.Entities;
using System;

namespace TestMaster.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Entity Framework configuration for TestSuite entity
    /// </summary>
    public class TestSuiteConfiguration : IEntityTypeConfiguration<TestSuite>
    {
        /// <summary>
        /// Configure entity mappings and relationships
        /// </summary>
        /// <param name="builder">Entity type builder</param>
        public void Configure(EntityTypeBuilder<TestSuite> builder)
        {
            // Table name
            builder.ToTable("TestSuites");

            // Primary key
            builder.HasKey(ts => ts.Id);

            // Properties
            builder.Property(ts => ts.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ts => ts.Description)
                .HasMaxLength(1000);

            builder.Property(ts => ts.CreatedAt)
                .IsRequired();

            builder.Property(ts => ts.CreatedBy)
                .HasMaxLength(100);

            builder.Property(ts => ts.LastModifiedBy)
                .HasMaxLength(100);

            builder.Property(ts => ts.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Self-referencing relationship (parent-child test suites)
            builder.HasOne<TestSuite>() // No navigation property defined for parent
                .WithMany(ts => ts.ChildTestSuites)
                .HasForeignKey(ts => ts.ParentTestSuiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); // Parent can be null for root suites

            // Indexing strategy
            builder.HasIndex(ts => ts.ProjectId);
            builder.HasIndex(ts => ts.ParentTestSuiteId);
            builder.HasIndex(ts => ts.Name);
            builder.HasIndex(ts => ts.IsActive);
        }
    }
} 