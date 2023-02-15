namespace HR.Project.Entities.Concrete
{
	public class Role
	{
		public Role()
		{
			this.Users = new List<UserEntity>();
		}
		public int RoleId { get; set; }
		public string RoleName { get; set; }

		public List<UserEntity> Users { get; set; }
	}

}
