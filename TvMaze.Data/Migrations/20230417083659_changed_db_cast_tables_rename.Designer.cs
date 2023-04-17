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
    [Migration("20230417083659_changed_db_cast_tables_rename")]
    partial class changed_db_cast_tables_rename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TvMaze.Domain.CastPersone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("ShowId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id")
                        .HasName("PK_CastPersone");

                    b.HasIndex("ShowId");

                    b.ToTable("CastPersone", (string)null);
                });

            modelBuilder.Entity("TvMaze.Domain.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id")
                        .HasName("PK_Show");

                    b.ToTable("Show", (string)null);
                });

            modelBuilder.Entity("TvMaze.Domain.ShowCastPersoneRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CastPersoneId")
                        .HasColumnType("int");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_ShowCastPersoneRelation");

                    b.HasIndex("CastPersoneId");

                    b.HasIndex("ShowId");

                    b.HasIndex("Id", "ShowId", "CastPersoneId")
                        .HasDatabaseName("IX_ShowCastPersoneRelation_ShowId_CastPersone_id");

                    b.ToTable("ShowCastPersoneRelation", (string)null);
                });

            modelBuilder.Entity("TvMaze.Domain.ShowLink", b =>
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

            modelBuilder.Entity("TvMaze.Domain.CastPersone", b =>
                {
                    b.HasOne("TvMaze.Domain.Show", null)
                        .WithMany("Cast")
                        .HasForeignKey("ShowId");
                });

            modelBuilder.Entity("TvMaze.Domain.ShowCastPersoneRelation", b =>
                {
                    b.HasOne("TvMaze.Domain.Show", "Show")
                        .WithMany("ShowCastRelation")
                        .HasForeignKey("CastPersoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ShowCastPersoneRelation_Show_Cascade_Delete");

                    b.HasOne("TvMaze.Domain.CastPersone", "CastPersone")
                        .WithMany("ShowCastRelation")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete");

                    b.Navigation("CastPersone");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("TvMaze.Domain.CastPersone", b =>
                {
                    b.Navigation("ShowCastRelation");
                });

            modelBuilder.Entity("TvMaze.Domain.Show", b =>
                {
                    b.Navigation("Cast");

                    b.Navigation("ShowCastRelation");
                });
#pragma warning restore 612, 618
        }
    }
}
