﻿using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using ScheduleIT.Application.Core.Abstractions.Common;
using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIT.Domain.Core.Abstractions;
using ScheduleIT.Domain.Core.Primitives.Events;
using ScheduleIT.Domain.Core.Primitives;
using System.Reflection;
using ScheduleIT.Domain.Core.Primitives.Maybe;
using ScheduleIT.Persistence.Extensions;
using Microsoft.Data.SqlClient;
using ScheduleIT.Persistence.Outbox;
using Newtonsoft.Json;


namespace ScheduleIT.Persistence
{
    /// <summary>
    /// Represents the applications database context.
    /// </summary>
    public sealed class ScheduleITDbContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleITDbContext"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        /// <param name="dateTime">The current date and time.</param>
        /// <param name="mediator">The mediator.</param>
        public ScheduleITDbContext(DbContextOptions options, IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity
            => base.Set<TEntity>();

        public DbSet<OutboxMessage>SetOutboxMessages()
            => base.Set<OutboxMessage>();

        /// <inheritdoc />
        public async Task<Maybe<TEntity>> GetBydIdAsync<TEntity>(Guid id)
            where TEntity : Entity
            => id == Guid.Empty ?
                Maybe<TEntity>.None :
                Maybe<TEntity>.From(await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id));

        /// <inheritdoc />
        public void Insert<TEntity>(TEntity entity)
            where TEntity : Entity
            => Set<TEntity>().Add(entity);

        /// <inheritdoc />
        public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity
            => Set<TEntity>().AddRange(entities);

        /// <inheritdoc />
        public new void Remove<TEntity>(TEntity entity)
            where TEntity : Entity
            => Set<TEntity>().Remove(entity);

        /// <inheritdoc />
        public Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
            => Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

        /// <summary>
        /// Saves all of the pending changes in the unit of work.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of entities that have been saved.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime utcNow = _dateTime.UtcNow;

            UpdateAuditableEntities(utcNow);

            UpdateSoftDeletableEntities(utcNow);

            var outboxMessages = ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(aggregateRoot =>
                {
                    var domainEvents = aggregateRoot.DomainEvents.ToList();
                    aggregateRoot.ClearDomainEvents();
                    return domainEvents;

                })
                .Select(domainEvent => new OutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    Type = domainEvent.GetType().Name,
                    OccurredOnUtc = DateTime.Now,
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All,
                        })
                })
                .ToList();

            if (outboxMessages != null && outboxMessages.Any())
                SetOutboxMessages().AddRange(outboxMessages);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => Database.BeginTransactionAsync(cancellationToken);

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyUtcDateTimeConverter();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Updates the entities implementing <see cref="IAuditableEntity"/> interface.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        private void UpdateAuditableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
                }
            }
        }

        /// <summary>
        /// Updates the entities implementing <see cref="ISoftDeletableEntity"/> interface.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        private void UpdateSoftDeletableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
            {
                if (entityEntry.State != EntityState.Deleted)
                {
                    continue;
                }

                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow;

                entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

                entityEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
            }
        }

        /// <summary>
        /// Updates the specified entity entry's referenced entries in the deleted state to the modified state.
        /// This method is recursive.
        /// </summary>
        /// <param name="entityEntry">The entity entry.</param>
        private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
        {
            if (!entityEntry.References.Any())
            {
                return;
            }

            foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
            {
                referenceEntry.TargetEntry.State = EntityState.Unchanged;

                UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
            }
        }

    }
}
