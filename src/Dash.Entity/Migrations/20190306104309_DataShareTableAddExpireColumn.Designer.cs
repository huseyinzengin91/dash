﻿// <auto-generated />
using System;
using Dash.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dash.Entity.Migrations
{
    [DbContext(typeof(DSDContext))]
    [Migration("20190306104309_DataShareTableAddExpireColumn")]
    partial class DataShareTableAddExpireColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dash.Entity.Objects.DSDAccessibleSite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccessibleSiteId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<Guid?>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("AccessibleSiteId");

                    b.HasIndex("SiteId");

                    b.ToTable("AccessibleSite");
                });

            modelBuilder.Entity("Dash.Entity.Objects.DSDDataShare", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DataShareCode");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<Guid?>("OwnerSiteId");

                    b.Property<short>("Status");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("OwnerSiteId");

                    b.ToTable("DataShares");
                });

            modelBuilder.Entity("Dash.Entity.Objects.DSDSite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessCode");

                    b.Property<DateTime>("AccessCodeEndDate");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("SiteAddress");

                    b.Property<short>("Status");

                    b.HasKey("Id");

                    b.ToTable("Site");
                });

            modelBuilder.Entity("Dash.Entity.Objects.DSDUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("Password");

                    b.Property<short>("Status");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Dash.Entity.Objects.DSDAccessibleSite", b =>
                {
                    b.HasOne("Dash.Entity.Objects.DSDSite", "AccessibleSite")
                        .WithMany()
                        .HasForeignKey("AccessibleSiteId");

                    b.HasOne("Dash.Entity.Objects.DSDSite", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("Dash.Entity.Objects.DSDDataShare", b =>
                {
                    b.HasOne("Dash.Entity.Objects.DSDSite", "OwnerSite")
                        .WithMany()
                        .HasForeignKey("OwnerSiteId");
                });
#pragma warning restore 612, 618
        }
    }
}
