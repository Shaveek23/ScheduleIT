using Microsoft.AspNetCore.Http;
using ScheduleIT.Application.Core.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleIT.Infrastructure.Authentication
{
    /// <summary>
    /// Represents the employee identifier provider.
    /// </summary>
    internal sealed class EmployeeIdentifierProvider : IEmployeeIdentifierProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeIdentifierProvider"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public EmployeeIdentifierProvider(IHttpContextAccessor httpContextAccessor)
        {
            string employeeIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("employeeId")
                ?? throw new ArgumentException("The employee identifier claim is required.", nameof(httpContextAccessor));

            EmployeeId = new Guid(employeeIdClaim);
        }

        /// <inheritdoc />
        public Guid EmployeeId { get; }
    }
}
