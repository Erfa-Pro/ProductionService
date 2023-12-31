﻿// <auto-generated />
using System;
using Erfa.ProductionManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    [DbContext(typeof(ErfaDbContext))]
    [Migration("20231230200745_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("production")
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Erfa.ProductionManagement.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MaterialProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ProductionTimeSec")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ProductNumber");

                    b.ToTable("Erfa_Pro_Catalog", "production");
                });
#pragma warning restore 612, 618
        }
    }
}
