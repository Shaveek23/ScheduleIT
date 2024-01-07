

using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Abstractions;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives;
using ScheduleIT.Domain.Core.Primitives.Result;

namespace ScheduleIT.Domain.Aggregates.Team;
public class Team : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{

    private readonly List<TeamMember> _members = [];
    public NonEmptyText Name { get; private set; }

    public string? Description { get; private set; } = string.Empty;

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    public Guid TeamLeaderEmployeeId { get; private set; }

    public IReadOnlyCollection<TeamMember> Members => _members;

    /// <inheritdoc />
    public DateTime? DeletedOnUtc { get; }
        
    /// <inheritdoc />
    public bool Deleted { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Team"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Team()
    {
    }

    private Team(NonEmptyText name, string description, Guid teamLeaderEmployeeId) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        TeamLeaderEmployeeId = teamLeaderEmployeeId;

        var teamLeader = TeamMember.Create(teamLeaderEmployeeId, TeamMemberRole.TeamLeader);
        _members.Add(teamLeader);

        AddDomainEvent(new TeamCreatedDomainEvent(Id, Name.Value, TeamLeaderEmployeeId, CreatedOnUtc));

    }

    public static Team Create(NonEmptyText name, string description, Guid employeeId)
    {
        return new Team(name, description, employeeId);
    }

    public void ChangeName(NonEmptyText name)
    {
        Name = name;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public Result ChangeMemberRole(Guid editorEmployeeId, Guid teamMemberId, TeamMemberRole newRole, TeamMemberRole? newRoleForOldLeader=null)
    {
        if (newRole == TeamMemberRole.TeamLeader && newRoleForOldLeader is null)
            return Result.Failure(DomainErrors.Team.CannotChangeRoleToTeamLeader);

        if (editorEmployeeId != TeamLeaderEmployeeId)
            return Result.Failure(DomainErrors.Team.NotATeamLeader);

        var teamMember = _members.Where(m => !m.Deleted).FirstOrDefault(m => m.Id == teamMemberId);

        if (teamMember is null)
            return Result.Failure(DomainErrors.Team.TeamMemberNotFoundOrDeleted);

        teamMember.ChangeRole(newRole);

        if (newRole == TeamMemberRole.TeamLeader)
        {
            var teamLeader = _members.Where(m => !m.Deleted).First(m => m.Id == TeamLeaderEmployeeId);
            teamLeader.ChangeRole(newRoleForOldLeader!);
        }

        return Result.Success();

    }

    public Result AddTeamMember(Guid creatorEmployeeId, Guid employeeId, TeamMemberRole role)
    {
        if (creatorEmployeeId != this.TeamLeaderEmployeeId)
        {
            return Result.Failure(DomainErrors.Team.NotATeamLeader);
        }

        if (!_members.Where(m => !m.Deleted).Any(m => m.EmployeeId == employeeId))
        {
            return Result.Failure(DomainErrors.Team.TeamMemberAlreadyExists);
        }

        var member = TeamMember.Create(employeeId, role);
        _members.Add(member);

        return Result.Success();
    }

    public Result DeleteTeamMember(Guid editorEmployeeId, Guid teamMemberId)
    {
        if (editorEmployeeId != this.TeamLeaderEmployeeId)
        {
            return Result.Failure(DomainErrors.Team.NotATeamLeader);
        }

        if (_members.Where(m => !m.Deleted).Any(m => m.Id == teamMemberId))
        {
            return Result.Failure(DomainErrors.Team.TeamMemberNotFoundOrDeleted);
        }

        var memberToBeDeleted = _members.First(m => m.Id == teamMemberId);

        if (memberToBeDeleted.Role == TeamMemberRole.TeamLeader)
        {
            return Result.Failure(DomainErrors.Team.CannotDeleteTeamMember);
        }

        memberToBeDeleted.MarkAsDeleted();

        return Result.Success();
    }
}

