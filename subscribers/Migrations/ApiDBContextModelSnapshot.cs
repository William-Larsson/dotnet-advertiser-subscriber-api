﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using subscribers.Data;

namespace subscribers.Migrations
{
    [DbContext(typeof(ApiDBContext))]
    partial class ApiDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("subscribers.Models.Subscriber", b =>
                {
                    b.Property<int>("SubscriberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("sub_subscriber_id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sub_city");

                    b.Property<string>("DistributionAddress")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sub_distribution_address");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sub_first_name");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sub_last_name");

                    b.Property<int>("PersonalId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("sub_personal_id");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sub_phone_number");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sub_zip_code");

                    b.HasKey("SubscriberId");

                    b.ToTable("tbl_subscribers");
                });
#pragma warning restore 612, 618
        }
    }
}
