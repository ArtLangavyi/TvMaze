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
    [Migration("20230417075645_added_showid_field")]
    partial class added_showid_field
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TvMaze.Domain.Cast", b =>
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

                    b.Property<int?>("ShowId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id")
                        .HasName("PK_Cast");

                    b.HasIndex("ShowId");

                    b.ToTable("Cast", (string)null);
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

            modelBuilder.Entity("TvMaze.Domain.ShowCastRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CastId")
                        .HasColumnType("int");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_ShowCastRelation");

                    b.HasIndex("CastId");

                    b.HasIndex("ShowId");

                    b.HasIndex("Id", "ShowId", "CastId")
                        .HasDatabaseName("IX_ShowCastRelation_ShowId_Cast_id");

                    b.ToTable("ShowCastRelation", (string)null);
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

            modelBuilder.Entity("TvMaze.Domain.Cast", b =>
                {
                    b.HasOne("TvMaze.Domain.Show", null)
                        .WithMany("Cast")
                        .HasForeignKey("ShowId");
                });

            modelBuilder.Entity("TvMaze.Domain.ShowCastRelation", b =>
                {
                    b.HasOne("TvMaze.Domain.Show", "Show")
                        .WithMany("ShowCastRelation")
                        .HasForeignKey("CastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ShowCastRelation_Show_Cascade");

                    b.HasOne("TvMaze.Domain.Cast", "Cast")
                        .WithMany("ShowCastRelation")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ShowCastRelation_Cast_Cascade");

                    b.Navigation("Cast");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("TvMaze.Domain.Cast", b =>
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
