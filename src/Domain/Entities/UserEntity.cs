﻿using System;

namespace Domain.Entities
{
	public class UserEntity: BaseEntity
	{
		public string Name { get; set; }

		public string Email { get; set; }
    }
}