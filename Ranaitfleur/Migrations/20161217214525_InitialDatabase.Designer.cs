using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ranaitfleur.Model;

namespace Ranaitfleur.Migrations
{
    [DbContext(typeof(RanaitfleurContext))]
    [Migration("20161217214525_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ranaitfleur.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description1");

                    b.Property<string>("Description2");

                    b.Property<string>("Dimentions");

                    b.Property<string>("ImagePath");

                    b.Property<int>("ItemType");

                    b.Property<string>("Name");

                    b.Property<int>("NoOfItemInStock");

                    b.Property<int>("Price");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });
        }
    }
}
