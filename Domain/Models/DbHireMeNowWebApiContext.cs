using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class DbHireMeNowWebApiContext : DbContext
{
    public DbHireMeNowWebApiContext()
    {
    }

    public DbHireMeNowWebApiContext(DbContextOptions<DbHireMeNowWebApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<CompanyUser> CompanyUsers { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<JobProviderCompany> JobProviderCompanies { get; set; }

    public virtual DbSet<JobResponsibility> JobResponsibilities { get; set; }

    public virtual DbSet<JobSeeker> JobSeekers { get; set; }

    public virtual DbSet<JobSeekerProfile> JobSeekerProfiles { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SignUpRequest> SignUpRequests { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
    public virtual DbSet<JobApplication> JobApplications { get; set; }
    public virtual DbSet<Interview> Interviews { get; set; }
    public virtual DbSet<JobPostSkill> JobPostSkills { get; set; }
    public virtual DbSet<JobPostQualification> JobPostQualifications { get; set; }
    public virtual DbSet<JobSaved> JobSaved { get; set; }
    public virtual DbSet<JobSeekerImage> JobSeekerImages { get; set; }
    public virtual DbSet<ProfileQualification> ProfileQualifications { get; set; }
    public virtual DbSet<ProfileSkill> ProfileSkills { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-FAPBG4Q0;Initial Catalog=DB_HireMeNow_WebApi;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.ToTable("AuthUser");
           
          


        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.ToTable("CompanyUser");

            entity.HasIndex(e => e.Company, "IX_CompanyUser_Company");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.Company)
                .HasConstraintName("FK_CompanyUser_JobProviderCompany");
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.ToTable("Industry");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.ToTable("JobCategory");

            entity.HasKey(e => e.Id); 

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.ToTable("JobPost");

            entity.HasIndex(e => e.LocationId, "IX_JobPost_LocationId");

            entity.HasIndex(e => e.PostedBy, "IX_JobPost_PostedBy");
            entity.HasIndex(e => e.CategoryId, "IX_JobPost_CategoryId");

            entity.HasIndex(e => e.CompanyId, "IX_JobPost_CompanyId");
            entity.HasIndex(e => e.IndustryId, "IX_JobPost_IndustryId");



            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.JobSummary).HasMaxLength(50);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.PostedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Location).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_Location");

            entity.HasOne(d => d.PostedByNavigation).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.PostedBy)
                .OnDelete(DeleteBehavior.Cascade)

                .HasConstraintName("FK_JobPost_CompanyUser");
            entity.HasIndex(e => e.CategoryId, "IX_JobPost_CategoryId");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_JobCategory");

        });

        modelBuilder.Entity<JobProviderCompany>(entity =>
        {
            entity.ToTable("JobProviderCompany");

            entity.HasIndex(e => e.Location, "IX_JobProviderCompany_Location");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LegalName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Summary)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.JobProviderCompanies)
                .HasForeignKey(d => d.Location)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobProviderCompany_Location");
        });

        modelBuilder.Entity<JobResponsibility>(entity =>
        {
            entity.ToTable("JobResponsibility");

            entity.HasIndex(e => e.JobPost, "IX_JobResponsibility_JobPost");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsFixedLength();

            entity.HasOne(d => d.JobPostNavigation).WithMany(p => p.JobResponsibilities)
                .HasForeignKey(d => d.JobPost)
                .OnDelete(DeleteBehavior.Cascade)

                .HasConstraintName("FK_JobResponsibility_JobPost");
        });

        modelBuilder.Entity<JobSeeker>(entity =>
        {
            entity.ToTable("JobSeeker");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(450);
            entity.HasOne(js => js.SystemUser)
         .WithOne(su => su.JobSeeker)
         .HasForeignKey<JobSeeker>(js => js.SystemUserId)
         .OnDelete(DeleteBehavior.ClientSetNull);
            


            //entity.HasOne(d => d.IdNavigation).WithOne(p => p.JobSeeker).HasForeignKey<JobSeeker>(d => d.Id);
        });

        modelBuilder.Entity<JobSeekerProfile>(entity =>
        {
            entity.ToTable("JobSeekerProfile");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            // Indexes
            entity.HasIndex(e => e.JobSeekerId, "IX_JobSeekerProfile_JobSeekerId");
           
            entity.HasIndex(e => e.LocationId, "IX_JobSeekerProfile_LocationId");

            // One-to-one: JobSeekerProfile → Resume (Resume is dependent)
            entity.HasOne(j => j.Resume)
                  .WithOne(r => r.JobSeekerProfile)
                  .HasForeignKey<Resume>(r => r.JobSeekerProfileId);

            // One-to-one: JobSeekerProfile → JobSeekerImage (JobSeekerImage is dependent)
            entity.HasOne(j => j.JobSeekerImage)
                  .WithOne(img => img.JobSeekerProfile)
                  .HasForeignKey<JobSeekerImage>(img => img.JobSeekerProfileId);

            // One-to-many: Location → JobSeekerProfiles
            entity.HasOne(j => j.Location)
                  .WithMany(loc => loc.JobSeekerProfiles)
                  .HasForeignKey(j => j.LocationId)
                  .IsRequired(false);
            

        });







        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Discription)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.ToTable("Qualification");

            entity.HasKey(q => q.Id);

            entity.Property(q => q.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(q => q.Description)
                  .IsRequired()
                  .HasMaxLength(500);

            // Relationship: Qualification → JobPostQualification (1-to-many)
            entity.HasMany(q => q.JobPostQualifications)
                  .WithOne(jpq => jpq.Qualification)
                  .HasForeignKey(jpq => jpq.QualificationId)
                  .OnDelete(DeleteBehavior.Cascade);
            // Relationship to ProfileQualification
            entity.HasMany(q => q.ProfileQualification)
                  .WithOne(pq => pq.Qualification)
                  .HasForeignKey(pq => pq.QualificationId);

        });


        modelBuilder.Entity<Resume>(entity =>
        {
            entity.ToTable("Resume");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Role");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SignUpRequest>(entity =>
        {
            entity.ToTable("SignUpRequests");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(450);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skill");

          

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
      .ValueGeneratedOnAdd();


            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                              .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500).IsUnicode(false); ;
            // Relationship: Skill ↔ JobPostSkill
            entity.HasMany(e => e.jobPostSkills)
                .WithOne(jps => jps.Skill)
                .HasForeignKey(jps => jps.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships
            entity.HasMany(e => e.ProfileSkill)
                 .WithOne(ps => ps.Skill)
                  .HasForeignKey(ps => ps.SkillId);



        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.ToTable("SystemUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(450);
          

        });

        modelBuilder.Entity<WorkExperience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Experiences");

            entity.ToTable("WorkExperience");

            entity.HasIndex(e => e.JobSeekerProfileId, "IX_WorkExperience_JobSeekerProfileId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.JobSeekerProfile).WithMany(p => p.WorkExperiences)
                .HasForeignKey(d => d.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkExperience_JobSeekerProfile");
        });
        modelBuilder.Entity<JobPostSkill>(entity =>
        {
            entity.ToTable("JobPostSkills");

            // Composite primary key
            entity.HasKey(e => new { e.JobPostId, e.SkillId });

            entity.HasOne(e => e.JobPost)
                .WithMany(j => j.JobPostSkills)
                .HasForeignKey(e => e.JobPostId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Skill)
                .WithMany(s => s.jobPostSkills)
                .HasForeignKey(e => e.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<JobPostQualification>(entity =>
        {
            entity.ToTable("JobPostQualifications");

            // Composite Key
            entity.HasKey(jpq => new { jpq.JobPostId, jpq.QualificationId });

            // Relationship: JobPost → JobPostQualification (1-to-many)
            entity.HasOne(jpq => jpq.JobPost)
                   .WithMany(jp => jp.JobPostQualifications)
                   .HasForeignKey(jpq => jpq.JobPostId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Qualification → JobPostQualification (1-to-many)
            entity.HasOne(jpq => jpq.Qualification)
                   .WithMany(q => q.JobPostQualifications)
                   .HasForeignKey(jpq => jpq.QualificationId)
                   .OnDelete(DeleteBehavior.Cascade);

            
        });
        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.ToTable("JobApplication");

            // Primary Key
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .ValueGeneratedNever();

            // Indexes
            entity.HasIndex(e => e.JobPostId)
                  .HasDatabaseName("IX_JobApplication_JobPostId");

            entity.HasIndex(e => e.JobSeekerProfileId)
                  .HasDatabaseName("IX_JobApplication_JobSeekerProfileId");

            entity.HasIndex(e => e.Applicant)
                  .HasDatabaseName("IX_JobApplication_Applicant");

            entity.HasIndex(e => e.Resume_id)
                  .HasDatabaseName("IX_JobApplication_ResumeId");

            // Relationship: JobApplication → JobSeekerProfile (many-to-one)
            entity.HasOne(e => e.JobSeekerProfile)
                  .WithMany(p => p.JobApplications)
                  .HasForeignKey(e => e.JobSeekerProfileId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relationship: JobApplication → JobPost (many-to-one)
            entity.HasOne(e => e.JobPost)
                  .WithMany(p => p.JobApplications)
                  .HasForeignKey(e => e.JobPostId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relationship: JobApplication → Resume (many-to-one)
            entity.HasOne(e => e.Resume)
                  .WithMany()
                  .HasForeignKey(e => e.Resume_id)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relationship: JobApplication → JobSeeker (Applicant)
            entity.HasOne(e => e.Seeker)
                  .WithMany()
                  .HasForeignKey(e => e.Applicant)
                  .OnDelete(DeleteBehavior.Restrict);

            // Required fields
            entity.Property(e => e.JobTitle)
                  .IsRequired();

            entity.Property(e => e.JobSummary)
                  .IsRequired();

            entity.Property(e => e.LocationId)
                  .IsRequired();

            entity.Property(e => e.CompanyId)
                  .IsRequired();

            entity.Property(e => e.AppliedDate)
                  .IsRequired();
        });

        modelBuilder.Entity<JobSaved>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobSaved__3214EC07C2A28AF5");

            entity.ToTable("JobSaved");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });
        modelBuilder.Entity<JobSeekerImage>(entity =>
        {
            entity.ToTable("JobSeekerImage");

            entity.HasIndex(e => e.JobSeekerProfileId, "IX_JobSeekerImage_JobSeekerProfileId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            //entity.HasOne(d => d.JobSeekerProfile).WithMany(p => p.JobSeekerImages).HasForeignKey(d => d.JobSeekerProfileId);
        });
        modelBuilder.Entity<ProfileQualification>(entity =>
        {
            entity.ToTable("ProfileQualification");

            // Composite primary key
            entity.HasKey(pq => new { pq.JobSeekerProfileId, pq.QualificationId });

            // Relationship to JobSeekerProfile
            entity.HasOne(pq => pq.JobSeekerProfile)
                  .WithMany(jp => jp.ProfileQualification)
                  .HasForeignKey(pq => pq.JobSeekerProfileId);

            // Relationship to Qualification
            entity.HasOne(pq => pq.Qualification)
                  .WithMany(q => q.ProfileQualification)
                  .HasForeignKey(pq => pq.QualificationId);
        });
        modelBuilder.Entity<ProfileSkill>(entity =>
        {
            // Map to table
            entity.ToTable("ProfileSkill");

            // Composite primary key
            entity.HasKey(ps => new { ps.JobSeekerProfileId, ps.SkillId });

            // Relationship to JobSeekerProfile
            entity.HasOne(ps => ps.JobSeekerProfile)
                  .WithMany(jp => jp.ProfileSkill)   // navigation property in JobSeekerProfile
                  .HasForeignKey(ps => ps.JobSeekerProfileId);

            // Relationship to Skill
            entity.HasOne(ps => ps.Skill)
                  .WithMany(s => s.ProfileSkill)     // navigation property in Skill
                  .HasForeignKey(ps => ps.SkillId);
        });









        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
