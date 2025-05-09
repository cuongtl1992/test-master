using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestMaster.Core.TestManagement.Entities;
using System;

namespace TestMaster.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Entity Framework configuration for TestCase entity
    /// </summary>
    public class TestCaseConfiguration : IEntityTypeConfiguration<TestCase>
    {
        /// <summary>
        /// Configure entity mappings and relationships
        /// </summary>
        /// <param name="builder">Entity type builder</param>
        public void Configure(EntityTypeBuilder<TestCase> builder)
        {
            // Table name
            builder.ToTable("TestCases");

            // Primary key
            builder.HasKey(tc => tc.Id);

            // Properties
            builder.Property(tc => tc.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(tc => tc.Description)
                .HasMaxLength(2000);

            builder.Property(tc => tc.Steps)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(tc => tc.ExpectedResults)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(tc => tc.PreConditions)
                .HasMaxLength(2000);

            builder.Property(tc => tc.Priority)
                .IsRequired();

            builder.Property(tc => tc.Status)
                .IsRequired();

            builder.Property(tc => tc.CreatedAt)
                .IsRequired();

            builder.Property(tc => tc.CreatedBy)
                .HasMaxLength(100);

            builder.Property(tc => tc.LastModifiedBy)
                .HasMaxLength(100);

            builder.Property(tc => tc.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Store Tags as JSON
            builder.Property(tc => tc.Tags)
                .HasColumnType("jsonb");

            // Relationships
            builder.HasOne<TestSuite>() // No navigation property defined on TestCase
                .WithMany(ts => ts.TestCases)
                .HasForeignKey(tc => tc.TestSuiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Indexing strategy
            builder.HasIndex(tc => tc.TestSuiteId);
            builder.HasIndex(tc => tc.Priority);
            builder.HasIndex(tc => tc.Status);
            builder.HasIndex(tc => tc.Title);
            builder.HasIndex(tc => tc.IsActive);
        }
    }
} 