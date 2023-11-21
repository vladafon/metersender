﻿// <auto-generated />
using System;
using MetersSender.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetersSender.DataAccess.Migrations
{
    [DbContext(typeof(MetersSenderDbContext))]
    [Migration("20231121172548_RemoveReceivingDateTime")]
    partial class RemoveReceivingDateTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.House", b =>
                {
                    b.Property<long>("HouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("HouseId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("RecepientServiceId")
                        .HasColumnType("bigint");

                    b.Property<long>("SourceServiceId")
                        .HasColumnType("bigint");

                    b.HasKey("HouseId");

                    b.HasIndex("RecepientServiceId");

                    b.HasIndex("SourceServiceId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Meter", b =>
                {
                    b.Property<long>("MeterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("MeterId"));

                    b.Property<long>("HouseId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RecepientMeterId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SourceMeterId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("MeterId");

                    b.HasIndex("HouseId");

                    b.ToTable("Meters");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Reading", b =>
                {
                    b.Property<long>("ReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ReadingId"));

                    b.Property<long>("MeterId")
                        .HasColumnType("bigint");

                    b.Property<long>("RequestId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("ReadingId");

                    b.HasIndex("MeterId");

                    b.HasIndex("RequestId");

                    b.ToTable("Readings");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Request", b =>
                {
                    b.Property<long>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("RequestId"));

                    b.Property<DateTimeOffset>("SendingDateTimeUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RequestId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Service", b =>
                {
                    b.Property<long>("Serviced")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Serviced"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Serviced");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.House", b =>
                {
                    b.HasOne("MetersSender.DataAccess.Database.Entities.Service", "RecepientService")
                        .WithMany("HousesRecipient")
                        .HasForeignKey("RecepientServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetersSender.DataAccess.Database.Entities.Service", "SourceService")
                        .WithMany("HousesSource")
                        .HasForeignKey("SourceServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecepientService");

                    b.Navigation("SourceService");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Meter", b =>
                {
                    b.HasOne("MetersSender.DataAccess.Database.Entities.House", "House")
                        .WithMany("Meters")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Reading", b =>
                {
                    b.HasOne("MetersSender.DataAccess.Database.Entities.Meter", "Meter")
                        .WithMany("Readings")
                        .HasForeignKey("MeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetersSender.DataAccess.Database.Entities.Request", "Request")
                        .WithMany("Readings")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meter");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.House", b =>
                {
                    b.Navigation("Meters");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Meter", b =>
                {
                    b.Navigation("Readings");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Request", b =>
                {
                    b.Navigation("Readings");
                });

            modelBuilder.Entity("MetersSender.DataAccess.Database.Entities.Service", b =>
                {
                    b.Navigation("HousesRecipient");

                    b.Navigation("HousesSource");
                });
#pragma warning restore 612, 618
        }
    }
}
