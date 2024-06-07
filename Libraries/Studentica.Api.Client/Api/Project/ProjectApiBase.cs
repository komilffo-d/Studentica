﻿using Studentica.Api.Client;
using Studentica.Api.Helpers;
using Studentica.Common.DTOs.Project;
using Studentica.Services.Common;

namespace Studentica.Api.Project
{
    public abstract class ProjectApiBase<T> : ApiBase<T>, IProjectApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        protected ProjectApiBase(IApiClient<T> client) : base(client, ServiceNames.Projects)
        {

        }

        public virtual async Task<ProjectDto<T>> Get(T projectId)
        {
            var request = CreateRequest(additional: $"{projectId}");

            var response = await ExecuteRequest(request);

            return response.Deserialize<ProjectDto<T>>();
        }
    }
}