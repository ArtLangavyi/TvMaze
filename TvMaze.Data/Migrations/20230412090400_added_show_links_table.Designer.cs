﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvMaze.Data.Context;

#nullable disable

namespace TvMaze.Data.Migrations
{
    [DbContext(typeof(TvMazeContext))]
    [Migration("20230412090400_added_show_links_table")]
    partial class added_show_links_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TvMaze.Domain.ShowLinkEntity.ShowLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_ShowLink");

                    b.ToTable("ShowLink", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
