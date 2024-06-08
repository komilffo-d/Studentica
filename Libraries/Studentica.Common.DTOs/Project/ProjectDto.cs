﻿using Studentica.Common.Interfaces;
using Studentica.UI.Common.Enums;

namespace Studentica.Common.DTOs.Project
{
    public record ProjectDto<T>(T Id, string Name, StatusProject Status, DateTimeOffset BeginDate, DateTimeOffset EndDate, string Description) : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>;
}
